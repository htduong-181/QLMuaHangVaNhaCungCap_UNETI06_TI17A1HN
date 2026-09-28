
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

public class ChiTietYeuCausController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

    public ChiTietYeuCausController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
    {
        _context = context;
    }

    // GET: CHITIETYEUCAUS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.ChiTietYeuCau.ToListAsync());
    }

    // GET: CHITIETYEUCAUS/Details/5
    public async Task<IActionResult> Details(int? machitietyeucau)
    {
        if (machitietyeucau == null)
        {
            return NotFound();
        }

        var chitietyeucau = await _context.ChiTietYeuCau
            .FirstOrDefaultAsync(m => m.MaChiTietYeuCau == machitietyeucau);
        if (chitietyeucau == null)
        {
            return NotFound();
        }

        return View(chitietyeucau);
    }

    // GET: CHITIETYEUCAUS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CHITIETYEUCAUS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaChiTietYeuCau,MaYeuCau,MaHang,SoLuongYeuCau,SoLuongDuyet,GhiChu,YeuCauMuaHang,HangHoa,ChiTietDonMuas")] ChiTietYeuCau chitietyeucau)
    {
        if (ModelState.IsValid)
        {
            _context.Add(chitietyeucau);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(chitietyeucau);
    }

    // GET: CHITIETYEUCAUS/Edit/5
    public async Task<IActionResult> Edit(int? machitietyeucau)
    {
        if (machitietyeucau == null)
        {
            return NotFound();
        }

        var chitietyeucau = await _context.ChiTietYeuCau.FindAsync(machitietyeucau);
        if (chitietyeucau == null)
        {
            return NotFound();
        }
        return View(chitietyeucau);
    }

    // POST: CHITIETYEUCAUS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? machitietyeucau, [Bind("MaChiTietYeuCau,MaYeuCau,MaHang,SoLuongYeuCau,SoLuongDuyet,GhiChu,YeuCauMuaHang,HangHoa,ChiTietDonMuas")] ChiTietYeuCau chitietyeucau)
    {
        if (machitietyeucau != chitietyeucau.MaChiTietYeuCau)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(chitietyeucau);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChiTietYeuCauExists(chitietyeucau.MaChiTietYeuCau))
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
        return View(chitietyeucau);
    }

    // GET: CHITIETYEUCAUS/Delete/5
    public async Task<IActionResult> Delete(int? machitietyeucau)
    {
        if (machitietyeucau == null)
        {
            return NotFound();
        }

        var chitietyeucau = await _context.ChiTietYeuCau
            .FirstOrDefaultAsync(m => m.MaChiTietYeuCau == machitietyeucau);
        if (chitietyeucau == null)
        {
            return NotFound();
        }

        return View(chitietyeucau);
    }

    // POST: CHITIETYEUCAUS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? machitietyeucau)
    {
        var chitietyeucau = await _context.ChiTietYeuCau.FindAsync(machitietyeucau);
        if (chitietyeucau != null)
        {
            _context.ChiTietYeuCau.Remove(chitietyeucau);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ChiTietYeuCauExists(int? machitietyeucau)
    {
        return _context.ChiTietYeuCau.Any(e => e.MaChiTietYeuCau == machitietyeucau);
    }
}
