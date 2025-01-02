using AspNetCoreHero.ToastNotification.Abstractions;
using IleriWebProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IleriWebProject.Controllers
{
    public class SignUpController : Controller
    {
        private readonly LibraryManagementSystemContext _context;
        private readonly INotyfService _notyf;

        public SignUpController(LibraryManagementSystemContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string fullName, string email, string password,string phoneNumber)
        {
            var userRole = 3;
            // Kullanıcı var mı kontrol et
            if (_context.Users.Any(u => u.Email == email))
            {
                _notyf.Error("This email is already registered!");
                return View("SignUp");
            }

            // RoleID'yi doğrula
            var role = _context.Roles.FirstOrDefault(r => r.RoleID == userRole);
            if (role == null)
            {
                ViewBag.Error = "Invalid role!";
                return View("SignUp");
            }
            _context.Database.ExecuteSqlInterpolated(
                        $"CALL AddNewUser({fullName}, {email}, {password}, {userRole})");

            return RedirectToAction("Login", "Login");
        }

    }
}

