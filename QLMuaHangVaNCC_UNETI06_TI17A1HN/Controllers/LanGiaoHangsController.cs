
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

public class LanGiaoHangsController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

    public LanGiaoHangsController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
    {
        _context = context;
    }

    // GET: LANGIAOHANGS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.LanGiaoHang.ToListAsync());
    }

    // GET: LANGIAOHANGS/Details/5
    public async Task<IActionResult> Details(int? malangiao)
    {
        if (malangiao == null)
        {
            return NotFound();
        }

        var langiaohang = await _context.LanGiaoHang
            .FirstOrDefaultAsync(m => m.MaLanGiao == malangiao);
        if (langiaohang == null)
        {
            return NotFound();
        }

        return View(langiaohang);
    }

    // GET: LANGIAOHANGS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LANGIAOHANGS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaLanGiao,MaDonMua,NgayGiao,NguoiNhan,SoChungTu,GhiChu,TrangThai,DonMuaHang,NguoiNhanNavigation,ChiTietGiaoHangs")] LanGiaoHang langiaohang)
    {
        if (ModelState.IsValid)
        {
            _context.Add(langiaohang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(langiaohang);
    }

    // GET: LANGIAOHANGS/Edit/5
    public async Task<IActionResult> Edit(int? malangiao)
    {
        if (malangiao == null)
        {
            return NotFound();
        }

        var langiaohang = await _context.LanGiaoHang.FindAsync(malangiao);
        if (langiaohang == null)
        {
            return NotFound();
        }
        return View(langiaohang);
    }

    // POST: LANGIAOHANGS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? malangiao, [Bind("MaLanGiao,MaDonMua,NgayGiao,NguoiNhan,SoChungTu,GhiChu,TrangThai,DonMuaHang,NguoiNhanNavigation,ChiTietGiaoHangs")] LanGiaoHang langiaohang)
    {
        if (malangiao != langiaohang.MaLanGiao)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(langiaohang);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanGiaoHangExists(langiaohang.MaLanGiao))
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
        return View(langiaohang);
    }

    // GET: LANGIAOHANGS/Delete/5
    public async Task<IActionResult> Delete(int? malangiao)
    {
        if (malangiao == null)
        {
            return NotFound();
        }

        var langiaohang = await _context.LanGiaoHang
            .FirstOrDefaultAsync(m => m.MaLanGiao == malangiao);
        if (langiaohang == null)
        {
            return NotFound();
        }

        return View(langiaohang);
    }

    // POST: LANGIAOHANGS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? malangiao)
    {
        var langiaohang = await _context.LanGiaoHang.FindAsync(malangiao);
        if (langiaohang != null)
        {
            _context.LanGiaoHang.Remove(langiaohang);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LanGiaoHangExists(int? malangiao)
    {
        return _context.LanGiaoHang.Any(e => e.MaLanGiao == malangiao);
    }
}
