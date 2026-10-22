// Họ và tên: Hoàng Thùy Dương
// Mã sinh viên: 23103100051
// Nội dung thực hiện: Quản lý bộ phận đề nghị, yêu cầu mua hàng và xét duyệt yêu cầu.

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models
{
    // Khớp với giá trị mặc định trong YeuCauMuaHang.cs ("Nhap", "BinhThuong")
    public static class TrangThaiYeuCau
    {
        public const string Nhap = "Nhap";
        public const string ChoDuyet = "ChoDuyet";
        public const string DaDuyet = "DaDuyet";
        public const string TuChoi = "TuChoi";
        public const string DaHuy = "DaHuy";

        public static readonly string[] TatCa = { Nhap, ChoDuyet, DaDuyet, TuChoi, DaHuy };

        public static string NhanTiengViet(string trangThai) => trangThai switch
        {
            Nhap => "Nháp",
            ChoDuyet => "Chờ duyệt",
            DaDuyet => "Đã duyệt",
            TuChoi => "Từ chối",
            DaHuy => "Đã hủy",
            _ => trangThai
        };

        public static string MauBadge(string trangThai) => trangThai switch
        {
            Nhap => "secondary",
            ChoDuyet => "warning",
            DaDuyet => "success",
            TuChoi => "danger",
            DaHuy => "dark",
            _ => "light"
        };
    }

    public static class MucDoUuTien
    {
        public const string Thap = "Thap";
        public const string BinhThuong = "BinhThuong";
        public const string Cao = "Cao";

        public static readonly string[] TatCa = { Thap, BinhThuong, Cao };

        public static string NhanTiengViet(string mucDo) => mucDo switch
        {
            Thap => "Thấp",
            BinhThuong => "Bình thường",
            Cao => "Cao",
            _ => mucDo
        };
    }
}
