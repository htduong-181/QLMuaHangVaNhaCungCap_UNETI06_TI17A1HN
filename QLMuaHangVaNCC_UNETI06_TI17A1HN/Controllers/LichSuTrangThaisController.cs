
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

public class LichSuTrangThaisController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

    public LichSuTrangThaisController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
    {
        _context = context;
    }

    // GET: LICHSUTRANGTHAIS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.LichSuTrangThai.ToListAsync());
    }

    // GET: LICHSUTRANGTHAIS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var lichsutrangthai = await _context.LichSuTrangThai
            .FirstOrDefaultAsync(m => m.Id == id);
        if (lichsutrangthai == null)
        {
            return NotFound();
        }

        return View(lichsutrangthai);
    }

    // GET: LICHSUTRANGTHAIS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LICHSUTRANGTHAIS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,LoaiDoiTuong,MaDoiTuong,TrangThaiCu,TrangThaiMoi,NguoiThucHien,ThoiGian,GhiChu,NguoiThucHienNavigation")] LichSuTrangThai lichsutrangthai)
    {
        if (ModelState.IsValid)
        {
            _context.Add(lichsutrangthai);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(lichsutrangthai);
    }

    // GET: LICHSUTRANGTHAIS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var lichsutrangthai = await _context.LichSuTrangThai.FindAsync(id);
        if (lichsutrangthai == null)
        {
            return NotFound();
        }
        return View(lichsutrangthai);
    }

    // POST: LICHSUTRANGTHAIS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,LoaiDoiTuong,MaDoiTuong,TrangThaiCu,TrangThaiMoi,NguoiThucHien,ThoiGian,GhiChu,NguoiThucHienNavigation")] LichSuTrangThai lichsutrangthai)
    {
        if (id != lichsutrangthai.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(lichsutrangthai);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LichSuTrangThaiExists(lichsutrangthai.Id))
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
        return View(lichsutrangthai);
    }

    // GET: LICHSUTRANGTHAIS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var lichsutrangthai = await _context.LichSuTrangThai
            .FirstOrDefaultAsync(m => m.Id == id);
        if (lichsutrangthai == null)
        {
            return NotFound();
        }

        return View(lichsutrangthai);
    }

    // POST: LICHSUTRANGTHAIS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var lichsutrangthai = await _context.LichSuTrangThai.FindAsync(id);
        if (lichsutrangthai != null)
        {
            _context.LichSuTrangThai.Remove(lichsutrangthai);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LichSuTrangThaiExists(int? id)
    {
        return _context.LichSuTrangThai.Any(e => e.Id == id);
    }
}
