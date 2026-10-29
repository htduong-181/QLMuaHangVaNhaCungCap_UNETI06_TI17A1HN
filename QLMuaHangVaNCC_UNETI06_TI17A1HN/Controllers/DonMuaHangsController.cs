using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models.ViewModels;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Controllers
{
    public class DonMuaHangsController : Controller
    {
        private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

        public DonMuaHangsController(
            QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(
            string? search,
            string? trangThai)
        {
            var query = _context.DonMuaHang
                .AsNoTracking()
                .Include(d => d.YeuCauMuaHang)
                .Include(d => d.NhaCungCap)
                .Include(d => d.NguoiLapNavigation)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();

                query = query.Where(d =>
                    d.MaDonMua.ToString().Contains(search) ||
                    d.NhaCungCap.TenNhaCungCap.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(trangThai))
                query = query.Where(d => d.TrangThai == trangThai);

            ViewBag.Search = search;
            ViewBag.TrangThai = trangThai;

            return View(
                await query
                    .OrderByDescending(d => d.NgayDat)
                    .ToListAsync());
        }

        public async Task<IActionResult> Create(int? maYeuCau)
        {
            if (maYeuCau == null)
                return BadRequest(
                    "Thiếu mã yêu cầu mua hàng.");

            var yeuCau = await _context.YeuCauMuaHang
                .AsNoTracking()
                .Include(y => y.ChiTietYeuCaus)
                    .ThenInclude(c => c.HangHoa)
                .FirstOrDefaultAsync(
                    y => y.MaYeuCau == maYeuCau.Value);

            if (yeuCau == null)
                return NotFound(
                    "Không tìm thấy yêu cầu mua hàng.");

            if (!string.Equals(
                    yeuCau.TrangThai,
                    "DaDuyet",
                    StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(
                    "Chỉ yêu cầu mua hàng ở trạng thái Đã duyệt mới được lập đơn mua.");
            }

            var itemIds = yeuCau.ChiTietYeuCaus
                .Select(x => x.MaChiTietYeuCau)
                .ToList();

            var daDat = await _context.ChiTietDonMua
                .Where(c =>
                    itemIds.Contains(c.MaChiTietYeuCau) &&
                    c.DonMuaHang.TrangThai != "DaHuy")
                .GroupBy(c => c.MaChiTietYeuCau)
                .Select(g => new
                {
                    MaChiTietYeuCau = g.Key,
                    SoLuong = g.Sum(x => x.SoLuongDat)
                })
                .ToDictionaryAsync(
                    x => x.MaChiTietYeuCau,
                    x => x.SoLuong);

            var vm = new DonMuaHangCreateViewModel
            {
                MaYeuCau = yeuCau.MaYeuCau,

                ChiTiet = yeuCau.ChiTietYeuCaus
                    .Select(c =>
                    {
                        var soLuongDaDat =
                            daDat.GetValueOrDefault(
                                c.MaChiTietYeuCau);

                        var con = Math.Max(
                            0,
                            c.SoLuongDuyet -
                            soLuongDaDat);

                        return new DonMuaHangItemViewModel
                        {
                            MaChiTietYeuCau =
                                c.MaChiTietYeuCau,

                            MaHang =
                                c.MaHang,

                            TenHang =
                                c.HangHoa.TenHang,

                            SoLuongYeuCau =
                                c.SoLuongYeuCau,

                            SoLuongDuyet =
                                c.SoLuongDuyet,

                            SoLuongDaDat =
                                soLuongDaDat,

                            SoLuongConDuocDat =
                                con,

                            SoLuongDat =
                                con,

                            DonGiaMua =
                                0
                        };
                    })
                    .Where(x =>
                        x.SoLuongConDuocDat > 0)
                    .ToList()
            };

            await LoadCreateSelectLists();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            DonMuaHangCreateViewModel vm)
        {
            var yeuCau = await _context.YeuCauMuaHang
                .Include(y => y.ChiTietYeuCaus)
                .FirstOrDefaultAsync(
                    y => y.MaYeuCau == vm.MaYeuCau);

            if (yeuCau == null)
                return NotFound(
                    "Không tìm thấy yêu cầu mua hàng.");

            if (!string.Equals(
                    yeuCau.TrangThai,
                    "DaDuyet",
                    StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Chỉ yêu cầu Đã duyệt mới được lập đơn mua.");
            }

            var nhaCungCap = await _context.NhaCungCap
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    n => n.MaNhaCungCap ==
                         vm.MaNhaCungCap);

            if (nhaCungCap == null ||
                !nhaCungCap.TrangThai)
            {
                ModelState.AddModelError(
                    nameof(vm.MaNhaCungCap),
                    "Nhà cung cấp không tồn tại hoặc đã ngừng hoạt động.");
            }

            var nguoiLap = await _context.TaiKhoan
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    t => t.MaTaiKhoan ==
                         vm.NguoiLap);

            if (nguoiLap == null ||
                !nguoiLap.TrangThai)
            {
                ModelState.AddModelError(
                    nameof(vm.NguoiLap),
                    "Tài khoản người lập không tồn tại hoặc đang bị khóa.");
            }

            if (vm.ChiTiet == null ||
                vm.ChiTiet.Count == 0)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Đơn mua phải có ít nhất một chi tiết.");
            }

            var validDetails =
                new List<ChiTietDonMua>();

            decimal tongTien = 0;

            if (yeuCau != null)
            {
                var requestDetails =
                    yeuCau.ChiTietYeuCaus
                        .ToDictionary(
                            x => x.MaChiTietYeuCau);

                var itemIds =
                    requestDetails.Keys.ToList();

                var daDat = await _context.ChiTietDonMua
                    .Where(c =>
                        itemIds.Contains(
                            c.MaChiTietYeuCau) &&
                        c.DonMuaHang.TrangThai !=
                            "DaHuy")
                    .GroupBy(
                        c => c.MaChiTietYeuCau)
                    .Select(g => new
                    {
                        MaChiTietYeuCau = g.Key,
                        SoLuong =
                            g.Sum(x => x.SoLuongDat)
                    })
                    .ToDictionaryAsync(
                        x => x.MaChiTietYeuCau,
                        x => x.SoLuong);

                foreach (
                    var input in
                    vm.ChiTiet ??
                    new List<DonMuaHangItemViewModel>())
                {
                    if (!requestDetails.TryGetValue(
                            input.MaChiTietYeuCau,
                            out var ycct))
                    {
                        ModelState.AddModelError(
                            string.Empty,
                            $"Chi tiết yêu cầu {input.MaChiTietYeuCau} không thuộc yêu cầu {vm.MaYeuCau}.");

                        continue;
                    }

                    if (ycct.MaHang != input.MaHang)
                    {
                        ModelState.AddModelError(
                            string.Empty,
                            $"Mặt hàng của chi tiết yêu cầu {input.MaChiTietYeuCau} không hợp lệ.");

                        continue;
                    }

                    var soLuongDaDat =
                        daDat.GetValueOrDefault(
                            input.MaChiTietYeuCau);

                    var conDuocDat =
                        Math.Max(
                            0,
                            ycct.SoLuongDuyet -
                            soLuongDaDat);

                    if (input.SoLuongDat <= 0)
                        continue;

                    if (input.SoLuongDat >
                        conDuocDat)
                    {
                        ModelState.AddModelError(
                            string.Empty,
                            $"{input.MaChiTietYeuCau}: chỉ còn được đặt tối đa {conDuocDat}.");

                        continue;
                    }

                    var gia =
                        input.DonGiaMua;

                    if (gia < 0)
                    {
                        ModelState.AddModelError(
                            string.Empty,
                            $"Đơn giá của {input.TenHang} không được âm.");

                        continue;
                    }

                    var giaBao =
                        await _context.NhaCungCapHangHoa
                            .AsNoTracking()
                            .Where(x =>
                                x.MaNhaCungCap ==
                                    vm.MaNhaCungCap &&
                                x.MaHang ==
                                    input.MaHang &&
                                x.TrangThai)
                            .Select(x =>
                                (decimal?)x.DonGiaBao)
                            .FirstOrDefaultAsync();

                    if (giaBao.HasValue &&
                        gia <= 0)
                    {
                        gia = giaBao.Value;
                    }

                    var chiTiet =
                        new ChiTietDonMua
                        {
                            MaChiTietYeuCau =
                                input.MaChiTietYeuCau,

                            MaHang =
                                input.MaHang,

                            SoLuongDat =
                                input.SoLuongDat,

                            DonGiaMua =
                                gia
                        };

                    validDetails.Add(
                        chiTiet);

                    tongTien +=
                        chiTiet.ThanhTien;
                }
            }

            if (validDetails.Count == 0)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Đơn mua phải có ít nhất một mặt hàng có số lượng đặt > 0.");
            }

            if (!ModelState.IsValid)
            {
                await LoadCreateSelectLists();
                await ReloadCreateQuantities(vm);

                return View(vm);
            }

            var donMua = new DonMuaHang
            {
                MaYeuCau =
                    vm.MaYeuCau,

                MaNhaCungCap =
                    vm.MaNhaCungCap,

                NguoiLap =
                    vm.NguoiLap,

                NgayDat =
                    DateTime.Today,

                NgayGiaoDuKien =
                    vm.NgayGiaoDuKien,

                TrangThai =
                    "DaDat",

                TongTien =
                    tongTien,

                GhiChu =
                    vm.GhiChu,

                ChiTietDonMuas =
                    validDetails
            };

            _context.DonMuaHang.Add(
                donMua);

            await _context.SaveChangesAsync();

            return RedirectToAction(
                nameof(Details),
                new
                {
                    madonmua =
                        donMua.MaDonMua
                });
        }

        public async Task<IActionResult> Details(
            int? madonmua)
        {
            if (madonmua == null)
                return NotFound();

            var donMua =
                await _context.DonMuaHang
                    .AsNoTracking()
                    .Include(d =>
                        d.YeuCauMuaHang)
                    .Include(d =>
                        d.NhaCungCap)
                    .Include(d =>
                        d.NguoiLapNavigation)
                    .Include(d =>
                        d.ChiTietDonMuas)
                        .ThenInclude(c =>
                            c.HangHoa)
                    .Include(d =>
                        d.LanGiaoHangs)
                        .ThenInclude(l =>
                            l.NguoiNhanNavigation)
                    .Include(d =>
                        d.ThanhToanDonMuas)
                    .FirstOrDefaultAsync(
                        d =>
                            d.MaDonMua ==
                            madonmua.Value);

            if (donMua == null)
                return NotFound();

            var received =
                await _context.ChiTietGiaoHang
                    .Where(g =>
                        g.ChiTietDonMua
                            .DonMuaHang
                            .MaDonMua ==
                            madonmua.Value &&
                        g.LanGiaoHang.TrangThai)
                    .GroupBy(
                        g => g.MaChiTietDon)
                    .Select(g => new
                    {
                        MaChiTietDon =
                            g.Key,

                        SoLuong =
                            g.Sum(x =>
                                x.SoLuongNhan)
                    })
                    .ToDictionaryAsync(
                        x =>
                            x.MaChiTietDon,
                        x =>
                            x.SoLuong);

            ViewBag.DaNhan =
                received;

            return View(donMua);
        }

        private async Task LoadCreateSelectLists()
        {
            ViewBag.NhaCungCaps =
                await _context.NhaCungCap
                    .AsNoTracking()
                    .Where(x =>
                        x.TrangThai)
                    .OrderBy(x =>
                        x.TenNhaCungCap)
                    .ToListAsync();

            ViewBag.TaiKhoans =
                await _context.TaiKhoan
                    .AsNoTracking()
                    .Where(x =>
                        x.TrangThai)
                    .OrderBy(x =>
                        x.HoTen)
                    .ToListAsync();
        }

        private async Task ReloadCreateQuantities(
            DonMuaHangCreateViewModel vm)
        {
            var ids =
                (vm.ChiTiet ??
                    new List<DonMuaHangItemViewModel>())
                .Select(x =>
                    x.MaChiTietYeuCau)
                .ToList();

            if (ids.Count == 0)
                return;

            var details =
                await _context.ChiTietYeuCau
                    .AsNoTracking()
                    .Include(x =>
                        x.HangHoa)
                    .Where(x =>
                        ids.Contains(
                            x.MaChiTietYeuCau))
                    .ToListAsync();

            var daDat =
                await _context.ChiTietDonMua
                    .Where(c =>
                        ids.Contains(
                            c.MaChiTietYeuCau) &&
                        c.DonMuaHang.TrangThai !=
                            "DaHuy")
                    .GroupBy(
                        c =>
                            c.MaChiTietYeuCau)
                    .Select(g => new
                    {
                        g.Key,
                        SoLuong =
                            g.Sum(x =>
                                x.SoLuongDat)
                    })
                    .ToDictionaryAsync(
                        x => x.Key,
                        x => x.SoLuong);

            foreach (var item in vm.ChiTiet)
            {
                var d =
                    details.FirstOrDefault(
                        x =>
                            x.MaChiTietYeuCau ==
                            item.MaChiTietYeuCau);

                if (d == null)
                    continue;

                item.MaHang =
                    d.MaHang;

                item.TenHang =
                    d.HangHoa.TenHang;

                item.SoLuongYeuCau =
                    d.SoLuongYeuCau;

                item.SoLuongDuyet =
                    d.SoLuongDuyet;

                item.SoLuongDaDat =
                    daDat.GetValueOrDefault(
                        item.MaChiTietYeuCau);

                item.SoLuongConDuocDat =
                    Math.Max(
                        0,
                        d.SoLuongDuyet -
                        item.SoLuongDaDat);
            }
        }
    }
}