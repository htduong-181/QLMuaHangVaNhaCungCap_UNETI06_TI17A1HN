using System.ComponentModel.DataAnnotations;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models
{
    public class TaiKhoan
    {
        [Key]
        public int MaTaiKhoan { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDangNhap { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string MatKhau { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string VaiTro { get; set; } = "NhanVienDeNghi";

        public bool TrangThai { get; set; } = true;


        public virtual ICollection<YeuCauMuaHang> YeuCauMuaHangs { get; set; }
            = new List<YeuCauMuaHang>();

        public virtual ICollection<DonMuaHang> DonMuaHangs { get; set; }
            = new List<DonMuaHang>();

        public virtual ICollection<LanGiaoHang> LanGiaoHangs { get; set; }
            = new List<LanGiaoHang>();

        public virtual ICollection<ThanhToanDonMua> ThanhToanDonMuas { get; set; }
            = new List<ThanhToanDonMua>();

        public virtual ICollection<LichSuTrangThai> LichSuTrangThais { get; set; }
            = new List<LichSuTrangThai>();
    }
}