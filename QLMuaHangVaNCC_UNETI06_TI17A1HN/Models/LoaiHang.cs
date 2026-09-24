using System.ComponentModel.DataAnnotations;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models
{
    public class LoaiHang
    {
        [Key]
        public int MaLoaiHang { get; set; }

        [Required]
        [StringLength(100)]
        public string TenLoaiHang { get; set; } = string.Empty;

        [StringLength(500)]
        public string? MoTa { get; set; }

        public bool TrangThai { get; set; } = true;

        // ===== Navigation =====
        public virtual ICollection<HangHoa> HangHoas { get; set; } = new List<HangHoa>();
    }
}