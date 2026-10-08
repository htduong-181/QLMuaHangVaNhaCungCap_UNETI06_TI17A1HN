using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoPhanDeNghi",
                columns: table => new
                {
                    MaBoPhan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenBoPhan = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NguoiPhuTrach = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoPhanDeNghi", x => x.MaBoPhan);
                });

            migrationBuilder.CreateTable(
                name: "DonViTinh",
                columns: table => new
                {
                    MaDonViTinh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDonViTinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonViTinh", x => x.MaDonViTinh);
                });

            migrationBuilder.CreateTable(
                name: "LoaiHang",
                columns: table => new
                {
                    MaLoaiHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiHang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiHang", x => x.MaLoaiHang);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCap",
                columns: table => new
                {
                    MaNhaCungCap = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhaCungCap = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MaSoThue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    NguoiLienHe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCap", x => x.MaNhaCungCap);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.MaTaiKhoan);
                });

            migrationBuilder.CreateTable(
                name: "HangHoa",
                columns: table => new
                {
                    MaHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHang = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MaLoaiHang = table.Column<int>(type: "int", nullable: false),
                    MaDonViTinh = table.Column<int>(type: "int", nullable: false),
                    GiaThamKhao = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangHoa", x => x.MaHang);
                    table.ForeignKey(
                        name: "FK_HangHoa_DonViTinh_MaDonViTinh",
                        column: x => x.MaDonViTinh,
                        principalTable: "DonViTinh",
                        principalColumn: "MaDonViTinh");
                    table.ForeignKey(
                        name: "FK_HangHoa_LoaiHang_MaLoaiHang",
                        column: x => x.MaLoaiHang,
                        principalTable: "LoaiHang",
                        principalColumn: "MaLoaiHang");
                });

            migrationBuilder.CreateTable(
                name: "LichSuTrangThai",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiDoiTuong = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MaDoiTuong = table.Column<int>(type: "int", nullable: false),
                    TrangThaiCu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TrangThaiMoi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NguoiThucHien = table.Column<int>(type: "int", nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuTrangThai", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LichSuTrangThai_TaiKhoan_NguoiThucHien",
                        column: x => x.NguoiThucHien,
                        principalTable: "TaiKhoan",
                        principalColumn: "MaTaiKhoan");
                });

            migrationBuilder.CreateTable(
                name: "YeuCauMuaHang",
                columns: table => new
                {
                    MaYeuCau = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaBoPhan = table.Column<int>(type: "int", nullable: false),
                    NgayYeuCau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCanHang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MucDoUuTien = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LyDoMua = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    NguoiLap = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NgayDuyet = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiDuyet = table.Column<int>(type: "int", nullable: true),
                    LyDoTuChoiHuy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YeuCauMuaHang", x => x.MaYeuCau);
                    table.ForeignKey(
                        name: "FK_YeuCauMuaHang_BoPhanDeNghi_MaBoPhan",
                        column: x => x.MaBoPhan,
                        principalTable: "BoPhanDeNghi",
                        principalColumn: "MaBoPhan");
                    table.ForeignKey(
                        name: "FK_YeuCauMuaHang_TaiKhoan_NguoiDuyet",
                        column: x => x.NguoiDuyet,
                        principalTable: "TaiKhoan",
                        principalColumn: "MaTaiKhoan");
                    table.ForeignKey(
                        name: "FK_YeuCauMuaHang_TaiKhoan_NguoiLap",
                        column: x => x.NguoiLap,
                        principalTable: "TaiKhoan",
                        principalColumn: "MaTaiKhoan");
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCapHangHoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhaCungCap = table.Column<int>(type: "int", nullable: false),
                    MaHang = table.Column<int>(type: "int", nullable: false),
                    DonGiaBao = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayCapNhatGia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThoiGianGiaoDuKien = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCapHangHoa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhaCungCapHangHoa_HangHoa_MaHang",
                        column: x => x.MaHang,
                        principalTable: "HangHoa",
                        principalColumn: "MaHang");
                    table.ForeignKey(
                        name: "FK_NhaCungCapHangHoa_NhaCungCap_MaNhaCungCap",
                        column: x => x.MaNhaCungCap,
                        principalTable: "NhaCungCap",
                        principalColumn: "MaNhaCungCap");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietYeuCau",
                columns: table => new
                {
                    MaChiTietYeuCau = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaYeuCau = table.Column<int>(type: "int", nullable: false),
                    MaHang = table.Column<int>(type: "int", nullable: false),
                    SoLuongYeuCau = table.Column<int>(type: "int", nullable: false),
                    SoLuongDuyet = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietYeuCau", x => x.MaChiTietYeuCau);
                    table.ForeignKey(
                        name: "FK_ChiTietYeuCau_HangHoa_MaHang",
                        column: x => x.MaHang,
                        principalTable: "HangHoa",
                        principalColumn: "MaHang");
                    table.ForeignKey(
                        name: "FK_ChiTietYeuCau_YeuCauMuaHang_MaYeuCau",
                        column: x => x.MaYeuCau,
                        principalTable: "YeuCauMuaHang",
                        principalColumn: "MaYeuCau");
                });

            migrationBuilder.CreateTable(
                name: "DonMuaHang",
                columns: table => new
                {
                    MaDonMua = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaYeuCau = table.Column<int>(type: "int", nullable: false),
                    MaNhaCungCap = table.Column<int>(type: "int", nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NguoiLap = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonMuaHang", x => x.MaDonMua);
                    table.ForeignKey(
                        name: "FK_DonMuaHang_NhaCungCap_MaNhaCungCap",
                        column: x => x.MaNhaCungCap,
                        principalTable: "NhaCungCap",
                        principalColumn: "MaNhaCungCap");
                    table.ForeignKey(
                        name: "FK_DonMuaHang_TaiKhoan_NguoiLap",
                        column: x => x.NguoiLap,
                        principalTable: "TaiKhoan",
                        principalColumn: "MaTaiKhoan");
                    table.ForeignKey(
                        name: "FK_DonMuaHang_YeuCauMuaHang_MaYeuCau",
                        column: x => x.MaYeuCau,
                        principalTable: "YeuCauMuaHang",
                        principalColumn: "MaYeuCau");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonMua",
                columns: table => new
                {
                    MaChiTietDon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDonMua = table.Column<int>(type: "int", nullable: false),
                    MaChiTietYeuCau = table.Column<int>(type: "int", nullable: false),
                    MaHang = table.Column<int>(type: "int", nullable: false),
                    SoLuongDat = table.Column<int>(type: "int", nullable: false),
                    DonGiaMua = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonMua", x => x.MaChiTietDon);
                    table.ForeignKey(
                        name: "FK_ChiTietDonMua_ChiTietYeuCau_MaChiTietYeuCau",
                        column: x => x.MaChiTietYeuCau,
                        principalTable: "ChiTietYeuCau",
                        principalColumn: "MaChiTietYeuCau");
                    table.ForeignKey(
                        name: "FK_ChiTietDonMua_DonMuaHang_MaDonMua",
                        column: x => x.MaDonMua,
                        principalTable: "DonMuaHang",
                        principalColumn: "MaDonMua");
                    table.ForeignKey(
                        name: "FK_ChiTietDonMua_HangHoa_MaHang",
                        column: x => x.MaHang,
                        principalTable: "HangHoa",
                        principalColumn: "MaHang");
                });

            migrationBuilder.CreateTable(
                name: "LanGiaoHang",
                columns: table => new
                {
                    MaLanGiao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDonMua = table.Column<int>(type: "int", nullable: false),
                    NgayGiao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiNhan = table.Column<int>(type: "int", nullable: false),
                    SoChungTu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanGiaoHang", x => x.MaLanGiao);
                    table.ForeignKey(
                        name: "FK_LanGiaoHang_DonMuaHang_MaDonMua",
                        column: x => x.MaDonMua,
                        principalTable: "DonMuaHang",
                        principalColumn: "MaDonMua");
                    table.ForeignKey(
                        name: "FK_LanGiaoHang_TaiKhoan_NguoiNhan",
                        column: x => x.NguoiNhan,
                        principalTable: "TaiKhoan",
                        principalColumn: "MaTaiKhoan");
                });

            migrationBuilder.CreateTable(
                name: "ThanhToanDonMua",
                columns: table => new
                {
                    MaThanhToan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDonMua = table.Column<int>(type: "int", nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoTienThanhToan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhuongThuc = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NguoiThucHien = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhToanDonMua", x => x.MaThanhToan);
                    table.ForeignKey(
                        name: "FK_ThanhToanDonMua_DonMuaHang_MaDonMua",
                        column: x => x.MaDonMua,
                        principalTable: "DonMuaHang",
                        principalColumn: "MaDonMua");
                    table.ForeignKey(
                        name: "FK_ThanhToanDonMua_TaiKhoan_NguoiThucHien",
                        column: x => x.NguoiThucHien,
                        principalTable: "TaiKhoan",
                        principalColumn: "MaTaiKhoan");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietGiaoHang",
                columns: table => new
                {
                    MaChiTietGiao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaLanGiao = table.Column<int>(type: "int", nullable: false),
                    MaChiTietDon = table.Column<int>(type: "int", nullable: false),
                    SoLuongNhan = table.Column<int>(type: "int", nullable: false),
                    SoLuongDatChatLuong = table.Column<int>(type: "int", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietGiaoHang", x => x.MaChiTietGiao);
                    table.ForeignKey(
                        name: "FK_ChiTietGiaoHang_ChiTietDonMua_MaChiTietDon",
                        column: x => x.MaChiTietDon,
                        principalTable: "ChiTietDonMua",
                        principalColumn: "MaChiTietDon");
                    table.ForeignKey(
                        name: "FK_ChiTietGiaoHang_LanGiaoHang_MaLanGiao",
                        column: x => x.MaLanGiao,
                        principalTable: "LanGiaoHang",
                        principalColumn: "MaLanGiao");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonMua_MaChiTietYeuCau",
                table: "ChiTietDonMua",
                column: "MaChiTietYeuCau");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonMua_MaDonMua",
                table: "ChiTietDonMua",
                column: "MaDonMua");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonMua_MaHang",
                table: "ChiTietDonMua",
                column: "MaHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGiaoHang_MaChiTietDon",
                table: "ChiTietGiaoHang",
                column: "MaChiTietDon");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGiaoHang_MaLanGiao",
                table: "ChiTietGiaoHang",
                column: "MaLanGiao");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietYeuCau_MaHang",
                table: "ChiTietYeuCau",
                column: "MaHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietYeuCau_MaYeuCau",
                table: "ChiTietYeuCau",
                column: "MaYeuCau");

            migrationBuilder.CreateIndex(
                name: "IX_DonMuaHang_MaNhaCungCap",
                table: "DonMuaHang",
                column: "MaNhaCungCap");

            migrationBuilder.CreateIndex(
                name: "IX_DonMuaHang_MaYeuCau",
                table: "DonMuaHang",
                column: "MaYeuCau");

            migrationBuilder.CreateIndex(
                name: "IX_DonMuaHang_NguoiLap",
                table: "DonMuaHang",
                column: "NguoiLap");

            migrationBuilder.CreateIndex(
                name: "IX_DonViTinh_TenDonViTinh",
                table: "DonViTinh",
                column: "TenDonViTinh",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HangHoa_MaDonViTinh",
                table: "HangHoa",
                column: "MaDonViTinh");

            migrationBuilder.CreateIndex(
                name: "IX_HangHoa_MaLoaiHang",
                table: "HangHoa",
                column: "MaLoaiHang");

            migrationBuilder.CreateIndex(
                name: "IX_LanGiaoHang_MaDonMua",
                table: "LanGiaoHang",
                column: "MaDonMua");

            migrationBuilder.CreateIndex(
                name: "IX_LanGiaoHang_NguoiNhan",
                table: "LanGiaoHang",
                column: "NguoiNhan");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuTrangThai_NguoiThucHien",
                table: "LichSuTrangThai",
                column: "NguoiThucHien");

            migrationBuilder.CreateIndex(
                name: "IX_LoaiHang_TenLoaiHang",
                table: "LoaiHang",
                column: "TenLoaiHang",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhaCungCap_MaSoThue",
                table: "NhaCungCap",
                column: "MaSoThue",
                unique: true,
                filter: "[MaSoThue] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NhaCungCapHangHoa_MaHang",
                table: "NhaCungCapHangHoa",
                column: "MaHang");

            migrationBuilder.CreateIndex(
                name: "IX_NhaCungCapHangHoa_MaNhaCungCap_MaHang",
                table: "NhaCungCapHangHoa",
                columns: new[] { "MaNhaCungCap", "MaHang" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_Email",
                table: "TaiKhoan",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_TenDangNhap",
                table: "TaiKhoan",
                column: "TenDangNhap",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToanDonMua_MaDonMua",
                table: "ThanhToanDonMua",
                column: "MaDonMua");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToanDonMua_NguoiThucHien",
                table: "ThanhToanDonMua",
                column: "NguoiThucHien");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauMuaHang_MaBoPhan",
                table: "YeuCauMuaHang",
                column: "MaBoPhan");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauMuaHang_NguoiDuyet",
                table: "YeuCauMuaHang",
                column: "NguoiDuyet");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauMuaHang_NguoiLap",
                table: "YeuCauMuaHang",
                column: "NguoiLap");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietGiaoHang");

            migrationBuilder.DropTable(
                name: "LichSuTrangThai");

            migrationBuilder.DropTable(
                name: "NhaCungCapHangHoa");

            migrationBuilder.DropTable(
                name: "ThanhToanDonMua");

            migrationBuilder.DropTable(
                name: "ChiTietDonMua");

            migrationBuilder.DropTable(
                name: "LanGiaoHang");

            migrationBuilder.DropTable(
                name: "ChiTietYeuCau");

            migrationBuilder.DropTable(
                name: "DonMuaHang");

            migrationBuilder.DropTable(
                name: "HangHoa");

            migrationBuilder.DropTable(
                name: "NhaCungCap");

            migrationBuilder.DropTable(
                name: "YeuCauMuaHang");

            migrationBuilder.DropTable(
                name: "DonViTinh");

            migrationBuilder.DropTable(
                name: "LoaiHang");

            migrationBuilder.DropTable(
                name: "BoPhanDeNghi");

            migrationBuilder.DropTable(
                name: "TaiKhoan");
        }
    }
}
