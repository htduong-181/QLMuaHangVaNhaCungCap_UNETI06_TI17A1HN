using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models
{
    public class LichSuTrangThai
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(30)]
        public string LoaiDoiTuong { get; set; } = string.Empty;


        [Required]
        public int MaDoiTuong { get; set; }


        [StringLength(30)]
        public string? TrangThaiCu { get; set; }


        [Required]
        [StringLength(30)]
        public string TrangThaiMoi { get; set; } = string.Empty;


        [Required]
        public int NguoiThucHien { get; set; }


        public DateTime ThoiGian { get; set; } = DateTime.Now;


        [StringLength(500)]
        public string? GhiChu { get; set; }


        [ForeignKey(nameof(NguoiThucHien))]
        public virtual TaiKhoan NguoiThucHienNavigation { get; set; } = null!;
    }
}