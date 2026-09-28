
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

public class ChiTietDonMuasController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

    public ChiTietDonMuasController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
    {
        _context = context;
    }

    // GET: CHITIETDONMUAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.ChiTietDonMua.ToListAsync());
    }

    // GET: CHITIETDONMUAS/Details/5
    public async Task<IActionResult> Details(int? machitietdon)
    {
        if (machitietdon == null)
        {
            return NotFound();
        }

        var chitietdonmua = await _context.ChiTietDonMua
            .FirstOrDefaultAsync(m => m.MaChiTietDon == machitietdon);
        if (chitietdonmua == null)
        {
            return NotFound();
        }

        return View(chitietdonmua);
    }

    // GET: CHITIETDONMUAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CHITIETDONMUAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaChiTietDon,MaDonMua,MaChiTietYeuCau,MaHang,SoLuongDat,DonGiaMua,ThanhTien,DonMuaHang,ChiTietYeuCau,HangHoa,ChiTietGiaoHangs")] ChiTietDonMua chitietdonmua)
    {
        if (ModelState.IsValid)
        {
            _context.Add(chitietdonmua);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(chitietdonmua);
    }

    // GET: CHITIETDONMUAS/Edit/5
    public async Task<IActionResult> Edit(int? machitietdon)
    {
        if (machitietdon == null)
        {
            return NotFound();
        }

        var chitietdonmua = await _context.ChiTietDonMua.FindAsync(machitietdon);
        if (chitietdonmua == null)
        {
            return NotFound();
        }
        return View(chitietdonmua);
    }

    // POST: CHITIETDONMUAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? machitietdon, [Bind("MaChiTietDon,MaDonMua,MaChiTietYeuCau,MaHang,SoLuongDat,DonGiaMua,ThanhTien,DonMuaHang,ChiTietYeuCau,HangHoa,ChiTietGiaoHangs")] ChiTietDonMua chitietdonmua)
    {
        if (machitietdon != chitietdonmua.MaChiTietDon)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(chitietdonmua);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChiTietDonMuaExists(chitietdonmua.MaChiTietDon))
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
        return View(chitietdonmua);
    }

    // GET: CHITIETDONMUAS/Delete/5
    public async Task<IActionResult> Delete(int? machitietdon)
    {
        if (machitietdon == null)
        {
            return NotFound();
        }

        var chitietdonmua = await _context.ChiTietDonMua
            .FirstOrDefaultAsync(m => m.MaChiTietDon == machitietdon);
        if (chitietdonmua == null)
        {
            return NotFound();
        }

        return View(chitietdonmua);
    }

    // POST: CHITIETDONMUAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? machitietdon)
    {
        var chitietdonmua = await _context.ChiTietDonMua.FindAsync(machitietdon);
        if (chitietdonmua != null)
        {
            _context.ChiTietDonMua.Remove(chitietdonmua);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ChiTietDonMuaExists(int? machitietdon)
    {
        return _context.ChiTietDonMua.Any(e => e.MaChiTietDon == machitietdon);
    }
}
