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

        // Kitap ekleme sayfası
        public IActionResult AddBook()
        {
            return View();
        }

        // Kitap seçme sayfası
        public IActionResult SelectBook()
        {
            return View();
        }
    }
}
