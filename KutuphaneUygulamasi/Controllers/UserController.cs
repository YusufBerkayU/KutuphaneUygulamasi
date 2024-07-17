using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneUygulamasi.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // Kullanıcı ana sayfası
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddBook()
        {
            return RedirectToAction("AddBook", "Create");
        }


        public IActionResult ListBooks()
        {
            return RedirectToAction("ListBooks", "Create");
        }


        // Kitap seçme sayfası
        public IActionResult SelectBook()
        {
            return View();
        }
    }
}
