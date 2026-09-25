USE QLMuaHangVaNCC;
GO


-- =====================================
-- TAI KHOAN
-- =====================================

INSERT INTO TaiKhoan
(TenDangNhap, MatKhau, HoTen, Email, VaiTro, TrangThai)
VALUES
('admin','123456','Nguyễn Văn Minh','minh.nguyen@qlmuahang.vn','Admin',1),
('nvmuahang01','123456','Trần Quốc Anh','anh.tran@qlmuahang.vn','NhanVienMuaHang',1),
('nvmuahang02','123456','Lê Hoàng Nam','nam.le@qlmuahang.vn','NhanVienMuaHang',1),
('nvdeghi01','123456','Phạm Thu Hà','ha.pham@qlmuahang.vn','NhanVienDeNghi',1),
('nvdeghi02','123456','Nguyễn Thị Lan','lan.nguyen@qlmuahang.vn','NhanVienDeNghi',1),
('ketoan01','123456','Vũ Minh Tuấn','tuan.vu@qlmuahang.vn','KeToan',1),
('kho01','123456','Đỗ Văn Hùng','hung.do@qlmuahang.vn','NhanVienKho',1),
('giamdoc','123456','Hoàng Đức Long','long.hoang@qlmuahang.vn','GiamDoc',1),
('nvdeghi03','123456','Bùi Ngọc Anh','anh.bui@qlmuahang.vn','NhanVienDeNghi',1),
('ketoan02','123456','Đặng Thanh Bình','binh.dang@qlmuahang.vn','KeToan',1);



-- =====================================
-- BO PHAN DE NGHI
-- =====================================

INSERT INTO BoPhanDeNghi
(TenBoPhan,NguoiPhuTrach,SoDienThoai,MoTa,TrangThai)
VALUES
(N'Phòng Công nghệ thông tin',N'Nguyễn Văn Minh','0901234567',N'Quản lý hệ thống CNTT',1),
(N'Phòng Kinh doanh',N'Trần Quốc Anh','0912345678',N'Bộ phận kinh doanh',1),
(N'Phòng Kế toán',N'Lê Hoàng Nam','0923456789',N'Quản lý tài chính',1),
(N'Phòng Nhân sự',N'Phạm Thu Hà','0934567890',N'Quản lý nhân sự',1),
(N'Phòng Hành chính',N'Nguyễn Thị Lan','0945678901',N'Hành chính văn phòng',1),
(N'Phòng Marketing',N'Vũ Minh Tuấn','0956789012',N'Truyền thông quảng bá',1),
(N'Kho vận',N'Đỗ Văn Hùng','0967890123',N'Quản lý kho hàng',1),
(N'Ban Giám Đốc',N'Hoàng Đức Long','0978901234',N'Điều hành công ty',1),
(N'Phòng Dự án',N'Bùi Ngọc Anh','0989012345',N'Triển khai dự án',1),
(N'Phòng Kỹ thuật',N'Đặng Thanh Bình','0990123456',N'Hỗ trợ kỹ thuật',1);



-- =====================================
-- LOAI HANG
-- =====================================

INSERT INTO LoaiHang
(TenLoaiHang,MoTa,TrangThai)
VALUES
(N'Thiết bị văn phòng',N'Máy in, máy scan, thiết bị văn phòng',1),
(N'Máy tính',N'Linh kiện và máy tính',1),
(N'Phụ kiện điện tử',N'Chuột, bàn phím, tai nghe',1),
(N'Vật tư tiêu hao',N'Giấy, mực in',1),
(N'Thiết bị mạng',N'Router, switch, wifi',1),
(N'Nội thất văn phòng',N'Bàn ghế tủ',1),
(N'Dụng cụ bảo hộ',N'Đồ bảo hộ lao động',1),
(N'Điện dân dụng',N'Thiết bị điện',1),
(N'Phần mềm',N'Bản quyền phần mềm',1),
(N'Thiết bị kho',N'Máy móc kho vận',1);



-- =====================================
-- DON VI TINH
-- =====================================

INSERT INTO DonViTinh
(TenDonViTinh,MoTa,TrangThai)
VALUES
(N'Cái',N'Đơn vị tính theo chiếc',1),
(N'Bộ',N'Đơn vị theo bộ sản phẩm',1),
(N'Hộp',N'Đóng gói theo hộp',1),
(N'Ram',N'Giấy in',1),
(N'Chiếc',N'Thiết bị đơn chiếc',1),
(N'Cuộn',N'Vật tư dạng cuộn',1),
(N'Thùng',N'Đóng gói số lượng lớn',1),
(N'Kg',N'Tính theo khối lượng',1),
(N'License',N'Bản quyền phần mềm',1),
(N'Combo',N'Bộ sản phẩm kết hợp',1);

-- =====================================
-- HANG HOA
-- =====================================

INSERT INTO HangHoa
(TenHang,MaLoaiHang,MaDonViTinh,GiaThamKhao,MoTa,TrangThai)
VALUES
(N'Máy in Canon LBP2900',1,1,3500000,N'Máy in laser văn phòng',1),
(N'Máy scan Epson DS-410',1,1,7200000,N'Máy scan tài liệu tốc độ cao',1),
(N'Laptop Dell Latitude 5440',2,1,18500000,N'Laptop doanh nghiệp',1),
(N'Màn hình Samsung 24 inch',2,1,3200000,N'Màn hình LED Full HD',1),
(N'Chuột Logitech M331',3,1,450000,N'Chuột không dây',1),
(N'Bàn phím Logitech K120',3,1,250000,N'Bàn phím USB',1),
(N'Giấy in A4 Double A',4,3,75000,N'Giấy in văn phòng 500 tờ',1),
(N'Mực in HP 12A',4,1,950000,N'Hộp mực máy in laser',1),
(N'Router Wifi TP-Link AX1500',5,1,1200000,N'Thiết bị mạng không dây',1),
(N'Switch Cisco 24 Port',5,1,6500000,N'Switch mạng doanh nghiệp',1),
(N'Bàn làm việc nhân viên',6,1,2500000,N'Bàn gỗ văn phòng',1),
(N'Ghế xoay văn phòng',6,1,1800000,N'Ghế có tựa lưng',1),
(N'Camera giám sát Hikvision',5,1,2200000,N'Camera IP',1),
(N'Phần mềm Microsoft Office',9,9,2500000,N'License Office bản quyền',1),
(N'Xe đẩy hàng inox',10,1,4500000,N'Dụng cụ vận chuyển kho',1);



-- =====================================
-- NHA CUNG CAP
-- =====================================

INSERT INTO NhaCungCap
(TenNhaCungCap,MaSoThue,SoDienThoai,Email,DiaChi,NguoiLienHe,TrangThai)
VALUES
(N'Công ty TNHH Công Nghệ Sao Việt','0101234567','0901111111',
'saoviet@gmail.com',N'Hà Nội',N'Nguyễn Văn An',1),

(N'Công ty CP Thiết Bị Văn Phòng Minh Long','0102345678','0912222222',
'minhlong@gmail.com',N'Hà Nội',N'Trần Minh Long',1),

(N'Công ty TNHH Phân Phối Thành Công','0103456789','0923333333',
'thanhcong@gmail.com',N'Hồ Chí Minh',N'Lê Thành Công',1),

(N'Công ty CP Máy Tính Hoàng Gia','0104567890','0934444444',
'hoanggia@gmail.com',N'Đà Nẵng',N'Phạm Hoàng Gia',1),

(N'Công ty TNHH Công Nghệ Nhật Nam','0105678901','0945555555',
'nhatnam@gmail.com',N'Hải Phòng',N'Nguyễn Nhật Nam',1),

(N'Công ty CP Thiết Bị Số Việt Anh','0106789012','0956666666',
'vietanh@gmail.com',N'Hà Nội',N'Đỗ Việt Anh',1),

(N'Công ty TNHH Giải Pháp Mạng Đông Á','0107890123','0967777777',
'donga@gmail.com',N'Hồ Chí Minh',N'Vũ Đông Á',1),

(N'Công ty CP Nội Thất Thành Đạt','0108901234','0978888888',
'thanhdat@gmail.com',N'Bình Dương',N'Nguyễn Thành Đạt',1),

(N'Công ty TNHH Phần Mềm Việt','0109012345','0989999999',
'phanmemviet@gmail.com',N'Hà Nội',N'Bùi Văn Việt',1),

(N'Công ty CP Kho Vận An Phát','0100123456','0990000000',
'anphat@gmail.com',N'Hải Dương',N'Lê An Phát',1);



-- =====================================
-- NHA CUNG CAP HANG HOA
-- =====================================

INSERT INTO NhaCungCapHangHoa
(MaNhaCungCap,MaHang,DonGiaBao,NgayCapNhatGia,ThoiGianGiaoDuKien,TrangThai)
VALUES
(1,1,3400000,'2026-01-10',3,1),
(1,2,7000000,'2026-01-11',5,1),
(2,3,18000000,'2026-01-12',4,1),
(2,4,3000000,'2026-01-13',3,1),
(3,5,420000,'2026-01-14',2,1),
(3,6,220000,'2026-01-15',2,1),
(4,7,70000,'2026-01-16',1,1),
(5,8,900000,'2026-01-17',3,1),
(6,9,1100000,'2026-01-18',4,1),
(7,10,6200000,'2026-01-19',5,1),
(8,11,2400000,'2026-01-20',7,1),
(8,12,1700000,'2026-01-21',7,1),
(7,13,2100000,'2026-01-22',4,1),
(9,14,2400000,'2026-01-23',2,1),
(10,15,4300000,'2026-01-24',6,1);



-- =====================================
-- YEU CAU MUA HANG
-- =====================================

INSERT INTO YeuCauMuaHang
(
MaBoPhan,
NgayYeuCau,
NgayCanHang,
MucDoUuTien,
LyDoMua,
NguoiLap,
TrangThai,
NgayDuyet,
NguoiDuyet
)
VALUES
(1,'2026-02-01','2026-02-10',N'Cao',
N'Mua thêm máy tính phục vụ nhân viên mới',4,N'DaDuyet',
'2026-02-02',1),

(2,'2026-02-03','2026-02-15',N'BinhThuong',
N'Thay mới thiết bị văn phòng',4,N'DaDuyet',
'2026-02-04',1),

(3,'2026-02-05','2026-02-20',N'Thap',
N'Mua giấy in và vật tư kế toán',5,N'DaDuyet',
'2026-02-06',1),

(4,'2026-02-07','2026-02-18',N'BinhThuong',
N'Bổ sung thiết bị phòng nhân sự',5,N'DaDuyet',
'2026-02-08',1),

(5,'2026-02-10','2026-02-25',N'Cao',
N'Nâng cấp thiết bị hành chính',4,N'DaDuyet',
'2026-02-11',1),

(6,'2026-02-12','2026-02-28',N'BinhThuong',
N'Mua thiết bị marketing',9,N'DaDuyet',
'2026-02-13',1),

(7,'2026-02-15','2026-03-01',N'Cao',
N'Mua thiết bị kho mới',7,N'DaDuyet',
'2026-02-16',1),

(8,'2026-02-17','2026-03-05',N'KhanCap',
N'Thay thế thiết bị lỗi',8,N'DaDuyet',
'2026-02-18',1),

(9,'2026-02-20','2026-03-10',N'BinhThuong',
N'Triển khai dự án mới',9,N'DaDuyet',
'2026-02-21',1),

(10,'2026-02-22','2026-03-15',N'Cao',
N'Bổ sung thiết bị kỹ thuật',10,N'DaDuyet',
'2026-02-23',1);

-- =====================================
-- CHI TIET YEU CAU
-- =====================================

INSERT INTO ChiTietYeuCau
(MaYeuCau,MaHang,SoLuongYeuCau,SoLuongDuyet,GhiChu)
VALUES
(1,3,5,5,N'Cấp cho nhân viên mới'),
(1,4,5,5,N'Màn hình làm việc'),
(2,1,3,3,N'Máy in phòng kinh doanh'),
(2,5,10,10,N'Chuột máy tính'),
(3,7,20,20,N'Giấy in hàng tháng'),
(3,8,5,5,N'Mực in dự phòng'),
(4,11,8,8,N'Bàn làm việc mới'),
(4,12,8,8,N'Ghế nhân viên'),
(5,9,5,5,N'Router wifi văn phòng'),
(6,13,10,10,N'Lắp đặt camera'),
(7,15,3,3,N'Dụng cụ kho'),
(8,2,2,2,N'Máy scan tài liệu'),
(9,14,20,20,N'Bản quyền phần mềm'),
(10,10,2,2,N'Thiết bị mạng');



-- =====================================
-- DON MUA HANG
-- =====================================

INSERT INTO DonMuaHang
(
MaYeuCau,
MaNhaCungCap,
NgayDat,
TrangThai,
TongTien,
NguoiLap,
GhiChu
)
VALUES
(1,4,'2026-02-03',N'HoanThanh',107500000,2,N'Đơn mua thiết bị CNTT'),
(2,1,'2026-02-05',N'HoanThanh',14750000,2,N'Mua máy in'),
(3,2,'2026-02-07',N'HoanThanh',6250000,2,N'Vật tư văn phòng'),
(4,8,'2026-02-09',N'HoanThanh',33600000,2,N'Nội thất văn phòng'),
(5,7,'2026-02-12',N'DaGiao',5500000,2,N'Thiết bị mạng'),
(6,7,'2026-02-14',N'DaGiao',21000000,2,N'Thiết bị camera'),
(7,10,'2026-02-17',N'DaGiao',12900000,2,N'Thiết bị kho'),
(8,1,'2026-02-19',N'DaGiao',14000000,2,N'Máy scan'),
(9,9,'2026-02-22',N'DaGiao',48000000,2,N'Phần mềm'),
(10,7,'2026-02-24',N'DaGiao',12400000,2,N'Switch mạng');



-- =====================================
-- CHI TIET DON MUA
-- =====================================

INSERT INTO ChiTietDonMua
(
MaDonMua,
MaChiTietYeuCau,
MaHang,
SoLuongDat,
DonGiaMua
)
VALUES
(1,1,3,5,18000000),
(1,2,4,5,3000000),

(2,3,1,3,3400000),
(2,4,5,10,420000),

(3,5,7,20,70000),
(3,6,8,5,900000),

(4,7,11,8,2400000),
(4,8,12,8,1700000),

(5,9,9,5,1100000),

(6,10,13,10,2100000),

(7,11,15,3,4300000),

(8,12,2,2,7000000),

(9,13,14,20,2400000),

(10,14,10,2,6200000);



-- =====================================
-- LAN GIAO HANG
-- =====================================

INSERT INTO LanGiaoHang
(
MaDonMua,
NgayGiao,
NguoiNhan,
SoChungTu,
GhiChu,
TrangThai
)
VALUES
(1,'2026-02-15',7,'GH001',N'Giao đủ hàng',1),
(2,'2026-02-18',7,'GH002',N'Giao đủ hàng',1),
(3,'2026-02-20',7,'GH003',N'Giao đủ vật tư',1),
(4,'2026-02-22',7,'GH004',N'Bàn ghế mới',1),
(5,'2026-02-25',7,'GH005',N'Thiết bị mạng',1),
(6,'2026-02-27',7,'GH006',N'Lắp camera',1),
(7,'2026-03-01',7,'GH007',N'Nhập kho',1),
(8,'2026-03-03',7,'GH008',N'Máy scan',1),
(9,'2026-03-08',7,'GH009',N'Cấp license',1),
(10,'2026-03-12',7,'GH010',N'Switch mạng',1);



-- =====================================
-- CHI TIET GIAO HANG
-- =====================================

INSERT INTO ChiTietGiaoHang
(
MaLanGiao,
MaChiTietDon,
SoLuongNhan,
SoLuongDatChatLuong,
GhiChu
)
VALUES
(1,1,5,5,N'Đạt yêu cầu'),
(1,2,5,5,N'Đạt yêu cầu'),

(2,3,3,3,N'Hoạt động tốt'),
(2,4,10,10,N'Đủ số lượng'),

(3,5,20,20,N'Giấy đúng loại'),
(3,6,5,5,N'Mực chính hãng'),

(4,7,8,8,N'Nội thất mới'),
(4,8,8,8,N'Không lỗi'),

(5,9,5,5,N'Test mạng OK'),

(6,10,10,10,N'Lắp đặt hoàn thành'),

(7,11,3,3,N'Nhập kho'),

(8,12,2,2,N'Máy scan hoạt động'),

(9,13,20,20,N'Cấp tài khoản'),

(10,14,2,2,N'Switch hoạt động');



-- =====================================
-- THANH TOAN DON MUA
-- =====================================

INSERT INTO ThanhToanDonMua
(
MaDonMua,
NgayThanhToan,
SoTienThanhToan,
PhuongThuc,
NguoiThucHien,
GhiChu
)
VALUES
(1,'2026-02-20',107500000,N'ChuyenKhoan',6,N'Đã thanh toán'),
(2,'2026-02-22',14750000,N'ChuyenKhoan',6,N'Đã thanh toán'),
(3,'2026-02-24',6250000,N'TienMat',6,N'Đã thanh toán'),
(4,'2026-02-26',33600000,N'ChuyenKhoan',6,N'Đã thanh toán'),
(5,'2026-03-01',5500000,N'ChuyenKhoan',6,N'Đã thanh toán'),
(6,'2026-03-03',21000000,N'ChuyenKhoan',6,N'Đã thanh toán'),
(7,'2026-03-05',12900000,N'TienMat',6,N'Đã thanh toán'),
(8,'2026-03-07',14000000,N'ChuyenKhoan',6,N'Đã thanh toán'),
(9,'2026-03-10',48000000,N'ChuyenKhoan',6,N'Đã thanh toán'),
(10,'2026-03-14',12400000,N'ChuyenKhoan',6,N'Đã thanh toán');

-- =====================================
-- LICH SU TRANG THAI
-- =====================================

INSERT INTO LichSuTrangThai
(
LoaiDoiTuong,
MaDoiTuong,
TrangThaiCu,
TrangThaiMoi,
NguoiThucHien,
ThoiGian,
GhiChu
)
VALUES

-- YeuCauMuaHang

('YeuCauMuaHang',1,NULL,N'ChoDuyet',4,'2026-02-01 08:30:00',N'Tạo yêu cầu mua hàng'),

('YeuCauMuaHang',1,N'ChoDuyet',N'DaDuyet',1,'2026-02-02 09:00:00',N'Giám đốc phê duyệt'),

('YeuCauMuaHang',2,NULL,N'ChoDuyet',4,'2026-02-03 10:00:00',N'Tạo yêu cầu mua hàng'),

('YeuCauMuaHang',2,N'ChoDuyet',N'DaDuyet',1,'2026-02-04 09:30:00',N'Đã duyệt mua'),


-- DonMuaHang

('DonMuaHang',1,NULL,N'DangXuLy',2,'2026-02-03 11:00:00',N'Tạo đơn mua hàng'),

('DonMuaHang',1,N'DangXuLy',N'HoanThanh',2,'2026-02-15 15:00:00',N'Hoàn tất giao hàng'),


('DonMuaHang',2,NULL,N'DangXuLy',2,'2026-02-05 13:00:00',N'Tạo đơn mua hàng'),

('DonMuaHang',2,N'DangXuLy',N'HoanThanh',2,'2026-02-18 16:00:00',N'Nhận đủ hàng'),


-- ThanhToanDonMua

('ThanhToanDonMua',1,NULL,N'DaThanhToan',6,'2026-02-20 10:00:00',N'Thanh toán chuyển khoản'),

('ThanhToanDonMua',2,NULL,N'DaThanhToan',6,'2026-02-22 10:30:00',N'Thanh toán hoàn tất');
GO