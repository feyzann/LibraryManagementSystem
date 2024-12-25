using System.Diagnostics;
using IleriWebProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace IleriWebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LibraryManagementSystemContext _libraryManagementSystemContext;

        public HomeController(ILogger<HomeController> logger, LibraryManagementSystemContext libraryManagementSystemContext)
        {
            _logger = logger;
            _libraryManagementSystemContext = libraryManagementSystemContext;
        }

        public IActionResult Index()
        {
            //var books = _libraryManagementSystemContext.Books.ToList(); EntityFramework linq sorgularý
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
