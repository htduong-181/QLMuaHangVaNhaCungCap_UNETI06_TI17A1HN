CREATE DATABASE QLMuaHangVaNCC;
GO

USE QLMuaHangVaNCC;
GO


-- ==============================
-- TAI KHOAN
-- ==============================

CREATE TABLE TaiKhoan
(
    MaTaiKhoan INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap VARCHAR(50) NOT NULL UNIQUE,
    MatKhau VARCHAR(255) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE,
    VaiTro NVARCHAR(30) NOT NULL DEFAULT N'NhanVienDeNghi',
    TrangThai BIT NOT NULL DEFAULT 1
);



-- ==============================
-- BO PHAN DE NGHI
-- ==============================

CREATE TABLE BoPhanDeNghi
(
    MaBoPhan INT IDENTITY(1,1) PRIMARY KEY,
    TenBoPhan NVARCHAR(150) NOT NULL,
    NguoiPhuTrach NVARCHAR(100),
    SoDienThoai VARCHAR(20),
    MoTa NVARCHAR(500),
    TrangThai BIT NOT NULL DEFAULT 1
);



-- ==============================
-- LOAI HANG
-- ==============================

CREATE TABLE LoaiHang
(
    MaLoaiHang INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiHang NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(500),
    TrangThai BIT NOT NULL DEFAULT 1
);



-- ==============================
-- DON VI TINH
-- ==============================

CREATE TABLE DonViTinh
(
    MaDonViTinh INT IDENTITY(1,1) PRIMARY KEY,
    TenDonViTinh NVARCHAR(50) NOT NULL,
    MoTa NVARCHAR(500),
    TrangThai BIT NOT NULL DEFAULT 1
);



-- ==============================
-- HANG HOA
-- ==============================

CREATE TABLE HangHoa
(
    MaHang INT IDENTITY(1,1) PRIMARY KEY,
    TenHang NVARCHAR(200) NOT NULL,

    MaLoaiHang INT NOT NULL,
    MaDonViTinh INT NOT NULL,

    GiaThamKhao DECIMAL(18,2) NOT NULL DEFAULT 0,

    MoTa NVARCHAR(1000),

    TrangThai BIT NOT NULL DEFAULT 1,


    CONSTRAINT FK_HangHoa_LoaiHang
    FOREIGN KEY(MaLoaiHang)
    REFERENCES LoaiHang(MaLoaiHang),


    CONSTRAINT FK_HangHoa_DonViTinh
    FOREIGN KEY(MaDonViTinh)
    REFERENCES DonViTinh(MaDonViTinh)
);



-- ==============================
-- NHA CUNG CAP
-- ==============================

CREATE TABLE NhaCungCap
(
    MaNhaCungCap INT IDENTITY(1,1) PRIMARY KEY,

    TenNhaCungCap NVARCHAR(200) NOT NULL,

    MaSoThue VARCHAR(20),

    SoDienThoai VARCHAR(20) NOT NULL,

    Email VARCHAR(150),

    DiaChi NVARCHAR(300),

    NguoiLienHe NVARCHAR(100),

    TrangThai BIT NOT NULL DEFAULT 1
);



-- ==============================
-- NCC HANG HOA
-- ==============================

CREATE TABLE NhaCungCapHangHoa
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    MaNhaCungCap INT NOT NULL,

    MaHang INT NOT NULL,

    DonGiaBao DECIMAL(18,2) NOT NULL,

    NgayCapNhatGia DATE,

    ThoiGianGiaoDuKien INT,

    TrangThai BIT NOT NULL DEFAULT 1,


    CONSTRAINT FK_NCCHH_NhaCungCap
    FOREIGN KEY(MaNhaCungCap)
    REFERENCES NhaCungCap(MaNhaCungCap),


    CONSTRAINT FK_NCCHH_HangHoa
    FOREIGN KEY(MaHang)
    REFERENCES HangHoa(MaHang),


    CONSTRAINT UQ_NhaCungCap_HangHoa
    UNIQUE(MaNhaCungCap,MaHang)
);



-- ==============================
-- YEU CAU MUA HANG
-- ==============================

CREATE TABLE YeuCauMuaHang
(
    MaYeuCau INT IDENTITY(1,1) PRIMARY KEY,

    MaBoPhan INT NOT NULL,

    NgayYeuCau DATE NOT NULL,

    NgayCanHang DATE NOT NULL,

    MucDoUuTien NVARCHAR(20) NOT NULL,

    LyDoMua NVARCHAR(1000) NOT NULL,

    NguoiLap INT NOT NULL,

    TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Nhap',

    NgayDuyet DATETIME NULL,

    NguoiDuyet INT NULL,

    LyDoTuChoiHuy NVARCHAR(500),


    CONSTRAINT FK_YeuCau_BoPhan
    FOREIGN KEY(MaBoPhan)
    REFERENCES BoPhanDeNghi(MaBoPhan),


    CONSTRAINT FK_YeuCau_NguoiLap
    FOREIGN KEY(NguoiLap)
    REFERENCES TaiKhoan(MaTaiKhoan),


    CONSTRAINT FK_YeuCau_NguoiDuyet
    FOREIGN KEY(NguoiDuyet)
    REFERENCES TaiKhoan(MaTaiKhoan)
);



-- ==============================
-- CHI TIET YEU CAU
-- ==============================

CREATE TABLE ChiTietYeuCau
(
    MaChiTietYeuCau INT IDENTITY(1,1) PRIMARY KEY,

    MaYeuCau INT NOT NULL,

    MaHang INT NOT NULL,

    SoLuongYeuCau INT NOT NULL,

    SoLuongDuyet INT DEFAULT 0,

    GhiChu NVARCHAR(500),


    CONSTRAINT FK_CT_YeuCau
    FOREIGN KEY(MaYeuCau)
    REFERENCES YeuCauMuaHang(MaYeuCau),


    CONSTRAINT FK_CT_HangHoa
    FOREIGN KEY(MaHang)
    REFERENCES HangHoa(MaHang)
);



-- ==============================
-- DON MUA HANG
-- ==============================

CREATE TABLE DonMuaHang
(
    MaDonMua INT IDENTITY(1,1) PRIMARY KEY,

    MaYeuCau INT NOT NULL,

    MaNhaCungCap INT NOT NULL,

    NgayDat DATE NOT NULL,

    TrangThai NVARCHAR(20) NOT NULL,

    TongTien DECIMAL(18,2) DEFAULT 0,

    NguoiLap INT NOT NULL,

    GhiChu NVARCHAR(500),


    CONSTRAINT FK_DonMua_YeuCau
    FOREIGN KEY(MaYeuCau)
    REFERENCES YeuCauMuaHang(MaYeuCau),


    CONSTRAINT FK_DonMua_NCC
    FOREIGN KEY(MaNhaCungCap)
    REFERENCES NhaCungCap(MaNhaCungCap),


    CONSTRAINT FK_DonMua_NguoiLap
    FOREIGN KEY(NguoiLap)
    REFERENCES TaiKhoan(MaTaiKhoan)
);

-- ==============================
-- CHI TIET DON MUA
-- ==============================

CREATE TABLE ChiTietDonMua
(
    MaChiTietDon INT IDENTITY(1,1) PRIMARY KEY,

    MaDonMua INT NOT NULL,

    MaChiTietYeuCau INT NOT NULL,

    MaHang INT NOT NULL,

    SoLuongDat INT NOT NULL,

    DonGiaMua DECIMAL(18,2) NOT NULL,

    ThanhTien AS (SoLuongDat * DonGiaMua),


    CONSTRAINT FK_CTDonMua_DonMua
    FOREIGN KEY(MaDonMua)
    REFERENCES DonMuaHang(MaDonMua),


    CONSTRAINT FK_CTDonMua_CTYeucau
    FOREIGN KEY(MaChiTietYeuCau)
    REFERENCES ChiTietYeuCau(MaChiTietYeuCau),


    CONSTRAINT FK_CTDonMua_HangHoa
    FOREIGN KEY(MaHang)
    REFERENCES HangHoa(MaHang)
);



-- ==============================
-- LAN GIAO HANG
-- ==============================

CREATE TABLE LanGiaoHang
(
    MaLanGiao INT IDENTITY(1,1) PRIMARY KEY,

    MaDonMua INT NOT NULL,

    NgayGiao DATE NOT NULL,

    NguoiNhan INT NOT NULL,

    SoChungTu VARCHAR(50),

    GhiChu NVARCHAR(500),

    TrangThai BIT NOT NULL DEFAULT 1,


    CONSTRAINT FK_LanGiao_DonMua
    FOREIGN KEY(MaDonMua)
    REFERENCES DonMuaHang(MaDonMua),


    CONSTRAINT FK_LanGiao_NguoiNhan
    FOREIGN KEY(NguoiNhan)
    REFERENCES TaiKhoan(MaTaiKhoan)
);



-- ==============================
-- CHI TIET GIAO HANG
-- ==============================

CREATE TABLE ChiTietGiaoHang
(
    MaChiTietGiao INT IDENTITY(1,1) PRIMARY KEY,

    MaLanGiao INT NOT NULL,

    MaChiTietDon INT NOT NULL,

    SoLuongNhan INT NOT NULL,

    SoLuongDatChatLuong INT NULL,

    GhiChu NVARCHAR(500),


    CONSTRAINT FK_CTGH_LanGiao
    FOREIGN KEY(MaLanGiao)
    REFERENCES LanGiaoHang(MaLanGiao),


    CONSTRAINT FK_CTGH_CTDonMua
    FOREIGN KEY(MaChiTietDon)
    REFERENCES ChiTietDonMua(MaChiTietDon)
);



-- ==============================
-- THANH TOAN DON MUA
-- ==============================

CREATE TABLE ThanhToanDonMua
(
    MaThanhToan INT IDENTITY(1,1) PRIMARY KEY,

    MaDonMua INT NOT NULL,

    NgayThanhToan DATE NOT NULL,

    SoTienThanhToan DECIMAL(18,2) NOT NULL,

    PhuongThuc NVARCHAR(30) NOT NULL,

    NguoiThucHien INT NOT NULL,

    GhiChu NVARCHAR(500),


    CONSTRAINT FK_ThanhToan_DonMua
    FOREIGN KEY(MaDonMua)
    REFERENCES DonMuaHang(MaDonMua),


    CONSTRAINT FK_ThanhToan_NguoiThucHien
    FOREIGN KEY(NguoiThucHien)
    REFERENCES TaiKhoan(MaTaiKhoan)
);



-- ==============================
-- LICH SU TRANG THAI
-- ==============================

CREATE TABLE LichSuTrangThai
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    LoaiDoiTuong VARCHAR(30) NOT NULL,

    MaDoiTuong INT NOT NULL,

    TrangThaiCu NVARCHAR(30),

    TrangThaiMoi NVARCHAR(30) NOT NULL,

    NguoiThucHien INT NOT NULL,

    ThoiGian DATETIME DEFAULT GETDATE(),

    GhiChu NVARCHAR(500),


    CONSTRAINT FK_LichSu_NguoiThucHien
    FOREIGN KEY(NguoiThucHien)
    REFERENCES TaiKhoan(MaTaiKhoan)
);



-- ==============================
-- INDEX TANG HIEU NANG
-- ==============================

CREATE INDEX IX_HangHoa_TenHang
ON HangHoa(TenHang);



CREATE INDEX IX_DonMua_TrangThai
ON DonMuaHang(TrangThai);



CREATE INDEX IX_YeuCau_TrangThai
ON YeuCauMuaHang(TrangThai);



CREATE INDEX IX_LichSu_MaDoiTuong
ON LichSuTrangThai(MaDoiTuong);
GO