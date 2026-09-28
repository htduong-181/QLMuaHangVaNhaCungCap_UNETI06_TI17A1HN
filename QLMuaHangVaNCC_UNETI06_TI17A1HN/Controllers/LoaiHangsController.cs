
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

public class LoaiHangsController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

    public LoaiHangsController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
    {
        _context = context;
    }

    // GET: LOAIHANGS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.LoaiHang.ToListAsync());
    }

    // GET: LOAIHANGS/Details/5
    public async Task<IActionResult> Details(int? maloaihang)
    {
        if (maloaihang == null)
        {
            return NotFound();
        }

        var loaihang = await _context.LoaiHang
            .FirstOrDefaultAsync(m => m.MaLoaiHang == maloaihang);
        if (loaihang == null)
        {
            return NotFound();
        }

        return View(loaihang);
    }

    // GET: LOAIHANGS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LOAIHANGS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaLoaiHang,TenLoaiHang,MoTa,TrangThai,HangHoas")] LoaiHang loaihang)
    {
        if (ModelState.IsValid)
        {
            _context.Add(loaihang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(loaihang);
    }

    // GET: LOAIHANGS/Edit/5
    public async Task<IActionResult> Edit(int? maloaihang)
    {
        if (maloaihang == null)
        {
            return NotFound();
        }

        var loaihang = await _context.LoaiHang.FindAsync(maloaihang);
        if (loaihang == null)
        {
            return NotFound();
        }
        return View(loaihang);
    }

    // POST: LOAIHANGS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? maloaihang, [Bind("MaLoaiHang,TenLoaiHang,MoTa,TrangThai,HangHoas")] LoaiHang loaihang)
    {
        if (maloaihang != loaihang.MaLoaiHang)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(loaihang);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoaiHangExists(loaihang.MaLoaiHang))
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
        return View(loaihang);
    }

    // GET: LOAIHANGS/Delete/5
    public async Task<IActionResult> Delete(int? maloaihang)
    {
        if (maloaihang == null)
        {
            return NotFound();
        }

        var loaihang = await _context.LoaiHang
            .FirstOrDefaultAsync(m => m.MaLoaiHang == maloaihang);
        if (loaihang == null)
        {
            return NotFound();
        }

        return View(loaihang);
    }

    // POST: LOAIHANGS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? maloaihang)
    {
        var loaihang = await _context.LoaiHang.FindAsync(maloaihang);
        if (loaihang != null)
        {
            _context.LoaiHang.Remove(loaihang);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LoaiHangExists(int? maloaihang)
    {
        return _context.LoaiHang.Any(e => e.MaLoaiHang == maloaihang);
    }
}
