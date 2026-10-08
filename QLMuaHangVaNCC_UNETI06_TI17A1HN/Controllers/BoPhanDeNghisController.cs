// Họ và tên: Hoàng Thùy Dương
// Mã sinh viên: 23103100051
// Nội dung thực hiện: Module 3 - Controller quản lý Bộ phận đề nghị (danh sách có tìm kiếm/lọc/phân trang,
// thêm, sửa, đổi trạng thái, xóa khi chưa phát sinh yêu cầu). Chỉ Admin được truy cập.

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Helpers;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.ViewModels;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Controllers;

[PhanQuyenYeuCau(VaiTroHeThong.Admin)]
public class BoPhanDeNghisController : Controller
{
    private const int PageSize = 10;
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

    public BoPhanDeNghisController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
    {
        _context = context;
    }

    // GET: BoPhanDeNghis?tuKhoa=&trangThai=&page=
    public async Task<IActionResult> Index(string? tuKhoa, bool? trangThai, int page = 1)
    {
        // Truy vấn -> Tìm kiếm -> Lọc -> Sắp xếp -> Phân trang
        var q = _context.BoPhanDeNghi.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(tuKhoa))
        {
            var kw = tuKhoa.Trim();
            q = q.Where(b => b.TenBoPhan.Contains(kw)
                          || (b.NguoiPhuTrach != null && b.NguoiPhuTrach.Contains(kw)));
        }
        if (trangThai.HasValue)
            q = q.Where(b => b.TrangThai == trangThai.Value);

        q = q.OrderBy(b => b.TenBoPhan);

        var tong = await q.CountAsync();
        var tongTrang = Math.Max(1, (int)Math.Ceiling(tong / (double)PageSize));
        page = Math.Clamp(page, 1, tongTrang);

        var items = await q.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

        // Số yêu cầu của từng bộ phận trong trang hiện tại (dùng để quyết định cho xóa hay chỉ ngừng hoạt động)
        var ids = items.Select(b => b.MaBoPhan).ToList();
        ViewBag.SoYeuCau = await _context.YeuCauMuaHang
            .Where(y => ids.Contains(y.MaBoPhan))
            .GroupBy(y => y.MaBoPhan)
            .Select(g => new { g.Key, SoLuong = g.Count() })
            .ToDictionaryAsync(x => x.Key, x => x.SoLuong);

        ViewBag.TuKhoa = tuKhoa;
        ViewBag.TrangThai = trangThai;

        return View(new PagedResult<BoPhanDeNghi>
        {
            Items = items,
            Page = page,
            PageSize = PageSize,
            TotalItems = tong
        });
    }

    // GET: BoPhanDeNghis/Create
    public IActionResult Create() => View(new BoPhanDeNghi());

    // POST: BoPhanDeNghis/Create  (Bind giới hạn thuộc tính để chống overposting)
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("TenBoPhan,NguoiPhuTrach,SoDienThoai,MoTa,TrangThai")] BoPhanDeNghi bp)
    {
        bp.TenBoPhan = bp.TenBoPhan?.Trim() ?? string.Empty;

        if (await _context.BoPhanDeNghi.AnyAsync(b => b.TenBoPhan == bp.TenBoPhan))
            ModelState.AddModelError(nameof(bp.TenBoPhan), "Tên bộ phận đã tồn tại");

        if (!ModelState.IsValid) return View(bp);

        _context.Add(bp);
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Đã thêm bộ phận đề nghị.";
        return RedirectToAction(nameof(Index));
    }

    // GET: BoPhanDeNghis/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var bp = await _context.BoPhanDeNghi.FindAsync(id);
        if (bp is null) return NotFound();
        return View(bp);
    }

    // POST: BoPhanDeNghis/Edit/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("MaBoPhan,TenBoPhan,NguoiPhuTrach,SoDienThoai,MoTa,TrangThai")] BoPhanDeNghi bp)
    {
        if (id != bp.MaBoPhan) return NotFound();

        bp.TenBoPhan = bp.TenBoPhan?.Trim() ?? string.Empty;

        if (await _context.BoPhanDeNghi.AnyAsync(b => b.TenBoPhan == bp.TenBoPhan && b.MaBoPhan != bp.MaBoPhan))
            ModelState.AddModelError(nameof(bp.TenBoPhan), "Tên bộ phận đã tồn tại");

        if (!ModelState.IsValid) return View(bp);

        try
        {
            _context.Update(bp);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.BoPhanDeNghi.AnyAsync(e => e.MaBoPhan == id)) return NotFound();
            throw;
        }
        TempData["ThongBao"] = "Đã cập nhật bộ phận đề nghị.";
        return RedirectToAction(nameof(Index));
    }

    // POST: đổi Hoạt động <-> Ngừng hoạt động (cách "xóa mềm", giữ lịch sử yêu cầu)
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DoiTrangThai(int id)
    {
        var bp = await _context.BoPhanDeNghi.FindAsync(id);
        if (bp is null) return NotFound();

        bp.TrangThai = !bp.TrangThai;
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = bp.TrangThai
            ? $"Bộ phận '{bp.TenBoPhan}' đã hoạt động trở lại."
            : $"Bộ phận '{bp.TenBoPhan}' đã ngừng hoạt động (không dùng cho yêu cầu mới).";
        return RedirectToAction(nameof(Index));
    }

    // POST: chỉ xóa khi bộ phận CHƯA có yêu cầu nào
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Xoa(int id)
    {
        var bp = await _context.BoPhanDeNghi.FindAsync(id);
        if (bp is null) return NotFound();

        if (await _context.YeuCauMuaHang.AnyAsync(y => y.MaBoPhan == id))
        {
            TempData["Loi"] = $"Bộ phận '{bp.TenBoPhan}' đã phát sinh yêu cầu mua hàng, không thể xóa. Hãy chuyển sang Ngừng hoạt động.";
            return RedirectToAction(nameof(Index));
        }

        _context.BoPhanDeNghi.Remove(bp);
        await _context.SaveChangesAsync();
        TempData["ThongBao"] = "Đã xóa bộ phận đề nghị.";
        return RedirectToAction(nameof(Index));
    }
}
