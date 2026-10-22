// Họ và tên: Hoàng Thùy Dương
// Mã sinh viên: 23103100051
// Nội dung thực hiện: Quản lý bộ phận đề nghị, yêu cầu mua hàng và xét duyệt yêu cầu.

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Helpers;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.ViewModels;

[PhanQuyen]
public class ChiTietYeuCausController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;
    public ChiTietYeuCausController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context) => _context = context;

    private int MaTaiKhoanHienTai => HttpContext.Session.GetInt32("MaTaiKhoan") ?? 0;
    private string VaiTroHienTai => HttpContext.Session.GetString("VaiTro") ?? "";

    // Danh sách tổng hợp chi tiết (lọc theo yêu cầu nếu truyền maYeuCau).
    // NhanVienDeNghi chỉ xem được chi tiết của yêu cầu do chính mình lập.
    public async Task<IActionResult> Index(int? maYeuCau, string? tuKhoa)
    {
        var q = _context.ChiTietYeuCau
            .Include(c => c.YeuCauMuaHang)
            .Include(c => c.HangHoa).ThenInclude(h => h.DonViTinh)
            .AsQueryable();

        if (VaiTroHienTai == VaiTroHeThong.NhanVienDeNghi)
            q = q.Where(c => c.YeuCauMuaHang.NguoiLap == MaTaiKhoanHienTai);

        if (maYeuCau.HasValue)
            q = q.Where(c => c.MaYeuCau == maYeuCau.Value);

        if (!string.IsNullOrWhiteSpace(tuKhoa))
            q = q.Where(c => c.HangHoa.TenHang.Contains(tuKhoa));

        ViewBag.MaYeuCau = maYeuCau;
        ViewBag.TuKhoa = tuKhoa;
        return View(await q.OrderByDescending(c => c.MaYeuCau).ToListAsync());
    }

    // ---------- Thêm dòng chi tiết (chỉ khi yêu cầu còn Nháp) ----------
    public async Task<IActionResult> Create(int maYeuCau)
    {
        var yc = await _context.YeuCauMuaHang.FindAsync(maYeuCau);
        if (yc == null) return NotFound();
        if (!await DuocSua(yc)) return RedirectToAction("TuChoiTruyCap", "TaiKhoans");

        await NapHangHoa();
        return View(new ChiTietYeuCauFormViewModel { MaYeuCau = maYeuCau });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ChiTietYeuCauFormViewModel model)
    {
        var yc = await _context.YeuCauMuaHang.FindAsync(model.MaYeuCau);
        if (yc == null) return NotFound();
        if (!await DuocSua(yc)) return RedirectToAction("TuChoiTruyCap", "TaiKhoans");

        if (!await _context.HangHoa.AnyAsync(h => h.MaHang == model.MaHang && h.TrangThai))
            ModelState.AddModelError(nameof(model.MaHang), "Hàng hóa không hợp lệ hoặc đã ngừng kinh doanh");

        if (await _context.ChiTietYeuCau.AnyAsync(c => c.MaYeuCau == model.MaYeuCau && c.MaHang == model.MaHang))
            ModelState.AddModelError(nameof(model.MaHang), "Hàng hóa này đã có trong yêu cầu, hãy sửa dòng đã có thay vì thêm mới");

        if (!ModelState.IsValid) { await NapHangHoa(); return View(model); }

        _context.ChiTietYeuCau.Add(new ChiTietYeuCau
        {
            MaYeuCau = model.MaYeuCau,
            MaHang = model.MaHang,
            SoLuongYeuCau = model.SoLuongYeuCau,
            SoLuongDuyet = 0,
            GhiChu = model.GhiChu?.Trim()
        });
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Đã thêm chi tiết yêu cầu";
        return RedirectToAction("Details", "YeuCauMuaHangs", new { id = model.MaYeuCau });
    }

    // ---------- Sửa dòng chi tiết ----------
    public async Task<IActionResult> Edit(int id)
    {
        var ct = await _context.ChiTietYeuCau.Include(c => c.YeuCauMuaHang).FirstOrDefaultAsync(c => c.MaChiTietYeuCau == id);
        if (ct == null) return NotFound();
        if (!await DuocSua(ct.YeuCauMuaHang)) return RedirectToAction("TuChoiTruyCap", "TaiKhoans");

        await NapHangHoa();
        return View(new ChiTietYeuCauFormViewModel
        {
            MaChiTietYeuCau = ct.MaChiTietYeuCau,
            MaYeuCau = ct.MaYeuCau,
            MaHang = ct.MaHang,
            SoLuongYeuCau = ct.SoLuongYeuCau,
            GhiChu = ct.GhiChu
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ChiTietYeuCauFormViewModel model)
    {
        if (id != model.MaChiTietYeuCau) return NotFound();
        var ct = await _context.ChiTietYeuCau.Include(c => c.YeuCauMuaHang).FirstOrDefaultAsync(c => c.MaChiTietYeuCau == id);
        if (ct == null) return NotFound();
        if (!await DuocSua(ct.YeuCauMuaHang)) return RedirectToAction("TuChoiTruyCap", "TaiKhoans");

        if (await _context.ChiTietYeuCau.AnyAsync(c => c.MaYeuCau == model.MaYeuCau && c.MaHang == model.MaHang && c.MaChiTietYeuCau != id))
            ModelState.AddModelError(nameof(model.MaHang), "Hàng hóa này đã có trong yêu cầu");

        if (!ModelState.IsValid) { await NapHangHoa(); return View(model); }

        ct.MaHang = model.MaHang;
        ct.SoLuongYeuCau = model.SoLuongYeuCau;
        ct.GhiChu = model.GhiChu?.Trim();
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Đã cập nhật chi tiết yêu cầu";
        return RedirectToAction("Details", "YeuCauMuaHangs", new { id = model.MaYeuCau });
    }

    // ---------- Xóa dòng chi tiết ----------
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var ct = await _context.ChiTietYeuCau.Include(c => c.YeuCauMuaHang).FirstOrDefaultAsync(c => c.MaChiTietYeuCau == id);
        if (ct == null) return NotFound();
        if (!await DuocSua(ct.YeuCauMuaHang)) return RedirectToAction("TuChoiTruyCap", "TaiKhoans");

        var maYeuCau = ct.MaYeuCau;
        _context.ChiTietYeuCau.Remove(ct);
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Đã xóa chi tiết yêu cầu";
        return RedirectToAction("Details", "YeuCauMuaHangs", new { id = maYeuCau });
    }

    // ---------- Hàm phụ trợ ----------
    private Task<bool> DuocSua(YeuCauMuaHang yc)
    {
        var laChuHoacAdmin = yc.NguoiLap == MaTaiKhoanHienTai || VaiTroHienTai == VaiTroHeThong.Admin;
        var conLaNhap = yc.TrangThai == TrangThaiYeuCau.Nhap;
        return Task.FromResult(laChuHoacAdmin && conLaNhap);
    }

    private async Task NapHangHoa() =>
        ViewBag.HangHoas = new SelectList(
            await _context.HangHoa.Where(h => h.TrangThai).OrderBy(h => h.TenHang).ToListAsync(),
            "MaHang", "TenHang");
}
