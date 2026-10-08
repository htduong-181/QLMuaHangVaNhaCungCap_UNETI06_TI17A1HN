using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

public class QLMuaHangVaNCC_UNETI06_TI17A1HNContext
    : DbContext
{
    public QLMuaHangVaNCC_UNETI06_TI17A1HNContext(
        DbContextOptions<QLMuaHangVaNCC_UNETI06_TI17A1HNContext> options)
        : base(options)
    {
    }


    // ==================== DBSET ====================

    public DbSet<BoPhanDeNghi> BoPhanDeNghi { get; set; } = default!;
    public DbSet<ChiTietDonMua> ChiTietDonMua { get; set; } = default!;
    public DbSet<ChiTietGiaoHang> ChiTietGiaoHang { get; set; } = default!;
    public DbSet<ChiTietYeuCau> ChiTietYeuCau { get; set; } = default!;
    public DbSet<DonMuaHang> DonMuaHang { get; set; } = default!;
    public DbSet<DonViTinh> DonViTinh { get; set; } = default!;
    public DbSet<HangHoa> HangHoa { get; set; } = default!;
    public DbSet<LanGiaoHang> LanGiaoHang { get; set; } = default!;
    public DbSet<LichSuTrangThai> LichSuTrangThai { get; set; } = default!;
    public DbSet<LoaiHang> LoaiHang { get; set; } = default!;
    public DbSet<NhaCungCap> NhaCungCap { get; set; } = default!;
    public DbSet<NhaCungCapHangHoa> NhaCungCapHangHoa { get; set; } = default!;
    public DbSet<TaiKhoan> TaiKhoan { get; set; } = default!;
    public DbSet<ThanhToanDonMua> ThanhToanDonMua { get; set; } = default!;
    public DbSet<YeuCauMuaHang> YeuCauMuaHang { get; set; } = default!;


    // ==================== RELATIONSHIPS ====================

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // ==================================================
        // 1. LoaiHang 1 - N HangHoa
        // ==================================================

        modelBuilder.Entity<HangHoa>()
            .HasOne(h => h.LoaiHang)
            .WithMany(l => l.HangHoas)
            .HasForeignKey(h => h.MaLoaiHang)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 2. DonViTinh 1 - N HangHoa
        // ==================================================

        modelBuilder.Entity<HangHoa>()
            .HasOne(h => h.DonViTinh)
            .WithMany(d => d.HangHoas)
            .HasForeignKey(h => h.MaDonViTinh)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 3. BoPhanDeNghi 1 - N YeuCauMuaHang
        // ==================================================

        modelBuilder.Entity<YeuCauMuaHang>()
            .HasOne(y => y.BoPhanDeNghi)
            .WithMany(b => b.YeuCauMuaHangs)
            .HasForeignKey(y => y.MaBoPhan)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 4. TaiKhoan 1 - N YeuCauMuaHang (NguoiLap)
        // ==================================================

        modelBuilder.Entity<YeuCauMuaHang>()
            .HasOne(y => y.NguoiLapNavigation)
            .WithMany(t => t.YeuCauMuaHangsNguoiLap)
            .HasForeignKey(y => y.NguoiLap)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 5. TaiKhoan 1 - N YeuCauMuaHang (NguoiDuyet)
        // ==================================================

        modelBuilder.Entity<YeuCauMuaHang>()
            .HasOne(y => y.NguoiDuyetNavigation)
            .WithMany(t => t.YeuCauMuaHangsNguoiDuyet)
            .HasForeignKey(y => y.NguoiDuyet)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 6. YeuCauMuaHang 1 - N ChiTietYeuCau
        // ==================================================

        modelBuilder.Entity<ChiTietYeuCau>()
            .HasOne(c => c.YeuCauMuaHang)
            .WithMany(y => y.ChiTietYeuCaus)
            .HasForeignKey(c => c.MaYeuCau)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 7. HangHoa 1 - N ChiTietYeuCau
        // ==================================================

        modelBuilder.Entity<ChiTietYeuCau>()
            .HasOne(c => c.HangHoa)
            .WithMany(h => h.ChiTietYeuCaus)
            .HasForeignKey(c => c.MaHang)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 8. YeuCauMuaHang 1 - N DonMuaHang
        // ==================================================

        modelBuilder.Entity<DonMuaHang>()
            .HasOne(d => d.YeuCauMuaHang)
            .WithMany(y => y.DonMuaHangs)
            .HasForeignKey(d => d.MaYeuCau)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 9. NhaCungCap 1 - N DonMuaHang
        // ==================================================

        modelBuilder.Entity<DonMuaHang>()
            .HasOne(d => d.NhaCungCap)
            .WithMany(n => n.DonMuaHangs)
            .HasForeignKey(d => d.MaNhaCungCap)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 10. TaiKhoan 1 - N DonMuaHang
        // ==================================================

        modelBuilder.Entity<DonMuaHang>()
            .HasOne(d => d.NguoiLapNavigation)
            .WithMany(t => t.DonMuaHangs)
            .HasForeignKey(d => d.NguoiLap)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 11. DonMuaHang 1 - N ChiTietDonMua
        // ==================================================

        modelBuilder.Entity<ChiTietDonMua>()
            .HasOne(c => c.DonMuaHang)
            .WithMany(d => d.ChiTietDonMuas)
            .HasForeignKey(c => c.MaDonMua)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 12. ChiTietYeuCau 1 - N ChiTietDonMua
        // ==================================================

        modelBuilder.Entity<ChiTietDonMua>()
            .HasOne(c => c.ChiTietYeuCau)
            .WithMany(y => y.ChiTietDonMuas)
            .HasForeignKey(c => c.MaChiTietYeuCau)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 13. HangHoa 1 - N ChiTietDonMua
        // ==================================================

        modelBuilder.Entity<ChiTietDonMua>()
            .HasOne(c => c.HangHoa)
            .WithMany(h => h.ChiTietDonMuas)
            .HasForeignKey(c => c.MaHang)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 14. DonMuaHang 1 - N LanGiaoHang
        // ==================================================

        modelBuilder.Entity<LanGiaoHang>()
            .HasOne(l => l.DonMuaHang)
            .WithMany(d => d.LanGiaoHangs)
            .HasForeignKey(l => l.MaDonMua)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 15. TaiKhoan 1 - N LanGiaoHang
        // ==================================================

        modelBuilder.Entity<LanGiaoHang>()
            .HasOne(l => l.NguoiNhanNavigation)
            .WithMany(t => t.LanGiaoHangs)
            .HasForeignKey(l => l.NguoiNhan)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 16. LanGiaoHang 1 - N ChiTietGiaoHang
        // ==================================================

        modelBuilder.Entity<ChiTietGiaoHang>()
            .HasOne(c => c.LanGiaoHang)
            .WithMany(l => l.ChiTietGiaoHangs)
            .HasForeignKey(c => c.MaLanGiao)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 17. ChiTietDonMua 1 - N ChiTietGiaoHang
        // ==================================================

        modelBuilder.Entity<ChiTietGiaoHang>()
            .HasOne(c => c.ChiTietDonMua)
            .WithMany(d => d.ChiTietGiaoHangs)
            .HasForeignKey(c => c.MaChiTietDon)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 18. DonMuaHang 1 - N ThanhToanDonMua
        // ==================================================

        modelBuilder.Entity<ThanhToanDonMua>()
            .HasOne(t => t.DonMuaHang)
            .WithMany(d => d.ThanhToanDonMuas)
            .HasForeignKey(t => t.MaDonMua)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 19. TaiKhoan 1 - N ThanhToanDonMua
        // ==================================================

        modelBuilder.Entity<ThanhToanDonMua>()
            .HasOne(t => t.NguoiThucHienNavigation)
            .WithMany(tk => tk.ThanhToanDonMuas)
            .HasForeignKey(t => t.NguoiThucHien)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 20. NhaCungCap 1 - N NhaCungCapHangHoa
        // ==================================================

        modelBuilder.Entity<NhaCungCapHangHoa>()
            .HasOne(n => n.NhaCungCap)
            .WithMany(n => n.NhaCungCapHangHoas)
            .HasForeignKey(n => n.MaNhaCungCap)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 21. HangHoa 1 - N NhaCungCapHangHoa
        // ==================================================

        modelBuilder.Entity<NhaCungCapHangHoa>()
            .HasOne(n => n.HangHoa)
            .WithMany(h => h.NhaCungCapHangHoas)
            .HasForeignKey(n => n.MaHang)
            .OnDelete(DeleteBehavior.NoAction);


        // ==================================================
        // 22. TaiKhoan 1 - N LichSuTrangThai
        // ==================================================

        modelBuilder.Entity<LichSuTrangThai>()
            .HasOne(l => l.NguoiThucHienNavigation)
            .WithMany(t => t.LichSuTrangThais)
            .HasForeignKey(l => l.NguoiThucHien)
            .OnDelete(DeleteBehavior.NoAction);

        // Mỗi mã số thuế chỉ thuộc một nhà cung cấp
        modelBuilder.Entity<NhaCungCap>()
            .HasIndex(n => n.MaSoThue)
            .IsUnique()
            .HasFilter("[MaSoThue] IS NOT NULL");

        // Không trùng cặp nhà cung cấp - hàng hóa
        modelBuilder.Entity<NhaCungCapHangHoa>()
            .HasIndex(n => new { n.MaNhaCungCap, n.MaHang })
            .IsUnique();
    }
}