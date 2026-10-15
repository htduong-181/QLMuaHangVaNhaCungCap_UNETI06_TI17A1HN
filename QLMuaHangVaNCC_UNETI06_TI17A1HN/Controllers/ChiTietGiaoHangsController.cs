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

    public async Task<IActionResult> Index()
    {
        var ds = await _context.ChiTietGiaoHang
            .Include(c => c.LanGiaoHang)
            .Include(c => c.ChiTietDonMua)
                .ThenInclude(d => d!.HangHoa)
            .OrderByDescending(c => c.MaChiTietGiao)
            .ToListAsync();
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