
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

public class YeuCauMuaHangsController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

    public YeuCauMuaHangsController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
    {
        _context = context;
    }

    // GET: YEUCAUMUAHANGS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.YeuCauMuaHang.ToListAsync());
    }

    // GET: YEUCAUMUAHANGS/Details/5
    public async Task<IActionResult> Details(int? mayeucau)
    {
        if (mayeucau == null)
        {
            return NotFound();
        }

        var yeucaumuahang = await _context.YeuCauMuaHang
            .FirstOrDefaultAsync(m => m.MaYeuCau == mayeucau);
        if (yeucaumuahang == null)
        {
            return NotFound();
        }

        return View(yeucaumuahang);
    }

    // GET: YEUCAUMUAHANGS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: YEUCAUMUAHANGS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaYeuCau,MaBoPhan,NgayYeuCau,NgayCanHang,MucDoUuTien,LyDoMua,NguoiLap,TrangThai,NgayDuyet,NguoiDuyet,LyDoTuChoiHuy,BoPhanDeNghi,NguoiLapNavigation,NguoiDuyetNavigation,ChiTietYeuCaus,DonMuaHangs")] YeuCauMuaHang yeucaumuahang)
    {
        if (ModelState.IsValid)
        {
            _context.Add(yeucaumuahang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(yeucaumuahang);
    }

    // GET: YEUCAUMUAHANGS/Edit/5
    public async Task<IActionResult> Edit(int? mayeucau)
    {
        if (mayeucau == null)
        {
            return NotFound();
        }

        var yeucaumuahang = await _context.YeuCauMuaHang.FindAsync(mayeucau);
        if (yeucaumuahang == null)
        {
            return NotFound();
        }
        return View(yeucaumuahang);
    }

    // POST: YEUCAUMUAHANGS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? mayeucau, [Bind("MaYeuCau,MaBoPhan,NgayYeuCau,NgayCanHang,MucDoUuTien,LyDoMua,NguoiLap,TrangThai,NgayDuyet,NguoiDuyet,LyDoTuChoiHuy,BoPhanDeNghi,NguoiLapNavigation,NguoiDuyetNavigation,ChiTietYeuCaus,DonMuaHangs")] YeuCauMuaHang yeucaumuahang)
    {
        if (mayeucau != yeucaumuahang.MaYeuCau)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(yeucaumuahang);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YeuCauMuaHangExists(yeucaumuahang.MaYeuCau))
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
        return View(yeucaumuahang);
    }

    // GET: YEUCAUMUAHANGS/Delete/5
    public async Task<IActionResult> Delete(int? mayeucau)
    {
        if (mayeucau == null)
        {
            return NotFound();
        }

        var yeucaumuahang = await _context.YeuCauMuaHang
            .FirstOrDefaultAsync(m => m.MaYeuCau == mayeucau);
        if (yeucaumuahang == null)
        {
            return NotFound();
        }

        return View(yeucaumuahang);
    }

    // POST: YEUCAUMUAHANGS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? mayeucau)
    {
        var yeucaumuahang = await _context.YeuCauMuaHang.FindAsync(mayeucau);
        if (yeucaumuahang != null)
        {
            _context.YeuCauMuaHang.Remove(yeucaumuahang);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool YeuCauMuaHangExists(int? mayeucau)
    {
        return _context.YeuCauMuaHang.Any(e => e.MaYeuCau == mayeucau);
    }
}
