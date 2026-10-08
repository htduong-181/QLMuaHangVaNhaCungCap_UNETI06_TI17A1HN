USE QLMuaHangVaNCC;
GO

/* =========================================================
   1. BO PHAN DE NGHI - THEM 20 DONG
   ========================================================= */
SET IDENTITY_INSERT BoPhanDeNghi ON;

INSERT INTO BoPhanDeNghi
(MaBoPhan, TenBoPhan, NguoiPhuTrach, SoDienThoai, MoTa, TrangThai)
VALUES
(11,N'Phòng Pháp chế',N'Nguyễn Hải Nam','0911000011',N'Phụ trách pháp lý doanh nghiệp',1),
(12,N'Phòng Nghiên cứu phát triển',N'Trần Minh Đức','0911000012',N'Nghiên cứu sản phẩm mới',1),
(13,N'Phòng Kiểm soát chất lượng',N'Lê Thanh Tùng','0911000013',N'Kiểm soát chất lượng sản phẩm',1),
(14,N'Phòng Đào tạo',N'Phạm Thu Hương','0911000014',N'Đào tạo nhân viên',1),
(15,N'Phòng Bảo trì',N'Đỗ Quốc Huy','0911000015',N'Bảo trì thiết bị',1),
(16,N'Phòng Dự án',N'Hoàng Minh Sơn','0911000016',N'Quản lý các dự án',1),
(17,N'Phòng Mua hàng',N'Vũ Hải Anh','0911000017',N'Phụ trách mua hàng',1),
(18,N'Phòng An ninh',N'Bùi Văn Trung','0911000018',N'Đảm bảo an ninh',1),
(19,N'Phòng Thiết kế',N'Nguyễn Khánh Linh','0911000019',N'Thiết kế sản phẩm',1),
(20,N'Phòng Sản xuất',N'Trần Quốc Dũng','0911000020',N'Quản lý sản xuất',1),
(21,N'Phòng Logistics',N'Lê Hoàng Nam','0911000021',N'Điều phối vận chuyển',1),
(22,N'Phòng Kiểm toán',N'Phạm Minh Hà','0911000022',N'Kiểm toán nội bộ',1),
(23,N'Phòng Đối ngoại',N'Đỗ Ngọc Anh','0911000023',N'Quan hệ đối tác',1),
(24,N'Phòng Dịch vụ',N'Hoàng Tuấn Anh','0911000024',N'Dịch vụ khách hàng',1),
(25,N'Phòng Phân tích dữ liệu',N'Vũ Minh Khang','0911000025',N'Phân tích dữ liệu',1),
(26,N'Phòng Hạ tầng',N'Bùi Quốc Việt','0911000026',N'Quản lý hạ tầng',1),
(27,N'Phòng Truyền thông',N'Nguyễn Thu Hà','0911000027',N'Truyền thông doanh nghiệp',1),
(28,N'Phòng Kế hoạch',N'Trần Đức Anh','0911000028',N'Lập kế hoạch kinh doanh',1),
(29,N'Phòng Điều hành',N'Lê Văn Sơn','0911000029',N'Điều phối hoạt động',1),
(30,N'Phòng Quản lý tài sản',N'Phạm Hoàng Long','0911000030',N'Quản lý tài sản công ty',1);

SET IDENTITY_INSERT BoPhanDeNghi OFF;
GO


/* =========================================================
   2. LOAI HANG
   ========================================================= */
SET IDENTITY_INSERT LoaiHang ON;

INSERT INTO LoaiHang
(MaLoaiHang,TenLoaiHang,MoTa,TrangThai)
VALUES
(11,N'Điện thoại',N'Điện thoại phục vụ công việc',1),
(12,N'Máy tính bảng',N'Thiết bị máy tính bảng',1),
(13,N'Camera',N'Camera giám sát',1),
(14,N'Âm thanh',N'Thiết bị âm thanh',1),
(15,N'Phụ kiện máy tính',N'Phụ kiện CNTT',1),
(16,N'Dây cáp',N'Cáp mạng và cáp kết nối',1),
(17,N'Bộ lưu điện',N'UPS và thiết bị nguồn',1),
(18,N'Điều hòa',N'Thiết bị điều hòa',1),
(19,N'Chiếu sáng',N'Thiết bị chiếu sáng',1),
(20,N'Bảo hộ lao động',N'Vật tư bảo hộ',1),
(21,N'Dụng cụ sửa chữa',N'Dụng cụ kỹ thuật',1),
(22,N'Đồ dùng vệ sinh',N'Vật tư vệ sinh',1),
(23,N'Thiết bị chấm công',N'Máy chấm công',1),
(24,N'Thiết bị hội nghị',N'Thiết bị phòng họp',1),
(25,N'Mực in',N'Mực cho máy in',1),
(26,N'Phần cứng máy tính',N'Linh kiện máy tính',1),
(27,N'Thiết bị bảo mật',N'Thiết bị bảo mật mạng',1),
(28,N'Tủ và kệ',N'Tủ kệ văn phòng',1),
(29,N'Thiết bị nhà kho',N'Thiết bị sử dụng trong kho',1),
(30,N'Khác',N'Nhóm hàng hóa khác',1);

SET IDENTITY_INSERT LoaiHang OFF;
GO


/* =========================================================
   3. DON VI TINH
   ========================================================= */
SET IDENTITY_INSERT DonViTinh ON;

INSERT INTO DonViTinh
(MaDonViTinh,TenDonViTinh,MoTa,TrangThai)
VALUES
(11,N'Kg',N'Kilogram',1),
(12,N'Mét',N'Đơn vị chiều dài',1),
(13,N'Lít',N'Đơn vị thể tích',1),
(14,N'Túi',N'Đơn vị túi',1),
(15,N'Chai',N'Đơn vị chai',1),
(16,N'Bao',N'Đơn vị bao',1),
(17,N'Tập',N'Đơn vị tập',1),
(18,N'Quyển',N'Đơn vị quyển',1),
(19,N'Ống',N'Đơn vị ống',1),
(20,N'Tấm',N'Đơn vị tấm',1),
(21,N'Khay',N'Đơn vị khay',1),
(22,N'Can',N'Đơn vị can',1),
(23,N'Cặp',N'Đơn vị cặp',1),
(24,N'Dây',N'Đơn vị dây',1),
(25,N'Bịch',N'Đơn vị bịch',1),
(26,N'Bó',N'Đơn vị bó',1),
(27,N'Thanh',N'Đơn vị thanh',1),
(28,N'Túi lớn',N'Đơn vị đóng gói',1),
(29,N'Kiện',N'Đơn vị kiện hàng',1),
(30,N'Bộ hoàn chỉnh',N'Bộ sản phẩm hoàn chỉnh',1);

SET IDENTITY_INSERT DonViTinh OFF;
GO


/* =========================================================
   4. TAI KHOAN
   ========================================================= */
SET IDENTITY_INSERT TaiKhoan ON;

INSERT INTO TaiKhoan
(MaTaiKhoan,TenDangNhap,MatKhau,HoTen,Email,VaiTro,TrangThai)
VALUES
(11,'user11','123456',N'Nguyễn Minh Anh','user11@company.vn','NhanVienDeNghi',1),
(12,'user12','123456',N'Trần Quốc Anh','user12@company.vn','NhanVienDeNghi',1),
(13,'user13','123456',N'Lê Văn Bình','user13@company.vn','NhanVienDeNghi',1),
(14,'user14','123456',N'Phạm Minh Châu','user14@company.vn','NhanVienDeNghi',1),
(15,'user15','123456',N'Đỗ Đức Dũng','user15@company.vn','NhanVienMuaHang',1),
(16,'user16','123456',N'Hoàng Thu Giang','user16@company.vn','NhanVienDeNghi',1),
(17,'user17','123456',N'Vũ Quốc Hải','user17@company.vn','NhanVienKho',1),
(18,'user18','123456',N'Bùi Minh Hiếu','user18@company.vn','NhanVienDeNghi',1),
(19,'user19','123456',N'Nguyễn Thu Hương','user19@company.vn','KeToan',1),
(20,'user20','123456',N'Trần Trung Kiên','user20@company.vn','NguoiDuyet',1),
(21,'user21','123456',N'Lê Hoàng Long','user21@company.vn','NhanVienDeNghi',1),
(22,'user22','123456',N'Phạm Ngọc Mai','user22@company.vn','NhanVienMuaHang',1),
(23,'user23','123456',N'Đỗ Thành Nam','user23@company.vn','NhanVienKho',1),
(24,'user24','123456',N'Hoàng Minh Phương','user24@company.vn','NhanVienDeNghi',1),
(25,'user25','123456',N'Vũ Đức Quân','user25@company.vn','KeToan',1),
(26,'user26','123456',N'Bùi Thu Trang','user26@company.vn','NhanVienDeNghi',1),
(27,'user27','123456',N'Nguyễn Văn Tuấn','user27@company.vn','NguoiDuyet',1),
(28,'user28','123456',N'Trần Minh Việt','user28@company.vn','NhanVienMuaHang',1),
(29,'user29','123456',N'Lê Thị Xuân','user29@company.vn','NhanVienKho',1),
(30,'user30','123456',N'Phạm Quốc Hưng','user30@company.vn','NhanVienDeNghi',1);

SET IDENTITY_INSERT TaiKhoan OFF;
GO


/* =========================================================
   5. NHA CUNG CAP
   ========================================================= */
SET IDENTITY_INSERT NhaCungCap ON;

INSERT INTO NhaCungCap
(MaNhaCungCap,TenNhaCungCap,MaSoThue,SoDienThoai,Email,DiaChi,NguoiLienHe,TrangThai)
VALUES
(11,N'Công ty Công nghệ Đại Việt','0102000011','0920000011','ncc11@gmail.com',N'Hà Nội',N'Nguyễn Đức Anh',1),
(12,N'Công ty Thiết bị Đông Á','0102000012','0920000012','ncc12@gmail.com',N'Hải Phòng',N'Trần Văn Bình',1),
(13,N'Công ty Công nghệ Minh Phát','0102000013','0920000013','ncc13@gmail.com',N'Hà Nội',N'Lê Quốc Dũng',1),
(14,N'Công ty Thiết bị Thành Đạt','0102000014','0920000014','ncc14@gmail.com',N'Bắc Ninh',N'Phạm Minh Hải',1),
(15,N'Công ty Tin học Hoàng Gia','0102000015','0920000015','ncc15@gmail.com',N'Hà Nội',N'Đỗ Văn Hùng',1),
(16,N'Công ty Văn phòng Việt','0102000016','0920000016','ncc16@gmail.com',N'Nam Định',N'Hoàng Minh Khoa',1),
(17,N'Công ty Công nghệ Tân Tiến','0102000017','0920000017','ncc17@gmail.com',N'Hà Nội',N'Vũ Văn Long',1),
(18,N'Công ty Thiết bị Phú Thành','0102000018','0920000018','ncc18@gmail.com',N'Hưng Yên',N'Bùi Đức Minh',1),
(19,N'Công ty Máy tính Đông Dương','0102000019','0920000019','ncc19@gmail.com',N'Hà Nội',N'Nguyễn Quốc Nam',1),
(20,N'Công ty Kỹ thuật Việt Phát','0102000020','0920000020','ncc20@gmail.com',N'Hà Nam',N'Trần Văn Phúc',1),
(21,N'Công ty Công nghệ Hưng Thịnh','0102000021','0920000021','ncc21@gmail.com',N'Hà Nội',N'Lê Minh Quân',1),
(22,N'Công ty Thiết bị Quốc Việt','0102000022','0920000022','ncc22@gmail.com',N'Bắc Giang',N'Phạm Đức Sơn',1),
(23,N'Công ty Văn phòng Thành Nam','0102000023','0920000023','ncc23@gmail.com',N'Hà Nội',N'Đỗ Minh Tuấn',1),
(24,N'Công ty Công nghệ Thiên Long','0102000024','0920000024','ncc24@gmail.com',N'Thái Nguyên',N'Hoàng Văn Việt',1),
(25,N'Công ty Thiết bị Ánh Dương','0102000025','0920000025','ncc25@gmail.com',N'Hà Nội',N'Vũ Minh Anh',1),
(26,N'Công ty Tin học Bắc Việt','0102000026','0920000026','ncc26@gmail.com',N'Phú Thọ',N'Bùi Quốc Bình',1),
(27,N'Công ty Công nghệ Việt Thành','0102000027','0920000027','ncc27@gmail.com',N'Hà Nội',N'Nguyễn Đức Công',1),
(28,N'Công ty Thiết bị Minh Đức','0102000028','0920000028','ncc28@gmail.com',N'Ninh Bình',N'Trần Minh Đức',1),
(29,N'Công ty Kỹ thuật Đại Thành','0102000029','0920000029','ncc29@gmail.com',N'Hà Nội',N'Lê Quốc Huy',1),
(30,N'Công ty Công nghệ Phương Nam','0102000030','0920000030','ncc30@gmail.com',N'TP. Hồ Chí Minh',N'Phạm Văn Khang',1);

SET IDENTITY_INSERT NhaCungCap OFF;
GO


/* =========================================================
   6. HANG HOA - 11 -> 30
   ========================================================= */
SET IDENTITY_INSERT HangHoa ON;

INSERT INTO HangHoa
(MaHang,TenHang,MaLoaiHang,MaDonViTinh,GiaThamKhao,MoTa,TrangThai)
VALUES
(11,N'iPhone 15 128GB',11,2,18500000,N'Điện thoại phục vụ công việc',1),
(12,N'iPad Gen 10 64GB',12,2,9500000,N'Máy tính bảng phục vụ công việc',1),
(13,N'Camera Hikvision 2MP',13,1,1250000,N'Camera giám sát văn phòng',1),
(14,N'Loa hội nghị Jabra Speak',14,1,3200000,N'Loa hội nghị trực tuyến',1),
(15,N'Webcam Logitech C920',15,1,1800000,N'Webcam Full HD',1),
(16,N'Cáp mạng CAT6',16,12,12000,N'Cáp mạng CAT6 tính theo mét',1),
(17,N'UPS APC 1000VA',17,1,3500000,N'Bộ lưu điện cho máy tính',1),
(18,N'Điều hòa Daikin 18000 BTU',18,2,14500000,N'Điều hòa phòng làm việc',1),
(19,N'Đèn LED Philips 20W',19,1,180000,N'Đèn chiếu sáng văn phòng',1),
(20,N'Mũ bảo hộ lao động',20,1,150000,N'Mũ bảo hộ tiêu chuẩn',1),
(21,N'Bộ tua vít kỹ thuật',21,3,450000,N'Dụng cụ sửa chữa thiết bị',1),
(22,N'Nước lau sàn 5L',22,22,180000,N'Dung dịch vệ sinh',1),
(23,N'Máy chấm công Ronald Jack',23,1,2850000,N'Máy chấm công vân tay',1),
(24,N'Micro hội nghị không dây',24,3,6800000,N'Bộ micro phòng họp',1),
(25,N'Mực in HP 107A',25,4,1350000,N'Mực máy in HP',1),
(26,N'RAM Kingston DDR4 16GB',26,1,1050000,N'RAM máy tính để bàn',1),
(27,N'Firewall TP-Link ER7206',27,1,3900000,N'Thiết bị bảo mật mạng',1),
(28,N'Tủ hồ sơ Hòa Phát',28,1,2800000,N'Tủ sắt lưu trữ hồ sơ',1),
(29,N'Xe đẩy hàng 300kg',29,1,1750000,N'Xe đẩy sử dụng trong kho',1),
(30,N'Ổ cắm điện 6 cổng',30,1,350000,N'Ổ cắm điện chống quá tải',1);

SET IDENTITY_INSERT HangHoa OFF;
GO


/* =========================================================
   7. NHA CUNG CAP HANG HOA
   ========================================================= */
SET IDENTITY_INSERT NhaCungCapHangHoa ON;

INSERT INTO NhaCungCapHangHoa
(Id,MaNhaCungCap,MaHang,DonGiaBao,NgayCapNhatGia,ThoiGianGiaoDuKien,TrangThai)
VALUES
(11,11,11,18000000,'2026-09-11',3,1),
(12,12,12,9200000,'2026-09-12',4,1),
(13,13,13,1200000,'2026-09-13',2,1),
(14,14,14,3100000,'2026-09-14',3,1),
(15,15,15,1700000,'2026-09-15',2,1),
(16,16,16,11000,'2026-09-16',2,1),
(17,17,17,3350000,'2026-09-17',4,1),
(18,18,18,14000000,'2026-09-18',5,1),
(19,19,19,165000,'2026-09-19',2,1),
(20,20,20,140000,'2026-09-20',2,1),
(21,21,21,420000,'2026-09-21',3,1),
(22,22,22,165000,'2026-09-22',2,1),
(23,23,23,2700000,'2026-09-23',4,1),
(24,24,24,6500000,'2026-09-24',5,1),
(25,25,25,1280000,'2026-09-25',3,1),
(26,26,26,980000,'2026-09-26',2,1),
(27,27,27,3750000,'2026-09-27',4,1),
(28,28,28,2650000,'2026-09-28',5,1),
(29,29,29,1650000,'2026-09-28',3,1),
(30,30,30,320000,'2026-09-28',2,1);

SET IDENTITY_INSERT NhaCungCapHangHoa OFF;
GO


/* =========================================================
   8. YEU CAU MUA HANG
   ========================================================= */
SET IDENTITY_INSERT YeuCauMuaHang ON;

INSERT INTO YeuCauMuaHang
(MaYeuCau,MaBoPhan,NgayYeuCau,NgayCanHang,MucDoUuTien,LyDoMua,
 NguoiLap,TrangThai,NgayDuyet,NguoiDuyet,LyDoTuChoiHuy)
VALUES
(11,11,'2026-09-11','2026-09-20',N'BinhThuong',N'Trang bị điện thoại công việc',11,N'DaDuyet','2026-09-12',20,NULL),
(12,12,'2026-09-12','2026-09-21',N'Cao',N'Trang bị máy tính bảng nghiên cứu',12,N'DaDuyet','2026-09-13',20,NULL),
(13,13,'2026-09-13','2026-09-22',N'Cao',N'Lắp camera giám sát',13,N'DaDuyet','2026-09-14',20,NULL),
(14,14,'2026-09-14','2026-09-23',N'BinhThuong',N'Mua loa hội nghị',14,N'DaDuyet','2026-09-15',20,NULL),
(15,15,'2026-09-15','2026-09-24',N'BinhThuong',N'Mua webcam làm việc',16,N'DaDuyet','2026-09-16',20,NULL),
(16,16,'2026-09-16','2026-09-25',N'Cao',N'Thi công hệ thống mạng',18,N'DaDuyet','2026-09-17',20,NULL),
(17,17,'2026-09-17','2026-09-26',N'Cao',N'Mua bộ lưu điện',21,N'DaDuyet','2026-09-18',20,NULL),
(18,18,'2026-09-18','2026-09-27',N'BinhThuong',N'Trang bị điều hòa',24,N'DaDuyet','2026-09-19',20,NULL),
(19,19,'2026-09-19','2026-09-28',N'Thap',N'Thay đèn văn phòng',26,N'DaDuyet','2026-09-20',27,NULL),
(20,20,'2026-09-20','2026-09-29',N'Cao',N'Mua thiết bị bảo hộ',30,N'DaDuyet','2026-09-21',27,NULL),
(21,21,'2026-09-21','2026-09-30',N'BinhThuong',N'Mua dụng cụ sửa chữa',11,N'DaDuyet','2026-09-22',27,NULL),
(22,22,'2026-09-22','2026-10-01',N'Thap',N'Mua vật tư vệ sinh',12,N'DaDuyet','2026-09-23',27,NULL),
(23,23,'2026-09-23','2026-10-02',N'Cao',N'Trang bị máy chấm công',13,N'DaDuyet','2026-09-24',20,NULL),
(24,24,'2026-09-24','2026-10-03',N'Cao',N'Nâng cấp phòng hội nghị',14,N'DaDuyet','2026-09-25',20,NULL),
(25,25,'2026-09-25','2026-10-04',N'BinhThuong',N'Mua mực máy in',16,N'DaDuyet','2026-09-26',20,NULL),
(26,26,'2026-09-26','2026-10-05',N'Cao',N'Nâng cấp RAM máy tính',18,N'DaDuyet','2026-09-27',27,NULL),
(27,27,'2026-09-27','2026-10-06',N'Cao',N'Nâng cấp bảo mật mạng',21,N'DaDuyet','2026-09-28',27,NULL),
(28,28,'2026-09-28','2026-10-07',N'BinhThuong',N'Mua tủ lưu trữ hồ sơ',24,N'DaDuyet','2026-09-28',27,NULL),
(29,29,'2026-09-28','2026-10-08',N'BinhThuong',N'Mua xe đẩy hàng',26,N'DaDuyet','2026-09-28',27,NULL),
(30,30,'2026-09-28','2026-10-09',N'Thap',N'Mua ổ cắm điện văn phòng',30,N'DaDuyet','2026-09-28',27,NULL);

SET IDENTITY_INSERT YeuCauMuaHang OFF;
GO


/* =========================================================
   9. CHI TIET YEU CAU
   Mỗi yêu cầu 1 mặt hàng để dữ liệu dễ kiểm thử.
   ========================================================= */
SET IDENTITY_INSERT ChiTietYeuCau ON;

INSERT INTO ChiTietYeuCau
(MaChiTietYeuCau,MaYeuCau,MaHang,SoLuongYeuCau,SoLuongDuyet,GhiChu)
VALUES
(11,11,11,2,2,N'Điện thoại 128GB'),
(12,12,12,3,3,N'Máy tính bảng'),
(13,13,13,8,8,N'Camera giám sát'),
(14,14,14,2,2,N'Loa hội nghị'),
(15,15,15,5,5,N'Webcam Full HD'),
(16,16,16,300,300,N'Cáp mạng 300 mét'),
(17,17,17,4,4,N'UPS cho máy tính'),
(18,18,18,2,2,N'Điều hòa phòng làm việc'),
(19,19,19,30,30,N'Đèn LED'),
(20,20,20,20,20,N'Mũ bảo hộ'),
(21,21,21,5,5,N'Bộ dụng cụ'),
(22,22,22,10,10,N'Nước lau sàn'),
(23,23,23,2,2,N'Máy chấm công'),
(24,24,24,2,2,N'Micro hội nghị'),
(25,25,25,6,6,N'Mực in'),
(26,26,26,10,10,N'RAM 16GB'),
(27,27,27,2,2,N'Firewall'),
(28,28,28,5,5,N'Tủ hồ sơ'),
(29,29,29,4,4,N'Xe đẩy kho'),
(30,30,30,20,20,N'Ổ cắm điện');

SET IDENTITY_INSERT ChiTietYeuCau OFF;
GO


/* =========================================================
   10. DON MUA HANG
   ========================================================= */
SET IDENTITY_INSERT DonMuaHang ON;

INSERT INTO DonMuaHang
(MaDonMua,MaYeuCau,MaNhaCungCap,NgayDat,TrangThai,TongTien,GhiChu,NguoiLap)
VALUES
(11,11,11,'2026-09-13',N'DaDat',36000000,N'Mua điện thoại',15),
(12,12,12,'2026-09-14',N'DaDat',27600000,N'Mua máy tính bảng',15),
(13,13,13,'2026-09-15',N'DaDat',9600000,N'Mua camera',15),
(14,14,14,'2026-09-16',N'DaDat',6200000,N'Mua loa hội nghị',15),
(15,15,15,'2026-09-17',N'DaDat',8500000,N'Mua webcam',15),
(16,16,16,'2026-09-18',N'DaDat',3300000,N'Mua cáp mạng',22),
(17,17,17,'2026-09-19',N'DaDat',13400000,N'Mua UPS',22),
(18,18,18,'2026-09-20',N'DaDat',28000000,N'Mua điều hòa',22),
(19,19,19,'2026-09-21',N'DaDat',4950000,N'Mua đèn LED',22),
(20,20,20,'2026-09-22',N'DaDat',2800000,N'Mua bảo hộ',22),
(21,21,21,'2026-09-23',N'DaDat',2100000,N'Mua dụng cụ',28),
(22,22,22,'2026-09-24',N'DaDat',1650000,N'Mua vật tư vệ sinh',28),
(23,23,23,'2026-09-25',N'DaDat',5400000,N'Mua máy chấm công',28),
(24,24,24,'2026-09-26',N'DaDat',13000000,N'Mua micro hội nghị',28),
(25,25,25,'2026-09-27',N'DaDat',7680000,N'Mua mực in',28),
(26,26,26,'2026-09-28',N'DaDat',9800000,N'Mua RAM',15),
(27,27,27,'2026-09-28',N'DaDat',7500000,N'Mua firewall',15),
(28,28,28,'2026-09-28',N'DaDat',13250000,N'Mua tủ hồ sơ',22),
(29,29,29,'2026-09-28',N'DaDat',6600000,N'Mua xe đẩy',22),
(30,30,30,'2026-09-28',N'DaDat',6400000,N'Mua ổ cắm',28);

SET IDENTITY_INSERT DonMuaHang OFF;
GO


/* =========================================================
   11. CHI TIET DON MUA
   ========================================================= */
SET IDENTITY_INSERT ChiTietDonMua ON;

INSERT INTO ChiTietDonMua
(MaChiTietDon,MaDonMua,MaChiTietYeuCau,MaHang,SoLuongDat,DonGiaMua)
VALUES
(11,11,11,11,2,18000000),
(12,12,12,12,3,9200000),
(13,13,13,13,8,1200000),
(14,14,14,14,2,3100000),
(15,15,15,15,5,1700000),
(16,16,16,16,300,11000),
(17,17,17,17,4,3350000),
(18,18,18,18,2,14000000),
(19,19,19,19,30,165000),
(20,20,20,20,20,140000),
(21,21,21,21,5,420000),
(22,22,22,22,10,165000),
(23,23,23,23,2,2700000),
(24,24,24,24,2,6500000),
(25,25,25,25,6,1280000),
(26,26,26,26,10,980000),
(27,27,27,27,2,3750000),
(28,28,28,28,5,2650000),
(29,29,29,29,4,1650000),
(30,30,30,30,20,320000);

SET IDENTITY_INSERT ChiTietDonMua OFF;
GO


/* =========================================================
   12. LAN GIAO HANG
   ========================================================= */
SET IDENTITY_INSERT LanGiaoHang ON;

INSERT INTO LanGiaoHang
(MaLanGiao,MaDonMua,NgayGiao,NguoiNhan,SoChungTu,GhiChu,TrangThai)
VALUES
(11,11,'2026-09-16',17,'PG011',N'Đã nhận đủ hàng',1),
(12,12,'2026-09-17',17,'PG012',N'Đã nhận đủ hàng',1),
(13,13,'2026-09-18',17,'PG013',N'Đã kiểm tra camera',1),
(14,14,'2026-09-19',17,'PG014',N'Loa hoạt động tốt',1),
(15,15,'2026-09-20',17,'PG015',N'Webcam nguyên hộp',1),
(16,16,'2026-09-21',23,'PG016',N'Đã nhận cáp mạng',1),
(17,17,'2026-09-22',23,'PG017',N'UPS hoạt động tốt',1),
(18,18,'2026-09-23',23,'PG018',N'Đã nhận điều hòa',1),
(19,19,'2026-09-24',23,'PG019',N'Đã nhận đèn',1),
(20,20,'2026-09-25',23,'PG020',N'Đã nhận đồ bảo hộ',1),
(21,21,'2026-09-26',29,'PG021',N'Đủ dụng cụ',1),
(22,22,'2026-09-27',29,'PG022',N'Đủ vật tư',1),
(23,23,'2026-09-28',29,'PG023',N'Máy hoạt động tốt',1),
(24,24,'2026-09-28',29,'PG024',N'Micro hoạt động tốt',1),
(25,25,'2026-09-28',29,'PG025',N'Đủ mực in',1),
(26,26,'2026-09-28',17,'PG026',N'RAM nguyên hộp',1),
(27,27,'2026-09-28',17,'PG027',N'Firewall nguyên hộp',1),
(28,28,'2026-09-28',23,'PG028',N'Tủ không hư hỏng',1),
(29,29,'2026-09-28',23,'PG029',N'Xe đẩy hoạt động tốt',1),
(30,30,'2026-09-28',29,'PG030',N'Ổ cắm nguyên hộp',1);

SET IDENTITY_INSERT LanGiaoHang OFF;
GO


/* =========================================================
   13. CHI TIET GIAO HANG
   ========================================================= */
SET IDENTITY_INSERT ChiTietGiaoHang ON;

INSERT INTO ChiTietGiaoHang
(MaChiTietGiao,MaLanGiao,MaChiTietDon,SoLuongNhan,SoLuongDatChatLuong,GhiChu)
VALUES
(11,11,11,2,2,N'Đạt chất lượng'),
(12,12,12,3,3,N'Đạt chất lượng'),
(13,13,13,8,8,N'Đạt chất lượng'),
(14,14,14,2,2,N'Đạt chất lượng'),
(15,15,15,5,5,N'Đạt chất lượng'),
(16,16,16,300,300,N'Đủ chiều dài'),
(17,17,17,4,4,N'Đạt chất lượng'),
(18,18,18,2,2,N'Đạt chất lượng'),
(19,19,19,30,30,N'Đạt chất lượng'),
(20,20,20,20,20,N'Đạt chất lượng'),
(21,21,21,5,5,N'Đạt chất lượng'),
(22,22,22,10,10,N'Đạt chất lượng'),
(23,23,23,2,2,N'Đạt chất lượng'),
(24,24,24,2,2,N'Đạt chất lượng'),
(25,25,25,6,6,N'Đạt chất lượng'),
(26,26,26,10,10,N'Đạt chất lượng'),
(27,27,27,2,2,N'Đạt chất lượng'),
(28,28,28,5,5,N'Đạt chất lượng'),
(29,29,29,4,4,N'Đạt chất lượng'),
(30,30,30,20,20,N'Đạt chất lượng');

SET IDENTITY_INSERT ChiTietGiaoHang OFF;
GO


/* =========================================================
   14. THANH TOAN DON MUA
   ========================================================= */
SET IDENTITY_INSERT ThanhToanDonMua ON;

INSERT INTO ThanhToanDonMua
(MaThanhToan,MaDonMua,NgayThanhToan,SoTienThanhToan,
 PhuongThuc,NguoiThucHien,GhiChu)
VALUES
(11,11,'2026-09-18',36000000,N'ChuyenKhoan',19,N'Đã thanh toán'),
(12,12,'2026-09-19',27600000,N'ChuyenKhoan',19,N'Đã thanh toán'),
(13,13,'2026-09-20',9600000,N'ChuyenKhoan',19,N'Đã thanh toán'),
(14,14,'2026-09-21',6200000,N'ChuyenKhoan',19,N'Đã thanh toán'),
(15,15,'2026-09-22',8500000,N'ChuyenKhoan',19,N'Đã thanh toán'),
(16,16,'2026-09-23',3300000,N'TienMat',19,N'Đã thanh toán'),
(17,17,'2026-09-24',13400000,N'ChuyenKhoan',19,N'Đã thanh toán'),
(18,18,'2026-09-25',28000000,N'ChuyenKhoan',19,N'Đã thanh toán'),
(19,19,'2026-09-26',4950000,N'TienMat',19,N'Đã thanh toán'),
(20,20,'2026-09-27',2800000,N'ChuyenKhoan',19,N'Đã thanh toán'),
(21,21,'2026-09-28',2100000,N'TienMat',25,N'Đã thanh toán'),
(22,22,'2026-09-28',1650000,N'TienMat',25,N'Đã thanh toán'),
(23,23,'2026-09-28',5400000,N'ChuyenKhoan',25,N'Đã thanh toán'),
(24,24,'2026-09-28',13000000,N'ChuyenKhoan',25,N'Đã thanh toán'),
(25,25,'2026-09-28',7680000,N'ChuyenKhoan',25,N'Đã thanh toán'),
(26,26,'2026-09-28',9800000,N'ChuyenKhoan',25,N'Đã thanh toán'),
(27,27,'2026-09-28',7500000,N'ChuyenKhoan',25,N'Đã thanh toán'),
(28,28,'2026-09-28',13250000,N'ChuyenKhoan',25,N'Đã thanh toán'),
(29,29,'2026-09-28',6600000,N'ChuyenKhoan',25,N'Đã thanh toán'),
(30,30,'2026-09-28',6400000,N'ChuyenKhoan',25,N'Đã thanh toán');

SET IDENTITY_INSERT ThanhToanDonMua OFF;
GO


/* =========================================================
   15. LICH SU TRANG THAI
   ========================================================= */
SET IDENTITY_INSERT LichSuTrangThai ON;

INSERT INTO LichSuTrangThai
(Id,LoaiDoiTuong,MaDoiTuong,TrangThaiCu,TrangThaiMoi,
 NguoiThucHien,ThoiGian,GhiChu)
VALUES
(11,N'YeuCauMuaHang',11,N'Nhap',N'DaDuyet',20,'2026-09-12 09:00',N'Đã duyệt yêu cầu'),
(12,N'YeuCauMuaHang',12,N'Nhap',N'DaDuyet',20,'2026-09-13 09:00',N'Đã duyệt yêu cầu'),
(13,N'YeuCauMuaHang',13,N'Nhap',N'DaDuyet',20,'2026-09-14 09:00',N'Đã duyệt yêu cầu'),
(14,N'YeuCauMuaHang',14,N'Nhap',N'DaDuyet',20,'2026-09-15 09:00',N'Đã duyệt yêu cầu'),
(15,N'YeuCauMuaHang',15,N'Nhap',N'DaDuyet',20,'2026-09-16 09:00',N'Đã duyệt yêu cầu'),
(16,N'DonMuaHang',16,N'ChoDuyet',N'DaDat',22,'2026-09-18 14:00',N'Đã đặt hàng'),
(17,N'DonMuaHang',17,N'ChoDuyet',N'DaDat',22,'2026-09-19 14:00',N'Đã đặt hàng'),
(18,N'DonMuaHang',18,N'ChoDuyet',N'DaDat',22,'2026-09-20 14:00',N'Đã đặt hàng'),
(19,N'DonMuaHang',19,N'ChoDuyet',N'DaDat',22,'2026-09-21 14:00',N'Đã đặt hàng'),
(20,N'DonMuaHang',20,N'ChoDuyet',N'DaDat',22,'2026-09-22 14:00',N'Đã đặt hàng'),
(21,N'DonMuaHang',21,N'ChoDuyet',N'DaDat',28,'2026-09-23 14:00',N'Đã đặt hàng'),
(22,N'DonMuaHang',22,N'ChoDuyet',N'DaDat',28,'2026-09-24 14:00',N'Đã đặt hàng'),
(23,N'DonMuaHang',23,N'ChoDuyet',N'DaDat',28,'2026-09-25 14:00',N'Đã đặt hàng'),
(24,N'DonMuaHang',24,N'ChoDuyet',N'DaDat',28,'2026-09-26 14:00',N'Đã đặt hàng'),
(25,N'DonMuaHang',25,N'ChoDuyet',N'DaDat',28,'2026-09-27 14:00',N'Đã đặt hàng'),
(26,N'DonMuaHang',26,N'ChoDuyet',N'DaDat',15,'2026-09-28 10:00',N'Đã đặt hàng'),
(27,N'DonMuaHang',27,N'ChoDuyet',N'DaDat',15,'2026-09-28 11:00',N'Đã đặt hàng'),
(28,N'DonMuaHang',28,N'ChoDuyet',N'DaDat',22,'2026-09-28 12:00',N'Đã đặt hàng'),
(29,N'DonMuaHang',29,N'ChoDuyet',N'DaDat',22,'2026-09-28 13:00',N'Đã đặt hàng'),
(30,N'DonMuaHang',30,N'ChoDuyet',N'DaDat',28,'2026-09-28 14:00',N'Đã đặt hàng');

SET IDENTITY_INSERT LichSuTrangThai OFF;
GO


/* =========================================================
   KIEM TRA TAT CA 15 BANG
   ========================================================= */

SELECT N'BoPhanDeNghi' AS TenBang, COUNT(*) AS SoLuong FROM BoPhanDeNghi
UNION ALL SELECT N'LoaiHang', COUNT(*) FROM LoaiHang
UNION ALL SELECT N'DonViTinh', COUNT(*) FROM DonViTinh
UNION ALL SELECT N'TaiKhoan', COUNT(*) FROM TaiKhoan
UNION ALL SELECT N'NhaCungCap', COUNT(*) FROM NhaCungCap
UNION ALL SELECT N'HangHoa', COUNT(*) FROM HangHoa
UNION ALL SELECT N'NhaCungCapHangHoa', COUNT(*) FROM NhaCungCapHangHoa
UNION ALL SELECT N'YeuCauMuaHang', COUNT(*) FROM YeuCauMuaHang
UNION ALL SELECT N'ChiTietYeuCau', COUNT(*) FROM ChiTietYeuCau
UNION ALL SELECT N'DonMuaHang', COUNT(*) FROM DonMuaHang
UNION ALL SELECT N'ChiTietDonMua', COUNT(*) FROM ChiTietDonMua
UNION ALL SELECT N'LanGiaoHang', COUNT(*) FROM LanGiaoHang
UNION ALL SELECT N'ChiTietGiaoHang', COUNT(*) FROM ChiTietGiaoHang
UNION ALL SELECT N'ThanhToanDonMua', COUNT(*) FROM ThanhToanDonMua
UNION ALL SELECT N'LichSuTrangThai', COUNT(*) FROM LichSuTrangThai
ORDER BY TenBang;
GO