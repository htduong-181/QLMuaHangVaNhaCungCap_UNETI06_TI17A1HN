// Họ và tên: Hoàng Thùy Dương
// Mã sinh viên: 23103100051
// Nội dung thực hiện: Quản lý bộ phận đề nghị, yêu cầu mua hàng và xét duyệt yêu cầu.

using System.ComponentModel.DataAnnotations;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models
{
    public class BoPhanDeNghi
    {
        [Key]
        public int MaBoPhan { get; set; }
         
        [Required]
        [StringLength(150)]
        public string TenBoPhan { get; set; } = string.Empty;

        [StringLength(100)]
        public string? NguoiPhuTrach { get; set; }

        [Phone]
        [StringLength(20)]
        public string? SoDienThoai { get; set; }

        [StringLength(500)]
        public string? MoTa { get; set; }

        public bool TrangThai { get; set; } = true;


        public virtual ICollection<YeuCauMuaHang> YeuCauMuaHangs { get; set; }
            = new List<YeuCauMuaHang>();
    }
}