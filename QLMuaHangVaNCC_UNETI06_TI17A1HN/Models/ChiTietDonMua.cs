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


        [Required]
        public int MaChiTietYeuCau { get; set; }


        [Required]
        public int MaHang { get; set; }


        [Required]
        [Range(1, int.MaxValue)]
        public int SoLuongDat { get; set; }


        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DonGiaMua { get; set; }


        [NotMapped]
        public decimal ThanhTien => SoLuongDat * DonGiaMua;


        [ForeignKey(nameof(MaDonMua))]
        public virtual DonMuaHang DonMuaHang { get; set; } = null!;


        [ForeignKey(nameof(MaChiTietYeuCau))]
        public virtual ChiTietYeuCau ChiTietYeuCau { get; set; } = null!;


        [ForeignKey(nameof(MaHang))]
        public virtual HangHoa HangHoa { get; set; } = null!;


        public virtual ICollection<ChiTietGiaoHang> ChiTietGiaoHangs { get; set; }
            = new List<ChiTietGiaoHang>();
    }
}