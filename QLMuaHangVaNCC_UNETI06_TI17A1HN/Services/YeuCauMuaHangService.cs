// Họ và tên: Hoàng Thùy Dương
// Mã sinh viên: 23103100051
// Nội dung thực hiện: Module 3 - Lớp xử lý nghiệp vụ yêu cầu mua hàng: tìm kiếm/lọc/sắp xếp/phân trang,
// lập yêu cầu, chi tiết yêu cầu (gộp dòng trùng), gửi duyệt, xét duyệt, từ chối, hủy, xóa,
// và hàm kiểm tra "được phép lập đơn" cho Module 4.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Helpers;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.ViewModels;

// Định danh rõ ràng enum/class VaiTroHeThong lấy từ Models để giải quyết triệt để lỗi CS0104
using VaiTroHeThong = QLMuaHangVaNCC_UNETI06_TI17A1HN.Models.VaiTroHeThong;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Services;

public class KetQuaXuLy
{
    public bool ThanhCong { get; set; }
    public string ThongBao { get; set; } = string.Empty;
    public static KetQuaXuLy Ok(string thongBao) => new() { ThanhCong = true, ThongBao = thongBao };
    public static KetQuaXuLy Loi(string thongBao) => new() { ThanhCong = false, ThongBao = thongBao };
}

public class KetQuaXuLy<T> : KetQuaXuLy
{
    public T? DuLieu { get; set; }
    public static KetQuaXuLy<T> Ok(T duLieu, string thongBao) =>
        new() { ThanhCong = true, ThongBao = thongBao, DuLieu = duLieu };
    public static new KetQuaXuLy<T> Loi(string thongBao) =>
        new() { ThanhCong = false, ThongBao = thongBao };
}

public interface IYeuCauMuaHangService
{
    Task<PagedResult<YeuCauListItemViewModel>> TimKiemAsync(YeuCauFilterViewModel f, NguoiDungHienTai nd);
    Task<YeuCauDetailsViewModel?> LayChiTietAsync(int maYeuCau, NguoiDungHienTai nd);
    Task<YeuCauFormViewModel?> LayFormSuaAsync(int maYeuCau, NguoiDungHienTai nd);
    Task<List<SelectListItem>> DanhSachBoPhanAsync(int? maBoPhanDangChon = null);
    Task<List<SelectListItem>> DanhSachHangAsync();

    Task<KetQuaXuLy<int>> TaoAsync(YeuCauFormViewModel vm, NguoiDungHienTai nd);
    Task<KetQuaXuLy> SuaAsync(YeuCauFormViewModel vm, NguoiDungHienTai nd);
    Task<KetQuaXuLy> XoaAsync(int maYeuCau, NguoiDungHienTai nd);

    Task<KetQuaXuLy> ThemChiTietAsync(ChiTietYeuCauFormViewModel vm, NguoiDungHienTai nd);
    Task<ChiTietYeuCauFormViewModel?> LayDongDeSuaAsync(int maChiTiet, NguoiDungHienTai nd);
    Task<KetQuaXuLy> SuaChiTietAsync(ChiTietYeuCauFormViewModel vm, NguoiDungHienTai nd);
    Task<KetQuaXuLy> XoaChiTietAsync(int maChiTiet, NguoiDungHienTai nd);

    Task<KetQuaXuLy> GuiDuyetAsync(int maYeuCau, NguoiDungHienTai nd);
    Task<DuyetYeuCauViewModel?> LayFormDuyetAsync(int maYeuCau, NguoiDungHienTai nd);
    Task<KetQuaXuLy> DuyetAsync(DuyetYeuCauViewModel vm, NguoiDungHienTai nd);
    Task<KetQuaXuLy> DuyetToanBoAsync(int maYeuCau, NguoiDungHienTai nd);
    Task<KetQuaXuLy> TuChoiAsync(LyDoViewModel vm, NguoiDungHienTai nd);
    Task<KetQuaXuLy> HuyAsync(LyDoViewModel vm, NguoiDungHienTai nd);

    /// <summary>Module 4 gọi trước khi tạo đơn mua: yêu cầu phải Đã duyệt (hoặc Đã tạo đơn nếu còn số lượng) và có SoLuongDuyet > 0.</summary>
    Task<KetQuaXuLy> KiemTraCoTheLapDonAsync(int maYeuCau);
}

public class YeuCauMuaHangService : IYeuCauMuaHangService
{
    public const int PageSize = 10;
    private readonly QLMuaHangVaNCC_UNETI06_TI17A1HNContext _db;

    public YeuCauMuaHangService(QLMuaHangVaNCC_UNETI06_TI17A1HNContext db) => _db = db;

    // =====================================================================
    // PHẠM VI DỮ LIỆU THEO VAI TRÒ
    // =====================================================================
    private static IQueryable<YeuCauMuaHang> ApDungPhamVi(IQueryable<YeuCauMuaHang> q, NguoiDungHienTai nd)
    {
        switch (nd.VaiTro)
        {
            case VaiTroHeThong.Admin:
                return q;
            case VaiTroHeThong.NhanVienDeNghi:
                // Nhân viên chỉ thấy yêu cầu do mình lập
                return q.Where(y => y.NguoiLap == nd.MaTaiKhoan);
            case VaiTroHeThong.NguoiDuyet:
                // Người duyệt thấy mọi yêu cầu đã gửi (không xem bản Nháp của người khác)
                return q.Where(y => y.TrangThai != TrangThaiYeuCau.Nhap);
            case VaiTroHeThong.NhanVienMuaHang:
                // Nhân viên mua hàng chỉ thấy yêu cầu đã được duyệt trở đi
                return q.Where(y => y.TrangThai == TrangThaiYeuCau.DaDuyet
                                 || y.TrangThai == TrangThaiYeuCau.DaTaoDon
                                 || y.TrangThai == TrangThaiYeuCau.HoanThanh);
            default:
                return q.Where(y => false);
        }
    }

    /// <summary>Admin hoặc chính người lập (vai trò NhanVienDeNghi) mới được sửa/gửi duyệt/hủy.</summary>
    private static bool LaChuYeuCau(int nguoiLap, NguoiDungHienTai nd) =>
        nd.VaiTro == VaiTroHeThong.Admin ||
        (nd.VaiTro == VaiTroHeThong.NhanVienDeNghi && nguoiLap == nd.MaTaiKhoan);

    private async Task<YeuCauMuaHang?> TimYeuCauAsync(int maYeuCau, NguoiDungHienTai nd, bool kemChiTiet = false)
    {
        var q = ApDungPhamVi(_db.YeuCauMuaHang, nd);
        if (kemChiTiet) q = q.Include(y => y.ChiTietYeuCaus);
        return await q.FirstOrDefaultAsync(y => y.MaYeuCau == maYeuCau);
    }

    private const string KhongTimThay = "Không tìm thấy yêu cầu hoặc bạn không có quyền với yêu cầu này.";

    // =====================================================================
    // DANH SÁCH: Truy vấn -> Tìm kiếm -> Lọc -> Sắp xếp -> Phân trang
    // =====================================================================
    public async Task<PagedResult<YeuCauListItemViewModel>> TimKiemAsync(YeuCauFilterViewModel f, NguoiDungHienTai nd)
    {
        // 1. Truy vấn (IQueryable - chưa chạy SQL) + giới hạn theo quyền
        var q = ApDungPhamVi(_db.YeuCauMuaHang.AsNoTracking(), nd);

        // 2. Tìm kiếm: mã yêu cầu / tên bộ phận / tên người lập
        if (!string.IsNullOrWhiteSpace(f.TuKhoa))
        {
            var kw = f.TuKhoa.Trim();
            var phanSo = kw.StartsWith("YC", StringComparison.OrdinalIgnoreCase) ? kw[2..] : kw;
            var laSo = int.TryParse(phanSo, out var maTim);
            q = q.Where(y => (laSo && y.MaYeuCau == maTim)
                          || y.BoPhanDeNghi.TenBoPhan.Contains(kw)
                          || y.NguoiLapNavigation.HoTen.Contains(kw));
        }

        // 3. Lọc
        if (!string.IsNullOrWhiteSpace(f.TrangThai))
            q = q.Where(y => y.TrangThai == f.TrangThai);
        if (!string.IsNullOrWhiteSpace(f.MucDoUuTien))
            q = q.Where(y => y.MucDoUuTien == f.MucDoUuTien);
        if (f.TuNgay.HasValue)
        {
            var d = f.TuNgay.Value.Date;
            q = q.Where(y => y.NgayYeuCau >= d);
        }
        if (f.DenNgay.HasValue)
        {
            var d = f.DenNgay.Value.Date;
            q = q.Where(y => y.NgayYeuCau < d.AddDays(1));
        }
        if (f.CanTuNgay.HasValue)
        {
            var d = f.CanTuNgay.Value.Date;
            q = q.Where(y => y.NgayCanHang >= d);
        }
        if (f.CanDenNgay.HasValue)
        {
            var d = f.CanDenNgay.Value.Date;
            q = q.Where(y => y.NgayCanHang < d.AddDays(1));
        }

        // 4. Sắp xếp (luôn thêm khóa phụ MaYeuCau để thứ tự ổn định khi phân trang)
        q = f.SapXep switch
        {
            "ngay_asc" => q.OrderBy(y => y.NgayYeuCau).ThenBy(y => y.MaYeuCau),
            "uutien_desc" => q.OrderByDescending(y => y.MucDoUuTien == MucUuTien.Khan ? 4
                                                    : y.MucDoUuTien == MucUuTien.Cao ? 3
                                                    : y.MucDoUuTien == MucUuTien.BinhThuong ? 2 : 1)
                              .ThenByDescending(y => y.MaYeuCau),
            "uutien_asc" => q.OrderBy(y => y.MucDoUuTien == MucUuTien.Khan ? 4
                                         : y.MucDoUuTien == MucUuTien.Cao ? 3
                                         : y.MucDoUuTien == MucUuTien.BinhThuong ? 2 : 1)
                             .ThenByDescending(y => y.MaYeuCau),
            "canhang_asc" => q.OrderBy(y => y.NgayCanHang).ThenByDescending(y => y.MaYeuCau),
            "canhang_desc" => q.OrderByDescending(y => y.NgayCanHang).ThenByDescending(y => y.MaYeuCau),
            _ => q.OrderByDescending(y => y.NgayYeuCau).ThenByDescending(y => y.MaYeuCau) // "ngay_desc" mặc định
        };

        // 5. Phân trang (Skip/Take chạy trên SQL Server, không tải hết rồi cắt trong bộ nhớ)
        var tong = await q.CountAsync();
        var tongTrang = Math.Max(1, (int)Math.Ceiling(tong / (double)PageSize));
        var trang = Math.Clamp(f.Page, 1, tongTrang);

        var items = await q
            .Skip((trang - 1) * PageSize)
            .Take(PageSize)
            .Select(y => new YeuCauListItemViewModel
            {
                MaYeuCau = y.MaYeuCau,
                TenBoPhan = y.BoPhanDeNghi.TenBoPhan,
                NgayYeuCau = y.NgayYeuCau,
                NgayCanHang = y.NgayCanHang,
                MucDoUuTien = y.MucDoUuTien,
                TrangThai = y.TrangThai,
                TenNguoiLap = y.NguoiLapNavigation.HoTen,
                SoDong = y.ChiTietYeuCaus.Count(),
                TongSoLuongYeuCau = y.ChiTietYeuCaus.Sum(c => (int?)c.SoLuongYeuCau) ?? 0
            })
            .ToListAsync();

        return new PagedResult<YeuCauListItemViewModel>
        {
            Items = items,
            Page = trang,
            PageSize = PageSize,
            TotalItems = tong
        };
    }

    // =====================================================================
    // CHI TIẾT
    // =====================================================================
    public async Task<YeuCauDetailsViewModel?> LayChiTietAsync(int maYeuCau, NguoiDungHienTai nd)
    {
        var vm = await ApDungPhamVi(_db.YeuCauMuaHang.AsNoTracking(), nd)
            .Where(y => y.MaYeuCau == maYeuCau)
            .Select(y => new YeuCauDetailsViewModel
            {
                MaYeuCau = y.MaYeuCau,
                TenBoPhan = y.BoPhanDeNghi.TenBoPhan,
                NgayYeuCau = y.NgayYeuCau,
                NgayCanHang = y.NgayCanHang,
                MucDoUuTien = y.MucDoUuTien,
                LyDoMua = y.LyDoMua,
                NguoiLap = y.NguoiLap,
                TenNguoiLap = y.NguoiLapNavigation.HoTen,
                TrangThai = y.TrangThai,
                NgayDuyet = y.NgayDuyet,
                TenNguoiDuyet = y.NguoiDuyetNavigation != null ? y.NguoiDuyetNavigation.HoTen : null,
                LyDoTuChoiHuy = y.LyDoTuChoiHuy,
                Dong = y.ChiTietYeuCaus
                    .OrderBy(c => c.MaChiTietYeuCau)
                    .Select(c => new DongChiTietViewModel
                    {
                        MaChiTietYeuCau = c.MaChiTietYeuCau,
                        MaHang = c.MaHang,
                        TenHang = c.HangHoa.TenHang,
                        TenDonViTinh = c.HangHoa.DonViTinh.TenDonViTinh,
                        SoLuongYeuCau = c.SoLuongYeuCau,
                        SoLuongDuyet = c.SoLuongDuyet,
                        GhiChu = c.GhiChu
                    }).ToList()
            })
            .FirstOrDefaultAsync();

        if (vm is null) return null;

        var laChu = LaChuYeuCau(vm.NguoiLap, nd);
        var laAdmin = nd.VaiTro == VaiTroHeThong.Admin;
        vm.CoTheSua = laChu && vm.TrangThai == TrangThaiYeuCau.Nhap;
        vm.CoTheXoa = vm.CoTheSua;
        vm.CoTheGuiDuyet = vm.CoTheSua && vm.Dong.Count > 0;
        vm.CoTheDuyet = VaiTroHeThong.CoQuyenDuyet(nd.VaiTro) && vm.TrangThai == TrangThaiYeuCau.ChoDuyet;
        // Yêu cầu Đã duyệt chỉ được hủy khi CHƯA có đơn mua nào phát sinh từ nó
        var daCoDon = vm.TrangThai == TrangThaiYeuCau.DaDuyet &&
                      await _db.DonMuaHang.AnyAsync(d => d.MaYeuCau == maYeuCau);
        vm.CoTheHuy = (laChu && (vm.TrangThai == TrangThaiYeuCau.Nhap || vm.TrangThai == TrangThaiYeuCau.ChoDuyet))
                      || (laAdmin && vm.TrangThai == TrangThaiYeuCau.DaDuyet && !daCoDon);
        vm.ChiTietMoi.MaYeuCau = vm.MaYeuCau;
        return vm;
    }

    public async Task<YeuCauFormViewModel?> LayFormSuaAsync(int maYeuCau, NguoiDungHienTai nd)
    {
        var y = await ApDungPhamVi(_db.YeuCauMuaHang.AsNoTracking(), nd)
            .FirstOrDefaultAsync(x => x.MaYeuCau == maYeuCau);
        if (y is null || !LaChuYeuCau(y.NguoiLap, nd)) return null;

        return new YeuCauFormViewModel
        {
            MaYeuCau = y.MaYeuCau,
            MaBoPhan = y.MaBoPhan,
            NgayYeuCau = y.NgayYeuCau,
            NgayCanHang = y.NgayCanHang,
            MucDoUuTien = y.MucDoUuTien,
            LyDoMua = y.LyDoMua
        };
    }

    public async Task<List<SelectListItem>> DanhSachBoPhanAsync(int? maBoPhanDangChon = null) =>
        await _db.BoPhanDeNghi.AsNoTracking()
            .Where(b => b.TrangThai || b.MaBoPhan == maBoPhanDangChon)   // chỉ bộ phận đang hoạt động
            .OrderBy(b => b.TenBoPhan)
            .Select(b => new SelectListItem { Value = b.MaBoPhan.ToString(), Text = b.TenBoPhan })
            .ToListAsync();

    public async Task<List<SelectListItem>> DanhSachHangAsync() =>
        await _db.HangHoa.AsNoTracking()
            .Where(h => h.TrangThai)                                      // chỉ hàng đang hoạt động
            .OrderBy(h => h.TenHang)
            .Select(h => new SelectListItem
            {
                Value = h.MaHang.ToString(),
                Text = h.TenHang + " (" + h.DonViTinh.TenDonViTinh + ")"
            })
            .ToListAsync();

    // =====================================================================
    // LẬP / SỬA / XÓA YÊU CẦU
    // =====================================================================
    public async Task<KetQuaXuLy<int>> TaoAsync(YeuCauFormViewModel vm, NguoiDungHienTai nd)
    {
        if (nd.VaiTro != VaiTroHeThong.Admin && nd.VaiTro != VaiTroHeThong.NhanVienDeNghi)
            return KetQuaXuLy<int>.Loi("Bạn không có quyền lập yêu cầu mua hàng.");

        var boPhanHopLe = await _db.BoPhanDeNghi.AnyAsync(b => b.MaBoPhan == vm.MaBoPhan && b.TrangThai);
        if (!boPhanHopLe)
            return KetQuaXuLy<int>.Loi("Bộ phận đề nghị không tồn tại hoặc đã ngừng hoạt động.");

        var y = new YeuCauMuaHang
        {
            MaBoPhan = vm.MaBoPhan!.Value,
            NgayYeuCau = vm.NgayYeuCau!.Value.Date,
            NgayCanHang = vm.NgayCanHang!.Value.Date,
            MucDoUuTien = vm.MucDoUuTien!,
            LyDoMua = vm.LyDoMua!.Trim(),
            NguoiLap = nd.MaTaiKhoan,
            TrangThai = TrangThaiYeuCau.Nhap
        };
        _db.YeuCauMuaHang.Add(y);
        await _db.SaveChangesAsync();
        return KetQuaXuLy<int>.Ok(y.MaYeuCau, "Đã tạo yêu cầu (Nháp). Hãy thêm các mặt hàng cần mua.");
    }

    public async Task<KetQuaXuLy> SuaAsync(YeuCauFormViewModel vm, NguoiDungHienTai nd)
    {
        var y = await TimYeuCauAsync(vm.MaYeuCau, nd);
        if (y is null) return KetQuaXuLy.Loi(KhongTimThay);
        if (!LaChuYeuCau(y.NguoiLap, nd)) return KetQuaXuLy.Loi("Bạn không có quyền sửa yêu cầu này.");
        if (y.TrangThai != TrangThaiYeuCau.Nhap)
            return KetQuaXuLy.Loi("Chỉ được sửa yêu cầu ở trạng thái Nháp. Sau khi gửi duyệt không được thay đổi nội dung mua.");

        // Bộ phận: nếu đổi thì bộ phận mới phải đang hoạt động
        if (vm.MaBoPhan != y.MaBoPhan)
        {
            var ok = await _db.BoPhanDeNghi.AnyAsync(b => b.MaBoPhan == vm.MaBoPhan && b.TrangThai);
            if (!ok) return KetQuaXuLy.Loi("Bộ phận đề nghị không tồn tại hoặc đã ngừng hoạt động.");
        }

        // Ngày yêu cầu không đổi khi sửa -> kiểm tra ngày cần hàng so với ngày yêu cầu gốc trong DB
        var canHang = vm.NgayCanHang!.Value.Date;
        if (canHang < y.NgayYeuCau.Date)
            return KetQuaXuLy.Loi("Ngày cần hàng không được trước ngày yêu cầu.");
        if (canHang > y.NgayYeuCau.Date.AddDays(365))
            return KetQuaXuLy.Loi("Ngày cần hàng không nên cách ngày yêu cầu quá 1 năm.");

        y.MaBoPhan = vm.MaBoPhan!.Value;
        y.NgayCanHang = canHang;
        y.MucDoUuTien = vm.MucDoUuTien!;
        y.LyDoMua = vm.LyDoMua!.Trim();
        await _db.SaveChangesAsync();
        return KetQuaXuLy.Ok("Đã cập nhật yêu cầu.");
    }

    public async Task<KetQuaXuLy> XoaAsync(int maYeuCau, NguoiDungHienTai nd)
    {
        var y = await TimYeuCauAsync(maYeuCau, nd, kemChiTiet: true);
        if (y is null) return KetQuaXuLy.Loi(KhongTimThay);
        if (!LaChuYeuCau(y.NguoiLap, nd)) return KetQuaXuLy.Loi("Bạn không có quyền xóa yêu cầu này.");
        if (y.TrangThai != TrangThaiYeuCau.Nhap)
            return KetQuaXuLy.Loi("Chỉ được xóa yêu cầu ở trạng thái Nháp. Yêu cầu đã gửi duyệt phải dùng chức năng Hủy để giữ lịch sử.");

        _db.ChiTietYeuCau.RemoveRange(y.ChiTietYeuCaus);
        _db.YeuCauMuaHang.Remove(y);
        await _db.SaveChangesAsync();
        return KetQuaXuLy.Ok("Đã xóa yêu cầu Nháp.");
    }

    // =====================================================================
    // CHI TIẾT YÊU CẦU (chỉ khi Nháp)
    // =====================================================================
    private static string? KiemTraChoSuaChiTiet(YeuCauMuaHang? y, NguoiDungHienTai nd)
    {
        if (y is null) return KhongTimThay;
        if (!LaChuYeuCau(y.NguoiLap, nd)) return "Bạn không có quyền thay đổi chi tiết của yêu cầu này.";
        if (y.TrangThai != TrangThaiYeuCau.Nhap)
            return "Chỉ được thêm/sửa/xóa chi tiết khi yêu cầu ở trạng thái Nháp.";
        return null;
    }

    public async Task<KetQuaXuLy> ThemChiTietAsync(ChiTietYeuCauFormViewModel vm, NguoiDungHienTai nd)
    {
        var y = await TimYeuCauAsync(vm.MaYeuCau, nd, kemChiTiet: true);
        var loi = KiemTraChoSuaChiTiet(y, nd);
        if (loi != null) return KetQuaXuLy.Loi(loi);

        if (vm.SoLuongYeuCau is null or <= 0)
            return KetQuaXuLy.Loi("Số lượng yêu cầu phải lớn hơn 0.");

        var hang = await _db.HangHoa.AsNoTracking().FirstOrDefaultAsync(h => h.MaHang == vm.MaHang);
        if (hang is null) return KetQuaXuLy.Loi("Hàng hóa không tồn tại.");
        if (!hang.TrangThai) return KetQuaXuLy.Loi($"Hàng hóa '{hang.TenHang}' đã ngừng hoạt động, không thể đưa vào yêu cầu.");

        var ghiChu = string.IsNullOrWhiteSpace(vm.GhiChu) ? null : vm.GhiChu.Trim();
        var dongCu = y!.ChiTietYeuCaus.FirstOrDefault(c => c.MaHang == hang.MaHang);
        if (dongCu != null)
        {
            // Không cho trùng mặt hàng nhiều dòng -> gộp số lượng
            if ((long)dongCu.SoLuongYeuCau + vm.SoLuongYeuCau.Value > 1_000_000)
                return KetQuaXuLy.Loi("Tổng số lượng sau khi gộp vượt giới hạn 1.000.000.");
            dongCu.SoLuongYeuCau += vm.SoLuongYeuCau.Value;
            if (ghiChu != null) dongCu.GhiChu = ghiChu;
            await _db.SaveChangesAsync();
            return KetQuaXuLy.Ok($"'{hang.TenHang}' đã có trong yêu cầu nên đã gộp số lượng (tổng {dongCu.SoLuongYeuCau}).");
        }

        _db.ChiTietYeuCau.Add(new ChiTietYeuCau
        {
            MaYeuCau = y.MaYeuCau,
            MaHang = hang.MaHang,
            SoLuongYeuCau = vm.SoLuongYeuCau.Value,
            SoLuongDuyet = 0,
            GhiChu = ghiChu
        });
        await _db.SaveChangesAsync();
        return KetQuaXuLy.Ok($"Đã thêm '{hang.TenHang}' vào yêu cầu.");
    }

    public async Task<ChiTietYeuCauFormViewModel?> LayDongDeSuaAsync(int maChiTiet, NguoiDungHienTai nd)
    {
        var c = await _db.ChiTietYeuCau.AsNoTracking()
            .Include(x => x.YeuCauMuaHang)
            .Include(x => x.HangHoa)
            .FirstOrDefaultAsync(x => x.MaChiTietYeuCau == maChiTiet);
        if (c?.YeuCauMuaHang is null) return null;
        if (!LaChuYeuCau(c.YeuCauMuaHang.NguoiLap, nd)) return null;

        return new ChiTietYeuCauFormViewModel
        {
            MaYeuCau = c.MaYeuCau,
            MaChiTietYeuCau = c.MaChiTietYeuCau,
            MaHang = c.MaHang,
            TenHang = c.HangHoa?.TenHang,
            SoLuongYeuCau = c.SoLuongYeuCau,
            GhiChu = c.GhiChu
        };
    }

    public async Task<KetQuaXuLy> SuaChiTietAsync(ChiTietYeuCauFormViewModel vm, NguoiDungHienTai nd)
    {
        var y = await TimYeuCauAsync(vm.MaYeuCau, nd, kemChiTiet: true);
        var loi = KiemTraChoSuaChiTiet(y, nd);
        if (loi != null) return KetQuaXuLy.Loi(loi);

        var dong = y!.ChiTietYeuCaus.FirstOrDefault(c => c.MaChiTietYeuCau == vm.MaChiTietYeuCau);
        if (dong is null) return KetQuaXuLy.Loi("Không tìm thấy dòng chi tiết.");
        if (vm.SoLuongYeuCau is null or <= 0) return KetQuaXuLy.Loi("Số lượng yêu cầu phải lớn hơn 0.");

        dong.SoLuongYeuCau = vm.SoLuongYeuCau.Value;
        dong.GhiChu = string.IsNullOrWhiteSpace(vm.GhiChu) ? null : vm.GhiChu.Trim();
        await _db.SaveChangesAsync();
        return KetQuaXuLy.Ok("Đã cập nhật dòng chi tiết.");
    }

    public async Task<KetQuaXuLy> XoaChiTietAsync(int maChiTiet, NguoiDungHienTai nd)
    {
        var c = await _db.ChiTietYeuCau.FirstOrDefaultAsync(x => x.MaChiTietYeuCau == maChiTiet);
        if (c is null) return KetQuaXuLy.Loi("Không tìm thấy dòng chi tiết.");

        var y = await TimYeuCauAsync(c.MaYeuCau, nd);
        var loi = KiemTraChoSuaChiTiet(y, nd);
        if (loi != null) return KetQuaXuLy.Loi(loi);

        _db.ChiTietYeuCau.Remove(c);
        await _db.SaveChangesAsync();
        return KetQuaXuLy.Ok("Đã xóa dòng chi tiết.");
    }

    // =====================================================================
    // GỬI DUYỆT: Nháp -> Chờ duyệt
    // =====================================================================
    public async Task<KetQuaXuLy> GuiDuyetAsync(int maYeuCau, NguoiDungHienTai nd)
    {
        var y = await TimYeuCauAsync(maYeuCau, nd, kemChiTiet: true);
        if (y is null) return KetQuaXuLy.Loi(KhongTimThay);
        if (!LaChuYeuCau(y.NguoiLap, nd)) return KetQuaXuLy.Loi("Bạn không có quyền gửi duyệt yêu cầu này.");
        if (y.TrangThai != TrangThaiYeuCau.Nhap)
            return KetQuaXuLy.Loi("Chỉ yêu cầu ở trạng thái Nháp mới được gửi duyệt.");

        var dongHopLe = y.ChiTietYeuCaus.Where(c => c.SoLuongYeuCau > 0).ToList();
        if (dongHopLe.Count == 0)
            return KetQuaXuLy.Loi("Yêu cầu phải có ít nhất một mặt hàng với số lượng > 0 mới được gửi duyệt.");

        // Hàng hóa / bộ phận phải còn hoạt động tại thời điểm gửi
        var maHangs = dongHopLe.Select(c => c.MaHang).ToList();
        var hangNgung = await _db.HangHoa
            .Where(h => maHangs.Contains(h.MaHang) && !h.TrangThai)
            .Select(h => h.TenHang).ToListAsync();
        if (hangNgung.Count > 0)
            return KetQuaXuLy.Loi("Các hàng sau đã ngừng hoạt động, hãy xóa khỏi yêu cầu: " + string.Join(", ", hangNgung));

        var boPhanOk = await _db.BoPhanDeNghi.AnyAsync(b => b.MaBoPhan == y.MaBoPhan && b.TrangThai);
        if (!boPhanOk) return KetQuaXuLy.Loi("Bộ phận đề nghị đã ngừng hoạt động.");

        y.TrangThai = TrangThaiYeuCau.ChoDuyet;
        y.LyDoTuChoiHuy = null;
        foreach (var c in y.ChiTietYeuCaus) c.SoLuongDuyet = 0;
        await _db.SaveChangesAsync();
        return KetQuaXuLy.Ok("Đã gửi duyệt yêu cầu.");
    }

    // =====================================================================
    // XÉT DUYỆT (chỉ Admin): Chờ duyệt -> Đã duyệt / Từ chối
    // =====================================================================
    public async Task<DuyetYeuCauViewModel?> LayFormDuyetAsync(int maYeuCau, NguoiDungHienTai nd)
    {
        if (!VaiTroHeThong.CoQuyenDuyet(nd.VaiTro)) return null;

        return await _db.YeuCauMuaHang.AsNoTracking()
            .Where(y => y.MaYeuCau == maYeuCau)
            .Select(y => new DuyetYeuCauViewModel
            {
                MaYeuCau = y.MaYeuCau,
                TenBoPhan = y.BoPhanDeNghi.TenBoPhan,
                LyDoMua = y.LyDoMua,
                TrangThai = y.TrangThai,
                Dong = y.ChiTietYeuCaus.OrderBy(c => c.MaChiTietYeuCau)
                    .Select(c => new DuyetDongViewModel
                    {
                        MaChiTietYeuCau = c.MaChiTietYeuCau,
                        TenHang = c.HangHoa.TenHang,
                        TenDonViTinh = c.HangHoa.DonViTinh.TenDonViTinh,
                        SoLuongYeuCau = c.SoLuongYeuCau,
                        SoLuongDuyet = c.SoLuongYeuCau   // mặc định duyệt đủ, người duyệt có thể giảm
                    }).ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<KetQuaXuLy> DuyetAsync(DuyetYeuCauViewModel vm, NguoiDungHienTai nd)
    {
        if (!VaiTroHeThong.CoQuyenDuyet(nd.VaiTro))
            return KetQuaXuLy.Loi("Chỉ Admin/Người duyệt mới được xét duyệt yêu cầu.");

        var y = await TimYeuCauAsync(vm.MaYeuCau, nd, kemChiTiet: true);
        if (y is null) return KetQuaXuLy.Loi(KhongTimThay);
        if (y.TrangThai != TrangThaiYeuCau.ChoDuyet)
            return KetQuaXuLy.Loi("Chỉ yêu cầu đang Chờ duyệt mới được xét duyệt.");

        var dsMaHang = y.ChiTietYeuCaus.Select(c => c.MaHang).ToList();
        var tenHang = await _db.HangHoa
            .Where(h => dsMaHang.Contains(h.MaHang))
            .ToDictionaryAsync(h => h.MaHang, h => h.TenHang);

        // Kiểm tra TOÀN BỘ trước, hợp lệ hết mới ghi
        var capNhat = new List<(ChiTietYeuCau dong, int soLuongDuyet)>();
        foreach (var dong in y.ChiTietYeuCaus)
        {
            var nhap = vm.Dong.FirstOrDefault(d => d.MaChiTietYeuCau == dong.MaChiTietYeuCau);
            var ten = tenHang.GetValueOrDefault(dong.MaHang, $"dòng #{dong.MaChiTietYeuCau}");
            if (nhap?.SoLuongDuyet is null)
                return KetQuaXuLy.Loi($"Thiếu số lượng duyệt của '{ten}'.");
            if (nhap.SoLuongDuyet < 0)
                return KetQuaXuLy.Loi($"Số lượng duyệt của '{ten}' không được âm.");
            if (nhap.SoLuongDuyet > dong.SoLuongYeuCau)
                return KetQuaXuLy.Loi($"Số lượng duyệt của '{ten}' ({nhap.SoLuongDuyet}) không được vượt số lượng yêu cầu ({dong.SoLuongYeuCau}).");
            capNhat.Add((dong, nhap.SoLuongDuyet.Value));
        }

        if (!capNhat.Any(x => x.soLuongDuyet > 0))
            return KetQuaXuLy.Loi("Phải có ít nhất một mặt hàng được duyệt với số lượng > 0. Nếu không duyệt mặt hàng nào, hãy chọn Từ chối.");

        foreach (var (dong, sl) in capNhat) dong.SoLuongDuyet = sl;
        y.TrangThai = TrangThaiYeuCau.DaDuyet;
        y.NgayDuyet = DateTime.Today;
        y.NguoiDuyet = nd.MaTaiKhoan;
        y.LyDoTuChoiHuy = null;
        await _db.SaveChangesAsync();
        return KetQuaXuLy.Ok("Đã duyệt yêu cầu.");
    }

    public async Task<KetQuaXuLy> DuyetToanBoAsync(int maYeuCau, NguoiDungHienTai nd)
    {
        if (!VaiTroHeThong.CoQuyenDuyet(nd.VaiTro))
            return KetQuaXuLy.Loi("Chỉ Admin/Người duyệt mới được xét duyệt yêu cầu.");

        var y = await TimYeuCauAsync(maYeuCau, nd, kemChiTiet: true);
        if (y is null) return KetQuaXuLy.Loi(KhongTimThay);
        if (y.TrangThai != TrangThaiYeuCau.ChoDuyet)
            return KetQuaXuLy.Loi("Chỉ yêu cầu đang Chờ duyệt mới được xét duyệt.");
        if (!y.ChiTietYeuCaus.Any(c => c.SoLuongYeuCau > 0))
            return KetQuaXuLy.Loi("Yêu cầu không có chi tiết hợp lệ để duyệt.");

        foreach (var c in y.ChiTietYeuCaus) c.SoLuongDuyet = c.SoLuongYeuCau;   // duyệt toàn bộ
        y.TrangThai = TrangThaiYeuCau.DaDuyet;
        y.NgayDuyet = DateTime.Today;
        y.NguoiDuyet = nd.MaTaiKhoan;
        y.LyDoTuChoiHuy = null;
        await _db.SaveChangesAsync();
        return KetQuaXuLy.Ok("Đã duyệt toàn bộ yêu cầu.");
    }

    public async Task<KetQuaXuLy> TuChoiAsync(LyDoViewModel vm, NguoiDungHienTai nd)
    {
        if (!VaiTroHeThong.CoQuyenDuyet(nd.VaiTro))
            return KetQuaXuLy.Loi("Chỉ Admin/Người duyệt mới được từ chối yêu cầu.");
        if (string.IsNullOrWhiteSpace(vm.LyDo))
            return KetQuaXuLy.Loi("Phải nhập lý do khi từ chối yêu cầu.");

        var y = await TimYeuCauAsync(vm.MaYeuCau, nd, kemChiTiet: true);
        if (y is null) return KetQuaXuLy.Loi(KhongTimThay);
        if (y.TrangThai != TrangThaiYeuCau.ChoDuyet)
            return KetQuaXuLy.Loi("Chỉ yêu cầu đang Chờ duyệt mới được từ chối.");

        foreach (var c in y.ChiTietYeuCaus) c.SoLuongDuyet = 0;
        y.TrangThai = TrangThaiYeuCau.TuChoi;
        y.NgayDuyet = DateTime.Today;
        y.NguoiDuyet = nd.MaTaiKhoan;
        y.LyDoTuChoiHuy = vm.LyDo.Trim();
        await _db.SaveChangesAsync();
        return KetQuaXuLy.Ok("Đã từ chối yêu cầu.");
    }

    // =====================================================================
    // HỦY: Nháp/Chờ duyệt (chủ yêu cầu hoặc Admin), Đã duyệt (chỉ Admin)
    // Yêu cầu "Đã tạo đơn" trở đi không được hủy ở đây (Module 4 giữ trạng thái này khi phát sinh đơn).
    // =====================================================================
    public async Task<KetQuaXuLy> HuyAsync(LyDoViewModel vm, NguoiDungHienTai nd)
    {
        if (string.IsNullOrWhiteSpace(vm.LyDo))
            return KetQuaXuLy.Loi("Phải nhập lý do khi hủy yêu cầu.");

        var y = await TimYeuCauAsync(vm.MaYeuCau, nd);
        if (y is null) return KetQuaXuLy.Loi(KhongTimThay);

        var laAdmin = nd.VaiTro == VaiTroHeThong.Admin;
        var choPhep = (LaChuYeuCau(y.NguoiLap, nd) &&
                       (y.TrangThai == TrangThaiYeuCau.Nhap || y.TrangThai == TrangThaiYeuCau.ChoDuyet))
                      || (laAdmin && y.TrangThai == TrangThaiYeuCau.DaDuyet);
        if (choPhep && y.TrangThai == TrangThaiYeuCau.DaDuyet &&
            await _db.DonMuaHang.AnyAsync(d => d.MaYeuCau == y.MaYeuCau))
            return KetQuaXuLy.Loi("Yêu cầu đã phát sinh đơn mua hàng nên không thể hủy.");
        if (!choPhep)
            return KetQuaXuLy.Loi($"Không thể hủy yêu cầu ở trạng thái '{TrangThaiYeuCau.Nhan(y.TrangThai)}' hoặc bạn không có quyền hủy.");

        y.TrangThai = TrangThaiYeuCau.DaHuy;
        y.LyDoTuChoiHuy = vm.LyDo.Trim();
        await _db.SaveChangesAsync();
        return KetQuaXuLy.Ok("Đã hủy yêu cầu.");
    }

    // =====================================================================
    // DÀNH CHO MODULE 4: không lập đơn từ yêu cầu chưa duyệt
    // =====================================================================
    public async Task<KetQuaXuLy> KiemTraCoTheLapDonAsync(int maYeuCau)
    {
        var y = await _db.YeuCauMuaHang.AsNoTracking()
            .Where(x => x.MaYeuCau == maYeuCau)
            .Select(x => new
            {
                x.TrangThai,
                CoDongDuocDuyet = x.ChiTietYeuCaus.Any(c => c.SoLuongDuyet > 0)
            })
            .FirstOrDefaultAsync();

        if (y is null) return KetQuaXuLy.Loi("Yêu cầu mua hàng không tồn tại.");

        if (y.TrangThai != TrangThaiYeuCau.DaDuyet && y.TrangThai != TrangThaiYeuCau.DaTaoDon)
            return KetQuaXuLy.Loi($"Không thể lập đơn mua từ yêu cầu ở trạng thái '{TrangThaiYeuCau.Nhan(y.TrangThai)}'. Chỉ yêu cầu Đã duyệt mới được lập đơn.");

        if (!y.CoDongDuocDuyet)
            return KetQuaXuLy.Loi("Yêu cầu không có mặt hàng nào được duyệt số lượng > 0.");

        // Phần "còn số lượng cần đặt" (SoLuongDuyet - TongSoLuongDaDatHieuLuc) do Module 4 kiểm tra.
        return KetQuaXuLy.Ok("Yêu cầu hợp lệ để lập đơn mua.");
    }
}