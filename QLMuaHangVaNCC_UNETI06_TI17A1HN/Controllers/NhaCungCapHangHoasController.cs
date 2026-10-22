using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.ViewModels;

public class NhaCungCapHangHoasController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;
    public NhaCungCapHangHoasController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context) => _context = context;

    // Index
    public async Task<IActionResult> Index(string? search, int? maHang, int? maNhaCungCap,
        bool? status, string? sort, int page = 1)
    {
        const int pageSize = 10;
        page = Math.Max(1, page);
        var query = _context.NhaCungCapHangHoa.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            query = query.Where(x => x.HangHoa.TenHang.Contains(search)
                || x.NhaCungCap.TenNhaCungCap.Contains(search));
        }
        if (maHang.HasValue) query = query.Where(x => x.MaHang == maHang.Value);
        if (maNhaCungCap.HasValue) query = query.Where(x => x.MaNhaCungCap == maNhaCungCap.Value);
        if (status.HasValue) query = query.Where(x => x.TrangThai == status.Value);
        query = sort switch
        {
            "date_asc" => query.OrderBy(x => x.NgayCapNhatGia).ThenBy(x => x.Id),
            "date_desc" => query.OrderByDescending(x => x.NgayCapNhatGia).ThenBy(x => x.Id),
            "price_asc" => query.OrderBy(x => x.DonGiaBao).ThenBy(x => x.Id),
            "price_desc" => query.OrderByDescending(x => x.DonGiaBao).ThenBy(x => x.Id),
            _ => query.OrderBy(x => x.NhaCungCap.TenNhaCungCap).ThenBy(x => x.Id)
        };
        var total = await query.CountAsync();
        var pages = Math.Max(1, (int)Math.Ceiling(total / (double)pageSize));
        page = Math.Min(page, pages);
        return View(new PagedListVm<NhaCungCapHangHoa>
        {
            Items = await query.Include(x => x.NhaCungCap).Include(x => x.HangHoa)
                .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
            Page = page, PageSize = pageSize, TotalItems = total, Search = search,
            MaHang = maHang, MaNhaCungCap = maNhaCungCap, Status = status, Sort = sort,
            HangHoas = await _context.HangHoa.AsNoTracking().OrderBy(x => x.TenHang).ToListAsync(),
            NhaCungCaps = await _context.NhaCungCap.AsNoTracking().OrderBy(x => x.TenNhaCungCap).ToListAsync()
        });
    }

    // Details
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var x = await _context.NhaCungCapHangHoa.AsNoTracking()
            .Include(y => y.NhaCungCap).Include(y => y.HangHoa).FirstOrDefaultAsync(y => y.Id == id);
        return x == null ? NotFound() : View(x);
    }

    private async Task Populate(CungUngFormVm vm, bool edit = false)
    {
        vm.HangHoas = await _context.HangHoa.AsNoTracking()
            .Where(x => x.TrangThai || (edit && x.MaHang == vm.MaHang))
            .OrderBy(x => x.TenHang).ToListAsync();
        vm.NhaCungCaps = await _context.NhaCungCap.AsNoTracking()
            .Where(x => x.TrangThai || (edit && x.MaNhaCungCap == vm.MaNhaCungCap))
            .OrderBy(x => x.TenNhaCungCap).ToListAsync();
    }

    private async Task Validate(CungUngFormVm vm, NhaCungCapHangHoa? existing = null)
    {
        if (!await _context.HangHoa.AnyAsync(x => x.MaHang == vm.MaHang &&
            (x.TrangThai || (existing != null && existing.MaHang == vm.MaHang))))
            ModelState.AddModelError(nameof(vm.MaHang), "Hàng hóa không hợp lệ hoặc đã ngừng hoạt động.");
        if (!await _context.NhaCungCap.AnyAsync(x => x.MaNhaCungCap == vm.MaNhaCungCap &&
            (x.TrangThai || (existing != null && existing.MaNhaCungCap == vm.MaNhaCungCap))))
            ModelState.AddModelError(nameof(vm.MaNhaCungCap), "Nhà cung cấp không hợp lệ hoặc đã ngừng hoạt động.");
        if (await _context.NhaCungCapHangHoa.AnyAsync(x => x.MaHang == vm.MaHang &&
            x.MaNhaCungCap == vm.MaNhaCungCap && (existing == null || x.Id != existing.Id)))
            ModelState.AddModelError(nameof(vm.MaHang), "Nhà cung cấp đã có liên kết với mặt hàng này.");
    }

    // Create
    public async Task<IActionResult> Create()
    {
        var vm = new CungUngFormVm();
        await Populate(vm);
        return View(vm);
    }

    // Create POST
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CungUngFormVm vm)
    {
        await Validate(vm);
        if (!ModelState.IsValid) { await Populate(vm); return View(vm); }
        _context.NhaCungCapHangHoa.Add(new NhaCungCapHangHoa
        {
            MaNhaCungCap = vm.MaNhaCungCap, MaHang = vm.MaHang,
            DonGiaBao = vm.DonGiaBao, NgayCapNhatGia = DateTime.Today,
            ThoiGianGiaoDuKien = vm.ThoiGianGiaoDuKien, TrangThai = vm.TrangThai
        });
        await _context.SaveChangesAsync();
        TempData["Success"] = "Đã tạo liên kết cung ứng.";
        return RedirectToAction(nameof(Index));
    }

    // Edit
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var x = await _context.NhaCungCapHangHoa.FindAsync(id);
        if (x == null) return NotFound();
        var vm = new CungUngFormVm
        {
            Id = x.Id, MaHang = x.MaHang, MaNhaCungCap = x.MaNhaCungCap,
            DonGiaBao = x.DonGiaBao, ThoiGianGiaoDuKien = x.ThoiGianGiaoDuKien,
            TrangThai = x.TrangThai
        };
        await Populate(vm, true);
        return View(vm);
    }

    // Edit POST
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CungUngFormVm vm)
    {
        if (id != vm.Id) return NotFound();
        var x = await _context.NhaCungCapHangHoa.FindAsync(id);
        if (x == null) return NotFound();
        await Validate(vm, x);
        if (!ModelState.IsValid) { await Populate(vm, true); return View(vm); }
        if (x.DonGiaBao != vm.DonGiaBao) x.NgayCapNhatGia = DateTime.Today;
        x.MaHang = vm.MaHang;
        x.MaNhaCungCap = vm.MaNhaCungCap;
        x.DonGiaBao = vm.DonGiaBao;
        x.ThoiGianGiaoDuKien = vm.ThoiGianGiaoDuKien;
        x.TrangThai = vm.TrangThai;
        await _context.SaveChangesAsync();
        TempData["Success"] = "Đã cập nhật thông tin cung ứng.";
        return RedirectToAction(nameof(Index));
    }

    // ChangeStatus POST
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeStatus(int id)
    {
        var x = await _context.NhaCungCapHangHoa.FindAsync(id);
        if (x == null) return NotFound();
        if (!x.TrangThai || (await _context.HangHoa.AnyAsync(y => y.MaHang == x.MaHang && y.TrangThai)
            && await _context.NhaCungCap.AnyAsync(y => y.MaNhaCungCap == x.MaNhaCungCap && y.TrangThai)))
        {
            x.TrangThai = !x.TrangThai;
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã thay đổi trạng thái cung ứng.";
        }
        else TempData["Error"] = "Không thể kích hoạt liên kết khi hàng hóa hoặc nhà cung cấp ngừng hoạt động.";
        return RedirectToAction(nameof(Index));
    }
}
