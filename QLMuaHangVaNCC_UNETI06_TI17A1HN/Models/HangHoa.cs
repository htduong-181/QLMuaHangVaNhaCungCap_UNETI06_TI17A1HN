using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models
{
    public class HangHoa
    {
        [Key]
        public int MaHang { get; set; }

        [Required]
        [StringLength(200)]
        public string TenHang { get; set; } = string.Empty;

        [Required]
        public int MaLoaiHang { get; set; }

        [Required]
        public int MaDonViTinh { get; set; }

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal GiaThamKhao { get; set; }

        [StringLength(1000)]
        public string? MoTa { get; set; }

        public bool TrangThai { get; set; } = true;

        // ===== Navigation =====
        [ForeignKey("MaLoaiHang")]
        public virtual LoaiHang? LoaiHang { get; set; }

        [ForeignKey("MaDonViTinh")]
        public virtual DonViTinh? DonViTinh { get; set; } 

        public virtual ICollection<ChiTietYeuCau> ChiTietYeuCaus { get; set; } = new List<ChiTietYeuCau>();
        public virtual ICollection<ChiTietDonMua> ChiTietDonMuas { get; set; } = new List<ChiTietDonMua>();
        public virtual ICollection<NhaCungCapHangHoa> NhaCungCapHangHoas { get; set; } = new List<NhaCungCapHangHoa>();
    }
}