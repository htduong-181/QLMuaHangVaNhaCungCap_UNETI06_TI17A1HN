
// Họ và tên: Lê Tiến Công
// Mã sinh viên: 23103100050
// Nội dung thực hiện: Chức năng phân quyền tài khoản, loại hàng, đơn vị tính

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Helpers;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

[PhanQuyen(VaiTroHeThong.Admin)]
public class DonViTinhsController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;
    public DonViTinhsController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context) => _context = context;

    public async Task<IActionResult> Index(string? tuKhoa)
    {
        var q = _context.DonViTinh.AsQueryable();
        if (!string.IsNullOrWhiteSpace(tuKhoa))
            q = q.Where(d => d.TenDonViTinh.Contains(tuKhoa));
        ViewBag.TuKhoa = tuKhoa;
        return View(await q.OrderBy(d => d.TenDonViTinh).ToListAsync());
    }

    public IActionResult Create() => View(new DonViTinh());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DonViTinh model)
    {
        model.TenDonViTinh = model.TenDonViTinh?.Trim() ?? "";
        if (await _context.DonViTinh.AnyAsync(d => d.TenDonViTinh == model.TenDonViTinh))
            ModelState.AddModelError(nameof(model.TenDonViTinh), "Tên đơn vị tính đã tồn tại");
        if (!ModelState.IsValid) return View(model);

        _context.DonViTinh.Add(model);
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Thêm đơn vị tính thành công";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var dvt = await _context.DonViTinh.FindAsync(id);
        return dvt == null ? NotFound() : View(dvt);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, DonViTinh model)
    {
        if (id != model.MaDonViTinh) return NotFound();
        model.TenDonViTinh = model.TenDonViTinh?.Trim() ?? "";
        if (await _context.DonViTinh.AnyAsync(d => d.TenDonViTinh == model.TenDonViTinh && d.MaDonViTinh != id))
            ModelState.AddModelError(nameof(model.TenDonViTinh), "Tên đơn vị tính đã tồn tại");
        if (!ModelState.IsValid) return View(model);

        _context.DonViTinh.Update(model);
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Cập nhật đơn vị tính thành công";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DoiTrangThai(int id)
    {
        var dvt = await _context.DonViTinh.FindAsync(id);
        if (dvt == null) return NotFound();
        dvt.TrangThai = !dvt.TrangThai;
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = dvt.TrangThai ? "Đã kích hoạt đơn vị tính" : "Đã ngừng hoạt động đơn vị tính";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var dvt = await _context.DonViTinh.FindAsync(id);
        if (dvt == null) return NotFound();

        if (await _context.HangHoa.AnyAsync(h => h.MaDonViTinh == id))
        {
            TempData["Loi"] = "Đơn vị tính đã có hàng hóa, chỉ có thể ngừng hoạt động";
            return RedirectToAction(nameof(Index));
        }
        _context.DonViTinh.Remove(dvt);
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Đã xóa đơn vị tính";
        return RedirectToAction(nameof(Index));
    }
}
