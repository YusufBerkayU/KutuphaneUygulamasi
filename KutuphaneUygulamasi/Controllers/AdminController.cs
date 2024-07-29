using KutuphaneUygulamasi.Data;
using KutuphaneUygulamasi.Models;
using KutuphaneUygulamasi.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KutuphaneUygulamasi.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddBookContent()
        {
            // AddBook formunu döndür
            return Content(@"
                <h3>Kitap Ekle</h3>
                <form action='/Create/AddBook' method='post' enctype='multipart/form-data'>
                    <div class='form-group'>
                        <label for='Title'>Başlık</label>
                        <input type='text' class='form-control' id='Title' name='Title' required>
                    </div>
                    <div class='form-group'>
                        <label for='Author'>Yazar</label>
                        <input type='text' class='form-control' id='Author' name='Author' required>
                    </div>
                    <div class='form-group'>
                        <label for='Description'>Açıklama</label>
                        <textarea class='form-control' id='Description' name='Description' required></textarea>
                    </div>
                    <div class='form-group'>
                        <label for='PdfFile'>PDF Dosyası</label>
                        <input type='file' class='form-control' id='PdfFile' name='PdfFile' required>
                    </div>
                    <button type='submit' class='btn btn-primary'>Ekle</button>
                </form>");
        }

        public async Task<IActionResult> ListBooksContent()
        {
            // Kitap listesini döndür
            var books = await _context.Books.ToListAsync();
            var booksHtml = "<h3>Kitap Listesi</h3><ul class='list-group'>";
            foreach (var book in books)
            {
                booksHtml += $"<li class='list-group-item'>{book.Title} - {book.Author}</li>";
            }
            booksHtml += "</ul>";
            return Content(booksHtml);
        }

        public async Task<IActionResult> ListMembersContent()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersHtml = "<h3>Üye Listesi</h3><ul class='list-group'>";
            foreach (var user in users)
            {
                usersHtml += $"<li class='list-group-item'>{user.Email}</li>";
            }
            usersHtml += "</ul>";
            return Content(usersHtml);
        }

        [HttpGet]
        public IActionResult AddMemberContent()
        {
            // AddMember formunu döndür
            return Content(@"
                <h3>Üye Ekle</h3>
                <form action='/Admin/AddMember' method='post'>
                    <div class='form-group'>
                        <label for='Email'>E-posta</label>
                        <input type='email' class='form-control' id='Email' name='Email' required>
                    </div>
                    <div class='form-group'>
                        <label for='FirstName'>Ad</label>
                        <input type='text' class='form-control' id='FirstName' name='FirstName' required>
                    </div>
                    <div class='form-group'>
                        <label for='LastName'>Soyad</label>
                        <input type='text' class='form-control' id='LastName' name='LastName' required>
                    </div>
                    <div class='form-group'>
                        <label for='Address'>Adres</label>
                        <input type='text' class='form-control' id='Address' name='Address' required>
                    </div>
                    <div class='form-group'>
                        <label for='Password'>Şifre</label>
                        <input type='password' class='form-control' id='Password' name='Password' required>
                    </div>
                    <button type='submit' class='btn btn-primary'>Ekle</button>
                </form>");
        }

        [HttpPost]
        public async Task<IActionResult> AddMember(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Content(@"
                <h3>Üye Ekle</h3>
                <form action='/Admin/AddMember' method='post'>
                    <div class='form-group'>
                        <label for='Email'>E-posta</label>
                        <input type='email' class='form-control' id='Email' name='Email' value='" + model.Email + @"' required>
                    </div>
                    <div class='form-group'>
                        <label for='FirstName'>Ad</label>
                        <input type='text' class='form-control' id='FirstName' name='FirstName' value='" + model.FirstName + @"' required>
                    </div>
                    <div class='form-group'>
                        <label for='LastName'>Soyad</label>
                        <input type='text' class='form-control' id='LastName' name='LastName' value='" + model.LastName + @"' required>
                    </div>
                    <div class='form-group'>
                        <label for='Address'>Adres</label>
                        <input type='text' class='form-control' id='Address' name='Address' value='" + model.Address + @"' required>
                    </div>
                    <div class='form-group'>
                        <label for='Password'>Şifre</label>
                        <input type='password' class='form-control' id='Password' name='Password' required>
                    </div>
                    <button type='submit' class='btn btn-primary'>Ekle</button>
                </form>");
        }

        [HttpGet]
        public async Task<IActionResult> SetMemberRoleContent()
        {
            var users = await _userManager.Users.ToListAsync();
            var model = new SetMemberRoleViewModel
            {
                Users = users.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = u.Email
                }).ToList()
            };

            var usersHtml = "<h3>Üye Rolü Belirle</h3>";
            usersHtml += @"
                <form action='/Admin/SetMemberRole' method='post'>
                    <div class='form-group'>
                        <label for='UserId'>Üye</label>
                        <select class='form-control' id='UserId' name='UserId'>";
            foreach (var user in model.Users)
            {
                usersHtml += $"<option value='{user.Value}'>{user.Text}</option>";
            }
            usersHtml += @"
                        </select>
                    </div>
                    <div class='form-group'>
                        <label for='Role'>Rol</label>
                        <input type='text' class='form-control' id='Role' name='Role' required>
                    </div>
                    <button type='submit' class='btn btn-primary'>Belirle</button>
                </form>";

            return Content(usersHtml);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetMemberRole(SetMemberRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    // Mevcut rolleri kaldır
                    var currentRoles = await _userManager.GetRolesAsync(user);
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    if (!removeResult.Succeeded)
                    {
                        ModelState.AddModelError("", "Failed to remove user roles");
                        return RedirectToAction("Index");
                    }

                    // Yeni rol ekle
                    var addResult = await _userManager.AddToRoleAsync(user, model.Role);
                    if (addResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    foreach (var error in addResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            var users = await _userManager.Users.ToListAsync();
            model.Users = users.Select(u => new SelectListItem
            {
                Value = u.Id,
                Text = u.Email
            }).ToList();

            return Content(@"
                <h3>Üye Rolü Belirle</h3>
                <form action='/Admin/SetMemberRole' method='post'>
                    <div class='form-group'>
                        <label for='UserId'>Üye</label>
                        <select class='form-control' id='UserId' name='UserId'>" + string.Join("", model.Users.Select(u => $"<option value='{u.Value}'>{u.Text}</option>")) + @"
                        </select>
                    </div>
                    <div class='form-group'>
                        <label for='Role'>Rol</label>
                        <input type='text' class='form-control' id='Role' name='Role' required>
                    </div>
                    <button type='submit' class='btn btn-primary'>Belirle</button>
                </form>");
        }
    }
}
