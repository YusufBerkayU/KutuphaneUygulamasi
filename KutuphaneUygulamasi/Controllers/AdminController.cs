using KutuphaneUygulamasi.Data;
using KutuphaneUygulamasi.Models;
using KutuphaneUygulamasi.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

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




    
        // GET: Admin
        public IActionResult Index()
        {
            return View();
        }

        // POST: Admin/AddMember
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    // Üye başarılı bir şekilde oluşturuldu
                    return Ok(new { success = true });
                }
                else
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return Ok(new { success = false, errors });
                }
            }

            var modelErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Ok(new { success = false, errors = modelErrors });
        }



        // POST: Admin/SetMemberRole
        [HttpPost]
        public IActionResult SetMemberRole(int userId, string role)
        {
            // Burada üyenin rolünü güncellemek için gerekli kodları ekle
            return Json(new { success = true }); // Başarılı yanıt için JSON döndür
        }
    }
}