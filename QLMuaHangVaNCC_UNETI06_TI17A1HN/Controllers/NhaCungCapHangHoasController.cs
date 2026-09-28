
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;

public class NhaCungCapHangHoasController : Controller
{
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _context;

    public NhaCungCapHangHoasController(QLMuaHangVaNCC_UNETI06_TI17A1HNContext context)
    {
        _context = context;
    }

    // GET: NHACUNGCAPHANGHOAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.NhaCungCapHangHoa.ToListAsync());
    }

    // GET: NHACUNGCAPHANGHOAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var nhacungcaphanghoa = await _context.NhaCungCapHangHoa
            .FirstOrDefaultAsync(m => m.Id == id);
        if (nhacungcaphanghoa == null)
        {
            return NotFound();
        }

        return View(nhacungcaphanghoa);
    }

    // GET: NHACUNGCAPHANGHOAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: NHACUNGCAPHANGHOAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,MaNhaCungCap,MaHang,DonGiaBao,NgayCapNhatGia,ThoiGianGiaoDuKien,TrangThai,NhaCungCap,HangHoa")] NhaCungCapHangHoa nhacungcaphanghoa)
    {
        if (ModelState.IsValid)
        {
            _context.Add(nhacungcaphanghoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(nhacungcaphanghoa);
    }

    // GET: NHACUNGCAPHANGHOAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var nhacungcaphanghoa = await _context.NhaCungCapHangHoa.FindAsync(id);
        if (nhacungcaphanghoa == null)
        {
            return NotFound();
        }
        return View(nhacungcaphanghoa);
    }

    // POST: NHACUNGCAPHANGHOAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,MaNhaCungCap,MaHang,DonGiaBao,NgayCapNhatGia,ThoiGianGiaoDuKien,TrangThai,NhaCungCap,HangHoa")] NhaCungCapHangHoa nhacungcaphanghoa)
    {
        if (id != nhacungcaphanghoa.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(nhacungcaphanghoa);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhaCungCapHangHoaExists(nhacungcaphanghoa.Id))
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
        return View(nhacungcaphanghoa);
    }

    // GET: NHACUNGCAPHANGHOAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var nhacungcaphanghoa = await _context.NhaCungCapHangHoa
            .FirstOrDefaultAsync(m => m.Id == id);
        if (nhacungcaphanghoa == null)
        {
            return NotFound();
        }

        return View(nhacungcaphanghoa);
    }

    // POST: NHACUNGCAPHANGHOAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var nhacungcaphanghoa = await _context.NhaCungCapHangHoa.FindAsync(id);
        if (nhacungcaphanghoa != null)
        {
            _context.NhaCungCapHangHoa.Remove(nhacungcaphanghoa);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool NhaCungCapHangHoaExists(int? id)
    {
        return _context.NhaCungCapHangHoa.Any(e => e.Id == id);
    }
}
