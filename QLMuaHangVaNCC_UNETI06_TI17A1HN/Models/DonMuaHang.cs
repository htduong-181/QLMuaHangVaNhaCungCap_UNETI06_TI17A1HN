using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models
{
    public class DonMuaHang
    {
        [Key]
        public int MaDonMua { get; set; }

        [Required]
        public int MaYeuCau { get; set; }

        [Required]
        public int MaNhaCungCap { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayDat { get; set; } = DateTime.Today;

        [DataType(DataType.Date)]
        public DateTime? NgayGiaoDuKien { get; set; }

        [Required]
        public int NguoiLap { get; set; }

        [Required]
        [StringLength(50)]
        public string TrangThai { get; set; } = "Nhap";

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal TongTien { get; set; }

        [StringLength(500)]
        public string? GhiChu { get; set; }

        [ForeignKey(nameof(MaYeuCau))]
        public virtual YeuCauMuaHang YeuCauMuaHang { get; set; } = null!;

        [ForeignKey(nameof(MaNhaCungCap))]
        public virtual NhaCungCap NhaCungCap { get; set; } = null!;

        [ForeignKey(nameof(NguoiLap))]
        public virtual TaiKhoan NguoiLapNavigation { get; set; } = null!;

        public virtual ICollection<ChiTietDonMua> ChiTietDonMuas { get; set; }
            = new List<ChiTietDonMua>();

        public virtual ICollection<LanGiaoHang> LanGiaoHangs { get; set; }
            = new List<LanGiaoHang>();

        public virtual ICollection<ThanhToanDonMua> ThanhToanDonMuas { get; set; }
            = new List<ThanhToanDonMua>();
    }
}