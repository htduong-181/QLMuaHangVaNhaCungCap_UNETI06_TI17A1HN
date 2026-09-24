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

        // ===== Navigation =====
        public virtual ICollection<YeuCauMuaHang> YeuCauMuaHangs { get; set; } = new List<YeuCauMuaHang>();
    }
}