using Microsoft.EntityFrameworkCore;

public class QLMuaHangVaNCC_UNETI06_TI17A1HNContext(DbContextOptions<QLMuaHangVaNCC_UNETI06_TI17A1HNContext> options) : DbContext(options)
{
    public DbSet<QLMuaHangVaNCC_UNETI06_TI17A1HN.Models.BoPhanDeNghi> BoPhanDeNghi { get; set; } = default!;
    public DbSet<QLMuaHangVaNCC_UNETI06_TI17A1HN.Models.ChiTietDonMua> ChiTietDonMua { get; set; } = default!;
    public DbSet<QLMuaHangVaNCC_UNETI06_TI17A1HN.Models.ChiTietGiaoHang> ChiTietGiaoHang { get; set; } = default!;
    public DbSet<QLMuaHangVaNCC_UNETI06_TI17A1HN.Models.DonMuaHang> DonMuaHang { get; set; } = default!;
    public DbSet<QLMuaHangVaNCC_UNETI06_TI17A1HN.Models.DonViTinh> DonViTinh { get; set; } = default!;
    public DbSet<QLMuaHangVaNCC_UNETI06_TI17A1HN.Models.HangHoa> HangHoa { get; set; } = default!;
    public DbSet<QLMuaHangVaNCC_UNETI06_TI17A1HN.Models.ChiTietYeuCau> ChiTietYeuCau { get; set; } = default!;
    public DbSet<QLMuaHangVaNCC_UNETI06_TI17A1HN.Models.LanGiaoHang> LanGiaoHang { get; set; } = default!;
    public DbSet<QLMuaHangVaNCC_UNETI06_TI17A1HN.Models.LichSuTrangThai> LichSuTrangThai { get; set; } = default!;
    public DbSet<QLMuaHangVaNCC_UNETI06_TI17A1HN.Models.LoaiHang> LoaiHang { get; set; } = default!;
    public DbSet<QLMuaHangVaNCC_UNETI06_TI17A1HN.Models.NhaCungCap> NhaCungCap { get; set; } = default!;
    public DbSet<QLMuaHangVaNCC_UNETI06_TI17A1HN.Models.NhaCungCapHangHoa> NhaCungCapHangHoa { get; set; } = default!;
    public DbSet<QLMuaHangVaNCC_UNETI06_TI17A1HN.Models.TaiKhoan> TaiKhoan { get; set; } = default!;
    public DbSet<QLMuaHangVaNCC_UNETI06_TI17A1HN.Models.ThanhToanDonMua> ThanhToanDonMua { get; set; } = default!;
    public DbSet<QLMuaHangVaNCC_UNETI06_TI17A1HN.Models.YeuCauMuaHang> YeuCauMuaHang { get; set; } = default!;
}
