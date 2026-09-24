using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models
{
    public class ChiTietDonMua
    {
        [Key]
        public int MaChiTietDon { get; set; }

        [Required]
        public int MaDonMua { get; set; }

        // Trỏ về dòng yêu cầu đã duyệt (kiểm tra số lượng còn được đặt)
        [Required]
        public int MaChiTietYeuCau { get; set; }

        [Required]
        public int MaHang { get; set; }

        [Range(1, int.MaxValue)]
        public int SoLuongDat { get; set; }

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DonGiaMua { get; set; }

        // Thành tiền = SoLuongDat * DonGiaMua (hệ thống tự tính)
        [Column(TypeName = "decimal(18,2)")]
        public decimal ThanhTien { get; set; }

        // ===== Navigation =====
        [ForeignKey("MaDonMua")]
        public virtual DonMuaHang? DonMuaHang { get; set; }

        [ForeignKey("MaChiTietYeuCau")]
        public virtual ChiTietYeuCau? ChiTietYeuCau { get; set; }

        [ForeignKey("MaHang")]
        public virtual HangHoa? HangHoa { get; set; }

        public virtual ICollection<ChiTietGiaoHang> ChiTietGiaoHangs { get; set; } = new List<ChiTietGiaoHang>();
    }
}