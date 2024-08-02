 using KutuphaneUygulamasi.Data;
using KutuphaneUygulamasi.Models;
using KutuphaneUygulamasi.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KutuphaneUygulamasi.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); // Çıkış yapıldıktan sonra yönlendirilecek sayfa
        }





        // GET: Admin
        public IActionResult Index()
        {
            return View();
        }

        // Kitap ekleme işlemi
        public IActionResult AddBook()
        {
            return RedirectToAction("AddBook", "Create");
        }




        public IActionResult ListBooks()
        {
            return RedirectToAction("ListBooks", "Create");
        }



        // POST: Admin/AddMember
        [HttpPost]
        public async Task<IActionResult> AddMember([FromBody] RegisterViewModel model)
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
                    return Ok(new { success = true });
                }
                else
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return BadRequest(new { success = false, errors });
                }
            }

            var modelErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(new { success = false, errors = modelErrors });
        }


        // GET: Admin/ListMembers
        [HttpGet]
        public async Task<IActionResult> ListMembers()
        {
            var users = await _userManager.Users
                .Select(u => new 
                {
                    u.Id,
                    u.FirstName,
                    u.LastName,
                    u.Email
                })
                .ToListAsync();

            return Ok(users);
        }

        // GET: Admin/GetMember/{id}
        [HttpGet]
        public async Task<IActionResult> GetMember(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var userModel = new 
            {
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                user.Address
            };

            return Ok(userModel);
        }

        // POST: Admin/EditMember
        [HttpPost]
        public async Task<IActionResult> EditMember([FromBody] ApplicationUser model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Address = model.Address;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(new { success = true });
            }
            else
            {
                // Hata mesajlarını döndür
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(new { success = false, errors });
            }
        }

        // DELETE: Admin/DeleteMember/{id}
        [HttpDelete]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(new { success = true });
            }
            else
            {
                return BadRequest(new { success = false });
            }
        }





        // POST: Admin/SetMemberRole
        [HttpPost]
        public async Task<IActionResult> SetMemberRole(int userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return Json(new { success = false, message = "Üye bulunamadı." });
            }

            var result = await _userManager.AddToRoleAsync(user, role);
            if (result.Succeeded)
            {
                return Json(new { success = true });
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Json(new { success = false, errors });
            }
        }
    }
}