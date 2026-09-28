
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

public class ChiTietGiaoHangsController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

    public ChiTietGiaoHangsController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
    {
        _context = context;
    }

    // GET: CHITIETGIAOHANGS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.ChiTietGiaoHang.ToListAsync());
    }

    // GET: CHITIETGIAOHANGS/Details/5
    public async Task<IActionResult> Details(int? machitietgiao)
    {
        if (machitietgiao == null)
        {
            return NotFound();
        }

        var chitietgiaohang = await _context.ChiTietGiaoHang
            .FirstOrDefaultAsync(m => m.MaChiTietGiao == machitietgiao);
        if (chitietgiaohang == null)
        {
            return NotFound();
        }

        return View(chitietgiaohang);
    }

    // GET: CHITIETGIAOHANGS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CHITIETGIAOHANGS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaChiTietGiao,MaLanGiao,MaChiTietDon,SoLuongNhan,SoLuongDatChatLuong,GhiChu,LanGiaoHang,ChiTietDonMua")] ChiTietGiaoHang chitietgiaohang)
    {
        if (ModelState.IsValid)
        {
            _context.Add(chitietgiaohang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(chitietgiaohang);
    }

    // GET: CHITIETGIAOHANGS/Edit/5
    public async Task<IActionResult> Edit(int? machitietgiao)
    {
        if (machitietgiao == null)
        {
            return NotFound();
        }

        var chitietgiaohang = await _context.ChiTietGiaoHang.FindAsync(machitietgiao);
        if (chitietgiaohang == null)
        {
            return NotFound();
        }
        return View(chitietgiaohang);
    }

    // POST: CHITIETGIAOHANGS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? machitietgiao, [Bind("MaChiTietGiao,MaLanGiao,MaChiTietDon,SoLuongNhan,SoLuongDatChatLuong,GhiChu,LanGiaoHang,ChiTietDonMua")] ChiTietGiaoHang chitietgiaohang)
    {
        if (machitietgiao != chitietgiaohang.MaChiTietGiao)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(chitietgiaohang);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChiTietGiaoHangExists(chitietgiaohang.MaChiTietGiao))
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
        return View(chitietgiaohang);
    }

    // GET: CHITIETGIAOHANGS/Delete/5
    public async Task<IActionResult> Delete(int? machitietgiao)
    {
        if (machitietgiao == null)
        {
            return NotFound();
        }

        var chitietgiaohang = await _context.ChiTietGiaoHang
            .FirstOrDefaultAsync(m => m.MaChiTietGiao == machitietgiao);
        if (chitietgiaohang == null)
        {
            return NotFound();
        }

        return View(chitietgiaohang);
    }

    // POST: CHITIETGIAOHANGS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? machitietgiao)
    {
        var chitietgiaohang = await _context.ChiTietGiaoHang.FindAsync(machitietgiao);
        if (chitietgiaohang != null)
        {
            _context.ChiTietGiaoHang.Remove(chitietgiaohang);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ChiTietGiaoHangExists(int? machitietgiao)
    {
        return _context.ChiTietGiaoHang.Any(e => e.MaChiTietGiao == machitietgiao);
    }
}
