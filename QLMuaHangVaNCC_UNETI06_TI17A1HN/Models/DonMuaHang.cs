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
        public DateTime NgayDat { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayGiaoDuKien { get; set; } = DateTime.Now.AddDays(7);

        [Required]
        public int NguoiLap { get; set; }

        // "Nhap" | "DaDatHang" | "GiaoMotPhan" | "DaGiaoDu" | "HoanThanh" | "DaHuy"
        [Required]
        [StringLength(20)]
        public string TrangThai { get; set; } = "Nhap";

        [StringLength(500)]
        public string? GhiChu { get; set; }

        // ===== Navigation =====
        [ForeignKey("MaYeuCau")]
        public virtual YeuCauMuaHang? YeuCauMuaHang { get; set; }

        [ForeignKey("MaNhaCungCap")]
        public virtual NhaCungCap? NhaCungCap { get; set; }

        [ForeignKey("NguoiLap")]
        public virtual TaiKhoan? NguoiLapNavigation { get; set; }

        public virtual ICollection<ChiTietDonMua> ChiTietDonMuas { get; set; } = new List<ChiTietDonMua>();
        public virtual ICollection<LanGiaoHang> LanGiaoHangs { get; set; } = new List<LanGiaoHang>();
        public virtual ICollection<ThanhToanDonMua> ThanhToanDonMuas { get; set; } = new List<ThanhToanDonMua>();
    }
}