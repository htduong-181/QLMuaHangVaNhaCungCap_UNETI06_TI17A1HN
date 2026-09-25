using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Data
{
    public class DbConnection : DbContext
    {
        public DbConnection(DbContextOptions<DbConnection> options)
            : base(options)
        {

        }


        public DbSet<TaiKhoan> TaiKhoans { get; set; }

        public DbSet<BoPhanDeNghi> BoPhanDeNghis { get; set; }

        public DbSet<LoaiHang> LoaiHangs { get; set; }

        public DbSet<DonViTinh> DonViTinhs { get; set; }

        public DbSet<HangHoa> HangHoas { get; set; }

        public DbSet<NhaCungCap> NhaCungCaps { get; set; }

        public DbSet<NhaCungCapHangHoa> NhaCungCapHangHoas { get; set; }

        public DbSet<YeuCauMuaHang> YeuCauMuaHangs { get; set; }

        public DbSet<ChiTietYeuCau> ChiTietYeuCaus { get; set; }

        public DbSet<DonMuaHang> DonMuaHangs { get; set; }

        public DbSet<ChiTietDonMua> ChiTietDonMuas { get; set; }

        public DbSet<LanGiaoHang> LanGiaoHangs { get; set; }

        public DbSet<ChiTietGiaoHang> ChiTietGiaoHangs { get; set; }

        public DbSet<ThanhToanDonMua> ThanhToanDonMuas { get; set; }

        public DbSet<LichSuTrangThai> LichSuTrangThais { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            // ===============================
            // MAP TEN BANG SQL SERVER
            // ===============================

            modelBuilder.Entity<TaiKhoan>()
                .ToTable("TaiKhoan");

            modelBuilder.Entity<BoPhanDeNghi>()
                .ToTable("BoPhanDeNghi");

            modelBuilder.Entity<LoaiHang>()
                .ToTable("LoaiHang");

            modelBuilder.Entity<DonViTinh>()
                .ToTable("DonViTinh");

            modelBuilder.Entity<HangHoa>()
                .ToTable("HangHoa");

            modelBuilder.Entity<NhaCungCap>()
                .ToTable("NhaCungCap");

            modelBuilder.Entity<NhaCungCapHangHoa>()
                .ToTable("NhaCungCapHangHoa");

            modelBuilder.Entity<YeuCauMuaHang>()
                .ToTable("YeuCauMuaHang");

            modelBuilder.Entity<ChiTietYeuCau>()
                .ToTable("ChiTietYeuCau");

            modelBuilder.Entity<DonMuaHang>()
                .ToTable("DonMuaHang");

            modelBuilder.Entity<ChiTietDonMua>()
                .ToTable("ChiTietDonMua");

            modelBuilder.Entity<LanGiaoHang>()
                .ToTable("LanGiaoHang");

            modelBuilder.Entity<ChiTietGiaoHang>()
                .ToTable("ChiTietGiaoHang");

            modelBuilder.Entity<ThanhToanDonMua>()
                .ToTable("ThanhToanDonMua");

            modelBuilder.Entity<LichSuTrangThai>()
                .ToTable("LichSuTrangThai");



            // ===============================
            // TAI KHOAN
            // ===============================

            modelBuilder.Entity<YeuCauMuaHang>()
                .HasOne(x => x.NguoiLapNavigation)
                .WithMany(x => x.YeuCauMuaHangs)
                .HasForeignKey(x => x.NguoiLap)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<YeuCauMuaHang>()
                .HasOne(x => x.NguoiDuyetNavigation)
                .WithMany()
                .HasForeignKey(x => x.NguoiDuyet)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<DonMuaHang>()
                .HasOne(x => x.NguoiLapNavigation)
                .WithMany(x => x.DonMuaHangs)
                .HasForeignKey(x => x.NguoiLap)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<LanGiaoHang>()
                .HasOne(x => x.NguoiNhanNavigation)
                .WithMany(x => x.LanGiaoHangs)
                .HasForeignKey(x => x.NguoiNhan)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<ThanhToanDonMua>()
                .HasOne(x => x.NguoiThucHienNavigation)
                .WithMany(x => x.ThanhToanDonMuas)
                .HasForeignKey(x => x.NguoiThucHien)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<LichSuTrangThai>()
                .HasOne(x => x.NguoiThucHienNavigation)
                .WithMany(x => x.LichSuTrangThais)
                .HasForeignKey(x => x.NguoiThucHien)
                .OnDelete(DeleteBehavior.Restrict);





            // ===============================
            // HANG HOA
            // ===============================

            modelBuilder.Entity<HangHoa>()
                .HasOne(x => x.LoaiHang)
                .WithMany(x => x.HangHoas)
                .HasForeignKey(x => x.MaLoaiHang)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<HangHoa>()
                .HasOne(x => x.DonViTinh)
                .WithMany(x => x.HangHoas)
                .HasForeignKey(x => x.MaDonViTinh)
                .OnDelete(DeleteBehavior.Restrict);





            // ===============================
            // YEU CAU MUA HANG
            // ===============================

            modelBuilder.Entity<YeuCauMuaHang>()
                .HasOne(x => x.BoPhanDeNghi)
                .WithMany(x => x.YeuCauMuaHangs)
                .HasForeignKey(x => x.MaBoPhan)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<ChiTietYeuCau>()
                .HasOne(x => x.YeuCauMuaHang)
                .WithMany(x => x.ChiTietYeuCaus)
                .HasForeignKey(x => x.MaYeuCau)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<ChiTietYeuCau>()
                .HasOne(x => x.HangHoa)
                .WithMany(x => x.ChiTietYeuCaus)
                .HasForeignKey(x => x.MaHang)
                .OnDelete(DeleteBehavior.Restrict);





            // ===============================
            // DON MUA HANG
            // ===============================

            modelBuilder.Entity<DonMuaHang>()
                .HasOne(x => x.YeuCauMuaHang)
                .WithMany(x => x.DonMuaHangs)
                .HasForeignKey(x => x.MaYeuCau)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<DonMuaHang>()
                .HasOne(x => x.NhaCungCap)
                .WithMany(x => x.DonMuaHangs)
                .HasForeignKey(x => x.MaNhaCungCap)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<ChiTietDonMua>()
                .HasOne(x => x.DonMuaHang)
                .WithMany(x => x.ChiTietDonMuas)
                .HasForeignKey(x => x.MaDonMua)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<ChiTietDonMua>()
                .HasOne(x => x.ChiTietYeuCau)
                .WithMany(x => x.ChiTietDonMuas)
                .HasForeignKey(x => x.MaChiTietYeuCau)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<ChiTietDonMua>()
                .HasOne(x => x.HangHoa)
                .WithMany(x => x.ChiTietDonMuas)
                .HasForeignKey(x => x.MaHang)
                .OnDelete(DeleteBehavior.Restrict);





            // ===============================
            // GIAO HANG
            // ===============================

            modelBuilder.Entity<LanGiaoHang>()
                .HasOne(x => x.DonMuaHang)
                .WithMany(x => x.LanGiaoHangs)
                .HasForeignKey(x => x.MaDonMua)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<ChiTietGiaoHang>()
                .HasOne(x => x.LanGiaoHang)
                .WithMany(x => x.ChiTietGiaoHangs)
                .HasForeignKey(x => x.MaLanGiao)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<ChiTietGiaoHang>()
                .HasOne(x => x.ChiTietDonMua)
                .WithMany(x => x.ChiTietGiaoHangs)
                .HasForeignKey(x => x.MaChiTietDon)
                .OnDelete(DeleteBehavior.Restrict);





            // ===============================
            // THANH TOAN
            // ===============================

            modelBuilder.Entity<ThanhToanDonMua>()
                .HasOne(x => x.DonMuaHang)
                .WithMany(x => x.ThanhToanDonMuas)
                .HasForeignKey(x => x.MaDonMua)
                .OnDelete(DeleteBehavior.Cascade);





            // ===============================
            // NCC - HANG HOA
            // ===============================

            modelBuilder.Entity<NhaCungCapHangHoa>()
                .HasIndex(x => new
                {
                    x.MaNhaCungCap,
                    x.MaHang
                })
                .IsUnique();


            modelBuilder.Entity<NhaCungCapHangHoa>()
                .HasOne(x => x.NhaCungCap)
                .WithMany(x => x.NhaCungCapHangHoas)
                .HasForeignKey(x => x.MaNhaCungCap)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<NhaCungCapHangHoa>()
                .HasOne(x => x.HangHoa)
                .WithMany(x => x.NhaCungCapHangHoas)
                .HasForeignKey(x => x.MaHang)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}