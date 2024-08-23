using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneUygulamasi.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
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


    }   
}
