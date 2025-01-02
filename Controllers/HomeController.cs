using System.Diagnostics;
using AspNetCoreHero.ToastNotification.Abstractions;
using IleriWebProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IleriWebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LibraryManagementSystemContext _libraryManagementSystemContext;
        private readonly INotyfService _notyf;

        public HomeController(ILogger<HomeController> logger, LibraryManagementSystemContext libraryManagementSystemContext, INotyfService notyf)
        {
            _logger = logger;
            _libraryManagementSystemContext = libraryManagementSystemContext;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            // Kitaplarý çekiyoruz
            var books = _libraryManagementSystemContext.Books
                .Include(b => b.Category)  // Kitaplarýn kategorisini de dahil ediyoruz
                .ToList();

            // Eðer kitap yoksa, hata mesajý gösterelim
            if (books == null || !books.Any())
            {
                _notyf.Error("Hiç kitap bulunamadý.");
                return View(new List<Book>());
            }

            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult BorrowBook(int bookId)
        {
            var book = _libraryManagementSystemContext.Books
                .FirstOrDefault(b => b.BookId == bookId);

            if (book == null)
            {
                _notyf.Error("Kitap bulunamadý.");
                return RedirectToAction("Index");
            }

            // Kiralama iþlemi yapýlabilir
            // Örneðin, BorrowTransaction kaydýný ekleyelim
            var transaction = new Borrowtransaction
            {
                UserId = 1, // Örnek kullanýcý id'si. Gerçek kullanýcý id'si buraya eklenmeli.
                BookId = book.BookId,
                BorrowDate = DateOnly.FromDateTime(DateTime.Now),
            };

            _libraryManagementSystemContext.Borrowtransactions.Add(transaction);
            _libraryManagementSystemContext.SaveChanges();

            _notyf.Success("Kitap baþarýyla kiralandý!");

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
