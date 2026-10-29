using System.ComponentModel.DataAnnotations;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Models.ViewModels
{
    public class DonMuaHangCreateViewModel
    {
        [Required]
        public int MaYeuCau { get; set; }

        [Required]
        public int MaNhaCungCap { get; set; }

        [Required]
        public int NguoiLap { get; set; }

        public DateTime? NgayGiaoDuKien { get; set; }

        [StringLength(500)]
        public string? GhiChu { get; set; }

        public List<DonMuaHangItemViewModel> ChiTiet { get; set; } = new();
    }

    public class DonMuaHangItemViewModel
    {
        public int MaChiTietYeuCau { get; set; }

        public int MaHang { get; set; }

        public string TenHang { get; set; } = "";

        public int SoLuongYeuCau { get; set; }

        public int SoLuongDuyet { get; set; }

        public int SoLuongDaDat { get; set; }

        public int SoLuongConDuocDat { get; set; }

        [Range(0, int.MaxValue)]
        public int SoLuongDat { get; set; }

        [Range(0, double.MaxValue)]
        public decimal DonGiaMua { get; set; }

        public decimal ThanhTien => SoLuongDat * DonGiaMua;
    }
}