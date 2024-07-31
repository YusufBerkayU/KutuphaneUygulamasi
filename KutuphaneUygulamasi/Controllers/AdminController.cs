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