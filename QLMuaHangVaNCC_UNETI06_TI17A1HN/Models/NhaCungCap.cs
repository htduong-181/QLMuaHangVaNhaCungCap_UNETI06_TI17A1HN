using System.ComponentModel.DataAnnotations;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models
{
    public class NhaCungCap
    {
        [Key]
        public int MaNhaCungCap { get; set; }


        [Required]
        [StringLength(200)]
        public string TenNhaCungCap { get; set; } = string.Empty;


        [StringLength(20)]
        public string? MaSoThue { get; set; }


        [Required]
        [Phone]
        [StringLength(20)]
        public string SoDienThoai { get; set; } = string.Empty;


        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }


        [StringLength(300)]
        public string? DiaChi { get; set; }


        [StringLength(100)]
        public string? NguoiLienHe { get; set; }


        public bool TrangThai { get; set; } = true;


        public virtual ICollection<DonMuaHang> DonMuaHangs { get; set; }
            = new List<DonMuaHang>();


        public virtual ICollection<NhaCungCapHangHoa> NhaCungCapHangHoas { get; set; }
            = new List<NhaCungCapHangHoa>();
    }
}