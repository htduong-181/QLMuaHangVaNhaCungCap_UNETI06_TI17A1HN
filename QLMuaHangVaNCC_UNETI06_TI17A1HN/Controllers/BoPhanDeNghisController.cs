// Họ và tên: Hoàng Thùy Dương
// Mã sinh viên: 23103100051
// Nội dung thực hiện: Quản lý bộ phận đề nghị, yêu cầu mua hàng và xét duyệt yêu cầu.

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Helpers;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

[PhanQuyen(VaiTroHeThong.Admin)]
public class BoPhanDeNghisController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;
    public BoPhanDeNghisController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context) => _context = context;

    public async Task<IActionResult> Index(string? tuKhoa)
    {
        var q = _context.BoPhanDeNghi.AsQueryable();
        if (!string.IsNullOrWhiteSpace(tuKhoa))
            q = q.Where(b => b.TenBoPhan.Contains(tuKhoa));
        ViewBag.TuKhoa = tuKhoa;
        return View(await q.OrderBy(b => b.TenBoPhan).ToListAsync());
    }

    public IActionResult Create() => View(new BoPhanDeNghi());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BoPhanDeNghi model)
    {
        model.TenBoPhan = model.TenBoPhan?.Trim() ?? "";
        if (await _context.BoPhanDeNghi.AnyAsync(b => b.TenBoPhan == model.TenBoPhan))
            ModelState.AddModelError(nameof(model.TenBoPhan), "Tên bộ phận đã tồn tại");
        if (!ModelState.IsValid) return View(model);

        _context.BoPhanDeNghi.Add(model);
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Thêm bộ phận thành công";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var bp = await _context.BoPhanDeNghi.FindAsync(id);
        return bp == null ? NotFound() : View(bp);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, BoPhanDeNghi model)
    {
        if (id != model.MaBoPhan) return NotFound();
        model.TenBoPhan = model.TenBoPhan?.Trim() ?? "";
        if (await _context.BoPhanDeNghi.AnyAsync(b => b.TenBoPhan == model.TenBoPhan && b.MaBoPhan != id))
            ModelState.AddModelError(nameof(model.TenBoPhan), "Tên bộ phận đã tồn tại");
        if (!ModelState.IsValid) return View(model);

        _context.BoPhanDeNghi.Update(model);
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Cập nhật bộ phận thành công";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DoiTrangThai(int id)
    {
        var bp = await _context.BoPhanDeNghi.FindAsync(id);
        if (bp == null) return NotFound();
        bp.TrangThai = !bp.TrangThai;
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = bp.TrangThai ? "Đã kích hoạt bộ phận" : "Đã ngừng hoạt động bộ phận";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var bp = await _context.BoPhanDeNghi.FindAsync(id);
        if (bp == null) return NotFound();

        if (await _context.YeuCauMuaHang.AnyAsync(y => y.MaBoPhan == id))
        {
            TempData["Loi"] = "Bộ phận đã có yêu cầu mua hàng, chỉ có thể ngừng hoạt động";
            return RedirectToAction(nameof(Index));
        }
        _context.BoPhanDeNghi.Remove(bp);
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Đã xóa bộ phận";
        return RedirectToAction(nameof(Index));
    }
}
