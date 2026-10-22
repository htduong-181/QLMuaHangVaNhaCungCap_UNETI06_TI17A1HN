// Họ và tên: Hoàng Thùy Dương
// Mã sinh viên: 23103100051
// Nội dung thực hiện: Quản lý bộ phận đề nghị, yêu cầu mua hàng và xét duyệt yêu cầu.

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Helpers;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.ViewModels;

// Mọi action đều yêu cầu đăng nhập; quyền chi tiết theo từng action/trạng thái
// được kiểm tra thêm bên trong thân hàm (vì phạm vi phụ thuộc "ai tạo ra" chứ
// không chỉ phụ thuộc vai trò).
[PhanQuyen]
public class YeuCauMuaHangsController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;
    public YeuCauMuaHangsController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context) => _context = context;

    private int MaTaiKhoanHienTai => HttpContext.Session.GetInt32("MaTaiKhoan") ?? 0;
    private string VaiTroHienTai => HttpContext.Session.GetString("VaiTro") ?? "";

    // ---------- Danh sách ----------
    // NhanVienDeNghi: chỉ thấy yêu cầu do chính mình lập (đúng yêu cầu "phạm vi được phép").
    // Các vai trò còn lại (Admin, NguoiDuyet, NhanVienMuaHang, KeToan, NhanVienKho): thấy toàn bộ.
    public async Task<IActionResult> Index(string? trangThai)
    {
        var q = _context.YeuCauMuaHang
            .Include(y => y.BoPhanDeNghi)
            .Include(y => y.NguoiLapNavigation)
            .AsQueryable();

        if (VaiTroHienTai == VaiTroHeThong.NhanVienDeNghi)
            q = q.Where(y => y.NguoiLap == MaTaiKhoanHienTai);

        if (!string.IsNullOrWhiteSpace(trangThai))
            q = q.Where(y => y.TrangThai == trangThai);

        ViewBag.TrangThai = trangThai;
        return View(await q.OrderByDescending(y => y.MaYeuCau).ToListAsync());
    }

    // ---------- Chi tiết (xem đầu mục + danh sách chi tiết) ----------
    public async Task<IActionResult> Details(int id)
    {
        var yc = await _context.YeuCauMuaHang
            .Include(y => y.BoPhanDeNghi)
            .Include(y => y.NguoiLapNavigation)
            .Include(y => y.NguoiDuyetNavigation)
            .Include(y => y.ChiTietYeuCaus).ThenInclude(c => c.HangHoa).ThenInclude(h => h.DonViTinh)
            .FirstOrDefaultAsync(y => y.MaYeuCau == id);

        if (yc == null) return NotFound();

        // Nhân viên đề nghị chỉ xem được yêu cầu của chính mình
        if (VaiTroHienTai == VaiTroHeThong.NhanVienDeNghi && yc.NguoiLap != MaTaiKhoanHienTai)
            return RedirectToAction("TuChoiTruyCap", "TaiKhoans");

        return View(yc);
    }

    // ---------- Tạo mới (NhanVienDeNghi, Admin) ----------
    [PhanQuyen(VaiTroHeThong.NhanVienDeNghi, VaiTroHeThong.Admin)]
    public async Task<IActionResult> Create()
    {
        await NapBoPhan();
        return View(new YeuCauMuaHangFormViewModel());
    }

    [HttpPost, ValidateAntiForgeryToken]
    [PhanQuyen(VaiTroHeThong.NhanVienDeNghi, VaiTroHeThong.Admin)]
    public async Task<IActionResult> Create(YeuCauMuaHangFormViewModel model)
    {
        if (model.NgayCanHang.Date < DateTime.Today)
            ModelState.AddModelError(nameof(model.NgayCanHang), "Ngày cần hàng không được ở quá khứ");

        if (!await _context.BoPhanDeNghi.AnyAsync(b => b.MaBoPhan == model.MaBoPhan && b.TrangThai))
            ModelState.AddModelError(nameof(model.MaBoPhan), "Bộ phận không hợp lệ hoặc đã ngừng hoạt động");

        if (!ModelState.IsValid) { await NapBoPhan(); return View(model); }

        var yc = new YeuCauMuaHang
        {
            MaBoPhan = model.MaBoPhan,
            NgayYeuCau = DateTime.Today,
            NgayCanHang = model.NgayCanHang,
            MucDoUuTien = model.MucDoUuTien,
            LyDoMua = model.LyDoMua.Trim(),
            NguoiLap = MaTaiKhoanHienTai,
            TrangThai = TrangThaiYeuCau.Nhap
        };
        _context.YeuCauMuaHang.Add(yc);
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Đã tạo yêu cầu (trạng thái Nháp). Thêm chi tiết rồi gửi duyệt.";
        return RedirectToAction(nameof(Details), new { id = yc.MaYeuCau });
    }

    // ---------- Sửa đầu mục (chỉ khi còn Nháp, chỉ chủ hoặc Admin) ----------
    public async Task<IActionResult> Edit(int id)
    {
        var yc = await _context.YeuCauMuaHang.FindAsync(id);
        if (yc == null) return NotFound();
        if (!DuocSuaXoa(yc)) return RedirectToAction("TuChoiTruyCap", "TaiKhoans");

        await NapBoPhan();
        return View(new YeuCauMuaHangFormViewModel
        {
            MaYeuCau = yc.MaYeuCau,
            MaBoPhan = yc.MaBoPhan,
            NgayCanHang = yc.NgayCanHang,
            MucDoUuTien = yc.MucDoUuTien,
            LyDoMua = yc.LyDoMua
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, YeuCauMuaHangFormViewModel model)
    {
        if (id != model.MaYeuCau) return NotFound();
        var yc = await _context.YeuCauMuaHang.FindAsync(id);
        if (yc == null) return NotFound();
        if (!DuocSuaXoa(yc)) return RedirectToAction("TuChoiTruyCap", "TaiKhoans");

        if (model.NgayCanHang.Date < DateTime.Today)
            ModelState.AddModelError(nameof(model.NgayCanHang), "Ngày cần hàng không được ở quá khứ");
        if (!await _context.BoPhanDeNghi.AnyAsync(b => b.MaBoPhan == model.MaBoPhan && b.TrangThai))
            ModelState.AddModelError(nameof(model.MaBoPhan), "Bộ phận không hợp lệ hoặc đã ngừng hoạt động");

        if (!ModelState.IsValid) { await NapBoPhan(); return View(model); }

        yc.MaBoPhan = model.MaBoPhan;
        yc.NgayCanHang = model.NgayCanHang;
        yc.MucDoUuTien = model.MucDoUuTien;
        yc.LyDoMua = model.LyDoMua.Trim();
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Cập nhật yêu cầu thành công";
        return RedirectToAction(nameof(Details), new { id });
    }

    // ---------- Gửi duyệt: Nháp -> Chờ duyệt (phải có ít nhất 1 dòng chi tiết) ----------
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> GuiDuyet(int id)
    {
        var yc = await _context.YeuCauMuaHang.Include(y => y.ChiTietYeuCaus).FirstOrDefaultAsync(y => y.MaYeuCau == id);
        if (yc == null) return NotFound();
        if (!DuocSuaXoa(yc)) return RedirectToAction("TuChoiTruyCap", "TaiKhoans");

        if (yc.ChiTietYeuCaus.Count == 0)
        {
            TempData["Loi"] = "Yêu cầu chưa có chi tiết, không thể gửi duyệt";
            return RedirectToAction(nameof(Details), new { id });
        }

        yc.TrangThai = TrangThaiYeuCau.ChoDuyet;
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Đã gửi duyệt";
        return RedirectToAction(nameof(Details), new { id });
    }

    // ---------- Hủy: Nháp hoặc Chờ duyệt -> Đã hủy (chủ yêu cầu hoặc Admin) ----------
    [HttpGet]
    public async Task<IActionResult> Huy(int id)
    {
        var yc = await _context.YeuCauMuaHang.FindAsync(id);
        if (yc == null) return NotFound();
        if (!DuocSuaXoa(yc)) return RedirectToAction("TuChoiTruyCap", "TaiKhoans");
        return View(new LyDoViewModel { MaYeuCau = id });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Huy(LyDoViewModel model)
    {
        var yc = await _context.YeuCauMuaHang.FindAsync(model.MaYeuCau);
        if (yc == null) return NotFound();
        if (!DuocSuaXoa(yc)) return RedirectToAction("TuChoiTruyCap", "TaiKhoans");
        if (!ModelState.IsValid) return View(model);

        yc.TrangThai = TrangThaiYeuCau.DaHuy;
        yc.LyDoTuChoiHuy = model.LyDo.Trim();
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Đã hủy yêu cầu";
        return RedirectToAction(nameof(Details), new { id = model.MaYeuCau });
    }

    // ---------- Xét duyệt (NguoiDuyet, Admin), chỉ khi Chờ duyệt ----------
    [PhanQuyen(VaiTroHeThong.NguoiDuyet, VaiTroHeThong.Admin)]
    public async Task<IActionResult> Duyet(int id)
    {
        var yc = await _context.YeuCauMuaHang
            .Include(y => y.BoPhanDeNghi)
            .Include(y => y.NguoiLapNavigation)
            .Include(y => y.ChiTietYeuCaus).ThenInclude(c => c.HangHoa).ThenInclude(h => h.DonViTinh)
            .FirstOrDefaultAsync(y => y.MaYeuCau == id);

        if (yc == null) return NotFound();
        if (yc.TrangThai != TrangThaiYeuCau.ChoDuyet)
        {
            TempData["Loi"] = "Yêu cầu không ở trạng thái chờ duyệt";
            return RedirectToAction(nameof(Details), new { id });
        }

        var vm = new XetDuyetViewModel
        {
            MaYeuCau = yc.MaYeuCau,
            TenBoPhan = yc.BoPhanDeNghi.TenBoPhan,
            NguoiLapHoTen = yc.NguoiLapNavigation.HoTen,
            NgayYeuCau = yc.NgayYeuCau,
            NgayCanHang = yc.NgayCanHang,
            LyDoMua = yc.LyDoMua,
            ChiTiet = yc.ChiTietYeuCaus.Select(c => new XetDuyetChiTietViewModel
            {
                MaChiTietYeuCau = c.MaChiTietYeuCau,
                TenHang = c.HangHoa.TenHang,
                DonViTinh = c.HangHoa.DonViTinh.TenDonViTinh,
                SoLuongYeuCau = c.SoLuongYeuCau,
                SoLuongDuyet = c.SoLuongYeuCau // mặc định duyệt đủ, người duyệt có thể sửa
            }).ToList()
        };
        return View(vm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    [PhanQuyen(VaiTroHeThong.NguoiDuyet, VaiTroHeThong.Admin)]
    public async Task<IActionResult> Duyet(XetDuyetViewModel model)
    {
        var yc = await _context.YeuCauMuaHang
            .Include(y => y.ChiTietYeuCaus)
            .FirstOrDefaultAsync(y => y.MaYeuCau == model.MaYeuCau);

        if (yc == null) return NotFound();
        if (yc.TrangThai != TrangThaiYeuCau.ChoDuyet)
        {
            TempData["Loi"] = "Yêu cầu không ở trạng thái chờ duyệt";
            return RedirectToAction(nameof(Details), new { id = model.MaYeuCau });
        }

        foreach (var dong in model.ChiTiet)
        {
            var ct = yc.ChiTietYeuCaus.FirstOrDefault(c => c.MaChiTietYeuCau == dong.MaChiTietYeuCau);
            if (ct == null) continue;
            if (dong.SoLuongDuyet < 0 || dong.SoLuongDuyet > ct.SoLuongYeuCau)
            {
                ModelState.AddModelError("", $"Số lượng duyệt của 1 dòng không hợp lệ (phải từ 0 đến {ct.SoLuongYeuCau})");
            }
        }
        if (!ModelState.IsValid) return View(model);

        foreach (var dong in model.ChiTiet)
        {
            var ct = yc.ChiTietYeuCaus.First(c => c.MaChiTietYeuCau == dong.MaChiTietYeuCau);
            ct.SoLuongDuyet = dong.SoLuongDuyet;
        }

        yc.TrangThai = TrangThaiYeuCau.DaDuyet;
        yc.NgayDuyet = DateTime.Now;
        yc.NguoiDuyet = MaTaiKhoanHienTai;
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Đã duyệt yêu cầu";
        return RedirectToAction(nameof(Details), new { id = model.MaYeuCau });
    }

    [HttpGet]
    [PhanQuyen(VaiTroHeThong.NguoiDuyet, VaiTroHeThong.Admin)]
    public async Task<IActionResult> TuChoi(int id)
    {
        var yc = await _context.YeuCauMuaHang.FindAsync(id);
        if (yc == null) return NotFound();
        if (yc.TrangThai != TrangThaiYeuCau.ChoDuyet)
        {
            TempData["Loi"] = "Yêu cầu không ở trạng thái chờ duyệt";
            return RedirectToAction(nameof(Details), new { id });
        }
        return View(new LyDoViewModel { MaYeuCau = id });
    }

    [HttpPost, ValidateAntiForgeryToken]
    [PhanQuyen(VaiTroHeThong.NguoiDuyet, VaiTroHeThong.Admin)]
    public async Task<IActionResult> TuChoi(LyDoViewModel model)
    {
        var yc = await _context.YeuCauMuaHang.FindAsync(model.MaYeuCau);
        if (yc == null) return NotFound();
        if (!ModelState.IsValid) return View(model);
        if (yc.TrangThai != TrangThaiYeuCau.ChoDuyet)
        {
            TempData["Loi"] = "Yêu cầu không ở trạng thái chờ duyệt";
            return RedirectToAction(nameof(Details), new { id = model.MaYeuCau });
        }

        yc.TrangThai = TrangThaiYeuCau.TuChoi;
        yc.NgayDuyet = DateTime.Now;
        yc.NguoiDuyet = MaTaiKhoanHienTai;
        yc.LyDoTuChoiHuy = model.LyDo.Trim();
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Đã từ chối yêu cầu";
        return RedirectToAction(nameof(Details), new { id = model.MaYeuCau });
    }

    // ---------- Hàm phụ trợ ----------
    private bool DuocSuaXoa(YeuCauMuaHang yc)
    {
        var laChuHoacAdmin = yc.NguoiLap == MaTaiKhoanHienTai || VaiTroHienTai == VaiTroHeThong.Admin;
        var dangNhapHoacChoDuyet = yc.TrangThai == TrangThaiYeuCau.Nhap || yc.TrangThai == TrangThaiYeuCau.ChoDuyet;
        return laChuHoacAdmin && dangNhapHoacChoDuyet;
    }

    private async Task NapBoPhan() =>
        ViewBag.BoPhans = new SelectList(
            await _context.BoPhanDeNghi.Where(b => b.TrangThai).OrderBy(b => b.TenBoPhan).ToListAsync(),
            "MaBoPhan", "TenBoPhan");
}
