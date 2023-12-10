using CyberWebSystem.Context;
using CyberWebSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Runtime.CompilerServices;
using System.Security.Claims;

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
        //Esta funcion se encargar de la autentificacion con la base de datos de los usuarios
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var usuario = await _context.Usuarios
                                        .Where(x => x.Email == email && x.Password == password)
                                        .FirstOrDefaultAsync();
            if (usuario != null)
            {
                await SetUserCookie(usuario);
                return RedirectToAction("Index","Home");
            }
            else
            {
                TempData["LoginError"] = "Cuenta o Password Incorrecto!";
                return RedirectToAction("Index");
            }
        }
        //Se agregaron Cookies, para la seguridad de los usuarios
		private async Task SetUserCookie(Usuario usuario)
		{
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuario!.NombreCompleto!),
                new Claim(ClaimTypes.Email, usuario!.Email!),
				new Claim(ClaimTypes.NameIdentifier, usuario!.Id.ToString()),
				new Claim(ClaimTypes.Role, usuario!.Rol!.ToString()),
			};
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
		}
        //El Logout es funcion encargada de la salida o cierre de secion
		public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);//Seguridad cookies para usuarios
            return RedirectToAction("Index", "Login");
        }
    }
}
