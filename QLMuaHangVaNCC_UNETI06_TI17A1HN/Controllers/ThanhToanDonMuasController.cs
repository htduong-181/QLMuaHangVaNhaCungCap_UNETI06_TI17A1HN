
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

public class ThanhToanDonMuasController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

    public ThanhToanDonMuasController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
    {
        _context = context;
    }

    // GET: THANHTOANDONMUAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.ThanhToanDonMua.ToListAsync());
    }

    // GET: THANHTOANDONMUAS/Details/5
    public async Task<IActionResult> Details(int? mathanhtoan)
    {
        if (mathanhtoan == null)
        {
            return NotFound();
        }

        var thanhtoandonmua = await _context.ThanhToanDonMua
            .FirstOrDefaultAsync(m => m.MaThanhToan == mathanhtoan);
        if (thanhtoandonmua == null)
        {
            return NotFound();
        }

        return View(thanhtoandonmua);
    }

    // GET: THANHTOANDONMUAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: THANHTOANDONMUAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaThanhToan,MaDonMua,NgayThanhToan,SoTienThanhToan,PhuongThuc,NguoiThucHien,GhiChu,DonMuaHang,NguoiThucHienNavigation")] ThanhToanDonMua thanhtoandonmua)
    {
        if (ModelState.IsValid)
        {
            _context.Add(thanhtoandonmua);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(thanhtoandonmua);
    }

    // GET: THANHTOANDONMUAS/Edit/5
    public async Task<IActionResult> Edit(int? mathanhtoan)
    {
        if (mathanhtoan == null)
        {
            return NotFound();
        }

        var thanhtoandonmua = await _context.ThanhToanDonMua.FindAsync(mathanhtoan);
        if (thanhtoandonmua == null)
        {
            return NotFound();
        }
        return View(thanhtoandonmua);
    }

    // POST: THANHTOANDONMUAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? mathanhtoan, [Bind("MaThanhToan,MaDonMua,NgayThanhToan,SoTienThanhToan,PhuongThuc,NguoiThucHien,GhiChu,DonMuaHang,NguoiThucHienNavigation")] ThanhToanDonMua thanhtoandonmua)
    {
        if (mathanhtoan != thanhtoandonmua.MaThanhToan)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(thanhtoandonmua);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThanhToanDonMuaExists(thanhtoandonmua.MaThanhToan))
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
        return View(thanhtoandonmua);
    }

    // GET: THANHTOANDONMUAS/Delete/5
    public async Task<IActionResult> Delete(int? mathanhtoan)
    {
        if (mathanhtoan == null)
        {
            return NotFound();
        }

        var thanhtoandonmua = await _context.ThanhToanDonMua
            .FirstOrDefaultAsync(m => m.MaThanhToan == mathanhtoan);
        if (thanhtoandonmua == null)
        {
            return NotFound();
        }

        return View(thanhtoandonmua);
    }

    // POST: THANHTOANDONMUAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? mathanhtoan)
    {
        var thanhtoandonmua = await _context.ThanhToanDonMua.FindAsync(mathanhtoan);
        if (thanhtoandonmua != null)
        {
            _context.ThanhToanDonMua.Remove(thanhtoandonmua);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ThanhToanDonMuaExists(int? mathanhtoan)
    {
        return _context.ThanhToanDonMua.Any(e => e.MaThanhToan == mathanhtoan);
    }
}
