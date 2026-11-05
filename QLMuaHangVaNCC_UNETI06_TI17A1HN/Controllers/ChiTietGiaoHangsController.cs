using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

public class ChiTietGiaoHangsController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

    public ChiTietGiaoHangsController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        int pageSize = 10;

        var query = _context.ChiTietGiaoHang
            .AsNoTracking()
            .Include(c => c.LanGiaoHang)
            .Include(c => c.ChiTietDonMua)
                .ThenInclude(d => d!.HangHoa);

        int totalItems = await query.CountAsync();
        int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        page = Math.Max(1, page);
        page = totalPages > 0 ? Math.Min(page, totalPages) : 1;

        var ds = await query
            .OrderByDescending(c => c.MaChiTietGiao)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        ViewBag.Page = page;
        ViewBag.TotalPages = totalPages;

        return View(ds);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var chitietgiaohang = await _context.ChiTietGiaoHang
            .Include(c => c.LanGiaoHang)
                .ThenInclude(l => l!.NguoiNhanNavigation)
            .Include(c => c.ChiTietDonMua)
                .ThenInclude(d => d!.HangHoa)
            .FirstOrDefaultAsync(m => m.MaChiTietGiao == id);
        if (chitietgiaohang == null) return NotFound();

        return View(chitietgiaohang);
    }
}