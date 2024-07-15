using Microsoft.AspNetCore.Mvc;

namespace KutuphaneUygulamasi.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
