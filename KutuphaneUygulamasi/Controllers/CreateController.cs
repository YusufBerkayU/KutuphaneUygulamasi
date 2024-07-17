using KutuphaneUygulamasi.Data;
using KutuphaneUygulamasi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace KutuphaneUygulamasi.Controllers
{
    public class CreateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CreateController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
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

                return RedirectToAction("ListBooks");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ListBooks()
        {
            var books = await _context.Books.ToListAsync();
            return View(books);
        }
    }
}
