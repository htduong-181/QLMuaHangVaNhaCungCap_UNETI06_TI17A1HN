// Họ và tên: Lê Tiến Công
// Mã sinh viên: 23103100050
// Nội dung thực hiện: Chức năng phân quyền tài khoản, loại hàng, đơn vị tính

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


        public virtual ICollection<HangHoa> HangHoas { get; set; }
            = new List<HangHoa>();
    }
}