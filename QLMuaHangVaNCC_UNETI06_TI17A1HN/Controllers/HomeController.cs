using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Helpers;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.ViewModels;
using System.Diagnostics;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Controllers;

public class HomeController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

    public HomeController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
    {
        _context = context;
    }

    [PhanQuyen]
    public async Task<IActionResult> Index()
    {
        var vm = new HomeDashboardVm();
        // Đếm trực tiếp toàn bộ từng bảng, không dựa vào phân trang ở Index.
        vm.Bangs = new()
        {
            new("Hàng hóa", "HangHoas", "box-seam", "Danh mục", await _context.HangHoa.CountAsync()),
            new("Loại hàng", "LoaiHangs", "tags", "Danh mục", await _context.LoaiHang.CountAsync()),
            new("Đơn vị tính", "DonViTinhs", "rulers", "Danh mục", await _context.DonViTinh.CountAsync()),
            new("Bộ phận đề nghị", "BoPhanDeNghis", "building", "Danh mục", await _context.BoPhanDeNghi.CountAsync()),
            new("Nhà cung cấp", "NhaCungCaps", "truck", "Nhà cung cấp", await _context.NhaCungCap.CountAsync()),
            new("Hàng NCC cung ứng", "NhaCungCapHangHoas", "boxes", "Nhà cung cấp", await _context.NhaCungCapHangHoa.CountAsync()),
            new("Yêu cầu mua hàng", "YeuCauMuaHangs", "file-earmark-text", "Mua hàng", await _context.YeuCauMuaHang.CountAsync()),
            new("Chi tiết yêu cầu", "ChiTietYeuCaus", "list-check", "Mua hàng", await _context.ChiTietYeuCau.CountAsync()),
            new("Đơn mua hàng", "DonMuaHangs", "receipt", "Mua hàng", await _context.DonMuaHang.CountAsync()),
            new("Chi tiết đơn mua", "ChiTietDonMuas", "cart-check", "Mua hàng", await _context.ChiTietDonMua.CountAsync()),
            new("Lần giao hàng", "LanGiaoHangs", "truck-front", "Giao hàng", await _context.LanGiaoHang.CountAsync()),
            new("Chi tiết giao hàng", "ChiTietGiaoHangs", "box-arrow-in-down", "Giao hàng", await _context.ChiTietGiaoHang.CountAsync()),
            new("Thanh toán đơn mua", "ThanhToanDonMuas", "cash-coin", "Giao hàng", await _context.ThanhToanDonMua.CountAsync()),
            new("Lịch sử trạng thái", "LichSuTrangThais", "clock-history", "Hệ thống", await _context.LichSuTrangThai.CountAsync())
        };

        // Không đưa quản lý tài khoản lên dashboard của người không phải Admin.
        if (HttpContext.Session.GetString("VaiTro") == "Admin")
            vm.Bangs.Add(new("Tài khoản", "TaiKhoans", "people", "Hệ thống", await _context.TaiKhoan.CountAsync()));

        vm.ChoDuyet = await _context.YeuCauMuaHang.CountAsync(x => x.TrangThai == "ChoDuyet");
        vm.DaDuyet = await _context.YeuCauMuaHang.CountAsync(x => x.TrangThai == "DaDuyet");
        vm.TongDonMua = await _context.DonMuaHang.CountAsync();
        vm.NhaCungCapDangHoatDong = await _context.NhaCungCap.CountAsync(x => x.TrangThai);
        vm.TongGiaTriDonMua = await _context.DonMuaHang
            .Where(x => x.TrangThai != "DaHuy" && x.TrangThai != "Nhap" && x.TrangThai != "Đã hủy" && x.TrangThai != "Nháp")
            .SumAsync(x => (decimal?)x.TongTien) ?? 0;
        vm.DaThanhToan = await _context.ThanhToanDonMua.SumAsync(x => (decimal?)x.SoTienThanhToan) ?? 0;
        ViewBag.HoTen = HttpContext.Session.GetString("HoTen");
        return View(vm);
    }

    [PhanQuyen]
    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
