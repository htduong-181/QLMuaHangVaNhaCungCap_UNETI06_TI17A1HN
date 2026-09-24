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

        [Range(1, int.MaxValue)]
        public int SoLuongNhan { get; set; }

        // Có thể null nếu nhóm chỉ quản lý số lượng
        [Range(0, int.MaxValue)]
        public int? SoLuongDatChatLuong { get; set; }

        [StringLength(500)]
        public string? GhiChu { get; set; }

        // ===== Navigation =====
        [ForeignKey("MaLanGiao")]
        public virtual LanGiaoHang? LanGiaoHang { get; set; }

        [ForeignKey("MaChiTietDon")]
        public virtual ChiTietDonMua? ChiTietDonMua { get; set; }
    }
}