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
            if (pdfFile != null && pdfFile.Length > 0)
            {
                // GUID kullanarak benzersiz bir dosya adı oluşturun
                var fileExtension = Path.GetExtension(pdfFile.FileName); // Dosya uzantısını alın
                var fileName = $"{Guid.NewGuid()}{fileExtension}"; // GUID ve uzantıyı birleştirin
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdfs", fileName);

                // Dosyayı kaydet
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await pdfFile.CopyToAsync(stream);
                }

                // Dosya yolunu veritabanına kaydedin
                model.PdfFilePath = "/pdfs/" + fileName;
            }

            
            if (ModelState.IsValid)
            {
                _context.Books.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("ListBooks", "Create");
            }

            // ModelState geçersizse, doğrulama hatalarını kontrol edin
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
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
