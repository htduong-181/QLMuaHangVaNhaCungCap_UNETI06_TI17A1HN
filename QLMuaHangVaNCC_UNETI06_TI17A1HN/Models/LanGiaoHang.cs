using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models
{
    public class LanGiaoHang
    {
        [Key]
        public int MaLanGiao { get; set; }


        [Required]
        public int MaDonMua { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayGiao { get; set; } = DateTime.Now;


        [Required]
        public int NguoiNhan { get; set; }


        [StringLength(50)]
        public string? SoChungTu { get; set; }


        [StringLength(500)]
        public string? GhiChu { get; set; }


        public bool TrangThai { get; set; } = true;


        [ForeignKey(nameof(MaDonMua))]
        public virtual DonMuaHang DonMuaHang { get; set; } = null!;


        [ForeignKey(nameof(NguoiNhan))]
        public virtual TaiKhoan NguoiNhanNavigation { get; set; } = null!;


        public virtual ICollection<ChiTietGiaoHang> ChiTietGiaoHangs { get; set; }
            = new List<ChiTietGiaoHang>();
    }
}