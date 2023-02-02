using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaxPayment.Models;

namespace TaxPayment.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       
    }
}