using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.ViewModels;

public class NhaCungCapsController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;
    public NhaCungCapsController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context) => _context = context;

    // Index
    public async Task<IActionResult> Index(string? search, bool? status, int? maHang,
        bool? coCungUng, string? sort, int page = 1)
    {
        page = Math.Max(1, page);
        const int pageSize = 10;
        var query = _context.NhaCungCap.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            query = query.Where(x => x.TenNhaCungCap.Contains(search)
                || x.MaNhaCungCap.ToString().Contains(search)
                || (x.MaSoThue != null && x.MaSoThue.Contains(search))
                || (x.NguoiLienHe != null && x.NguoiLienHe.Contains(search)));
        }
        if (status.HasValue) query = query.Where(x => x.TrangThai == status.Value);
        if (maHang.HasValue && coCungUng.HasValue)
        {
            query = coCungUng.Value
                ? query.Where(x => x.NhaCungCapHangHoas.Any(y => y.MaHang == maHang.Value))
                : query.Where(x => !x.NhaCungCapHangHoas.Any(y => y.MaHang == maHang.Value));
        }
        query = sort == "name_desc"
            ? query.OrderByDescending(x => x.TenNhaCungCap).ThenBy(x => x.MaNhaCungCap)
            : query.OrderBy(x => x.TenNhaCungCap).ThenBy(x => x.MaNhaCungCap);
        var total = await query.CountAsync();
        var pages = Math.Max(1, (int)Math.Ceiling(total / (double)pageSize));
        page = Math.Min(page, pages);
        return View(new PagedListVm<NhaCungCap>
        {
            Items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
            Page = page, PageSize = pageSize, TotalItems = total,
            Search = search, Status = status, MaHang = maHang, CoCungUng = coCungUng, Sort = sort,
            HangHoas = await _context.HangHoa.AsNoTracking().OrderBy(x => x.TenHang).ToListAsync()
        });
    }

    // Details
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var x = await _context.NhaCungCap.AsNoTracking().FirstOrDefaultAsync(y => y.MaNhaCungCap == id);
        return x == null ? NotFound() : View(x);
    }

    private async Task CheckTax(NhaCungCapFormVm vm, int? excludedId = null)
    {
        vm.MaSoThue = string.IsNullOrWhiteSpace(vm.MaSoThue) ? null : vm.MaSoThue.Trim();
        if (vm.MaSoThue != null && await _context.NhaCungCap.AnyAsync(x =>
            x.MaSoThue == vm.MaSoThue && (!excludedId.HasValue || x.MaNhaCungCap != excludedId.Value)))
            ModelState.AddModelError(nameof(vm.MaSoThue), "Mã số thuế đã tồn tại.");
    }

    // Create
    public IActionResult Create() => View(new NhaCungCapFormVm());

    // Create POST
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NhaCungCapFormVm vm)
    {
        await CheckTax(vm);
        if (!ModelState.IsValid) return View(vm);
        _context.NhaCungCap.Add(new NhaCungCap
        {
            TenNhaCungCap = vm.TenNhaCungCap.Trim(), MaSoThue = vm.MaSoThue,
            SoDienThoai = vm.SoDienThoai.Trim(), Email = vm.Email,
            DiaChi = vm.DiaChi, NguoiLienHe = vm.NguoiLienHe, TrangThai = vm.TrangThai
        });
        await _context.SaveChangesAsync();
        TempData["Success"] = "Đã thêm nhà cung cấp.";
        return RedirectToAction(nameof(Index));
    }

    // Edit
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var x = await _context.NhaCungCap.FindAsync(id);
        if (x == null) return NotFound();
        return View(new NhaCungCapFormVm
        {
            MaNhaCungCap = x.MaNhaCungCap, TenNhaCungCap = x.TenNhaCungCap,
            MaSoThue = x.MaSoThue, SoDienThoai = x.SoDienThoai, Email = x.Email,
            DiaChi = x.DiaChi, NguoiLienHe = x.NguoiLienHe, TrangThai = x.TrangThai
        });
    }

    // Edit POST
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, NhaCungCapFormVm vm)
    {
        if (id != vm.MaNhaCungCap) return NotFound();
        var x = await _context.NhaCungCap.FindAsync(id);
        if (x == null) return NotFound();
        await CheckTax(vm, id);
        if (!ModelState.IsValid) return View(vm);
        x.TenNhaCungCap = vm.TenNhaCungCap.Trim();
        x.MaSoThue = vm.MaSoThue;
        x.SoDienThoai = vm.SoDienThoai.Trim();
        x.Email = vm.Email;
        x.DiaChi = vm.DiaChi;
        x.NguoiLienHe = vm.NguoiLienHe;
        x.TrangThai = vm.TrangThai;
        await _context.SaveChangesAsync();
        TempData["Success"] = "Đã cập nhật nhà cung cấp.";
        return RedirectToAction(nameof(Index));
    }

    // ChangeStatus POST
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeStatus(int id)
    {
        var x = await _context.NhaCungCap.FindAsync(id);
        if (x == null) return NotFound();
        x.TrangThai = !x.TrangThai;
        await _context.SaveChangesAsync();
        TempData["Success"] = "Đã cập nhật trạng thái nhà cung cấp.";
        return RedirectToAction(nameof(Index));
    }

    // Delete
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var x = await _context.NhaCungCap.AsNoTracking().FirstOrDefaultAsync(y => y.MaNhaCungCap == id);
        return x == null ? NotFound() : View(x);
    }

    // Delete POST
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var x = await _context.NhaCungCap.FindAsync(id);
        if (x == null) return NotFound();
        var used = await _context.DonMuaHang.AnyAsync(y => y.MaNhaCungCap == id);
        var linked = await _context.NhaCungCapHangHoa.AnyAsync(y => y.MaNhaCungCap == id);
        if (used || linked)
        {
            x.TrangThai = false;
            await _context.SaveChangesAsync();
            TempData["Success"] = "Nhà cung cấp có dữ liệu liên quan; đã ngừng hoạt động.";
        }
        else
        {
            _context.NhaCungCap.Remove(x);
            try { await _context.SaveChangesAsync(); TempData["Success"] = "Đã xóa nhà cung cấp."; }
            catch (DbUpdateException) { _context.Entry(x).State = EntityState.Detached; TempData["Error"] = "Không thể xóa vì còn dữ liệu liên quan."; }
        }
        return RedirectToAction(nameof(Index));
    }
}
