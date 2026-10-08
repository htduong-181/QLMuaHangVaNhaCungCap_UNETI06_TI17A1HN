
// Họ và tên: Lê Tiến Công
// Mã sinh viên: 23103100050
// Nội dung thực hiện: Chức năng phân quyền tài khoản, loại hàng, đơn vị tính

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Helpers;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

[PhanQuyen(VaiTroHeThong.Admin)]
public class LoaiHangsController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;
    public LoaiHangsController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context) => _context = context;

    public async Task<IActionResult> Index(string? tuKhoa)
    {
        var q = _context.LoaiHang.AsQueryable();
        if (!string.IsNullOrWhiteSpace(tuKhoa))
            q = q.Where(l => l.TenLoaiHang.Contains(tuKhoa));
        ViewBag.TuKhoa = tuKhoa;
        return View(await q.OrderBy(l => l.TenLoaiHang).ToListAsync());
    }

    public IActionResult Create() => View(new LoaiHang());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LoaiHang model)
    {
        model.TenLoaiHang = model.TenLoaiHang?.Trim() ?? "";
        if (await _context.LoaiHang.AnyAsync(l => l.TenLoaiHang == model.TenLoaiHang))
            ModelState.AddModelError(nameof(model.TenLoaiHang), "Tên loại hàng đã tồn tại");
        if (!ModelState.IsValid) return View(model);

        _context.LoaiHang.Add(model);
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Thêm loại hàng thành công";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var lh = await _context.LoaiHang.FindAsync(id);
        return lh == null ? NotFound() : View(lh);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, LoaiHang model)
    {
        if (id != model.MaLoaiHang) return NotFound();
        model.TenLoaiHang = model.TenLoaiHang?.Trim() ?? "";
        if (await _context.LoaiHang.AnyAsync(l => l.TenLoaiHang == model.TenLoaiHang && l.MaLoaiHang != id))
            ModelState.AddModelError(nameof(model.TenLoaiHang), "Tên loại hàng đã tồn tại");
        if (!ModelState.IsValid) return View(model);

        _context.LoaiHang.Update(model);
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Cập nhật loại hàng thành công";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DoiTrangThai(int id)
    {
        var lh = await _context.LoaiHang.FindAsync(id);
        if (lh == null) return NotFound();
        lh.TrangThai = !lh.TrangThai;
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = lh.TrangThai ? "Đã kích hoạt loại hàng" : "Đã ngừng hoạt động loại hàng";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var lh = await _context.LoaiHang.FindAsync(id);
        if (lh == null) return NotFound();

        if (await _context.HangHoa.AnyAsync(h => h.MaLoaiHang == id))
        {
            TempData["Loi"] = "Loại hàng đã có hàng hóa, chỉ có thể ngừng hoạt động";
            return RedirectToAction(nameof(Index));
        }
        _context.LoaiHang.Remove(lh);
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Đã xóa loại hàng";
        return RedirectToAction(nameof(Index));
    }
}
