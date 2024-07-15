using Microsoft.AspNetCore.Mvc;

namespace KutuphaneUygulamasi.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddBook()
        {
            return View();
        }

        public IActionResult ListBooks()
        {
            return View();
        }

        public IActionResult ListMembers()
        {
            return View();
        }

        public IActionResult AddMember()
        {
            return View();
        }

        public IActionResult SetMemberRole()
        {
            return View();
        }

    }
}
