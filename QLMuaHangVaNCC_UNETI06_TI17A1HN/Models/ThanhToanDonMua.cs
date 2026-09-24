using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models
{
    public class ThanhToanDonMua
    {
        [Key]
        public int MaThanhToan { get; set; }

        [Required]
        public int MaDonMua { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayThanhToan { get; set; } = DateTime.Now;

        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SoTienThanhToan { get; set; }

        // "TienMat" | "ChuyenKhoan" | "The" | "Khac"
        [Required]
        [StringLength(30)]
        public string PhuongThuc { get; set; } = "ChuyenKhoan";

        [Required]
        public int NguoiThucHien { get; set; }

        [StringLength(500)]
        public string? GhiChu { get; set; }

        // ===== Navigation =====
        [ForeignKey("MaDonMua")]
        public virtual DonMuaHang? DonMuaHang { get; set; }

        [ForeignKey("NguoiThucHien")]
        public virtual TaiKhoan? NguoiThucHienNavigation { get; set; }
    }
}