using Microsoft.AspNetCore.Mvc;
using KartMaster.Models;

namespace KartMaster.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(Utilizador model)
        {
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(Utilizador model)
        {

            return RedirectToAction("Login");
        }
    }
}
