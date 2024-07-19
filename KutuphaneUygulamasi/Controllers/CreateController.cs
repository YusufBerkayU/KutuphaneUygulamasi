using KutuphaneUygulamasi.Data;
using KutuphaneUygulamasi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
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
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(Book model, IFormFile pdfFile)
        {
            if (ModelState.IsValid)
            {
                if (pdfFile != null && pdfFile.Length > 0)
                {
                    var fileExtension = Path.GetExtension(pdfFile.FileName);
                    var fileName = $"{Guid.NewGuid()}{fileExtension}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdfs", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await pdfFile.CopyToAsync(stream);
                    }

                    model.PdfFilePath = "/pdfs/" + fileName;
                }

                _context.Books.Add(model);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, errors = errors });
        }

        [HttpGet]
        public async Task<IActionResult> ListBooks()
        {
            var books = await _context.Books.ToListAsync();
            return PartialView(books);
        }
    }
}