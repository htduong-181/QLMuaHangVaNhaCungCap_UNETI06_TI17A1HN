using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Controllers
{
    public class ChiTietDonMuasController : Controller
    {
        private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

        public ChiTietDonMuasController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? madonmua)
        {
            var query = _context.ChiTietDonMua
                .AsNoTracking()
                .Include(x => x.DonMuaHang)
                    .ThenInclude(x => x.NhaCungCap)
                .Include(x => x.ChiTietYeuCau)
                    .ThenInclude(x => x.YeuCauMuaHang)
                .Include(x => x.HangHoa)
                .AsQueryable();

            if (madonmua.HasValue)
            {
                query = query.Where(x => x.MaDonMua == madonmua.Value);
                ViewBag.MaDonMua = madonmua.Value;
            }

            var danhSach = await query
                .OrderByDescending(x => x.MaChiTietDon)
                .ToListAsync();

            var ids = danhSach.Select(x => x.MaChiTietDon).ToList();

            var daNhan = await _context.ChiTietGiaoHang
                .Where(x => ids.Contains(x.MaChiTietDon) && x.LanGiaoHang.TrangThai)
                .GroupBy(x => x.MaChiTietDon)
                .Select(x => new
                {
                    MaChiTietDon = x.Key,
                    SoLuong = x.Sum(y => y.SoLuongNhan)
                })
                .ToDictionaryAsync(x => x.MaChiTietDon, x => x.SoLuong);

            ViewBag.DaNhan = daNhan;

            return View(danhSach);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var chiTiet = await _context.ChiTietDonMua
                .AsNoTracking()
                .Include(x => x.DonMuaHang)
                    .ThenInclude(x => x.NhaCungCap)
                .Include(x => x.DonMuaHang)
                    .ThenInclude(x => x.YeuCauMuaHang)
                .Include(x => x.ChiTietYeuCau)
                    .ThenInclude(x => x.YeuCauMuaHang)
                .Include(x => x.ChiTietYeuCau)
                    .ThenInclude(x => x.HangHoa)
                .Include(x => x.HangHoa)
                .Include(x => x.ChiTietGiaoHangs)
                    .ThenInclude(x => x.LanGiaoHang)
                .FirstOrDefaultAsync(x => x.MaChiTietDon == id.Value);

            if (chiTiet == null)
                return NotFound();

            var daNhan = chiTiet.ChiTietGiaoHangs
                .Where(x => x.LanGiaoHang.TrangThai)
                .Sum(x => x.SoLuongNhan);

            ViewBag.DaNhan = daNhan;
            ViewBag.ConLai = Math.Max(0, chiTiet.SoLuongDat - daNhan);

            return View(chiTiet);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var chiTiet = await _context.ChiTietDonMua
                .Include(x => x.DonMuaHang)
                .Include(x => x.ChiTietYeuCau)
                .Include(x => x.HangHoa)
                .FirstOrDefaultAsync(x => x.MaChiTietDon == id.Value);

            if (chiTiet == null)
                return NotFound();

            if (chiTiet.DonMuaHang.TrangThai == "DaHuy")
            {
                TempData["Error"] = "Không thể sửa chi tiết của đơn mua đã hủy.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var daNhan = await _context.ChiTietGiaoHang
                .Where(x =>
                    x.MaChiTietDon == chiTiet.MaChiTietDon &&
                    x.LanGiaoHang.TrangThai)
                .SumAsync(x => (int?)x.SoLuongNhan) ?? 0;

            ViewBag.DaNhan = daNhan;
            ViewBag.SoLuongDuyet = chiTiet.ChiTietYeuCau.SoLuongDuyet;

            return View(chiTiet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            int soLuongDat,
            decimal donGiaMua)
        {
            var chiTiet = await _context.ChiTietDonMua
                .Include(x => x.DonMuaHang)
                .Include(x => x.ChiTietYeuCau)
                .FirstOrDefaultAsync(x => x.MaChiTietDon == id);

            if (chiTiet == null)
                return NotFound();

            if (chiTiet.DonMuaHang.TrangThai == "DaHuy")
            {
                TempData["Error"] = "Không thể sửa đơn mua đã hủy.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var daNhan = await _context.ChiTietGiaoHang
                .Where(x =>
                    x.MaChiTietDon == id &&
                    x.LanGiaoHang.TrangThai)
                .SumAsync(x => (int?)x.SoLuongNhan) ?? 0;

            if (soLuongDat <= 0)
            {
                ModelState.AddModelError(
                    "soLuongDat",
                    "Số lượng đặt phải lớn hơn 0.");
            }

            if (soLuongDat < daNhan)
            {
                ModelState.AddModelError(
                    "soLuongDat",
                    $"Số lượng đặt không được nhỏ hơn số lượng đã nhận ({daNhan}).");
            }

            if (soLuongDat > chiTiet.ChiTietYeuCau.SoLuongDuyet)
            {
                ModelState.AddModelError(
                    "soLuongDat",
                    $"Số lượng đặt không được vượt số lượng duyệt ({chiTiet.ChiTietYeuCau.SoLuongDuyet}).");
            }

            if (donGiaMua < 0)
            {
                ModelState.AddModelError(
                    "donGiaMua",
                    "Đơn giá mua không được âm.");
            }

            if (!ModelState.IsValid)
            {
                chiTiet.SoLuongDat = soLuongDat;
                chiTiet.DonGiaMua = donGiaMua;

                ViewBag.DaNhan = daNhan;
                ViewBag.SoLuongDuyet = chiTiet.ChiTietYeuCau.SoLuongDuyet;

                return View(chiTiet);
            }

            chiTiet.SoLuongDat = soLuongDat;
            chiTiet.DonGiaMua = donGiaMua;

            await CapNhatTongTien(chiTiet.MaDonMua);
            await CapNhatTrangThai(chiTiet.MaDonMua);

            await _context.SaveChangesAsync();

            TempData["Success"] = "Đã cập nhật chi tiết đơn mua.";

            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var chiTiet = await _context.ChiTietDonMua
                .Include(x => x.DonMuaHang)
                .Include(x => x.HangHoa)
                .Include(x => x.ChiTietGiaoHangs)
                .FirstOrDefaultAsync(x => x.MaChiTietDon == id.Value);

            if (chiTiet == null)
                return NotFound();

            if (chiTiet.ChiTietGiaoHangs.Any())
            {
                TempData["Error"] = "Không thể xóa chi tiết đã phát sinh giao hàng.";
                return RedirectToAction(nameof(Details), new { id });
            }

            return View(chiTiet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTiet = await _context.ChiTietDonMua
                .Include(x => x.DonMuaHang)
                .Include(x => x.ChiTietGiaoHangs)
                .FirstOrDefaultAsync(x => x.MaChiTietDon == id);

            if (chiTiet == null)
                return NotFound();

            if (chiTiet.ChiTietGiaoHangs.Any())
            {
                TempData["Error"] = "Không thể xóa chi tiết đã phát sinh giao hàng.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var maDonMua = chiTiet.MaDonMua;

            _context.ChiTietDonMua.Remove(chiTiet);

            await CapNhatTongTien(maDonMua);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đã xóa chi tiết đơn mua.";

            return RedirectToAction(nameof(Index), new { madonmua = maDonMua });
        }

        private async Task CapNhatTongTien(int maDonMua)
        {
            var tongTien = await _context.ChiTietDonMua
                .Where(x => x.MaDonMua == maDonMua)
                .SumAsync(x => x.SoLuongDat * x.DonGiaMua);

            var donMua = await _context.DonMuaHang
                .FirstOrDefaultAsync(x => x.MaDonMua == maDonMua);

            if (donMua != null)
                donMua.TongTien = tongTien;
        }

        private async Task CapNhatTrangThai(int maDonMua)
        {
            var donMua = await _context.DonMuaHang
                .Include(x => x.ChiTietDonMuas)
                .FirstOrDefaultAsync(x => x.MaDonMua == maDonMua);

            if (donMua == null || donMua.TrangThai == "DaHuy")
                return;

            if (!donMua.ChiTietDonMuas.Any())
            {
                donMua.TrangThai = "DaDat";
                return;
            }

            var daNhan = await _context.ChiTietGiaoHang
                .Where(x =>
                    x.ChiTietDonMua.MaDonMua == maDonMua &&
                    x.LanGiaoHang.TrangThai)
                .SumAsync(x => (int?)x.SoLuongNhan) ?? 0;

            var tongDat = donMua.ChiTietDonMuas
                .Sum(x => x.SoLuongDat);

            if (daNhan >= tongDat)
                donMua.TrangThai = "DaGiaoDu";
            else if (daNhan > 0)
                donMua.TrangThai = "GiaoMotPhan";
            else
                donMua.TrangThai = "DaDat";
        }
    }
}