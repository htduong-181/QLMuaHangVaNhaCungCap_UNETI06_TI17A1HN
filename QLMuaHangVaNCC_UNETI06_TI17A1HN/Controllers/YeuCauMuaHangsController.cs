// Họ và tên: Hoàng Thùy Dương
// Mã sinh viên: 23103100051
// Nội dung thực hiện: Module 3 - Controller yêu cầu mua hàng: danh sách (tìm kiếm/lọc/sắp xếp/phân trang),
// lập/sửa/xóa yêu cầu Nháp, thêm/sửa/xóa chi tiết, gửi duyệt, xét duyệt, từ chối, hủy.
// Quyền được kiểm tra TẠI CONTROLLER bằng [PhanQuyenYeuCau] và lại ở Service.

using Microsoft.AspNetCore.Mvc;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Helpers;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Services;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.ViewModels;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Controllers;

// Mặc định: ai đã đăng nhập với 3 vai trò hợp lệ mới vào được controller này.
// Từng action bên dưới thu hẹp quyền hơn nếu cần (filter cấp action chạy cùng filter cấp class).
[PhanQuyenYeuCau(VaiTroHeThong.Admin, VaiTroHeThong.NhanVienDeNghi, VaiTroHeThong.NhanVienMuaHang, VaiTroHeThong.NguoiDuyet)]
public class YeuCauMuaHangsController : Controller
{
    private readonly IYeuCauMuaHangService _svc;

    public YeuCauMuaHangsController(IYeuCauMuaHangService svc) => _svc = svc;

    // Filter đã đảm bảo Session có người dùng nên có thể dùng "!"
    private NguoiDungHienTai NguoiDung => HttpContext.Session.LayNguoiDung()!;

    private void GhiThongBao(KetQuaXuLy kq)
    {
        if (kq.ThanhCong) TempData["ThongBao"] = kq.ThongBao;
        else TempData["Loi"] = kq.ThongBao;
    }

    // ------------------------------------------------------------------ DANH SÁCH
    public async Task<IActionResult> Index([FromQuery] YeuCauFilterViewModel filter)
    {
        var vm = new YeuCauIndexViewModel
        {
            Filter = filter,
            KetQua = await _svc.TimKiemAsync(filter, NguoiDung)
        };
        // Service có thể chỉnh lại số trang (vd. trang quá lớn) -> đồng bộ lại để giữ đúng điều kiện
        filter.Page = vm.KetQua.Page;
        return View(vm);
    }

    // ------------------------------------------------------------------ CHI TIẾT
    public async Task<IActionResult> Details(int id)
    {
        var vm = await _svc.LayChiTietAsync(id, NguoiDung);
        if (vm is null)
        {
            TempData["Loi"] = "Không tìm thấy yêu cầu hoặc bạn không có quyền xem.";
            return RedirectToAction(nameof(Index));
        }
        if (vm.CoTheSua) vm.DanhSachHang = await _svc.DanhSachHangAsync();
        return View(vm);
    }

    // ------------------------------------------------------------------ TẠO
    [PhanQuyenYeuCau(VaiTroHeThong.Admin, VaiTroHeThong.NhanVienDeNghi)]
    public async Task<IActionResult> Create()
    {
        var vm = new YeuCauFormViewModel
        {
            NgayYeuCau = DateTime.Today,
            NgayCanHang = DateTime.Today.AddDays(7),
            DanhSachBoPhan = await _svc.DanhSachBoPhanAsync()
        };
        return View(vm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    [PhanQuyenYeuCau(VaiTroHeThong.Admin, VaiTroHeThong.NhanVienDeNghi)]
    public async Task<IActionResult> Create(YeuCauFormViewModel vm)
    {
        if (ModelState.IsValid)
        {
            var kq = await _svc.TaoAsync(vm, NguoiDung);
            if (kq.ThanhCong)
            {
                TempData["ThongBao"] = kq.ThongBao;
                return RedirectToAction(nameof(Details), new { id = kq.DuLieu });
            }
            ModelState.AddModelError(string.Empty, kq.ThongBao);
        }
        vm.DanhSachBoPhan = await _svc.DanhSachBoPhanAsync(vm.MaBoPhan);
        return View(vm);
    }

    // ------------------------------------------------------------------ SỬA
    [PhanQuyenYeuCau(VaiTroHeThong.Admin, VaiTroHeThong.NhanVienDeNghi)]
    public async Task<IActionResult> Edit(int id)
    {
        var vm = await _svc.LayFormSuaAsync(id, NguoiDung);
        if (vm is null)
        {
            TempData["Loi"] = "Không tìm thấy yêu cầu hoặc bạn không có quyền sửa.";
            return RedirectToAction(nameof(Index));
        }
        vm.DanhSachBoPhan = await _svc.DanhSachBoPhanAsync(vm.MaBoPhan);
        return View(vm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    [PhanQuyenYeuCau(VaiTroHeThong.Admin, VaiTroHeThong.NhanVienDeNghi)]
    public async Task<IActionResult> Edit(int id, YeuCauFormViewModel vm)
    {
        if (id != vm.MaYeuCau) return NotFound();

        if (ModelState.IsValid)
        {
            var kq = await _svc.SuaAsync(vm, NguoiDung);
            if (kq.ThanhCong)
            {
                TempData["ThongBao"] = kq.ThongBao;
                return RedirectToAction(nameof(Details), new { id });
            }
            ModelState.AddModelError(string.Empty, kq.ThongBao);
        }
        vm.DanhSachBoPhan = await _svc.DanhSachBoPhanAsync(vm.MaBoPhan);
        return View(vm);
    }

    // ------------------------------------------------------------------ XÓA (chỉ Nháp)
    [HttpPost, ValidateAntiForgeryToken]
    [PhanQuyenYeuCau(VaiTroHeThong.Admin, VaiTroHeThong.NhanVienDeNghi)]
    public async Task<IActionResult> Xoa(int id)
    {
        var kq = await _svc.XoaAsync(id, NguoiDung);
        GhiThongBao(kq);
        return kq.ThanhCong ? RedirectToAction(nameof(Index)) : RedirectToAction(nameof(Details), new { id });
    }

    // ------------------------------------------------------------------ CHI TIẾT YÊU CẦU
    [HttpPost, ValidateAntiForgeryToken]
    [PhanQuyenYeuCau(VaiTroHeThong.Admin, VaiTroHeThong.NhanVienDeNghi)]
    public async Task<IActionResult> ThemChiTiet(ChiTietYeuCauFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            TempData["Loi"] = string.Join(" ", ModelState.Values
                .SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return RedirectToAction(nameof(Details), new { id = vm.MaYeuCau });
        }

        GhiThongBao(await _svc.ThemChiTietAsync(vm, NguoiDung));
        return RedirectToAction(nameof(Details), new { id = vm.MaYeuCau });
    }

    [PhanQuyenYeuCau(VaiTroHeThong.Admin, VaiTroHeThong.NhanVienDeNghi)]
    public async Task<IActionResult> SuaChiTiet(int id)
    {
        var vm = await _svc.LayDongDeSuaAsync(id, NguoiDung);
        if (vm is null)
        {
            TempData["Loi"] = "Không tìm thấy dòng chi tiết hoặc bạn không có quyền sửa.";
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    [PhanQuyenYeuCau(VaiTroHeThong.Admin, VaiTroHeThong.NhanVienDeNghi)]
    public async Task<IActionResult> SuaChiTiet(int id, ChiTietYeuCauFormViewModel vm)
    {
        if (id != vm.MaChiTietYeuCau) return NotFound();

        if (ModelState.IsValid)
        {
            var kq = await _svc.SuaChiTietAsync(vm, NguoiDung);
            if (kq.ThanhCong)
            {
                TempData["ThongBao"] = kq.ThongBao;
                return RedirectToAction(nameof(Details), new { id = vm.MaYeuCau });
            }
            ModelState.AddModelError(string.Empty, kq.ThongBao);
        }
        // Tên hàng chỉ để hiển thị nên nạp lại khi trả form về
        var goc = await _svc.LayDongDeSuaAsync(id, NguoiDung);
        vm.TenHang = goc?.TenHang;
        return View(vm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    [PhanQuyenYeuCau(VaiTroHeThong.Admin, VaiTroHeThong.NhanVienDeNghi)]
    public async Task<IActionResult> XoaChiTiet(int id, int maYeuCau)
    {
        GhiThongBao(await _svc.XoaChiTietAsync(id, NguoiDung));
        return RedirectToAction(nameof(Details), new { id = maYeuCau });
    }

    // ------------------------------------------------------------------ GỬI DUYỆT
    [HttpPost, ValidateAntiForgeryToken]
    [PhanQuyenYeuCau(VaiTroHeThong.Admin, VaiTroHeThong.NhanVienDeNghi)]
    public async Task<IActionResult> GuiDuyet(int id)
    {
        GhiThongBao(await _svc.GuiDuyetAsync(id, NguoiDung));
        return RedirectToAction(nameof(Details), new { id });
    }

    // ------------------------------------------------------------------ XÉT DUYỆT (chỉ Admin)
    [PhanQuyenYeuCau(VaiTroHeThong.Admin, VaiTroHeThong.NguoiDuyet)]
    public async Task<IActionResult> Duyet(int id)
    {
        var vm = await _svc.LayFormDuyetAsync(id, NguoiDung);
        if (vm is null)
        {
            TempData["Loi"] = "Không tìm thấy yêu cầu.";
            return RedirectToAction(nameof(Index));
        }
        if (vm.TrangThai != Models.TrangThaiYeuCau.ChoDuyet)
        {
            TempData["Loi"] = "Chỉ yêu cầu đang Chờ duyệt mới được xét duyệt.";
            return RedirectToAction(nameof(Details), new { id });
        }
        return View(vm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    [PhanQuyenYeuCau(VaiTroHeThong.Admin, VaiTroHeThong.NguoiDuyet)]
    public async Task<IActionResult> Duyet(int id, DuyetYeuCauViewModel vm)
    {
        if (id != vm.MaYeuCau) return NotFound();

        if (ModelState.IsValid)
        {
            var kq = await _svc.DuyetAsync(vm, NguoiDung);
            if (kq.ThanhCong)
            {
                TempData["ThongBao"] = kq.ThongBao;
                return RedirectToAction(nameof(Details), new { id });
            }
            ModelState.AddModelError(string.Empty, kq.ThongBao);
        }

        // Nạp lại phần chỉ để hiển thị (tên hàng, số lượng yêu cầu), giữ nguyên số lượng người dùng đã nhập
        var goc = await _svc.LayFormDuyetAsync(id, NguoiDung);
        if (goc is null) return NotFound();
        foreach (var dong in goc.Dong)
        {
            var nhap = vm.Dong.FirstOrDefault(d => d.MaChiTietYeuCau == dong.MaChiTietYeuCau);
            if (nhap != null) dong.SoLuongDuyet = nhap.SoLuongDuyet;
        }
        return View(goc);
    }

    [HttpPost, ValidateAntiForgeryToken]
    [PhanQuyenYeuCau(VaiTroHeThong.Admin, VaiTroHeThong.NguoiDuyet)]
    public async Task<IActionResult> DuyetToanBo(int id)
    {
        GhiThongBao(await _svc.DuyetToanBoAsync(id, NguoiDung));
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost, ValidateAntiForgeryToken]
    [PhanQuyenYeuCau(VaiTroHeThong.Admin, VaiTroHeThong.NguoiDuyet)]
    public async Task<IActionResult> TuChoi(LyDoViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            TempData["Loi"] = "Phải nhập lý do khi từ chối yêu cầu.";
            return RedirectToAction(nameof(Details), new { id = vm.MaYeuCau });
        }
        GhiThongBao(await _svc.TuChoiAsync(vm, NguoiDung));
        return RedirectToAction(nameof(Details), new { id = vm.MaYeuCau });
    }

    // ------------------------------------------------------------------ HỦY
    [HttpPost, ValidateAntiForgeryToken]
    [PhanQuyenYeuCau(VaiTroHeThong.Admin, VaiTroHeThong.NhanVienDeNghi)]
    public async Task<IActionResult> Huy(LyDoViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            TempData["Loi"] = "Phải nhập lý do khi hủy yêu cầu.";
            return RedirectToAction(nameof(Details), new { id = vm.MaYeuCau });
        }
        GhiThongBao(await _svc.HuyAsync(vm, NguoiDung));
        return RedirectToAction(nameof(Details), new { id = vm.MaYeuCau });
    }
}
