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
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBook(Book model, IFormFile pdfFile)
        {
            if (ModelState.IsValid)
            {
                if (pdfFile != null && pdfFile.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/pdfs", pdfFile.FileName);

                    // Save the file to the server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await pdfFile.CopyToAsync(stream);
                    }

                    model.PdfFilePath = filePath;
                }

                _context.Books.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("ListBooks","Admin");
            }

            return View(model);
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
