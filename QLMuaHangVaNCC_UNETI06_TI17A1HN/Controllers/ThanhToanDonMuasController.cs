using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;


// Họ và tên: Trương Huy Đồng
// Mã sinh viên: 23103100036
// Nội dung thực hiện: Module 5 - Quản Lý Thanh Toán & Công Nợ
namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Controllers
{
    public class DonMuaThanhToanVM
    {
        public int MaDonMua { get; set; }
        public string TenNhaCungCap { get; set; } = string.Empty;
        public DateTime NgayDat { get; set; }
        public string TrangThaiDon { get; set; } = string.Empty;
        public decimal TongTienDon { get; set; }
        public decimal TongDaThanhToan { get; set; }
        public decimal SoTienConLai => TongTienDon - TongDaThanhToan;
        public string TrangThaiThanhToan =>
            TongDaThanhToan <= 0 ? "Chưa thanh toán" :
            (TongTienDon > TongDaThanhToan ? "Thanh toán một phần" : "Đã thanh toán");
    }

    public class ThanhToanDonMuasController : Controller
    {
        private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

        public ThanhToanDonMuasController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
        {
            _context = context;
        }

        // Kiểm tra phân quyền tại Controller
        private bool KiemTraQuyen()
        {
            // ĐÃ TẮT ĐOẠN GỌI SESSION ĐỂ KHÔNG BỊ LỖI VIEW TRÊN TRÌNH DUYỆT
            /*
            var vaiTro = HttpContext.Session.GetString("VaiTro");
            if (string.IsNullOrEmpty(vaiTro)) return true;
            return vaiTro == "Admin" || vaiTro == "QuanLyMuaHang" || vaiTro == "Quản lý mua hàng";
            */
            return true;
        }

        // GET: ThanhToanDonMuas(Danh sách + Tìm kiếm + Lọc theo phương thức, trạng thái, ngày)
        public IActionResult Index(string? searchString, string? phuongThuc, string? trangThaiTT, DateTime? tuNgay, DateTime? denNgay)
        {
            if (!KiemTraQuyen())
            {
                return StatusCode(403, "Bạn không có quyền truy cập chức năng Quản lý Thanh toán!");
            }

            var query = _context.ThanhToanDonMua
                .Include(t => t.NguoiThucHienNavigation)
                .Include(t => t.DonMuaHang)
                    .ThenInclude(d => d.NhaCungCap)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(t =>
                    t.MaDonMua.ToString().Contains(searchString) ||
                    (t.NguoiThucHienNavigation != null && t.NguoiThucHienNavigation.HoTen.Contains(searchString)) ||
                    (t.DonMuaHang != null && t.DonMuaHang.NhaCungCap != null && t.DonMuaHang.NhaCungCap.TenNhaCungCap.Contains(searchString)));
            }

            if (!string.IsNullOrWhiteSpace(phuongThuc))
            {
                query = query.Where(t => t.PhuongThuc == phuongThuc);
            }

            if (tuNgay.HasValue)
            {
                query = query.Where(t => t.NgayThanhToan.Date >= tuNgay.Value.Date);
            }
            if (denNgay.HasValue)
            {
                query = query.Where(t => t.NgayThanhToan.Date <= denNgay.Value.Date);
            }

            var danhSachDon = _context.DonMuaHang
                .Where(d => d.TrangThai != "Đã hủy" && d.TrangThai != "DaHuy" && d.TrangThai != "Nháp" && d.TrangThai != "Nhap")
                .Include(d => d.NhaCungCap)
                .Select(d => new DonMuaThanhToanVM
                {
                    MaDonMua = d.MaDonMua,
                    TenNhaCungCap = d.NhaCungCap != null ? d.NhaCungCap.TenNhaCungCap : "",
                    NgayDat = d.NgayDat,
                    TrangThaiDon = d.TrangThai,
                    TongTienDon = _context.ChiTietDonMua
                        .Where(ct => ct.MaDonMua == d.MaDonMua)
                        .Sum(ct => (decimal?)(ct.SoLuongDat * ct.DonGiaMua)) ?? 0,
                    TongDaThanhToan = _context.ThanhToanDonMua
                        .Where(tt => tt.MaDonMua == d.MaDonMua)
                        .Sum(tt => (decimal?)tt.SoTienThanhToan) ?? 0
                })
                .ToList();

            if (!string.IsNullOrWhiteSpace(trangThaiTT))
            {
                danhSachDon = danhSachDon.Where(d => d.TrangThaiThanhToan == trangThaiTT).ToList();
            }

            ViewBag.DanhSachDonThanhToan = danhSachDon;
            ViewBag.SearchString = searchString;
            ViewBag.PhuongThuc = phuongThuc;
            ViewBag.TrangThaiTT = trangThaiTT;
            ViewBag.TuNgay = tuNgay?.ToString("yyyy-MM-dd");
            ViewBag.DenNgay = denNgay?.ToString("yyyy-MM-dd");

            var ketQua = query.OrderByDescending(t => t.NgayThanhToan).ToList();
            return View(ketQua);
        }

        // GET: ThanhToanDonMuas/Details
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var thanhToan = _context.ThanhToanDonMua
                .Include(t => t.DonMuaHang)
                    .ThenInclude(d => d.NhaCungCap)
                .Include(t => t.NguoiThucHienNavigation)
                .FirstOrDefault(m => m.MaThanhToan == id);

            if (thanhToan == null) return NotFound();

            ViewBag.SoTienConLai = TinhSoTienConLai(thanhToan.MaDonMua);
            return View(thanhToan);
        }

        // GET: ThanhToanDonMuas/Create
        public IActionResult Create(int? maDonMua)
        {
            if (!KiemTraQuyen())
            {
                return StatusCode(403, "Bạn không có quyền truy cập chức năng Quản lý Thanh toán!");
            }

            var model = new ThanhToanDonMua
            {
                NgayThanhToan = DateTime.Now,
                PhuongThuc = "ChuyenKhoan"
            };

            // ĐÃ TẮT ĐOẠN GỌI SESSION
            /*
            var sessionMaTK = HttpContext.Session.GetInt32("MaTaiKhoan");
            if (sessionMaTK.HasValue)
            {
                model.NguoiThucHien = sessionMaTK.Value;
            }
            */

            if (maDonMua.HasValue)
            {
                model.MaDonMua = maDonMua.Value;
                decimal soTienConLai = TinhSoTienConLai(maDonMua.Value);
                model.SoTienThanhToan = soTienConLai > 0 ? soTienConLai : 0;
                ViewBag.SoTienConLai = soTienConLai;
            }

            LoadDropdownLists(model.MaDonMua, model.NguoiThucHien);
            return View(model);
        }

        // POST: ThanhToanDonMuas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("MaThanhToan,MaDonMua,NgayThanhToan,SoTienThanhToan,PhuongThuc,NguoiThucHien,GhiChu")] ThanhToanDonMua thanhToanDonMua)
        {
            if (!KiemTraQuyen())
            {
                return StatusCode(403, "Bạn không có quyền truy cập chức năng Quản lý Thanh toán!");
            }

            ModelState.Remove(nameof(thanhToanDonMua.DonMuaHang));
            ModelState.Remove(nameof(thanhToanDonMua.NguoiThucHienNavigation));

            var donMua = _context.DonMuaHang
                .FirstOrDefault(d => d.MaDonMua == thanhToanDonMua.MaDonMua);

            if (donMua == null)
            {
                ModelState.AddModelError("MaDonMua", "Đơn mua hàng không tồn tại.");
            }
            else if (donMua.TrangThai == "Đã hủy" || donMua.TrangThai == "DaHuy" || donMua.TrangThai == "Nháp" || donMua.TrangThai == "Nhap")
            {
                ModelState.AddModelError("MaDonMua", "Không được thanh toán cho đơn mua ở trạng thái Nháp hoặc Đã hủy.");
            }
            else
            {
                //Tính tiền SoTienConLai = TongTienDon - TongDaThanhToan
                decimal tongTienDon = _context.ChiTietDonMua
                    .Where(ct => ct.MaDonMua == donMua.MaDonMua)
                    .Sum(ct => (decimal?)(ct.SoLuongDat * ct.DonGiaMua)) ?? 0;

                decimal tongDaThanhToan = _context.ThanhToanDonMua
                    .Where(tt => tt.MaDonMua == donMua.MaDonMua)
                    .Sum(tt => (decimal?)tt.SoTienThanhToan) ?? 0;

                decimal soTienConLai = tongTienDon - tongDaThanhToan;

                if (soTienConLai <= 0)
                {
                    ModelState.AddModelError("SoTienThanhToan", "Đơn mua này đã được thanh toán đủ.");
                }
                else if (thanhToanDonMua.SoTienThanhToan > soTienConLai)
                {
                    ModelState.AddModelError("SoTienThanhToan", $"Số tiền thanh toán ({thanhToanDonMua.SoTienThanhToan:N0} đ) không được vượt quá số tiền còn lại ({soTienConLai:N0} đ).");
                }

                if (donMua.NgayDat.Date > thanhToanDonMua.NgayThanhToan.Date)
                {
                    ModelState.AddModelError("NgayThanhToan", $"Ngày thanh toán phải từ ngày đặt đơn ({donMua.NgayDat:dd/MM/yyyy}) trở đi.");
                }

                if (ModelState.IsValid)
                {
                    _context.Add(thanhToanDonMua);

                    if ((donMua.TrangThai == "Đã giao đủ" || donMua.TrangThai == "DaGiaoDu") &&
                        (tongDaThanhToan + thanhToanDonMua.SoTienThanhToan) == tongTienDon)
                    {
                        donMua.TrangThai = donMua.TrangThai == "DaGiaoDu" ? "HoanThanh" : "Hoàn thành";
                        _context.Update(donMua);
                    }

                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Ghi nhận thanh toán đơn mua thành công!";
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewBag.SoTienConLai = TinhSoTienConLai(thanhToanDonMua.MaDonMua);
            LoadDropdownLists(thanhToanDonMua.MaDonMua, thanhToanDonMua.NguoiThucHien);
            return View(thanhToanDonMua);
        }

        private decimal TinhSoTienConLai(int maDonMua)
        {
            decimal tongTienDon = _context.ChiTietDonMua
                .Where(ct => ct.MaDonMua == maDonMua)
                .Sum(ct => (decimal?)(ct.SoLuongDat * ct.DonGiaMua)) ?? 0;

            decimal tongDaTT = _context.ThanhToanDonMua
                .Where(tt => tt.MaDonMua == maDonMua)
                .Sum(tt => (decimal?)tt.SoTienThanhToan) ?? 0;

            return tongTienDon - tongDaTT;
        }

        private void LoadDropdownLists(int? selectedDonMua = null, int? selectedNguoiThucHien = null)
        {
            var donMuas = _context.DonMuaHang
                .Where(d => d.TrangThai != "Đã hủy" && d.TrangThai != "DaHuy" && d.TrangThai != "Nháp" && d.TrangThai != "Nhap")
                .Include(d => d.NhaCungCap)
                .Select(d => new
                {
                    d.MaDonMua,
                    TenNCC = d.NhaCungCap != null ? d.NhaCungCap.TenNhaCungCap : "",
                    TongTien = _context.ChiTietDonMua
                        .Where(ct => ct.MaDonMua == d.MaDonMua)
                        .Sum(ct => (decimal?)(ct.SoLuongDat * ct.DonGiaMua)) ?? 0,
                    DaTT = _context.ThanhToanDonMua
                        .Where(tt => tt.MaDonMua == d.MaDonMua)
                        .Sum(tt => (decimal?)tt.SoTienThanhToan) ?? 0
                })
                .ToList();

            var donMuaItems = donMuas.Select(d =>
            {
                var conLai = d.TongTien - d.DaTT;
                return new
                {
                    d.MaDonMua,
                    HienThi = $"Đơn #{d.MaDonMua} - {d.TenNCC} | Tổng: {d.TongTien:N0}đ | Còn lại: {conLai:N0}đ",
                    ConLai = conLai
                };
            })
            .Where(x => x.ConLai > 0 || x.MaDonMua == selectedDonMua)
            .ToList();

            ViewData["MaDonMua"] = new SelectList(donMuaItems, "MaDonMua", "HienThi", selectedDonMua);
            ViewData["NguoiThucHien"] = new SelectList(_context.TaiKhoan.ToList(), "MaTaiKhoan", "HoTen", selectedNguoiThucHien);
        }
    }
}