using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models
{
    public class NhaCungCapHangHoa
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public int MaNhaCungCap { get; set; }


        [Required]
        public int MaHang { get; set; }


        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DonGiaBao { get; set; }


        [DataType(DataType.Date)]
        public DateTime? NgayCapNhatGia { get; set; }


        [Range(0, 365)]
        public int? ThoiGianGiaoDuKien { get; set; }


        public bool TrangThai { get; set; } = true;


        [ForeignKey(nameof(MaNhaCungCap))]
        public virtual NhaCungCap NhaCungCap { get; set; } = null!;


        [ForeignKey(nameof(MaHang))]
        public virtual HangHoa HangHoa { get; set; } = null!;
    }
}