// Họ và tên: Trương Huy Đồng
// Mã sinh viên: 23103100036
// Nội dung thực hiện: Module 5 - Dashboard, Thống kê LINQ và Báo cáo

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Controllers
{
    // ==========================================
    // 1. CÁC LỚP VIEWMODEL
    // ==========================================
    public class DashboardVM
    {
        public int TongNCCDangHoatDong { get; set; }
        public int SoYeuCauChoDuyet { get; set; }
        public int SoYeuCauDaDuyet { get; set; }
        public int SoYeuCauTuChoi { get; set; }
        public int SoDonDaDatHang { get; set; }
        public int SoDonGiaoMotPhan { get; set; }
        public int SoDonDaGiaoDu { get; set; }
        public int SoDonHoanThanh { get; set; }
        public decimal TongGiaTriMuaTrongThang { get; set; }
        public int SoDonChamGiao { get; set; }
        public decimal TongTienConPhaiThanhToan { get; set; }
        public List<YeuCauMuaHang> YeuCauUuTienCao { get; set; } = new List<YeuCauMuaHang>();
    }

    public class ThongKeNCCItem
    {
        public int MaNhaCungCap { get; set; }
        public string TenNhaCungCap { get; set; } = string.Empty;
        public int SoLuongDon { get; set; }
        public decimal TongGiaTriMua { get; set; }
    }

    public class ThongKeHangHoaItem
    {
        public int MaHang { get; set; }
        public string TenHang { get; set; } = string.Empty;
        public int TongSoLuongYeuCau { get; set; }
        public int TongSoLuongMua { get; set; }
        public decimal GiaMuaTrungBinh { get; set; }
        public decimal TongThanhTien { get; set; }
    }

    public class ThongKeThoiGianItem
    {
        public int Nam { get; set; }
        public int Quy { get; set; }
        public int Thang { get; set; }
        public decimal TongGiaTriMua { get; set; }
    }

    public class ThongKeTongHopVM
    {
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }

        public List<ThongKeNCCItem> ThongKeTheoNCC { get; set; } = new List<ThongKeNCCItem>();
        public List<ThongKeHangHoaItem> ThongKeTheoHangHoa { get; set; } = new List<ThongKeHangHoaItem>();

        public ThongKeNCCItem? NCCNhieuDonNhat { get; set; }
        public ThongKeNCCItem? NCCGiaTriLonNhat { get; set; }
        public ThongKeHangHoaItem? MatHangYeuCauNhieuNhat { get; set; }

        public Dictionary<string, int> SoYeuCauTheoBoPhan { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> SoDonTheoTrangThai { get; set; } = new Dictionary<string, int>();
        public List<ThongKeThoiGianItem> GiaTriTheoThangQuyNam { get; set; } = new List<ThongKeThoiGianItem>();
    }

    public class LichSuNCCVM
    {
        public NhaCungCap? NhaCungCap { get; set; }
        public List<DonMuaHang> DanhSachDonMua { get; set; } = new List<DonMuaHang>();
        public Dictionary<int, decimal> TongGiaTriDon { get; set; } = new Dictionary<int, decimal>();
        public Dictionary<int, decimal> DaThanhToan { get; set; } = new Dictionary<int, decimal>();
    }

    public class LichSuHangHoaVM
    {
        public HangHoa? HangHoa { get; set; }
        public List<ChiTietYeuCau> LichSuYeuCau { get; set; } = new List<ChiTietYeuCau>();
        public List<ChiTietDonMua> LichSuDatMua { get; set; } = new List<ChiTietDonMua>();
        public Dictionary<int, int> SoLuongDaNhan { get; set; } = new Dictionary<int, int>();
    }

    public class ThongKeGiaoHangVM
    {
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public int SoDonGiaoDu { get; set; }
        public int SoDonGiaoMotPhan { get; set; }
        public int SoDonChamGiao { get; set; }
        public List<TyLeGiaoHangNCC> TyLeTheoNCC { get; set; } = new List<TyLeGiaoHangNCC>();
    }

    public class TyLeGiaoHangNCC
    {
        public string TenNhaCungCap { get; set; } = string.Empty;
        public int TongSoDon { get; set; }
        public int SoDonGiaoDu { get; set; }
        public double TyLeHoanThanh { get; set; }
    }

    // ==========================================
    // 2. LỚP CONTROLLER CHÍNH
    // ==========================================
    public class ThongKeController : Controller
    {
        private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

        public ThongKeController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
        {
            _context = context;
        }

        private bool KiemTraQuyenQuanLy()
        {
            return true; // Tắt tạm check Session để test
        }

        // ==========================================
        // MODULE 5: DASHBOARD TỔNG QUAN
        // ==========================================
        public IActionResult Dashboard(int? thang, int? nam)
        {
            if (!KiemTraQuyenQuanLy()) return StatusCode(403, "Không có quyền truy cập Dashboard.");

            int currentMonth = thang ?? DateTime.Now.Month;
            int currentYear = nam ?? DateTime.Now.Year;

            var vm = new DashboardVM();

            vm.TongNCCDangHoatDong = _context.NhaCungCap.Count(n => n.TrangThai == true);

            // Gom các trạng thái có thể lưu trùng lặp vào chung 1 điều kiện
            vm.SoYeuCauChoDuyet = _context.YeuCauMuaHang.Count(y => y.TrangThai == "ChoDuyet" || y.TrangThai == "Chờ duyệt");
            vm.SoYeuCauDaDuyet = _context.YeuCauMuaHang.Count(y => y.TrangThai == "DaDuyet" || y.TrangThai == "Đã duyệt");
            vm.SoYeuCauTuChoi = _context.YeuCauMuaHang.Count(y => y.TrangThai == "TuChoi" || y.TrangThai == "Từ chối");

            vm.SoDonDaDatHang = _context.DonMuaHang.Count(d => d.TrangThai == "Đã đặt hàng" || d.TrangThai == "DaDatHang" || d.TrangThai == "DaDat");
            vm.SoDonGiaoMotPhan = _context.DonMuaHang.Count(d => d.TrangThai == "Giao một phần" || d.TrangThai == "GiaoMotPhan");
            vm.SoDonDaGiaoDu = _context.DonMuaHang.Count(d => d.TrangThai == "Đã giao đủ" || d.TrangThai == "DaGiaoDu");
            vm.SoDonHoanThanh = _context.DonMuaHang.Count(d => d.TrangThai == "Hoàn thành" || d.TrangThai == "HoanThanh");

            vm.TongGiaTriMuaTrongThang = _context.ChiTietDonMua
                .Where(ct => ct.DonMuaHang != null
                          && ct.DonMuaHang.TrangThai != "Đã hủy" && ct.DonMuaHang.TrangThai != "DaHuy"
                          && ct.DonMuaHang.TrangThai != "Nháp" && ct.DonMuaHang.TrangThai != "Nhap"
                          && ct.DonMuaHang.NgayDat.Month == currentMonth
                          && ct.DonMuaHang.NgayDat.Year == currentYear)
                .Sum(ct => (decimal?)(ct.SoLuongDat * ct.DonGiaMua)) ?? 0;

            vm.SoDonChamGiao = _context.DonMuaHang
                .Count(d => d.TrangThai != "Đã giao đủ" && d.TrangThai != "DaGiaoDu"
                         && d.TrangThai != "Hoàn thành" && d.TrangThai != "HoanThanh"
                         && d.TrangThai != "Đã hủy" && d.TrangThai != "DaHuy"
                         && d.TrangThai != "Nháp" && d.TrangThai != "Nhap"
                         && d.NgayGiaoDuKien < DateTime.Now.Date);

            decimal tongGiaTriTatCaDon = _context.ChiTietDonMua
                .Where(ct => ct.DonMuaHang != null && ct.DonMuaHang.TrangThai != "Đã hủy" && ct.DonMuaHang.TrangThai != "Nháp")
                .Sum(ct => (decimal?)(ct.SoLuongDat * ct.DonGiaMua)) ?? 0;

            decimal tongDaThanhToan = _context.ThanhToanDonMua
                .Where(tt => tt.DonMuaHang != null && tt.DonMuaHang.TrangThai != "Đã hủy" && tt.DonMuaHang.TrangThai != "Nháp")
                .Sum(tt => (decimal?)tt.SoTienThanhToan) ?? 0;

            vm.TongTienConPhaiThanhToan = tongGiaTriTatCaDon - tongDaThanhToan;

            vm.YeuCauUuTienCao = _context.YeuCauMuaHang
                .Include(y => y.BoPhanDeNghi)
                .Where(y => (y.MucDoUuTien == "Cao" || y.MucDoUuTien == "Khẩn cấp")
                         && y.TrangThai != "Hoàn thành" && y.TrangThai != "Đã hủy")
                .OrderByDescending(y => y.NgayYeuCau)
                .Take(10)
                .ToList();

            ViewBag.ThangHienTai = currentMonth;
            ViewBag.NamHienTai = currentYear;

            return View(vm);
        }

        // ==========================================
        // MODULE 5: THỐNG KÊ BẰNG LINQ
        // ==========================================
        public IActionResult Index(DateTime? tuNgay, DateTime? denNgay)
        {
            var vm = new ThongKeTongHopVM { TuNgay = tuNgay, DenNgay = denNgay };

            var donMuaQuery = _context.DonMuaHang
                .Include(d => d.NhaCungCap)
                .Include(d => d.ChiTietDonMuas)
                    .ThenInclude(ct => ct.HangHoa)
                .Where(d => d.TrangThai != "Đã hủy" && d.TrangThai != "DaHuy" && d.TrangThai != "Nháp" && d.TrangThai != "Nhap")
                .AsQueryable();

            var yeuCauQuery = _context.YeuCauMuaHang
                .Include(y => y.BoPhanDeNghi)
                .Include(y => y.ChiTietYeuCaus)
                    .ThenInclude(ct => ct.HangHoa)
                .AsQueryable();

            if (tuNgay.HasValue)
            {
                donMuaQuery = donMuaQuery.Where(d => d.NgayDat.Date >= tuNgay.Value.Date);
                yeuCauQuery = yeuCauQuery.Where(y => y.NgayYeuCau.Date >= tuNgay.Value.Date);
            }
            if (denNgay.HasValue)
            {
                donMuaQuery = donMuaQuery.Where(d => d.NgayDat.Date <= denNgay.Value.Date);
                yeuCauQuery = yeuCauQuery.Where(y => y.NgayYeuCau.Date <= denNgay.Value.Date);
            }

            var danhSachDon = donMuaQuery.ToList();
            var danhSachYeuCau = yeuCauQuery.ToList();

            vm.ThongKeTheoNCC = danhSachDon
                .Where(d => d.NhaCungCap != null)
                .GroupBy(d => new { d.MaNhaCungCap, d.NhaCungCap!.TenNhaCungCap })
                .Select(g => new ThongKeNCCItem
                {
                    MaNhaCungCap = g.Key.MaNhaCungCap,
                    TenNhaCungCap = g.Key.TenNhaCungCap,
                    SoLuongDon = g.Count(),
                    TongGiaTriMua = g.SelectMany(d => d.ChiTietDonMuas).Sum(ct => ct.SoLuongDat * ct.DonGiaMua)
                })
                .OrderByDescending(x => x.TongGiaTriMua)
                .ToList();

            vm.NCCNhieuDonNhat = vm.ThongKeTheoNCC.OrderByDescending(x => x.SoLuongDon).FirstOrDefault();
            vm.NCCGiaTriLonNhat = vm.ThongKeTheoNCC.OrderByDescending(x => x.TongGiaTriMua).FirstOrDefault();

            var chiTietDonList = danhSachDon.SelectMany(d => d.ChiTietDonMuas).ToList();
            var chiTietYeuCauList = danhSachYeuCau.SelectMany(y => y.ChiTietYeuCaus).ToList();

            var tatCaHang = _context.HangHoa.ToList();
            vm.ThongKeTheoHangHoa = tatCaHang.Select(h =>
            {
                var ctMuaCuaHang = chiTietDonList.Where(ct => ct.MaHang == h.MaHang).ToList();
                var ctYeuCauCuaHang = chiTietYeuCauList.Where(ct => ct.MaHang == h.MaHang).ToList();

                return new ThongKeHangHoaItem
                {
                    MaHang = h.MaHang,
                    TenHang = h.TenHang,
                    TongSoLuongYeuCau = ctYeuCauCuaHang.Sum(ct => ct.SoLuongYeuCau),
                    TongSoLuongMua = ctMuaCuaHang.Sum(ct => ct.SoLuongDat),
                    GiaMuaTrungBinh = ctMuaCuaHang.Any() ? Math.Round(ctMuaCuaHang.Average(ct => ct.DonGiaMua), 0) : 0,
                    TongThanhTien = ctMuaCuaHang.Sum(ct => ct.SoLuongDat * ct.DonGiaMua)
                };
            })
            .Where(x => x.TongSoLuongMua > 0 || x.TongSoLuongYeuCau > 0)
            .OrderByDescending(x => x.TongSoLuongMua)
            .ToList();

            vm.MatHangYeuCauNhieuNhat = vm.ThongKeTheoHangHoa.OrderByDescending(x => x.TongSoLuongYeuCau).FirstOrDefault();

            vm.SoYeuCauTheoBoPhan = danhSachYeuCau
                .Where(y => y.BoPhanDeNghi != null)
                .GroupBy(y => y.BoPhanDeNghi!.TenBoPhan)
                .ToDictionary(g => g.Key, g => g.Count());

            var tatCaDonTheoThoiGian = _context.DonMuaHang
                .Where(d => (!tuNgay.HasValue || d.NgayDat.Date >= tuNgay.Value.Date)
                         && (!denNgay.HasValue || d.NgayDat.Date <= denNgay.Value.Date))
                .GroupBy(d => d.TrangThai)
                .Select(g => new { TrangThai = g.Key, SoLuong = g.Count() })
                .ToList();
            vm.SoDonTheoTrangThai = tatCaDonTheoThoiGian.ToDictionary(x => x.TrangThai, x => x.SoLuong);

            vm.GiaTriTheoThangQuyNam = danhSachDon
                .GroupBy(d => new { d.NgayDat.Year, Quy = (d.NgayDat.Month - 1) / 3 + 1, d.NgayDat.Month })
                .Select(g => new ThongKeThoiGianItem
                {
                    Nam = g.Key.Year,
                    Quy = g.Key.Quy,
                    Thang = g.Key.Month,
                    TongGiaTriMua = g.SelectMany(d => d.ChiTietDonMuas).Sum(ct => ct.SoLuongDat * ct.DonGiaMua)
                })
                .OrderByDescending(x => x.Nam).ThenByDescending(x => x.Thang)
                .ToList();

            return View(vm);
        }

        // ==========================================
        // MODULE 5: LỊCH SỬ NHÀ CUNG CẤP & HÀNG HÓA
        // ==========================================
        public IActionResult LichSuNhaCungCap(int id)
        {
            var ncc = _context.NhaCungCap.FirstOrDefault(n => n.MaNhaCungCap == id);
            if (ncc == null) return NotFound();

            var vm = new LichSuNCCVM { NhaCungCap = ncc };

            vm.DanhSachDonMua = _context.DonMuaHang
                .Include(d => d.ChiTietDonMuas)
                .Include(d => d.ThanhToanDonMuas)
                .Where(d => d.MaNhaCungCap == id && d.TrangThai != "Nháp")
                .OrderByDescending(d => d.NgayDat)
                .ToList();

            foreach (var don in vm.DanhSachDonMua)
            {
                vm.TongGiaTriDon[don.MaDonMua] = don.ChiTietDonMuas.Sum(ct => (decimal?)(ct.SoLuongDat * ct.DonGiaMua)) ?? 0;
                vm.DaThanhToan[don.MaDonMua] = don.ThanhToanDonMuas.Sum(tt => (decimal?)tt.SoTienThanhToan) ?? 0;
            }

            return View(vm);
        }

        public IActionResult LichSuHangHoa(int id)
        {
            var hang = _context.HangHoa.FirstOrDefault(h => h.MaHang == id);
            if (hang == null) return NotFound();

            var vm = new LichSuHangHoaVM { HangHoa = hang };

            vm.LichSuYeuCau = _context.ChiTietYeuCau
                .Include(ct => ct.YeuCauMuaHang)
                    .ThenInclude(y => y.BoPhanDeNghi)
                .Where(ct => ct.MaHang == id && ct.YeuCauMuaHang!.TrangThai != "Nháp")
                .OrderByDescending(ct => ct.YeuCauMuaHang!.NgayYeuCau)
                .ToList();

            vm.LichSuDatMua = _context.ChiTietDonMua
                .Include(ct => ct.DonMuaHang)
                    .ThenInclude(d => d.NhaCungCap)
                .Include(ct => ct.ChiTietGiaoHangs)
                .Where(ct => ct.MaHang == id && ct.DonMuaHang!.TrangThai != "Nháp")
                .OrderByDescending(ct => ct.DonMuaHang!.NgayDat)
                .ToList();

            foreach (var ct in vm.LichSuDatMua)
            {
                vm.SoLuongDaNhan[ct.MaChiTietDon] = ct.ChiTietGiaoHangs?.Sum(g => g.SoLuongNhan) ?? 0;
            }

            return View(vm);
        }

        // ==========================================
        // MODULE 5: THỐNG KÊ GIAO HÀNG (CÓ BỘ LỌC)
        // ==========================================
        public IActionResult ThongKeGiaoHang(DateTime? tuNgay, DateTime? denNgay)
        {
            var vm = new ThongKeGiaoHangVM { TuNgay = tuNgay, DenNgay = denNgay };

            var donMuaQuery = _context.DonMuaHang
                .Include(d => d.NhaCungCap)
                .Where(d => d.TrangThai != "Đã hủy" && d.TrangThai != "Nháp")
                .AsQueryable();

            if (tuNgay.HasValue) donMuaQuery = donMuaQuery.Where(d => d.NgayDat.Date >= tuNgay.Value.Date);
            if (denNgay.HasValue) donMuaQuery = donMuaQuery.Where(d => d.NgayDat.Date <= denNgay.Value.Date);

            var danhSachDon = donMuaQuery.ToList();
            var now = DateTime.Now;

            vm.SoDonGiaoDu = danhSachDon.Count(d => d.TrangThai == "Đã giao đủ" || d.TrangThai == "Hoàn thành");
            vm.SoDonGiaoMotPhan = danhSachDon.Count(d => d.TrangThai == "Giao một phần");

            vm.SoDonChamGiao = danhSachDon.Count(d =>
                d.NgayGiaoDuKien.HasValue && d.NgayGiaoDuKien.Value.Date < now.Date
                && d.TrangThai != "Đã giao đủ" && d.TrangThai != "Hoàn thành");

            vm.TyLeTheoNCC = danhSachDon
                .Where(d => d.NhaCungCap != null)
                .GroupBy(d => new { d.MaNhaCungCap, d.NhaCungCap!.TenNhaCungCap })
                .Select(g => {
                    int tongDon = g.Count();
                    int giaoDu = g.Count(d => d.TrangThai == "Đã giao đủ" || d.TrangThai == "Hoàn thành");
                    return new TyLeGiaoHangNCC
                    {
                        TenNhaCungCap = g.Key.TenNhaCungCap,
                        TongSoDon = tongDon,
                        SoDonGiaoDu = giaoDu,
                        TyLeHoanThanh = tongDon > 0 ? Math.Round((double)giaoDu / tongDon * 100, 2) : 0
                    };
                })
                .OrderByDescending(x => x.TyLeHoanThanh)
                .ToList();

            return View(vm);
        }
    }
}