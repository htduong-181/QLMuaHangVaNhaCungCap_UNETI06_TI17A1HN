// Họ và tên: Lê Tiến Công
// Mã sinh viên: 23103100050
// Nội dung thực hiện: Chức năng phân quyền tài khoản, loại hàng, đơn vị tính
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Helpers;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.ViewModels;

// Định danh rõ ràng VaiTroHeThong lấy từ Models để tránh xung đột CS0104 với Helpers
using VaiTroHeThong = QLMuaHangVaNCC_UNETI06_TI17A1HN.Models.VaiTroHeThong;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Controllers;

public class TaiKhoansController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;
    public TaiKhoansController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context) => _context = context;

    // ---------- Đăng nhập / đăng xuất (không cần quyền) ----------
    [HttpGet]
    public IActionResult DangNhap() => View(new DangNhapViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DangNhap(DangNhapViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var hash = MatKhauHelper.Hash(model.MatKhau);
        var tk = await _context.TaiKhoan
            .FirstOrDefaultAsync(t => t.TenDangNhap == model.TenDangNhap && t.MatKhau == hash);

        if (tk == null)
        {
            ModelState.AddModelError("", "Sai tên đăng nhập hoặc mật khẩu");
            return View(model);
        }
        if (!tk.TrangThai)
        {
            ModelState.AddModelError("", "Tài khoản đã bị khóa, liên hệ quản trị viên");
            return View(model);
        }

        HttpContext.Session.SetInt32("MaTaiKhoan", tk.MaTaiKhoan);
        HttpContext.Session.SetString("HoTen", tk.HoTen);
        HttpContext.Session.SetString("VaiTro", tk.VaiTro);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult DangXuat()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(DangNhap));
    }

    public IActionResult TuChoiTruyCap() => View();

    // ---------- Quản lý tài khoản (chỉ Admin) ----------
    [PhanQuyenYeuCau(VaiTroHeThong.Admin)]
    public async Task<IActionResult> Index(string? tuKhoa)
    {
        var q = _context.TaiKhoan.AsQueryable();
        if (!string.IsNullOrWhiteSpace(tuKhoa))
            q = q.Where(t => t.TenDangNhap.Contains(tuKhoa) || t.HoTen.Contains(tuKhoa));

        ViewBag.TuKhoa = tuKhoa;
        return View(await q.OrderBy(t => t.TenDangNhap).ToListAsync());
    }

    [PhanQuyenYeuCau(VaiTroHeThong.Admin)]
    public IActionResult Create()
    {
        NapVaiTro();
        return View(new TaiKhoanFormViewModel());
    }

    [HttpPost, ValidateAntiForgeryToken]
    [PhanQuyenYeuCau(VaiTroHeThong.Admin)]
    public async Task<IActionResult> Create(TaiKhoanFormViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.MatKhau))
            ModelState.AddModelError(nameof(model.MatKhau), "Mật khẩu là bắt buộc");

        if (await _context.TaiKhoan.AnyAsync(t => t.TenDangNhap == model.TenDangNhap))
            ModelState.AddModelError(nameof(model.TenDangNhap), "Tên đăng nhập đã tồn tại");

        if (await _context.TaiKhoan.AnyAsync(t => t.Email == model.Email))
            ModelState.AddModelError(nameof(model.Email), "Email đã được sử dụng");

        if (!VaiTroHeThong.TatCa.Contains(model.VaiTro))
            ModelState.AddModelError(nameof(model.VaiTro), "Vai trò không hợp lệ");

        if (!ModelState.IsValid) { NapVaiTro(); return View(model); }

        _context.TaiKhoan.Add(new TaiKhoan
        {
            TenDangNhap = model.TenDangNhap.Trim(),
            MatKhau = MatKhauHelper.Hash(model.MatKhau!),
            HoTen = model.HoTen.Trim(),
            Email = model.Email.Trim(),
            VaiTro = model.VaiTro,
            TrangThai = true
        });
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Thêm tài khoản thành công";
        return RedirectToAction(nameof(Index));
    }

    [PhanQuyenYeuCau(VaiTroHeThong.Admin)]
    public async Task<IActionResult> Edit(int id)
    {
        var tk = await _context.TaiKhoan.FindAsync(id);
        if (tk == null) return NotFound();

        NapVaiTro();
        return View(new TaiKhoanFormViewModel
        {
            MaTaiKhoan = tk.MaTaiKhoan,
            TenDangNhap = tk.TenDangNhap,
            HoTen = tk.HoTen,
            Email = tk.Email,
            VaiTro = tk.VaiTro
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    [PhanQuyenYeuCau(VaiTroHeThong.Admin)]
    public async Task<IActionResult> Edit(int id, TaiKhoanFormViewModel model)
    {
        if (id != model.MaTaiKhoan) return NotFound();
        var tk = await _context.TaiKhoan.FindAsync(id);
        if (tk == null) return NotFound();

        if (await _context.TaiKhoan.AnyAsync(t => t.TenDangNhap == model.TenDangNhap && t.MaTaiKhoan != id))
            ModelState.AddModelError(nameof(model.TenDangNhap), "Tên đăng nhập đã tồn tại");

        if (await _context.TaiKhoan.AnyAsync(t => t.Email == model.Email && t.MaTaiKhoan != id))
            ModelState.AddModelError(nameof(model.Email), "Email đã được sử dụng");

        if (!VaiTroHeThong.TatCa.Contains(model.VaiTro))
            ModelState.AddModelError(nameof(model.VaiTro), "Vai trò không hợp lệ");

        // Không cho Admin tự hạ quyền của chính mình
        if (id == HttpContext.Session.GetInt32("MaTaiKhoan") && model.VaiTro != VaiTroHeThong.Admin)
            ModelState.AddModelError(nameof(model.VaiTro), "Không thể tự bỏ quyền Admin của chính mình");

        if (!ModelState.IsValid) { NapVaiTro(); return View(model); }

        tk.TenDangNhap = model.TenDangNhap.Trim();
        tk.HoTen = model.HoTen.Trim();
        tk.Email = model.Email.Trim();
        tk.VaiTro = model.VaiTro;
        if (!string.IsNullOrWhiteSpace(model.MatKhau))
            tk.MatKhau = MatKhauHelper.Hash(model.MatKhau);

        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Cập nhật tài khoản thành công";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    [PhanQuyenYeuCau(VaiTroHeThong.Admin)]
    public async Task<IActionResult> DoiTrangThai(int id)
    {
        if (id == HttpContext.Session.GetInt32("MaTaiKhoan"))
        {
            TempData["Loi"] = "Không thể tự khóa tài khoản của chính mình";
            return RedirectToAction(nameof(Index));
        }
        var tk = await _context.TaiKhoan.FindAsync(id);
        if (tk == null) return NotFound();

        tk.TrangThai = !tk.TrangThai;
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = tk.TrangThai ? "Đã mở khóa tài khoản" : "Đã khóa tài khoản";
        return RedirectToAction(nameof(Index));
    }

    private void NapVaiTro() => ViewBag.VaiTros = new SelectList(VaiTroHeThong.TatCa);
}