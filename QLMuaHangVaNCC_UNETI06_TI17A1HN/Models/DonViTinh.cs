using System.ComponentModel.DataAnnotations;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models
{
    public class DonViTinh
    {
        [Key]
        public int MaDonViTinh { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDonViTinh { get; set; } = string.Empty;

        [StringLength(500)]
        public string? MoTa { get; set; }

        public bool TrangThai { get; set; } = true;

        // ===== Navigation =====
        public virtual ICollection<HangHoa> HangHoas { get; set; } = new List<HangHoa>();
    }
}