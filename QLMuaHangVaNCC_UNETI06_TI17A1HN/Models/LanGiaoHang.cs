using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models
{
    public class LanGiaoHang
    {
        [Key]
        public int MaLanGiao { get; set; }

        [Required]
        [Display(Name = "Mã đơn mua")]
        public int MaDonMua { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày giao")]
        public DateTime NgayGiao { get; set; } = DateTime.Today;

        [Required]
        [Display(Name = "Người nhận")]
        public int NguoiNhan { get; set; }

        [StringLength(50)]
        [Display(Name = "Số chứng từ")]
        public string? SoChungTu { get; set; }

        [StringLength(500)]
        [Display(Name = "Ghi chú")]
        public string? GhiChu { get; set; }

        [Display(Name = "Còn hiệu lực")]
        public bool TrangThai { get; set; } = true;

        [ForeignKey(nameof(MaDonMua))]
        public virtual DonMuaHang? DonMuaHang { get; set; }

        [ForeignKey(nameof(NguoiNhan))]
        public virtual TaiKhoan? NguoiNhanNavigation { get; set; }

        public virtual ICollection<ChiTietGiaoHang> ChiTietGiaoHangs { get; set; }
            = new List<ChiTietGiaoHang>();
    }
}