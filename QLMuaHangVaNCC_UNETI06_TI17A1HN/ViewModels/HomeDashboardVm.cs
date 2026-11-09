using System.Collections.Generic;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.ViewModels;

public class HomeDashboardVm
{
    public record BangThongKe(string Ten, string Controller, string Icon, string Nhom, int SoLuong);
    public List<BangThongKe> Bangs { get; set; } = new();
    public int ChoDuyet { get; set; }
    public int DaDuyet { get; set; }
    public int TongDonMua { get; set; }
    public decimal TongGiaTriDonMua { get; set; }
    public decimal DaThanhToan { get; set; }
    public int NhaCungCapDangHoatDong { get; set; }
}
