using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models
{
    public class ChiTietGiaoHang
    {
        [Key]
        public int MaChiTietGiao { get; set; }


        [Required]
        public int MaLanGiao { get; set; }


        [Required]
        public int MaChiTietDon { get; set; }


        [Required]
        [Range(1, int.MaxValue)]
        public int SoLuongNhan { get; set; }


        [Range(0, int.MaxValue)]
        public int? SoLuongDatChatLuong { get; set; }


        [StringLength(500)]
        public string? GhiChu { get; set; }


        [ForeignKey(nameof(MaLanGiao))]
        public virtual LanGiaoHang LanGiaoHang { get; set; } = null!;


        [ForeignKey(nameof(MaChiTietDon))]
        public virtual ChiTietDonMua ChiTietDonMua { get; set; } = null!;
    }
}