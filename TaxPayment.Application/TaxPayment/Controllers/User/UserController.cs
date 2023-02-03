using Microsoft.AspNetCore.Mvc;

namespace TaxPayment.Controllers.User
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return RedirectToAction("Index","Home");
        }
    }
}
