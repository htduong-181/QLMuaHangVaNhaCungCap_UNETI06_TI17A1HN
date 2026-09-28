
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

public class HangHoasController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

    public HangHoasController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
    {
        _context = context;
    }

    // GET: HANGHOAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.HangHoa.ToListAsync());
    }

    // GET: HANGHOAS/Details/5
    public async Task<IActionResult> Details(int? mahang)
    {
        if (mahang == null)
        {
            return NotFound();
        }

        var hanghoa = await _context.HangHoa
            .FirstOrDefaultAsync(m => m.MaHang == mahang);
        if (hanghoa == null)
        {
            return NotFound();
        }

        return View(hanghoa);
    }

    // GET: HANGHOAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: HANGHOAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaHang,TenHang,MaLoaiHang,MaDonViTinh,GiaThamKhao,MoTa,TrangThai,LoaiHang,DonViTinh,ChiTietYeuCaus,ChiTietDonMuas,NhaCungCapHangHoas")] HangHoa hanghoa)
    {
        if (ModelState.IsValid)
        {
            _context.Add(hanghoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(hanghoa);
    }

    // GET: HANGHOAS/Edit/5
    public async Task<IActionResult> Edit(int? mahang)
    {
        if (mahang == null)
        {
            return NotFound();
        }

        var hanghoa = await _context.HangHoa.FindAsync(mahang);
        if (hanghoa == null)
        {
            return NotFound();
        }
        return View(hanghoa);
    }

    // POST: HANGHOAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? mahang, [Bind("MaHang,TenHang,MaLoaiHang,MaDonViTinh,GiaThamKhao,MoTa,TrangThai,LoaiHang,DonViTinh,ChiTietYeuCaus,ChiTietDonMuas,NhaCungCapHangHoas")] HangHoa hanghoa)
    {
        if (mahang != hanghoa.MaHang)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(hanghoa);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HangHoaExists(hanghoa.MaHang))
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
        return View(hanghoa);
    }

    // GET: HANGHOAS/Delete/5
    public async Task<IActionResult> Delete(int? mahang)
    {
        if (mahang == null)
        {
            return NotFound();
        }

        var hanghoa = await _context.HangHoa
            .FirstOrDefaultAsync(m => m.MaHang == mahang);
        if (hanghoa == null)
        {
            return NotFound();
        }

        return View(hanghoa);
    }

    // POST: HANGHOAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? mahang)
    {
        var hanghoa = await _context.HangHoa.FindAsync(mahang);
        if (hanghoa != null)
        {
            _context.HangHoa.Remove(hanghoa);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool HangHoaExists(int? mahang)
    {
        return _context.HangHoa.Any(e => e.MaHang == mahang);
    }
}
