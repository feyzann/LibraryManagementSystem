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
            var books = _libraryManagementSystemContext.VBooksByCategories.ToList();

            // Eðer kitap yoksa, hata mesajý gösterelim
            if (books == null || !books.Any())
            {
                _notyf.Error("Hiç kitap bulunamadý.");
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
                _notyf.Error("Kitap bulunamadý.");
                return RedirectToAction("Index");
            }

            if (book.StockCount <= 0)
            {
                _notyf.Error("Kitap Stokta Mevcut Deðil");
                return RedirectToAction("Index");
            }

            if (book.StatusId == 1)
            {
                _notyf.Error("Kitap mevcut deðil durumundadýr");
                return RedirectToAction("Index");
            }
            var userId = HttpContext.Session.GetInt32("UserId") ?? 1;


            // Kiralama iþlemi yapýlabilir
            _libraryManagementSystemContext.Database.ExecuteSqlInterpolated($"CALL BorrowBook({userId}, {bookId})");

            if (book.StockCount <= 1)
            {
                book.StatusId = 1;
                _libraryManagementSystemContext.SaveChanges();
            }


            _notyf.Success("Kitap baþarýyla kiralandý!");

            return RedirectToAction("Index");
        }

        public IActionResult BorrowTransactionBook(int transId)
        {
            var transBook = _libraryManagementSystemContext.Borrowtransactions
                .FirstOrDefault(b => b.TransactionId == transId);

            if (transBook == null)
            {
                _notyf.Error("Kiralama bulunamadý.");
                return RedirectToAction("Index");
            }
            var userId = HttpContext.Session.GetInt32("UserId") ?? 1;

            transBook.ReturnDate = DateOnly.FromDateTime(DateTime.Now);

            var books = _libraryManagementSystemContext.Books.Where(b => b.BookId == transBook.BookId).FirstOrDefault();
            books.StockCount += 1;

            if (books.StockCount >= 1)
            {
                books.StatusId = 2;
            }

            _libraryManagementSystemContext.SaveChanges();

            _notyf.Success("Kiralama iadesi baþarýlý");

            return RedirectToAction("Index");
        }

        public IActionResult GetCategories()
        {
            var categories = _libraryManagementSystemContext.VCategoryBookCounts.ToList();
            return PartialView("_PartialCategories", categories);  // Partial View adýný veriyoruz
        }

        public IActionResult GetLast10Books()
        {
            var categories = _libraryManagementSystemContext.Top10Books.ToList();
            return PartialView("_PartialLast10Books", categories);  // Partial View adýný veriyoruz
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // **Yeni Eklenen Kýsým**
        public IActionResult MembersList()
        {
            // `v_members_list` görünümünden verileri çekiyoruz
            var membersList = _libraryManagementSystemContext.VMemberList.ToList();

            // Eðer veri yoksa, hata mesajý gösterelim
            if (membersList == null || !membersList.Any())
            {
                _notyf.Warning("Hiç üye bulunamadý.");
                return View(new List<VMemberList>()); // Boþ bir liste döndür
            }

            // Verileri View'e gönderiyoruz
            return View(membersList);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
