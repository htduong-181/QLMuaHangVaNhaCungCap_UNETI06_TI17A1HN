using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models
{
    public class YeuCauMuaHang
    {
        [Key]
        public int MaYeuCau { get; set; }


        [Required]
        public int MaBoPhan { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayYeuCau { get; set; } = DateTime.Now;


        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayCanHang { get; set; } = DateTime.Now.AddDays(7);


        [Required]
        [StringLength(20)]
        public string MucDoUuTien { get; set; } = "BinhThuong";


        [Required]
        [StringLength(1000)]
        public string LyDoMua { get; set; } = string.Empty;


        [Required]
        public int NguoiLap { get; set; }


        [Required]
        [StringLength(20)]
        public string TrangThai { get; set; } = "Nhap";


        public DateTime? NgayDuyet { get; set; }


        public int? NguoiDuyet { get; set; }


        [StringLength(500)]
        public string? LyDoTuChoiHuy { get; set; }


        [ForeignKey(nameof(MaBoPhan))]
        public virtual BoPhanDeNghi BoPhanDeNghi { get; set; } = null!;


        [ForeignKey(nameof(NguoiLap))]
        public virtual TaiKhoan NguoiLapNavigation { get; set; } = null!;


        [ForeignKey(nameof(NguoiDuyet))]
        public virtual TaiKhoan? NguoiDuyetNavigation { get; set; }


        public virtual ICollection<ChiTietYeuCau> ChiTietYeuCaus { get; set; }
            = new List<ChiTietYeuCau>();


        public virtual ICollection<DonMuaHang> DonMuaHangs { get; set; }
            = new List<DonMuaHang>();
    }
}