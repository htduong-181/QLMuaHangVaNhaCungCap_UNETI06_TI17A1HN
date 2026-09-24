using Microsoft.AspNetCore.Mvc;
using QLMuaHangVaNCC_UNETI06_TI17A1HN.Models;
using System.Diagnostics;

namespace QLMuaHangVaNCC_UNETI06_TI17A1HN.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
