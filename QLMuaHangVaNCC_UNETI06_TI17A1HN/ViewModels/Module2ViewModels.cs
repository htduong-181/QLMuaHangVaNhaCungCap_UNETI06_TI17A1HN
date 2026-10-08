using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.ViewModels
{
    public class PagedListVm<T>
    {
        public List<T> Items { get; set; } = new();
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        public string? Search { get; set; }
        public string? Sort { get; set; }
        public bool? Status { get; set; }
        public int? MaLoaiHang { get; set; }
        public int? MaDonViTinh { get; set; }
        public int? MaHang { get; set; }
        public int? MaNhaCungCap { get; set; }
        public bool? CoCungUng { get; set; }
        public decimal? GiaTu { get; set; }
        public decimal? GiaDen { get; set; }
        public List<LoaiHang> LoaiHangs { get; set; } = new();
        public List<DonViTinh> DonViTinhs { get; set; } = new();
        public List<HangHoa> HangHoas { get; set; } = new();
        public List<NhaCungCap> NhaCungCaps { get; set; } = new();
    }

    public class HangHoaFormVm
    {
        public int MaHang { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên hàng")]
        [StringLength(200)]
        public string TenHang { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Chọn loại hàng")]
        public int MaLoaiHang { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Chọn đơn vị tính")]
        public int MaDonViTinh { get; set; }
        [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Giá không được âm")]
        public decimal GiaThamKhao { get; set; }
        [StringLength(1000)]
        public string? MoTa { get; set; }
        public bool TrangThai { get; set; } = true;
        public List<LoaiHang> LoaiHangs { get; set; } = new();
        public List<DonViTinh> DonViTinhs { get; set; } = new();
    }

    public class NhaCungCapFormVm
    {
        public int MaNhaCungCap { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên nhà cung cấp")]
        [StringLength(200)]
        public string TenNhaCungCap { get; set; } = string.Empty;
        [StringLength(20)]
        public string? MaSoThue { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression(@"^(\+84|0)[0-9]{9}$", ErrorMessage = "Số điện thoại Việt Nam không hợp lệ")]
        [StringLength(20)]
        public string SoDienThoai { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(150)]
        public string? Email { get; set; }
        [StringLength(300)]
        public string? DiaChi { get; set; }
        [StringLength(100)]
        public string? NguoiLienHe { get; set; }
        public bool TrangThai { get; set; } = true;
    }

    public class CungUngFormVm
    {
        public int Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Chọn nhà cung cấp")]
        public int MaNhaCungCap { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Chọn hàng hóa")]
        public int MaHang { get; set; }
        [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Đơn giá không được âm")]
        public decimal DonGiaBao { get; set; }
        [Range(0, 365, ErrorMessage = "Thời gian giao từ 0 đến 365 ngày")]
        public int? ThoiGianGiaoDuKien { get; set; }
        public bool TrangThai { get; set; } = true;
        public List<NhaCungCap> NhaCungCaps { get; set; } = new();
        public List<HangHoa> HangHoas { get; set; } = new();
    }
}
