using CyberWebSystem.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Runtime.CompilerServices;

namespace CyberWebSystem.Controllers
{
    public class LoginController : Controller
    {
        private MiContext _context;
        public LoginController(MiContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var usuario = await _context.Usuarios
                                        .Where(x => x.Email == email && x.Password == password)
                                        .FirstOrDefaultAsync();
            if (usuario != null)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                TempData["LoginError"] = "Cuenta o Password Incorrecto!";
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Index", "Login");
        }
    }
}
