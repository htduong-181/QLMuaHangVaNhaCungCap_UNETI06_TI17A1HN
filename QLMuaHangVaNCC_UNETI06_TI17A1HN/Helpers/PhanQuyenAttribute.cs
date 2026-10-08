// Họ và tên: Lê Tiến Công
// Mã sinh viên: 23103100050
// Nội dung thực hiện: Chức năng phân quyền tài khoản, loại hàng, đơn vị tính

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Helpers
{
    // [PhanQuyen]                      -> chỉ cần đăng nhập
    // [PhanQuyen(VaiTroHeThong.Admin)] -> chỉ Admin
    public class PhanQuyenAttribute : ActionFilterAttribute
    {
        private readonly string[] _vaiTroChoPhep;
        public PhanQuyenAttribute(params string[] vaiTroChoPhep) => _vaiTroChoPhep = vaiTroChoPhep;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            if (session.GetInt32("MaTaiKhoan") == null)
            {
                context.Result = new RedirectToActionResult("DangNhap", "TaiKhoans", null);
                return;
            }
            var vaiTro = session.GetString("VaiTro");
            if (_vaiTroChoPhep.Length > 0 && !_vaiTroChoPhep.Contains(vaiTro))
                context.Result = new RedirectToActionResult("TuChoiTruyCap", "TaiKhoans", null);
        }
    }
}
