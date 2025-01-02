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
            // Kitaplar� �ekiyoruz
            var books = _libraryManagementSystemContext.VBooksByCategories.ToList();

            // E�er kitap yoksa, hata mesaj� g�sterelim
            if (books == null || !books.Any())
            {
                _notyf.Error("Hi� kitap bulunamad�.");
                return View(new List<Book>());
            }

            return View(books);
        }

        public IActionResult MyTransactionBooks()
        {
            var userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            var result = _libraryManagementSystemContext.VUserBorrowedBooks.Where(x => x.UserID == userId).ToList();
            return View(result);
        }

        public IActionResult BorrowBook(int bookId)
        {
            var book = _libraryManagementSystemContext.Books
                .FirstOrDefault(b => b.BookId == bookId);

            if (book == null)
            {
                _notyf.Error("Kitap bulunamad�.");
                return RedirectToAction("Index");
            }
            var userId = HttpContext.Session.GetInt32("UserId") ?? 1;

            // Kiralama i�lemi yap�labilir
            _libraryManagementSystemContext.Database.ExecuteSqlInterpolated($"CALL BorrowBook({userId}, {bookId})");


            _notyf.Success("Kitap ba�ar�yla kiraland�!");

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
