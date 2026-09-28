
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

public class DonMuaHangsController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

    public DonMuaHangsController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
    {
        _context = context;
    }

    // GET: DONMUAHANGS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.DonMuaHang.ToListAsync());
    }

    // GET: DONMUAHANGS/Details/5
    public async Task<IActionResult> Details(int? madonmua)
    {
        if (madonmua == null)
        {
            return NotFound();
        }

        var donmuahang = await _context.DonMuaHang
            .FirstOrDefaultAsync(m => m.MaDonMua == madonmua);
        if (donmuahang == null)
        {
            return NotFound();
        }

        return View(donmuahang);
    }

    // GET: DONMUAHANGS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: DONMUAHANGS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaDonMua,MaYeuCau,MaNhaCungCap,NgayDat,TrangThai,TongTien,GhiChu,NguoiLap,YeuCauMuaHang,NhaCungCap,NguoiLapNavigation,ChiTietDonMuas,LanGiaoHangs,ThanhToanDonMuas")] DonMuaHang donmuahang)
    {
        if (ModelState.IsValid)
        {
            _context.Add(donmuahang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(donmuahang);
    }

    // GET: DONMUAHANGS/Edit/5
    public async Task<IActionResult> Edit(int? madonmua)
    {
        if (madonmua == null)
        {
            return NotFound();
        }

        var donmuahang = await _context.DonMuaHang.FindAsync(madonmua);
        if (donmuahang == null)
        {
            return NotFound();
        }
        return View(donmuahang);
    }

    // POST: DONMUAHANGS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? madonmua, [Bind("MaDonMua,MaYeuCau,MaNhaCungCap,NgayDat,TrangThai,TongTien,GhiChu,NguoiLap,YeuCauMuaHang,NhaCungCap,NguoiLapNavigation,ChiTietDonMuas,LanGiaoHangs,ThanhToanDonMuas")] DonMuaHang donmuahang)
    {
        if (madonmua != donmuahang.MaDonMua)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(donmuahang);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonMuaHangExists(donmuahang.MaDonMua))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(donmuahang);
    }

    // GET: DONMUAHANGS/Delete/5
    public async Task<IActionResult> Delete(int? madonmua)
    {
        if (madonmua == null)
        {
            return NotFound();
        }

        var donmuahang = await _context.DonMuaHang
            .FirstOrDefaultAsync(m => m.MaDonMua == madonmua);
        if (donmuahang == null)
        {
            return NotFound();
        }

        return View(donmuahang);
    }

    // POST: DONMUAHANGS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? madonmua)
    {
        var donmuahang = await _context.DonMuaHang.FindAsync(madonmua);
        if (donmuahang != null)
        {
            _context.DonMuaHang.Remove(donmuahang);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DonMuaHangExists(int? madonmua)
    {
        return _context.DonMuaHang.Any(e => e.MaDonMua == madonmua);
    }
}
