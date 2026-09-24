using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models
{
    public class ChiTietYeuCau
    {
        [Key]
        public int MaChiTietYeuCau { get; set; }

        [Required]
        public int MaYeuCau { get; set; }

        [Required]
        public int MaHang { get; set; }

        [Range(1, int.MaxValue)]
        public int SoLuongYeuCau { get; set; }

        [Range(0, int.MaxValue)]
        public int SoLuongDuyet { get; set; } = 0;

        [StringLength(500)]
        public string? GhiChu { get; set; }

        // ===== Navigation =====
        [ForeignKey("MaYeuCau")]
        public virtual YeuCauMuaHang? YeuCauMuaHang { get; set; }

        [ForeignKey("MaHang")]
        public virtual HangHoa? HangHoa { get; set; }

        public virtual ICollection<ChiTietDonMua> ChiTietDonMuas { get; set; } = new List<ChiTietDonMua>();
    }
}