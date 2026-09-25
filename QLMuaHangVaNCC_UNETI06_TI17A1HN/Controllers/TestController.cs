using Microsoft.AspNetCore.Mvc;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Data;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Controllers
{
    public class TestController : Controller
    {
        private readonly DbConnection _context;

        public TestController(DbConnection context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var result = new
            {
                TaiKhoan = _context.TaiKhoans.Count(),

                BoPhanDeNghi = _context.BoPhanDeNghis.Count(),

                LoaiHang = _context.LoaiHangs.Count(),

                DonViTinh = _context.DonViTinhs.Count(),

                HangHoa = _context.HangHoas.Count(),

                NhaCungCap = _context.NhaCungCaps.Count(),

                NhaCungCapHangHoa = _context.NhaCungCapHangHoas.Count(),

                YeuCauMuaHang = _context.YeuCauMuaHangs.Count(),

                ChiTietYeuCau = _context.ChiTietYeuCaus.Count(),

                DonMuaHang = _context.DonMuaHangs.Count(),

                ChiTietDonMua = _context.ChiTietDonMuas.Count(),

                LanGiaoHang = _context.LanGiaoHangs.Count(),

                ChiTietGiaoHang = _context.ChiTietGiaoHangs.Count(),

                ThanhToanDonMua = _context.ThanhToanDonMuas.Count(),

                LichSuTrangThai = _context.LichSuTrangThais.Count()
            };


            return Json(result);
        }
    }
}