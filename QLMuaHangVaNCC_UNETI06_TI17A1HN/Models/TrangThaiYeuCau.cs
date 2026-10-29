// Họ và tên: Hoàng Thùy Dương
// Mã sinh viên: 23103100051
// Nội dung thực hiện: Module 3 - Hằng số trạng thái / mức ưu tiên của yêu cầu mua hàng
// (giá trị lưu DB không dấu, có hàm đổi sang nhãn tiếng Việt để hiển thị).
// Module 4, 5 dùng chung các hằng số này khi kiểm tra / đổi trạng thái yêu cầu.

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

public static class TrangThaiYeuCau
{
    public const string Nhap = "Nhap";
    public const string ChoDuyet = "ChoDuyet";
    public const string DaDuyet = "DaDuyet";
    public const string TuChoi = "TuChoi";
    public const string DaTaoDon = "DaTaoDon";
    public const string HoanThanh = "HoanThanh";
    public const string DaHuy = "DaHuy";

    public static readonly string[] TatCa =
        { Nhap, ChoDuyet, DaDuyet, TuChoi, DaTaoDon, HoanThanh, DaHuy };

    public static string Nhan(string? ma) => ma switch
    {
        Nhap => "Nháp",
        ChoDuyet => "Chờ duyệt",
        DaDuyet => "Đã duyệt",
        TuChoi => "Từ chối",
        DaTaoDon => "Đã tạo đơn",
        HoanThanh => "Hoàn thành",
        DaHuy => "Đã hủy",
        _ => ma ?? ""
    };

    public static string MauBadge(string? ma) => ma switch
    {
        Nhap => "secondary",
        ChoDuyet => "warning text-dark",
        DaDuyet => "success",
        TuChoi => "danger",
        DaTaoDon => "info text-dark",
        HoanThanh => "primary",
        DaHuy => "dark",
        _ => "secondary"
    };
}

public static class MucUuTien
{
    public const string Thap = "Thap";
    public const string BinhThuong = "BinhThuong";
    public const string Cao = "Cao";
    public const string Khan = "Khan";

    public static readonly string[] TatCa = { Thap, BinhThuong, Cao, Khan };

    public static string Nhan(string? ma) => ma switch
    {
        Thap => "Thấp",
        BinhThuong => "Bình thường",
        Cao => "Cao",
        Khan => "Khẩn cấp",
        _ => ma ?? ""
    };

    public static string MauBadge(string? ma) => ma switch
    {
        Khan => "danger",
        Cao => "warning text-dark",
        BinhThuong => "primary",
        _ => "secondary"
    };
}
