// Họ và tên: Hoàng Thùy Dương
// Mã sinh viên: 23103100051
// Nội dung thực hiện: Quản lý bộ phận đề nghị, yêu cầu mua hàng và xét duyệt yêu cầu.
using System.ComponentModel.DataAnnotations;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.ViewModels
{
    // Form tạo/sửa phần đầu Yêu cầu mua hàng (NguoiLap, TrangThai do Controller tự gán)
    public class YeuCauMuaHangFormViewModel
    {
        public int MaYeuCau { get; set; }

        [Required(ErrorMessage = "Chọn bộ phận đề nghị")]
        [Display(Name = "Bộ phận đề nghị")]
        public int MaBoPhan { get; set; }

        [Required(ErrorMessage = "Chọn ngày cần hàng")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày cần hàng")]
        public DateTime NgayCanHang { get; set; } = DateTime.Today.AddDays(7);

        [Required(ErrorMessage = "Chọn mức độ ưu tiên")]
        [Display(Name = "Mức độ ưu tiên")]
        public string MucDoUuTien { get; set; } = Models.MucDoUuTien.BinhThuong;

        [Required(ErrorMessage = "Nhập lý do mua")]
        [StringLength(1000)]
        [Display(Name = "Lý do mua")]
        public string LyDoMua { get; set; } = "";
    }

    // Một dòng chi tiết trong form thêm/sửa
    public class ChiTietYeuCauFormViewModel
    {
        public int MaChiTietYeuCau { get; set; }
        public int MaYeuCau { get; set; }

        [Required(ErrorMessage = "Chọn hàng hóa")]
        [Display(Name = "Hàng hóa")]
        public int MaHang { get; set; }

        [Required(ErrorMessage = "Nhập số lượng")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng yêu cầu phải > 0")]
        [Display(Name = "Số lượng yêu cầu")]
        public int SoLuongYeuCau { get; set; }

        [StringLength(500)]
        [Display(Name = "Ghi chú")]
        public string? GhiChu { get; set; }
    }

    // Màn hình xét duyệt: cho phép Người duyệt sửa số lượng duyệt từng dòng
    public class XetDuyetViewModel
    {
        public int MaYeuCau { get; set; }
        public string TenBoPhan { get; set; } = "";
        public string NguoiLapHoTen { get; set; } = "";
        public DateTime NgayYeuCau { get; set; }
        public DateTime NgayCanHang { get; set; }
        public string LyDoMua { get; set; } = "";

        public List<XetDuyetChiTietViewModel> ChiTiet { get; set; } = new();
    }

    public class XetDuyetChiTietViewModel
    {
        public int MaChiTietYeuCau { get; set; }
        public string TenHang { get; set; } = "";
        public string DonViTinh { get; set; } = "";
        public int SoLuongYeuCau { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng duyệt phải >= 0")]
        public int SoLuongDuyet { get; set; }
    }

    // Form từ chối / hủy (cần ghi lý do)
    public class LyDoViewModel
    {
        public int MaYeuCau { get; set; }
        [Required(ErrorMessage = "Nhập lý do")]
        [StringLength(500)]
        public string LyDo { get; set; } = "";
    }
}
