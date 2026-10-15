using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.ViewModels;

public class LanGiaoHangsController : Controller
{
    private const string DaDatHang = "DaDat";
    private const string GiaoMotPhan = "GiaoMotPhan";
    private const string DaGiaoDu = "DaGiaoDu";
    private const string HoanThanh = "HoanThanh";
    private const string DaHuy = "DaHuy";
    private const string Nhap = "Nhap";

    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

    public LanGiaoHangsController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var ds = await _context.LanGiaoHang
            .Include(l => l.DonMuaHang)
            .Include(l => l.NguoiNhanNavigation)
            .Include(l => l.ChiTietGiaoHangs)
            .OrderByDescending(l => l.MaLanGiao)
            .ToListAsync();
        return View(ds);
    }

    public async Task<IActionResult> Details(int? malangiao)
    {
        if (malangiao == null) return NotFound();

        var lan = await _context.LanGiaoHang
            .Include(l => l.DonMuaHang)
            .Include(l => l.NguoiNhanNavigation)
            .Include(l => l.ChiTietGiaoHangs)
                .ThenInclude(g => g.ChiTietDonMua)
                    .ThenInclude(c => c!.HangHoa)
            .FirstOrDefaultAsync(m => m.MaLanGiao == malangiao);
        if (lan == null) return NotFound();

        return View(lan);
    }

    public async Task<IActionResult> DoiChieu(int? maDonMua)
    {
        if (maDonMua == null) return NotFound();

        var don = await _context.DonMuaHang.AsNoTracking()
            .FirstOrDefaultAsync(d => d.MaDonMua == maDonMua);
        if (don == null) return NotFound();

        ViewBag.Don = don;
        return View(await _context.LayDoiChieuAsync(don.MaDonMua));
    }

    public async Task<IActionResult> Create(int? maDonMua)
    {
        var vm = new LanGiaoHangVM();

        if (maDonMua != null)
        {
            var don = await _context.DonMuaHang.AsNoTracking()
                .FirstOrDefaultAsync(d => d.MaDonMua == maDonMua);
            if (don == null) return NotFound();

            if (!ChoPhepNhan(don))
            {
                TempData["Loi"] = "Đơn mua không ở trạng thái cho phép nhận hàng.";
                return RedirectToAction(nameof(Index));
            }

            vm.MaDonMua = don.MaDonMua;
            vm.ChiTiets = await _context.LayDoiChieuAsync(don.MaDonMua);
        }

        await NapDanhSach(vm.MaDonMua, vm.NguoiNhan);
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LanGiaoHangVM vm)
    {
        await using var tran = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

        var don = await _context.DonMuaHang.FirstOrDefaultAsync(d => d.MaDonMua == vm.MaDonMua);
        var dong = new List<DongDoiChieuVM>();

        if (don == null)
        {
            ModelState.AddModelError("", "Phải chọn đơn mua.");
        }
        else
        {
            if (!ChoPhepNhan(don))
                ModelState.AddModelError("", "Đơn mua không ở trạng thái cho phép nhận hàng.");
            if (vm.NgayGiao.Date < don.NgayDat.Date)
                ModelState.AddModelError("", "Ngày giao không được trước ngày đặt hàng.");
            dong = await _context.LayDoiChieuAsync(don.MaDonMua);
        }

        if (!await _context.TaiKhoan.AnyAsync(t => t.MaTaiKhoan == vm.NguoiNhan && t.TrangThai))
            ModelState.AddModelError("", "Người nhận không hợp lệ.");

        var nhap = vm.ChiTiets ?? new List<DongDoiChieuVM>();

        if (nhap.Any(x => x.SoLuongNhan < 0))
            ModelState.AddModelError("", "Số lượng nhận không được âm.");

        var chon = nhap.Where(x => x.SoLuongNhan > 0).ToList();

        if (don != null && chon.Count == 0)
            ModelState.AddModelError("", "Phải nhận ít nhất một mặt hàng.");

        if (chon.GroupBy(x => x.MaChiTietDon).Any(g => g.Count() > 1))
            ModelState.AddModelError("", "Một chi tiết đơn bị lặp trong lần giao.");

        foreach (var ct in chon)
        {
            var d = dong.FirstOrDefault(x => x.MaChiTietDon == ct.MaChiTietDon);
            if (d == null)
            {
                ModelState.AddModelError("", "Chi tiết không thuộc đơn mua đã chọn.");
                continue;
            }
            if (ct.SoLuongNhan > d.ConPhaiGiao)
                ModelState.AddModelError("", $"{d.TenHang}: nhận {ct.SoLuongNhan} vượt số còn phải giao ({d.ConPhaiGiao}).");
        }

        foreach (var ct in nhap.Where(x => x.SoLuongDatChatLuong.HasValue))
        {
            if (ct.SoLuongDatChatLuong < 0 || ct.SoLuongDatChatLuong > ct.SoLuongNhan)
            {
                var ten = dong.FirstOrDefault(x => x.MaChiTietDon == ct.MaChiTietDon)?.TenHang ?? "Mặt hàng";
                ModelState.AddModelError("", $"{ten}: số lượng đạt chất lượng phải từ 0 đến số lượng nhận.");
            }
        }

        if (!ModelState.IsValid)
        {
            await tran.RollbackAsync();

            foreach (var d in dong)
            {
                var n = nhap.FirstOrDefault(x => x.MaChiTietDon == d.MaChiTietDon);
                if (n != null)
                {
                    d.SoLuongNhan = n.SoLuongNhan;
                    d.SoLuongDatChatLuong = n.SoLuongDatChatLuong;
                    d.GhiChu = n.GhiChu;
                }
            }
            vm.ChiTiets = dong;
            await NapDanhSach(vm.MaDonMua, vm.NguoiNhan);
            return View(vm);
        }

        var lan = new LanGiaoHang
        {
            MaDonMua = vm.MaDonMua,
            NgayGiao = vm.NgayGiao,
            NguoiNhan = vm.NguoiNhan,
            SoChungTu = vm.SoChungTu,
            GhiChu = vm.GhiChu,
            TrangThai = true,
            ChiTietGiaoHangs = chon.Select(x => new ChiTietGiaoHang
            {
                MaChiTietDon = x.MaChiTietDon,
                SoLuongNhan = x.SoLuongNhan,
                SoLuongDatChatLuong = x.SoLuongDatChatLuong,
                GhiChu = x.GhiChu
            }).ToList()
        };

        _context.LanGiaoHang.Add(lan);
        await _context.SaveChangesAsync();
        await CapNhatTrangThaiDon(vm.MaDonMua);
        await tran.CommitAsync();

        TempData["ThanhCong"] = "Đã ghi nhận lần giao hàng.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? malangiao)
    {
        if (malangiao == null) return NotFound();

        var lan = await _context.LanGiaoHang
            .Include(l => l.DonMuaHang)
            .FirstOrDefaultAsync(l => l.MaLanGiao == malangiao);
        if (lan == null) return NotFound();

        if (!ChoPhepSua(lan))
        {
            TempData["Loi"] = "Lần giao đã vô hiệu hoặc đơn mua đã hoàn thành/đã hủy, không thể sửa.";
            return RedirectToAction(nameof(Index));
        }

        await NapDanhSach(lan.MaDonMua, lan.NguoiNhan);
        return View(lan);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? malangiao, [Bind("MaLanGiao,NgayGiao,NguoiNhan,SoChungTu,GhiChu")] LanGiaoHang input)
    {
        if (malangiao != input.MaLanGiao) return NotFound();

        var lan = await _context.LanGiaoHang
            .Include(l => l.DonMuaHang)
            .FirstOrDefaultAsync(l => l.MaLanGiao == malangiao);
        if (lan == null) return NotFound();

        if (!ChoPhepSua(lan))
        {
            TempData["Loi"] = "Lần giao đã vô hiệu hoặc đơn mua đã hoàn thành/đã hủy, không thể sửa.";
            return RedirectToAction(nameof(Index));
        }

        ModelState.Remove(nameof(LanGiaoHang.MaDonMua));
        ModelState.Remove(nameof(LanGiaoHang.DonMuaHang));
        ModelState.Remove(nameof(LanGiaoHang.NguoiNhanNavigation));
        ModelState.Remove(nameof(LanGiaoHang.TrangThai));

        if (lan.DonMuaHang != null && input.NgayGiao.Date < lan.DonMuaHang.NgayDat.Date)
            ModelState.AddModelError(nameof(LanGiaoHang.NgayGiao), "Ngày giao không được trước ngày đặt hàng.");

        if (!await _context.TaiKhoan.AnyAsync(t => t.MaTaiKhoan == input.NguoiNhan && t.TrangThai))
            ModelState.AddModelError(nameof(LanGiaoHang.NguoiNhan), "Người nhận không hợp lệ.");

        if (!ModelState.IsValid)
        {
            input.MaDonMua = lan.MaDonMua;
            input.TrangThai = lan.TrangThai;
            await NapDanhSach(lan.MaDonMua, input.NguoiNhan);
            return View(input);
        }

        lan.NgayGiao = input.NgayGiao;
        lan.NguoiNhan = input.NguoiNhan;
        lan.SoChungTu = input.SoChungTu;
        lan.GhiChu = input.GhiChu;
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? malangiao)
    {
        if (malangiao == null) return NotFound();

        var lan = await _context.LanGiaoHang
            .Include(l => l.DonMuaHang)
            .Include(l => l.NguoiNhanNavigation)
            .Include(l => l.ChiTietGiaoHangs)
            .FirstOrDefaultAsync(m => m.MaLanGiao == malangiao);
        if (lan == null) return NotFound();

        return View(lan);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? malangiao)
    {
        if (malangiao == null) return NotFound();

        var lan = await _context.LanGiaoHang
            .Include(l => l.DonMuaHang)
            .FirstOrDefaultAsync(l => l.MaLanGiao == malangiao);
        if (lan == null) return NotFound();

        if (!ChoPhepSua(lan))
        {
            TempData["Loi"] = "Không thể vô hiệu: lần giao đã vô hiệu hoặc đơn mua đã hoàn thành/đã hủy.";
            return RedirectToAction(nameof(Index));
        }

        await using var tran = await _context.Database.BeginTransactionAsync();
        lan.TrangThai = false;
        await _context.SaveChangesAsync();
        await CapNhatTrangThaiDon(lan.MaDonMua);
        await tran.CommitAsync();

        return RedirectToAction(nameof(Index));
    }

    private static bool ChoPhepNhan(DonMuaHang don)
    {
        return don.TrangThai == DaDatHang || don.TrangThai == GiaoMotPhan;
    }

    private static bool ChoPhepSua(LanGiaoHang lan)
    {
        return lan.TrangThai
            && lan.DonMuaHang != null
            && lan.DonMuaHang.TrangThai != HoanThanh
            && lan.DonMuaHang.TrangThai != DaHuy;
    }

    private async Task CapNhatTrangThaiDon(int maDon)
    {
        var don = await _context.DonMuaHang.FirstOrDefaultAsync(d => d.MaDonMua == maDon);
        if (don == null) return;
        if (don.TrangThai == Nhap || don.TrangThai == HoanThanh || don.TrangThai == DaHuy) return;

        var dong = await _context.LayDoiChieuAsync(maDon);
        var tongNhan = dong.Sum(x => x.TongDaNhan);

        if (tongNhan == 0)
            don.TrangThai = DaDatHang;
        else if (dong.All(x => x.ConPhaiGiao <= 0))
            don.TrangThai = DaGiaoDu;
        else
            don.TrangThai = GiaoMotPhan;

        await _context.SaveChangesAsync();
    }

    private async Task NapDanhSach(int maDonMua, int nguoiNhan)
    {
        var don = await _context.DonMuaHang
            .Where(d => d.TrangThai == DaDatHang || d.TrangThai == GiaoMotPhan || d.MaDonMua == maDonMua)
            .OrderBy(d => d.MaDonMua)
            .Select(d => new { d.MaDonMua, Ten = "Đơn #" + d.MaDonMua + " - " + d.TrangThai })
            .ToListAsync();
        ViewBag.DonMua = new SelectList(don, "MaDonMua", "Ten", maDonMua);

        var taiKhoan = await _context.TaiKhoan
            .Where(t => t.TrangThai || t.MaTaiKhoan == nguoiNhan)
            .OrderBy(t => t.HoTen)
            .ToListAsync();
        ViewBag.NguoiNhan = new SelectList(taiKhoan, "MaTaiKhoan", "HoTen", nguoiNhan);
    }
}