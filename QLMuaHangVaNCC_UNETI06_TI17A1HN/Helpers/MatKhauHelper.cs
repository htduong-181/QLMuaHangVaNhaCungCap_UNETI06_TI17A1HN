// Họ và tên: Lê Tiến Công
// Mã sinh viên: 23103100050
// Nội dung thực hiện: Chức năng phân quyền tài khoản, loại hàng, đơn vị tính

using System.Security.Cryptography;
using System.Text;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Helpers
{
    public static class MatKhauHelper
    {
        public static string Hash(string matKhau) =>
            Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(matKhau)));
    }
}