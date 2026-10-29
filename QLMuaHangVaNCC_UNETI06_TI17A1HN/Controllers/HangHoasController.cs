using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.ViewModels;

public class HangHoasController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;
    public HangHoasController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context) => _context = context;

    // Index
    public async Task<IActionResult> Index(string? search, int? maLoaiHang, int? maDonViTinh,
        bool? status, decimal? giaTu, decimal? giaDen, int? maNhaCungCap,
        bool? coCungUng, string? sort, int page = 1)
    {
        page = Math.Max(1, page);
        const int pageSize = 10;
        var query = _context.HangHoa.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            query = query.Where(x => x.TenHang.Contains(search) || x.MaHang.ToString().Contains(search));
        }
        if (maLoaiHang.HasValue) query = query.Where(x => x.MaLoaiHang == maLoaiHang.Value);
        if (maDonViTinh.HasValue) query = query.Where(x => x.MaDonViTinh == maDonViTinh.Value);
        if (status.HasValue) query = query.Where(x => x.TrangThai == status.Value);
        if (giaTu.HasValue) query = query.Where(x => x.GiaThamKhao >= giaTu.Value);
        if (giaDen.HasValue) query = query.Where(x => x.GiaThamKhao <= giaDen.Value);
        if (maNhaCungCap.HasValue && coCungUng.HasValue)
        {
            query = coCungUng.Value
                ? query.Where(x => x.NhaCungCapHangHoas.Any(y => y.MaNhaCungCap == maNhaCungCap.Value))
                : query.Where(x => !x.NhaCungCapHangHoas.Any(y => y.MaNhaCungCap == maNhaCungCap.Value));
        }
        query = sort switch
        {
            "name_desc" => query.OrderByDescending(x => x.TenHang).ThenBy(x => x.MaHang),
            "price_asc" => query.OrderBy(x => x.GiaThamKhao).ThenBy(x => x.MaHang),
            "price_desc" => query.OrderByDescending(x => x.GiaThamKhao).ThenBy(x => x.MaHang),
            _ => query.OrderBy(x => x.TenHang).ThenBy(x => x.MaHang)
        };
        var total = await query.CountAsync();
        var pages = Math.Max(1, (int)Math.Ceiling(total / (double)pageSize));
        page = Math.Min(page, pages);
        var items = await query.Include(x => x.LoaiHang).Include(x => x.DonViTinh)
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return View(new PagedListVm<HangHoa>
        {
            Items = items, Page = page, PageSize = pageSize, TotalItems = total,
            Search = search, MaLoaiHang = maLoaiHang, MaDonViTinh = maDonViTinh,
            Status = status, GiaTu = giaTu, GiaDen = giaDen, MaNhaCungCap = maNhaCungCap,
            CoCungUng = coCungUng, Sort = sort,
            LoaiHangs = await _context.LoaiHang.AsNoTracking().OrderBy(x => x.TenLoaiHang).ToListAsync(),
            DonViTinhs = await _context.DonViTinh.AsNoTracking().OrderBy(x => x.TenDonViTinh).ToListAsync(),
            NhaCungCaps = await _context.NhaCungCap.AsNoTracking().OrderBy(x => x.TenNhaCungCap).ToListAsync()
        });
    }

    // Details
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var item = await _context.HangHoa.AsNoTracking().Include(x => x.LoaiHang)
            .Include(x => x.DonViTinh).FirstOrDefaultAsync(x => x.MaHang == id);
        return item == null ? NotFound() : View(item);
    }

    private async Task Populate(HangHoaFormVm vm, bool edit = false)
    {
        vm.LoaiHangs = await _context.LoaiHang.AsNoTracking()
            .Where(x => x.TrangThai || (edit && x.MaLoaiHang == vm.MaLoaiHang))
            .OrderBy(x => x.TenLoaiHang).ToListAsync();
        vm.DonViTinhs = await _context.DonViTinh.AsNoTracking()
            .Where(x => x.TrangThai || (edit && x.MaDonViTinh == vm.MaDonViTinh))
            .OrderBy(x => x.TenDonViTinh).ToListAsync();
    }

    private async Task CheckReferences(HangHoaFormVm vm, HangHoa? existing = null)
    {
        if (!await _context.LoaiHang.AnyAsync(x => x.MaLoaiHang == vm.MaLoaiHang &&
            (x.TrangThai || (existing != null && existing.MaLoaiHang == vm.MaLoaiHang))))
            ModelState.AddModelError(nameof(vm.MaLoaiHang), "Loại hàng không hợp lệ hoặc đã ngừng hoạt động.");
        if (!await _context.DonViTinh.AnyAsync(x => x.MaDonViTinh == vm.MaDonViTinh &&
            (x.TrangThai || (existing != null && existing.MaDonViTinh == vm.MaDonViTinh))))
            ModelState.AddModelError(nameof(vm.MaDonViTinh), "Đơn vị tính không hợp lệ hoặc đã ngừng hoạt động.");
    }

    // Create
    public async Task<IActionResult> Create()
    {
        var vm = new HangHoaFormVm();
        await Populate(vm);
        return View(vm);
    }

    // Create POST
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HangHoaFormVm vm)
    {
        await CheckReferences(vm);
        if (!ModelState.IsValid) { await Populate(vm); return View(vm); }
        _context.HangHoa.Add(new HangHoa
        {
            TenHang = vm.TenHang.Trim(), MaLoaiHang = vm.MaLoaiHang,
            MaDonViTinh = vm.MaDonViTinh, GiaThamKhao = vm.GiaThamKhao,
            MoTa = vm.MoTa, TrangThai = vm.TrangThai
        });
        await _context.SaveChangesAsync();
        TempData["Success"] = "Đã thêm hàng hóa.";
        return RedirectToAction(nameof(Index));
    }

    // Edit
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var x = await _context.HangHoa.FindAsync(id);
        if (x == null) return NotFound();
        var vm = new HangHoaFormVm { MaHang = x.MaHang, TenHang = x.TenHang,
            MaLoaiHang = x.MaLoaiHang, MaDonViTinh = x.MaDonViTinh, GiaThamKhao = x.GiaThamKhao,
            MoTa = x.MoTa, TrangThai = x.TrangThai };
        await Populate(vm, true);
        return View(vm);
    }

    // Edit POST
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, HangHoaFormVm vm)
    {
        if (id != vm.MaHang) return NotFound();
        var x = await _context.HangHoa.FindAsync(id);
        if (x == null) return NotFound();
        await CheckReferences(vm, x);
        if (!ModelState.IsValid) { await Populate(vm, true); return View(vm); }
        x.TenHang = vm.TenHang.Trim();
        x.MaLoaiHang = vm.MaLoaiHang;
        x.MaDonViTinh = vm.MaDonViTinh;
        x.GiaThamKhao = vm.GiaThamKhao;
        x.MoTa = vm.MoTa;
        x.TrangThai = vm.TrangThai;
        await _context.SaveChangesAsync();
        TempData["Success"] = "Đã cập nhật hàng hóa.";
        return RedirectToAction(nameof(Index));
    }

    // ChangeStatus POST
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeStatus(int id)
    {
        var x = await _context.HangHoa.FindAsync(id);
        if (x == null) return NotFound();
        x.TrangThai = !x.TrangThai;
        await _context.SaveChangesAsync();
        TempData["Success"] = "Đã thay đổi trạng thái hàng hóa.";
        return RedirectToAction(nameof(Index));
    }

    // Delete
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var x = await _context.HangHoa.AsNoTracking().FirstOrDefaultAsync(y => y.MaHang == id);
        return x == null ? NotFound() : View(x);
    }

    // Delete POST
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var x = await _context.HangHoa.FindAsync(id);
        if (x == null) return NotFound();
        var used = await _context.ChiTietYeuCau.AnyAsync(y => y.MaHang == id)
            || await _context.ChiTietDonMua.AnyAsync(y => y.MaHang == id);
        var linked = await _context.NhaCungCapHangHoa.AnyAsync(y => y.MaHang == id);
        if (used || linked)
        {
            x.TrangThai = false;
            await _context.SaveChangesAsync();
            TempData["Success"] = "Hàng hóa có dữ liệu liên quan; đã chuyển sang ngừng hoạt động.";
        }
        else
        {
            _context.HangHoa.Remove(x);
            try { await _context.SaveChangesAsync(); TempData["Success"] = "Đã xóa hàng hóa."; }
            catch (DbUpdateException) { _context.Entry(x).State = EntityState.Detached; TempData["Error"] = "Không thể xóa vì còn dữ liệu liên quan."; }
        }
        return RedirectToAction(nameof(Index));
    }
}
