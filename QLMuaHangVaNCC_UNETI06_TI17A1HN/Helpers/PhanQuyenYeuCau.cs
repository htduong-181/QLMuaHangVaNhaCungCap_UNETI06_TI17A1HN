// Họ và tên: Hoàng Thùy Dương
// Mã sinh viên: 23103100051
// Nội dung thực hiện: Module 3 - Đọc người dùng từ Session và Filter kiểm tra quyền tại Controller.
// (Nếu SV1 đã có Helper/Attribute tương đương thì có thể thay bằng của SV1 và xóa file này.)

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Helpers;

public record NguoiDungHienTai(int MaTaiKhoan, string HoTen, string VaiTro);

public static class VaiTroHeThong
{
    public const string Admin = "Admin";
    public const string NhanVienDeNghi = "NhanVienDeNghi";
    public const string NhanVienMuaHang = "NhanVienMuaHang";
    public const string NguoiDuyet = "NguoiDuyet";

    /// <summary>Vai trò được xét duyệt / từ chối yêu cầu.</summary>
    public static bool CoQuyenDuyet(string vaiTro) => vaiTro == Admin || vaiTro == NguoiDuyet;
}

public static class PhienDangNhapExtensions
{
    // Quy ước với SV1 khi đăng nhập:
    //   Session.SetInt32("MaTaiKhoan", ...); Session.SetString("HoTen", ...); Session.SetString("VaiTro", ...);
    public static NguoiDungHienTai? LayNguoiDung(this ISession session)
    {
        var ma = session.GetInt32("MaTaiKhoan");
        var vaiTro = session.GetString("VaiTro");
        if (ma is null || string.IsNullOrEmpty(vaiTro)) return null;
        return new NguoiDungHienTai(ma.Value, session.GetString("HoTen") ?? string.Empty, vaiTro);
    }
}

/// <summary>
/// Kiểm tra đăng nhập + vai trò TRƯỚC KHI action chạy (không chỉ ẩn nút trên View).
/// Dùng: [PhanQuyenYeuCau(VaiTroHeThong.Admin, VaiTroHeThong.NhanVienDeNghi)]
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class PhanQuyenYeuCauAttribute : ActionFilterAttribute
{
    private readonly string[] _vaiTroChoPhep;

    public PhanQuyenYeuCauAttribute(params string[] vaiTroChoPhep)
    {
        _vaiTroChoPhep = vaiTroChoPhep;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var nguoiDung = context.HttpContext.Session.LayNguoiDung();
        if (nguoiDung is null)
        {
            context.Result = new RedirectToActionResult("DangNhap", "TaiKhoan", null);
            return;
        }

        if (!_vaiTroChoPhep.Contains(nguoiDung.VaiTro))
        {
            if (context.Controller is Controller c)
            {
                c.TempData["Loi"] = "Bạn không có quyền thực hiện chức năng này.";
            }
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}
