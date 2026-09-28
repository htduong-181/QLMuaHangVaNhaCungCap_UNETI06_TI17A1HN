
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

public class DonViTinhsController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

    public DonViTinhsController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
    {
        _context = context;
    }

    // GET: DONVITINHS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.DonViTinh.ToListAsync());
    }

    // GET: DONVITINHS/Details/5
    public async Task<IActionResult> Details(int? madonvitinh)
    {
        if (madonvitinh == null)
        {
            return NotFound();
        }

        var donvitinh = await _context.DonViTinh
            .FirstOrDefaultAsync(m => m.MaDonViTinh == madonvitinh);
        if (donvitinh == null)
        {
            return NotFound();
        }

        return View(donvitinh);
    }

    // GET: DONVITINHS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: DONVITINHS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaDonViTinh,TenDonViTinh,MoTa,TrangThai,HangHoas")] DonViTinh donvitinh)
    {
        if (ModelState.IsValid)
        {
            _context.Add(donvitinh);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(donvitinh);
    }

    // GET: DONVITINHS/Edit/5
    public async Task<IActionResult> Edit(int? madonvitinh)
    {
        if (madonvitinh == null)
        {
            return NotFound();
        }

        var donvitinh = await _context.DonViTinh.FindAsync(madonvitinh);
        if (donvitinh == null)
        {
            return NotFound();
        }
        return View(donvitinh);
    }

    // POST: DONVITINHS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? madonvitinh, [Bind("MaDonViTinh,TenDonViTinh,MoTa,TrangThai,HangHoas")] DonViTinh donvitinh)
    {
        if (madonvitinh != donvitinh.MaDonViTinh)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(donvitinh);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonViTinhExists(donvitinh.MaDonViTinh))
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
        return View(donvitinh);
    }

    // GET: DONVITINHS/Delete/5
    public async Task<IActionResult> Delete(int? madonvitinh)
    {
        if (madonvitinh == null)
        {
            return NotFound();
        }

        var donvitinh = await _context.DonViTinh
            .FirstOrDefaultAsync(m => m.MaDonViTinh == madonvitinh);
        if (donvitinh == null)
        {
            return NotFound();
        }

        return View(donvitinh);
    }

    // POST: DONVITINHS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? madonvitinh)
    {
        var donvitinh = await _context.DonViTinh.FindAsync(madonvitinh);
        if (donvitinh != null)
        {
            _context.DonViTinh.Remove(donvitinh);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DonViTinhExists(int? madonvitinh)
    {
        return _context.DonViTinh.Any(e => e.MaDonViTinh == madonvitinh);
    }
}
