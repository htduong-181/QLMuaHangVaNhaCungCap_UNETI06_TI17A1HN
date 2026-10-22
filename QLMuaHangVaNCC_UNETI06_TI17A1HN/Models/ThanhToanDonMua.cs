using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models
{
    public class ThanhToanDonMua
    {
        [Key]
        [Display(Name = "Mã thanh toán")]
        public int MaThanhToan { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn đơn mua hàng")]
        [Display(Name = "Mã đơn mua")]
        public int MaDonMua { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày thanh toán")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày thanh toán")]
        public DateTime NgayThanhToan { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Vui lòng nhập số tiền thanh toán")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Số tiền thanh toán phải lớn hơn 0")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Số tiền thanh toán")]
        public decimal SoTienThanhToan { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
        [StringLength(30, ErrorMessage = "Phương thức không vượt quá 30 ký tự")]
        [Display(Name = "Phương thức thanh toán")]
        public string PhuongThuc { get; set; } = "ChuyenKhoan";

        [Required(ErrorMessage = "Vui lòng chọn người thực hiện")]
        [Display(Name = "Người thực hiện")]
        public int NguoiThucHien { get; set; }

        [StringLength(500, ErrorMessage = "Ghi chú không vượt quá 500 ký tự")]
        [Display(Name = "Ghi chú")]
        public string? GhiChu { get; set; }

        [ForeignKey(nameof(MaDonMua))]
        [ValidateNever]
        [Display(Name = "Đơn mua hàng")]
        public virtual DonMuaHang DonMuaHang { get; set; } = null!;

        [ForeignKey(nameof(NguoiThucHien))]
        [ValidateNever]
        [Display(Name = "Người thực hiện")]
        public virtual TaiKhoan NguoiThucHienNavigation { get; set; } = null!;
    }
}