using Microsoft.AspNetCore.Mvc;

namespace CyberWebSystem.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
