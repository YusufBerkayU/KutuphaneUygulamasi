using KutuphaneUygulamasi.Data;
using KutuphaneUygulamasi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneUygulamasi.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
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
