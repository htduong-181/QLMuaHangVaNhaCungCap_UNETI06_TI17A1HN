
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

public class BoPhanDeNghisController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

    public BoPhanDeNghisController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
    {
        _context = context;
    }

    // GET: BOPHANDENGHIS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.BoPhanDeNghi.ToListAsync());
    }

    // GET: BOPHANDENGHIS/Details/5
    public async Task<IActionResult> Details(int? mabophan)
    {
        if (mabophan == null)
        {
            return NotFound();
        }

        var bophandenghi = await _context.BoPhanDeNghi
            .FirstOrDefaultAsync(m => m.MaBoPhan == mabophan);
        if (bophandenghi == null)
        {
            return NotFound();
        }

        return View(bophandenghi);
    }

    // GET: BOPHANDENGHIS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: BOPHANDENGHIS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaBoPhan,TenBoPhan,NguoiPhuTrach,SoDienThoai,MoTa,TrangThai,YeuCauMuaHangs")] BoPhanDeNghi bophandenghi)
    {
        if (ModelState.IsValid)
        {
            _context.Add(bophandenghi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(bophandenghi);
    }

    // GET: BOPHANDENGHIS/Edit/5
    public async Task<IActionResult> Edit(int? mabophan)
    {
        if (mabophan == null)
        {
            return NotFound();
        }

        var bophandenghi = await _context.BoPhanDeNghi.FindAsync(mabophan);
        if (bophandenghi == null)
        {
            return NotFound();
        }
        return View(bophandenghi);
    }

    // POST: BOPHANDENGHIS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? mabophan, [Bind("MaBoPhan,TenBoPhan,NguoiPhuTrach,SoDienThoai,MoTa,TrangThai,YeuCauMuaHangs")] BoPhanDeNghi bophandenghi)
    {
        if (mabophan != bophandenghi.MaBoPhan)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(bophandenghi);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoPhanDeNghiExists(bophandenghi.MaBoPhan))
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
        return View(bophandenghi);
    }

    // GET: BOPHANDENGHIS/Delete/5
    public async Task<IActionResult> Delete(int? mabophan)
    {
        if (mabophan == null)
        {
            return NotFound();
        }

        var bophandenghi = await _context.BoPhanDeNghi
            .FirstOrDefaultAsync(m => m.MaBoPhan == mabophan);
        if (bophandenghi == null)
        {
            return NotFound();
        }

        return View(bophandenghi);
    }

    // POST: BOPHANDENGHIS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? mabophan)
    {
        var bophandenghi = await _context.BoPhanDeNghi.FindAsync(mabophan);
        if (bophandenghi != null)
        {
            _context.BoPhanDeNghi.Remove(bophandenghi);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BoPhanDeNghiExists(int? mabophan)
    {
        return _context.BoPhanDeNghi.Any(e => e.MaBoPhan == mabophan);
    }
}
