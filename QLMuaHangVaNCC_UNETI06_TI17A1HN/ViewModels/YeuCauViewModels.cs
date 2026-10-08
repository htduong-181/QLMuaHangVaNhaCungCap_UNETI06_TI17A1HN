// Họ và tên: Hoàng Thùy Dương
// Mã sinh viên: 23103100051
// Nội dung thực hiện: Module 3 - ViewModel cho danh sách (tìm kiếm/lọc/sắp xếp/phân trang),
// form lập yêu cầu, chi tiết yêu cầu, xét duyệt và từ chối/hủy.

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.ViewModels;

// Nếu SV2 đã có lớp phân trang chung thì dùng chung và xóa lớp này.
public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalItems { get; set; }
    public int TotalPages => Math.Max(1, (int)Math.Ceiling(TotalItems / (double)PageSize));
    public bool HasPrevious => Page > 1;
    public bool HasNext => Page < TotalPages;
}

// ---------- Danh sách ----------
public class YeuCauFilterViewModel
{
    [Display(Name = "Từ khóa")] public string? TuKhoa { get; set; }
    [Display(Name = "Trạng thái")] public string? TrangThai { get; set; }
    [Display(Name = "Mức ưu tiên")] public string? MucDoUuTien { get; set; }
    [DataType(DataType.Date)] public DateTime? TuNgay { get; set; }
    [DataType(DataType.Date)] public DateTime? DenNgay { get; set; }
    [DataType(DataType.Date)] public DateTime? CanTuNgay { get; set; }
    [DataType(DataType.Date)] public DateTime? CanDenNgay { get; set; }
    public string? SapXep { get; set; }
    public int Page { get; set; } = 1;
}

public class YeuCauListItemViewModel
{
    public int MaYeuCau { get; set; }
    public string MaHienThi => $"YC{MaYeuCau:D4}";
    public string? TenBoPhan { get; set; }
    public DateTime NgayYeuCau { get; set; }
    public DateTime NgayCanHang { get; set; }
    public string MucDoUuTien { get; set; } = string.Empty;
    public string TrangThai { get; set; } = string.Empty;
    public string? TenNguoiLap { get; set; }
    public int SoDong { get; set; }
    public int TongSoLuongYeuCau { get; set; }
}

public class YeuCauIndexViewModel
{
    public YeuCauFilterViewModel Filter { get; set; } = new();
    public PagedResult<YeuCauListItemViewModel> KetQua { get; set; } = new();
}

// ---------- Form tạo / sửa ----------
public class YeuCauFormViewModel : IValidatableObject
{
    public int MaYeuCau { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn bộ phận đề nghị")]
    [Display(Name = "Bộ phận đề nghị")]
    public int? MaBoPhan { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập ngày yêu cầu")]
    [DataType(DataType.Date)]
    [Display(Name = "Ngày yêu cầu")]
    public DateTime? NgayYeuCau { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Vui lòng nhập ngày cần hàng")]
    [DataType(DataType.Date)]
    [Display(Name = "Ngày cần hàng")]
    public DateTime? NgayCanHang { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn mức độ ưu tiên")]
    [Display(Name = "Mức độ ưu tiên")]
    public string? MucDoUuTien { get; set; } = MucUuTien.BinhThuong;

    [Required(ErrorMessage = "Vui lòng nhập lý do mua")]
    [StringLength(1000, ErrorMessage = "Lý do mua tối đa 1000 ký tự")]
    [Display(Name = "Lý do mua")]
    public string? LyDoMua { get; set; }

    public IEnumerable<SelectListItem>? DanhSachBoPhan { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (NgayYeuCau.HasValue && NgayCanHang.HasValue)
        {
            var yc = NgayYeuCau.Value.Date;
            var ch = NgayCanHang.Value.Date;
            if (ch < yc)
                yield return new ValidationResult("Ngày cần hàng không được trước ngày yêu cầu",
                    new[] { nameof(NgayCanHang) });
            else if (ch > yc.AddDays(365))
                yield return new ValidationResult("Ngày cần hàng không nên cách ngày yêu cầu quá 1 năm",
                    new[] { nameof(NgayCanHang) });
        }

        // Chỉ kiểm tra khi tạo mới (khi sửa, ngày yêu cầu không đổi)
        if (MaYeuCau == 0 && NgayYeuCau.HasValue && NgayYeuCau.Value.Date > DateTime.Today)
            yield return new ValidationResult("Ngày yêu cầu không được ở tương lai",
                new[] { nameof(NgayYeuCau) });

        if (!string.IsNullOrEmpty(MucDoUuTien) && !MucUuTien.TatCa.Contains(MucDoUuTien))
            yield return new ValidationResult("Mức độ ưu tiên không hợp lệ", new[] { nameof(MucDoUuTien) });
    }
}

// ---------- Chi tiết ----------
public class ChiTietYeuCauFormViewModel
{
    public int MaYeuCau { get; set; }
    public int MaChiTietYeuCau { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn hàng hóa")]
    [Display(Name = "Hàng hóa")]
    public int? MaHang { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số lượng")]
    [Range(1, 1000000, ErrorMessage = "Số lượng yêu cầu phải lớn hơn 0")]
    [Display(Name = "Số lượng yêu cầu")]
    public int? SoLuongYeuCau { get; set; }

    [StringLength(500, ErrorMessage = "Ghi chú tối đa 500 ký tự")]
    [Display(Name = "Ghi chú")]
    public string? GhiChu { get; set; }

    // Chỉ để hiển thị khi sửa dòng
    public string? TenHang { get; set; }
}

public class DongChiTietViewModel
{
    public int MaChiTietYeuCau { get; set; }
    public int MaHang { get; set; }
    public string? TenHang { get; set; }
    public string? TenDonViTinh { get; set; }
    public int SoLuongYeuCau { get; set; }
    public int SoLuongDuyet { get; set; }
    public string? GhiChu { get; set; }
}

public class YeuCauDetailsViewModel
{
    public int MaYeuCau { get; set; }
    public string MaHienThi => $"YC{MaYeuCau:D4}";
    public string? TenBoPhan { get; set; }
    public DateTime NgayYeuCau { get; set; }
    public DateTime NgayCanHang { get; set; }
    public string MucDoUuTien { get; set; } = string.Empty;
    public string LyDoMua { get; set; } = string.Empty;
    public int NguoiLap { get; set; }
    public string? TenNguoiLap { get; set; }
    public string TrangThai { get; set; } = string.Empty;
    public DateTime? NgayDuyet { get; set; }
    public string? TenNguoiDuyet { get; set; }
    public string? LyDoTuChoiHuy { get; set; }
    public List<DongChiTietViewModel> Dong { get; set; } = new();

    // Cờ hiển thị nút theo trạng thái + quyền (Controller/Service vẫn kiểm tra lại)
    public bool CoTheSua { get; set; }
    public bool CoTheGuiDuyet { get; set; }
    public bool CoTheDuyet { get; set; }
    public bool CoTheHuy { get; set; }
    public bool CoTheXoa { get; set; }

    public int TongSoLuongYeuCau => Dong.Sum(d => d.SoLuongYeuCau);
    public int TongSoLuongDuyet => Dong.Sum(d => d.SoLuongDuyet);

    // Dùng cho form thêm dòng
    public ChiTietYeuCauFormViewModel ChiTietMoi { get; set; } = new();
    public IEnumerable<SelectListItem>? DanhSachHang { get; set; }
}

// ---------- Xét duyệt ----------
public class DuyetDongViewModel
{
    public int MaChiTietYeuCau { get; set; }
    public string? TenHang { get; set; }
    public string? TenDonViTinh { get; set; }
    public int SoLuongYeuCau { get; set; }

    [Required(ErrorMessage = "Nhập số lượng duyệt")]
    [Range(0, 1000000, ErrorMessage = "Số lượng duyệt không hợp lệ")]
    [Display(Name = "Số lượng duyệt")]
    public int? SoLuongDuyet { get; set; }
}

public class DuyetYeuCauViewModel
{
    public int MaYeuCau { get; set; }
    public string MaHienThi => $"YC{MaYeuCau:D4}";
    public string? TenBoPhan { get; set; }
    public string? LyDoMua { get; set; }
    public string? TrangThai { get; set; }
    public List<DuyetDongViewModel> Dong { get; set; } = new();
}

public class LyDoViewModel
{
    public int MaYeuCau { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập lý do")]
    [StringLength(500, ErrorMessage = "Lý do tối đa 500 ký tự")]
    [Display(Name = "Lý do")]
    public string? LyDo { get; set; }
}
