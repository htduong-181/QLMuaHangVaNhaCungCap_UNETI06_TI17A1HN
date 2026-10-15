using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.ViewModels
{
    public class DongDoiChieuVM
    {
        public int MaChiTietDon { get; set; }
        public int MaHang { get; set; }
        public string TenHang { get; set; } = string.Empty;
        public int SoLuongDat { get; set; }
        public int TongDaNhan { get; set; }
        public int ConPhaiGiao => SoLuongDat - TongDaNhan;
        public double TyLeGiao => SoLuongDat == 0 ? 0 : Math.Round(TongDaNhan * 100.0 / SoLuongDat, 1);

        public int SoLuongNhan { get; set; }
        public int? SoLuongDatChatLuong { get; set; }
        [StringLength(500)]
        public string? GhiChu { get; set; }
    }

    public class LanGiaoHangVM
    {
        [Range(1, int.MaxValue, ErrorMessage = "Phải chọn đơn mua.")]
        public int MaDonMua { get; set; }

        [Required(ErrorMessage = "Phải nhập ngày giao.")]
        public DateTime NgayGiao { get; set; } = DateTime.Today;

        [Range(1, int.MaxValue, ErrorMessage = "Phải chọn người nhận.")]
        public int NguoiNhan { get; set; }

        [StringLength(50)]
        public string? SoChungTu { get; set; }

        [StringLength(500)]
        public string? GhiChu { get; set; }

        public List<DongDoiChieuVM> ChiTiets { get; set; } = new();
    }

    public static class DoiChieuExtensions
    {
        public static Task<List<DongDoiChieuVM>> LayDoiChieuAsync(
            this QLMuaHangVaNCC_UNETI06_TI17A1HNContext db, int maDon)
        {
            return db.ChiTietDonMua
                .Where(c => c.MaDonMua == maDon)
                .OrderBy(c => c.MaChiTietDon)
                .Select(c => new DongDoiChieuVM
                {
                    MaChiTietDon = c.MaChiTietDon,
                    MaHang = c.MaHang,
                    TenHang = c.HangHoa.TenHang,
                    SoLuongDat = c.SoLuongDat,
                    TongDaNhan = db.ChiTietGiaoHang
                        .Where(g => g.MaChiTietDon == c.MaChiTietDon && g.LanGiaoHang!.TrangThai)
                        .Sum(g => (int?)g.SoLuongNhan) ?? 0
                })
                .ToListAsync();
        }
    }
}