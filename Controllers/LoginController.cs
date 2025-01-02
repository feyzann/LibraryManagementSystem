using AspNetCoreHero.ToastNotification.Abstractions;
using IleriWebProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IleriWebProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly LibraryManagementSystemContext _context;
        private readonly INotyfService _notyf;

        public LoginController(LibraryManagementSystemContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // E-posta ile kullanıcıyı bul
            var user = _context.Users.FirstOrDefault(u => u.Email == username);

            if (user != null && user.Password == password) // Güvenlik için şifreyi hash'lemeniz önerilir
            {
                _notyf.Success("Login Success");
                HttpContext.Session.SetInt32("UserId", user.UserId);
                // Başarılı giriş
                return RedirectToAction("Index", "Home");
            }

            _notyf.Error("Invalid username or password.");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}


