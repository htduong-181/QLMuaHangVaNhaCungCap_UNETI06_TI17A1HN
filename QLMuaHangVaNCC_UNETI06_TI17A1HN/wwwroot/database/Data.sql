USE QLMuaHangVaNCC;
GO

/* =========================================================
   1. BO PHAN DE NGHI
   ========================================================= */

SET IDENTITY_INSERT BoPhanDeNghi ON;

INSERT INTO BoPhanDeNghi
(MaBoPhan, TenBoPhan, NguoiPhuTrach, SoDienThoai, MoTa, TrangThai)
VALUES
(1, N'Phòng Công nghệ thông tin', N'Nguyễn Văn Hùng', '0901000001', N'Quản lý hệ thống và hạ tầng CNTT', 1),
(2, N'Phòng Kế toán', N'Trần Thu Hà', '0901000002', N'Quản lý tài chính và kế toán', 1),
(3, N'Phòng Nhân sự', N'Lê Minh Anh', '0901000003', N'Quản lý nhân sự và tuyển dụng', 1),
(4, N'Phòng Kinh doanh', N'Phạm Quốc Bảo', '0901000004', N'Phụ trách hoạt động kinh doanh', 1),
(5, N'Phòng Marketing', N'Nguyễn Thùy Linh', '0901000005', N'Phụ trách truyền thông và quảng bá', 1),
(6, N'Phòng Hành chính', N'Đỗ Văn Nam', '0901000006', N'Quản lý hành chính văn phòng', 1),
(7, N'Phòng Kỹ thuật', N'Hoàng Đức Long', '0901000007', N'Phụ trách kỹ thuật và bảo trì', 1),
(8, N'Phòng Kho vận', N'Vũ Thành Công', '0901000008', N'Quản lý kho và vận chuyển', 1),
(9, N'Phòng Chăm sóc khách hàng', N'Bùi Ngọc Mai', '0901000009', N'Hỗ trợ và chăm sóc khách hàng', 1),
(10, N'Ban Giám đốc', N'Nguyễn Hoàng Minh', '0901000010', N'Điều hành hoạt động công ty', 1);

SET IDENTITY_INSERT BoPhanDeNghi OFF;
GO


/* =========================================================
   2. LOAI HANG
   ========================================================= */

SET IDENTITY_INSERT LoaiHang ON;

INSERT INTO LoaiHang
(MaLoaiHang, TenLoaiHang, MoTa, TrangThai)
VALUES
(1, N'Máy tính', N'Máy tính để bàn và máy tính xách tay', 1),
(2, N'Thiết bị ngoại vi', N'Chuột, bàn phím và thiết bị ngoại vi', 1),
(3, N'Thiết bị mạng', N'Router, switch và thiết bị mạng', 1),
(4, N'Thiết bị in ấn', N'Máy in và thiết bị phục vụ in ấn', 1),
(5, N'Văn phòng phẩm', N'Các loại vật tư văn phòng', 1),
(6, N'Thiết bị lưu trữ', N'Ổ cứng và thiết bị lưu trữ dữ liệu', 1),
(7, N'Thiết bị trình chiếu', N'Máy chiếu và phụ kiện', 1),
(8, N'Nội thất văn phòng', N'Bàn ghế và nội thất văn phòng', 1),
(9, N'Thiết bị điện', N'Thiết bị điện phục vụ văn phòng', 1),
(10, N'Vật tư kỹ thuật', N'Vật tư phục vụ sửa chữa và bảo trì', 1);

SET IDENTITY_INSERT LoaiHang OFF;
GO


/* =========================================================
   3. DON VI TINH
   ========================================================= */

SET IDENTITY_INSERT DonViTinh ON;

INSERT INTO DonViTinh
(MaDonViTinh, TenDonViTinh, MoTa, TrangThai)
VALUES
(1, N'Cái', N'Đơn vị tính theo cái', 1),
(2, N'Chiếc', N'Đơn vị tính theo chiếc', 1),
(3, N'Bộ', N'Đơn vị tính theo bộ', 1),
(4, N'Hộp', N'Đơn vị tính theo hộp', 1),
(5, N'Thùng', N'Đơn vị tính theo thùng', 1),
(6, N'Ram', N'Đơn vị giấy', 1),
(7, N'Cuộn', N'Đơn vị tính theo cuộn', 1),
(8, N'Gói', N'Đơn vị tính theo gói', 1),
(9, N'Bình', N'Đơn vị tính theo bình', 1),
(10, N'Bộ thiết bị', N'Đơn vị cho nhóm thiết bị', 1);

SET IDENTITY_INSERT DonViTinh OFF;
GO


/* =========================================================
   4. TAI KHOAN
   MatKhau chỉ là dữ liệu demo.
   ========================================================= */

SET IDENTITY_INSERT TaiKhoan ON;

INSERT INTO TaiKhoan
(MaTaiKhoan, TenDangNhap, MatKhau, HoTen, Email, VaiTro, TrangThai)
VALUES
(1, 'admin', '123456', N'Nguyễn Văn Admin', 'admin@company.vn', 'Admin', 1),
(2, 'duongbt', '123456', N'Bùi Tùng Dương', 'duongbt@company.vn', 'NhanVienDeNghi', 1),
(3, 'hungnv', '123456', N'Nguyễn Văn Hùng', 'hungnv@company.vn', 'NhanVienDeNghi', 1),
(4, 'hatt', '123456', N'Trần Thu Hà', 'hatt@company.vn', 'KeToan', 1),
(5, 'minhna', '123456', N'Nguyễn Anh Minh', 'minhna@company.vn', 'NguoiDuyet', 1),
(6, 'linhnt', '123456', N'Nguyễn Thùy Linh', 'linhnt@company.vn', 'NhanVienDeNghi', 1),
(7, 'longhd', '123456', N'Hoàng Đức Long', 'longhd@company.vn', 'NhanVienMuaHang', 1),
(8, 'congvt', '123456', N'Vũ Thành Công', 'congvt@company.vn', 'NhanVienKho', 1),
(9, 'maibn', '123456', N'Bùi Ngọc Mai', 'maibn@company.vn', 'NhanVienDeNghi', 1),
(10, 'namdv', '123456', N'Đỗ Văn Nam', 'namdv@company.vn', 'NhanVienMuaHang', 1);

SET IDENTITY_INSERT TaiKhoan OFF;
GO


/* =========================================================
   5. NHA CUNG CAP
   ========================================================= */

SET IDENTITY_INSERT NhaCungCap ON;

INSERT INTO NhaCungCap
(MaNhaCungCap, TenNhaCungCap, MaSoThue, SoDienThoai,
 Email, DiaChi, NguoiLienHe, TrangThai)
VALUES
(1, N'Công ty TNHH Thiết bị An Phát', '0101000001', '02473008888',
 'contact@anphat.vn', N'Hà Nội', N'Nguyễn Minh Tuấn', 1),

(2, N'Công ty TNHH Máy tính Hà Nội', '0101000002', '02436208888',
 'sales@hanoicomputer.vn', N'Hà Nội', N'Trần Văn Hải', 1),

(3, N'Công ty TNHH Phong Vũ', '0301000003', '02873008888',
 'sales@phongvu.vn', N'TP. Hồ Chí Minh', N'Lê Thanh Sơn', 1),

(4, N'Công ty Thiết bị Văn phòng Minh Long', '0101000004', '02435556666',
 'contact@minhlong.vn', N'Hà Nội', N'Phạm Minh Long', 1),

(5, N'Công ty Văn phòng phẩm Hồng Hà', '0101000005', '02436667777',
 'sales@hongha.vn', N'Hà Nội', N'Nguyễn Thu Trang', 1),

(6, N'Công ty Công nghệ Sao Việt', '0101000006', '02437778888',
 'info@saoviet.vn', N'Hà Nội', N'Hoàng Quốc Việt', 1),

(7, N'Công ty Thiết bị Mạng Việt Nam', '0101000007', '02438889999',
 'sales@networkvn.vn', N'Hà Nội', N'Vũ Đức Anh', 1),

(8, N'Công ty Nội thất Hòa Phát', '0101000008', '02439990000',
 'sales@noithat.vn', N'Hưng Yên', N'Đỗ Minh Hoàng', 1),

(9, N'Công ty Thiết bị Điện Á Châu', '0101000009', '02431112222',
 'contact@achau.vn', N'Hà Nội', N'Nguyễn Văn Đức', 1),

(10, N'Công ty Kỹ thuật Thành Công', '0101000010', '02432223333',
 'sales@thanhcong.vn', N'Hà Nội', N'Bùi Quốc Khánh', 1);

SET IDENTITY_INSERT NhaCungCap OFF;
GO


/* =========================================================
   6. HANG HOA
   ========================================================= */

SET IDENTITY_INSERT HangHoa ON;

INSERT INTO HangHoa
(MaHang, TenHang, MaLoaiHang, MaDonViTinh,
 GiaThamKhao, MoTa, TrangThai)
VALUES
(1, N'Laptop Dell Latitude 5450', 1, 2, 23500000,
 N'Laptop phục vụ công việc văn phòng và lập trình', 1),

(2, N'Màn hình Dell 24 inch', 2, 2, 4200000,
 N'Màn hình Full HD 24 inch', 1),

(3, N'Chuột Logitech M331', 2, 1, 350000,
 N'Chuột không dây Logitech', 1),

(4, N'Bàn phím Logitech K120', 2, 1, 250000,
 N'Bàn phím USB dùng cho máy tính văn phòng', 1),

(5, N'Router TP-Link AX3000', 3, 1, 1800000,
 N'Router Wi-Fi 6 dùng cho văn phòng', 1),

(6, N'Máy in HP LaserJet Pro', 4, 2, 6200000,
 N'Máy in laser phục vụ phòng hành chính', 1),

(7, N'Giấy A4 Double A 70gsm', 5, 6, 85000,
 N'Giấy A4 sử dụng cho in ấn văn phòng', 1),

(8, N'Ổ cứng SSD Samsung 1TB', 6, 1, 2200000,
 N'SSD dung lượng 1TB', 1),

(9, N'Máy chiếu Epson EB-E01', 7, 1, 11500000,
 N'Máy chiếu phục vụ phòng họp', 1),

(10, N'Ghế xoay văn phòng Hòa Phát', 8, 2, 1450000,
 N'Ghế nhân viên văn phòng', 1);

SET IDENTITY_INSERT HangHoa OFF;
GO


/* =========================================================
   7. NHA CUNG CAP - HANG HOA
   ========================================================= */

SET IDENTITY_INSERT NhaCungCapHangHoa ON;

INSERT INTO NhaCungCapHangHoa
(Id, MaNhaCungCap, MaHang, DonGiaBao,
 NgayCapNhatGia, ThoiGianGiaoDuKien, TrangThai)
VALUES
(1, 1, 1, 22800000, '2026-09-01', 5, 1),
(2, 2, 1, 23100000, '2026-09-02', 4, 1),
(3, 3, 2, 4050000, '2026-09-03', 3, 1),
(4, 1, 3, 330000, '2026-09-04', 2, 1),
(5, 2, 4, 235000, '2026-09-05', 2, 1),
(6, 7, 5, 1720000, '2026-09-06', 3, 1),
(7, 4, 6, 6000000, '2026-09-07', 5, 1),
(8, 5, 7, 80000, '2026-09-08', 2, 1),
(9, 6, 8, 2100000, '2026-09-09', 3, 1),
(10, 8, 10, 1380000, '2026-09-10', 7, 1);

SET IDENTITY_INSERT NhaCungCapHangHoa OFF;
GO


/* =========================================================
   8. YEU CAU MUA HANG
   ========================================================= */

SET IDENTITY_INSERT YeuCauMuaHang ON;

INSERT INTO YeuCauMuaHang
(MaYeuCau, MaBoPhan, NgayYeuCau, NgayCanHang,
 MucDoUuTien, LyDoMua, NguoiLap, TrangThai,
 NgayDuyet, NguoiDuyet, LyDoTuChoiHuy)
VALUES
(1, 1, '2026-09-01', '2026-09-10', N'Cao',
 N'Trang bị laptop cho nhân viên IT mới', 2, N'DaDuyet',
 '2026-09-02', 5, NULL),

(2, 2, '2026-09-02', '2026-09-12', N'BinhThuong',
 N'Trang bị màn hình cho phòng kế toán', 4, N'DaDuyet',
 '2026-09-03', 5, NULL),

(3, 3, '2026-09-03', '2026-09-15', N'BinhThuong',
 N'Bổ sung chuột máy tính cho nhân viên', 3, N'DaDuyet',
 '2026-09-04', 5, NULL),

(4, 4, '2026-09-04', '2026-09-16', N'BinhThuong',
 N'Bổ sung bàn phím cho phòng kinh doanh', 2, N'DaDuyet',
 '2026-09-05', 5, NULL),

(5, 1, '2026-09-05', '2026-09-10', N'Cao',
 N'Nâng cấp hệ thống mạng Wi-Fi văn phòng', 3, N'DaDuyet',
 '2026-09-06', 5, NULL),

(6, 6, '2026-09-06', '2026-09-18', N'BinhThuong',
 N'Thay máy in cũ của phòng hành chính', 10, N'DaDuyet',
 '2026-09-07', 5, NULL),

(7, 6, '2026-09-07', '2026-09-20', N'Thap',
 N'Bổ sung giấy in sử dụng trong tháng', 10, N'DaDuyet',
 '2026-09-08', 5, NULL),

(8, 1, '2026-09-08', '2026-09-15', N'Cao',
 N'Nâng cấp ổ cứng máy trạm', 3, N'DaDuyet',
 '2026-09-09', 5, NULL),

(9, 5, '2026-09-09', '2026-09-25', N'BinhThuong',
 N'Mua máy chiếu phục vụ thuyết trình', 6, N'DaDuyet',
 '2026-09-10', 5, NULL),

(10, 3, '2026-09-10', '2026-09-30', N'Thap',
 N'Bổ sung ghế cho nhân viên mới', 2, N'DaDuyet',
 '2026-09-11', 5, NULL);

SET IDENTITY_INSERT YeuCauMuaHang OFF;
GO


/* =========================================================
   9. CHI TIET YEU CAU
   ========================================================= */

SET IDENTITY_INSERT ChiTietYeuCau ON;

INSERT INTO ChiTietYeuCau
(MaChiTietYeuCau, MaYeuCau, MaHang,
 SoLuongYeuCau, SoLuongDuyet, GhiChu)
VALUES
(1, 1, 1, 5, 5, N'Cấu hình RAM tối thiểu 16GB'),
(2, 2, 2, 8, 8, N'Màn hình Full HD'),
(3, 3, 3, 15, 15, NULL),
(4, 4, 4, 10, 10, NULL),
(5, 5, 5, 3, 3, N'Hỗ trợ Wi-Fi 6'),
(6, 6, 6, 2, 2, N'Máy in laser'),
(7, 7, 7, 50, 50, N'Giấy A4 70gsm'),
(8, 8, 8, 6, 6, N'SSD dung lượng 1TB'),
(9, 9, 9, 1, 1, N'Dùng cho phòng họp'),
(10, 10, 10, 12, 12, N'Ghế có tựa lưng');

SET IDENTITY_INSERT ChiTietYeuCau OFF;
GO


/* =========================================================
   10. DON MUA HANG
   ========================================================= */

SET IDENTITY_INSERT DonMuaHang ON;

INSERT INTO DonMuaHang
(MaDonMua, MaYeuCau, MaNhaCungCap, NgayDat,
 TrangThai, TongTien, GhiChu, NguoiLap)
VALUES
(1, 1, 1, '2026-09-03', N'DaDat', 114000000,
 N'Đơn mua 5 laptop Dell', 7),

(2, 2, 3, '2026-09-04', N'DaDat', 32400000,
 N'Đơn mua màn hình phòng kế toán', 7),

(3, 3, 1, '2026-09-05', N'DaDat', 4950000,
 N'Đơn mua chuột không dây', 10),

(4, 4, 2, '2026-09-06', N'DaDat', 2350000,
 N'Đơn mua bàn phím', 10),

(5, 5, 7, '2026-09-07', N'DaDat', 5160000,
 N'Đơn mua router Wi-Fi 6', 7),

(6, 6, 4, '2026-09-08', N'DaDat', 12000000,
 N'Đơn mua máy in', 7),

(7, 7, 5, '2026-09-09', N'DaDat', 4000000,
 N'Đơn mua giấy A4', 10),

(8, 8, 6, '2026-09-10', N'DaDat', 12600000,
 N'Đơn mua SSD', 7),

(9, 9, 6, '2026-09-11', N'DaDat', 11500000,
 N'Đơn mua máy chiếu', 10),

(10, 10, 8, '2026-09-12', N'DaDat', 16560000,
 N'Đơn mua ghế văn phòng', 7);

SET IDENTITY_INSERT DonMuaHang OFF;
GO


/* =========================================================
   11. CHI TIET DON MUA
   ========================================================= */

SET IDENTITY_INSERT ChiTietDonMua ON;

INSERT INTO ChiTietDonMua
(MaChiTietDon, MaDonMua, MaChiTietYeuCau,
 MaHang, SoLuongDat, DonGiaMua)
VALUES
(1, 1, 1, 1, 5, 22800000),
(2, 2, 2, 2, 8, 4050000),
(3, 3, 3, 3, 15, 330000),
(4, 4, 4, 4, 10, 235000),
(5, 5, 5, 5, 3, 1720000),
(6, 6, 6, 6, 2, 6000000),
(7, 7, 7, 7, 50, 80000),
(8, 8, 8, 8, 6, 2100000),
(9, 9, 9, 9, 1, 11500000),
(10, 10, 10, 10, 12, 1380000);

SET IDENTITY_INSERT ChiTietDonMua OFF;
GO


/* =========================================================
   12. LAN GIAO HANG
   ========================================================= */

SET IDENTITY_INSERT LanGiaoHang ON;

INSERT INTO LanGiaoHang
(MaLanGiao, MaDonMua, NgayGiao, NguoiNhan,
 SoChungTu, GhiChu, TrangThai)
VALUES
(1, 1, '2026-09-08', 8, 'PG001', N'Giao đủ laptop', 1),
(2, 2, '2026-09-09', 8, 'PG002', N'Giao đủ màn hình', 1),
(3, 3, '2026-09-10', 8, 'PG003', N'Đã kiểm tra hàng', 1),
(4, 4, '2026-09-11', 8, 'PG004', N'Giao đủ bàn phím', 1),
(5, 5, '2026-09-12', 8, 'PG005', N'Router nguyên hộp', 1),
(6, 6, '2026-09-13', 8, 'PG006', N'Máy in hoạt động tốt', 1),
(7, 7, '2026-09-14', 8, 'PG007', N'Nhận đủ giấy', 1),
(8, 8, '2026-09-15', 8, 'PG008', N'Nhận đủ SSD', 1),
(9, 9, '2026-09-16', 8, 'PG009', N'Máy chiếu hoạt động tốt', 1),
(10, 10, '2026-09-17', 8, 'PG010', N'Ghế giao đủ số lượng', 1);

SET IDENTITY_INSERT LanGiaoHang OFF;
GO


/* =========================================================
   13. CHI TIET GIAO HANG
   ========================================================= */

SET IDENTITY_INSERT ChiTietGiaoHang ON;

INSERT INTO ChiTietGiaoHang
(MaChiTietGiao, MaLanGiao, MaChiTietDon,
 SoLuongNhan, SoLuongDatChatLuong, GhiChu)
VALUES
(1, 1, 1, 5, 5, N'Đạt chất lượng'),
(2, 2, 2, 8, 8, N'Đạt chất lượng'),
(3, 3, 3, 15, 15, N'Đạt chất lượng'),
(4, 4, 4, 10, 10, N'Đạt chất lượng'),
(5, 5, 5, 3, 3, N'Đạt chất lượng'),
(6, 6, 6, 2, 2, N'Đạt chất lượng'),
(7, 7, 7, 50, 50, N'Đủ số lượng'),
(8, 8, 8, 6, 6, N'Đạt chất lượng'),
(9, 9, 9, 1, 1, N'Đạt chất lượng'),
(10, 10, 10, 12, 12, N'Đạt chất lượng');

SET IDENTITY_INSERT ChiTietGiaoHang OFF;
GO


/* =========================================================
   14. THANH TOAN DON MUA
   ========================================================= */

SET IDENTITY_INSERT ThanhToanDonMua ON;

INSERT INTO ThanhToanDonMua
(MaThanhToan, MaDonMua, NgayThanhToan,
 SoTienThanhToan, PhuongThuc, NguoiThucHien, GhiChu)
VALUES
(1, 1, '2026-09-10', 114000000, N'ChuyenKhoan', 4, N'Đã thanh toán đủ'),
(2, 2, '2026-09-11', 32400000, N'ChuyenKhoan', 4, N'Đã thanh toán đủ'),
(3, 3, '2026-09-12', 4950000, N'ChuyenKhoan', 4, N'Đã thanh toán đủ'),
(4, 4, '2026-09-13', 2350000, N'TienMat', 4, N'Đã thanh toán'),
(5, 5, '2026-09-14', 5160000, N'ChuyenKhoan', 4, N'Đã thanh toán'),
(6, 6, '2026-09-15', 12000000, N'ChuyenKhoan', 4, N'Đã thanh toán'),
(7, 7, '2026-09-16', 4000000, N'TienMat', 4, N'Đã thanh toán'),
(8, 8, '2026-09-17', 12600000, N'ChuyenKhoan', 4, N'Đã thanh toán'),
(9, 9, '2026-09-18', 11500000, N'ChuyenKhoan', 4, N'Đã thanh toán'),
(10, 10, '2026-09-19', 16560000, N'ChuyenKhoan', 4, N'Đã thanh toán');

SET IDENTITY_INSERT ThanhToanDonMua OFF;
GO


/* =========================================================
   15. LICH SU TRANG THAI
   ========================================================= */

SET IDENTITY_INSERT LichSuTrangThai ON;

INSERT INTO LichSuTrangThai
(Id, LoaiDoiTuong, MaDoiTuong, TrangThaiCu,
 TrangThaiMoi, NguoiThucHien, ThoiGian, GhiChu)
VALUES
(1, N'YeuCauMuaHang', 1, N'Nhap', N'DaDuyet', 5, '2026-09-02 09:00:00', N'Duyệt yêu cầu mua laptop'),
(2, N'YeuCauMuaHang', 2, N'Nhap', N'DaDuyet', 5, '2026-09-03 09:15:00', N'Duyệt yêu cầu mua màn hình'),
(3, N'YeuCauMuaHang', 3, N'Nhap', N'DaDuyet', 5, '2026-09-04 10:00:00', N'Duyệt yêu cầu mua chuột'),
(4, N'DonMuaHang', 1, N'ChoDuyet', N'DaDat', 7, '2026-09-03 14:00:00', N'Đã gửi đơn tới nhà cung cấp'),
(5, N'DonMuaHang', 2, N'ChoDuyet', N'DaDat', 7, '2026-09-04 14:30:00', N'Đã đặt hàng'),
(6, N'DonMuaHang', 3, N'ChoDuyet', N'DaDat', 10, '2026-09-05 15:00:00', N'Đã đặt hàng'),
(7, N'DonMuaHang', 4, N'ChoDuyet', N'DaDat', 10, '2026-09-06 15:20:00', N'Đã đặt hàng'),
(8, N'DonMuaHang', 5, N'ChoDuyet', N'DaDat', 7, '2026-09-07 16:00:00', N'Đã đặt router'),
(9, N'DonMuaHang', 6, N'ChoDuyet', N'DaDat', 7, '2026-09-08 16:30:00', N'Đã đặt máy in'),
(10, N'DonMuaHang', 7, N'ChoDuyet', N'DaDat', 10, '2026-09-09 17:00:00', N'Đã đặt giấy văn phòng');

SET IDENTITY_INSERT LichSuTrangThai OFF;
GO


/* =========================================================
   KIEM TRA DU LIEU
   ========================================================= */

SELECT N'BoPhanDeNghi' AS Bang, COUNT(*) AS SoLuong FROM BoPhanDeNghi
UNION ALL
SELECT N'LoaiHang', COUNT(*) FROM LoaiHang
UNION ALL
SELECT N'DonViTinh', COUNT(*) FROM DonViTinh
UNION ALL
SELECT N'TaiKhoan', COUNT(*) FROM TaiKhoan
UNION ALL
SELECT N'NhaCungCap', COUNT(*) FROM NhaCungCap
UNION ALL
SELECT N'HangHoa', COUNT(*) FROM HangHoa
UNION ALL
SELECT N'NhaCungCapHangHoa', COUNT(*) FROM NhaCungCapHangHoa
UNION ALL
SELECT N'YeuCauMuaHang', COUNT(*) FROM YeuCauMuaHang
UNION ALL
SELECT N'ChiTietYeuCau', COUNT(*) FROM ChiTietYeuCau
UNION ALL
SELECT N'DonMuaHang', COUNT(*) FROM DonMuaHang
UNION ALL
SELECT N'ChiTietDonMua', COUNT(*) FROM ChiTietDonMua
UNION ALL
SELECT N'LanGiaoHang', COUNT(*) FROM LanGiaoHang
UNION ALL
SELECT N'ChiTietGiaoHang', COUNT(*) FROM ChiTietGiaoHang
UNION ALL
SELECT N'ThanhToanDonMua', COUNT(*) FROM ThanhToanDonMua
UNION ALL
SELECT N'LichSuTrangThai', COUNT(*) FROM LichSuTrangThai;
GO