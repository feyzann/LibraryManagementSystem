using AspNetCoreHero.ToastNotification.Abstractions;
using IleriWebProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IleriWebProject.Controllers
{
    public class BooksController : Controller
    {
        private LibraryManagementSystemContext LibraryManagementSystemContext;
        private readonly INotyfService _notyf;

        public BooksController(LibraryManagementSystemContext libraryManagementSystemContext, INotyfService notyf)
        {
            LibraryManagementSystemContext = libraryManagementSystemContext;
            _notyf = notyf;
        }

        public IActionResult AddBook()
        {
            // Veritabanından status ve kategori listelerini alıyoruz
            var status = LibraryManagementSystemContext.Bookstatuses.ToList();
            var categories = LibraryManagementSystemContext.Categories.ToList();

            // StatusList'i dolduruyoruz
            ViewBag.StatusList = status.Select(s => new SelectListItem
            {
                Text = s.StatusDescription, // Status adını uygun property ile değiştirin
                Value = s.StatusId.ToString() // Status ID'yi uygun property ile değiştirin
            }).ToList();

            // CategoryList'i dolduruyoruz
            ViewBag.CategoryList = categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName, // Kategori adını uygun property ile değiştirin
                Value = c.CategoryId.ToString() // Kategori ID'yi uygun property ile değiştirin
            }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            LibraryManagementSystemContext.Books.Add(book);
            LibraryManagementSystemContext.SaveChanges();

            _notyf.Success("Kitap başarıyla eklendi");

            return RedirectToAction("Index", "Home");
        }


        public IActionResult BookDelete(int Id)
        {
            // Veritabanından silinecek kitabı bul
            var book = LibraryManagementSystemContext.Books.FirstOrDefault(b => b.BookId == Id);

            if (book != null)
            {
                // Kitabı veritabanından sil
                LibraryManagementSystemContext.Books.Remove(book);
                LibraryManagementSystemContext.SaveChanges();

                // Başarı mesajı göster
                _notyf.Success("Kitap başarıyla silindi");
            }
            else
            {
                // Hata mesajı göster
                _notyf.Error("Silinecek kitap bulunamadı");
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
