using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace CyberWebSystem.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login(string email, string password)
        {
            return RedirectToAction("Index");
        }
    }
}
