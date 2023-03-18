using Microsoft.AspNetCore.Mvc;
using TaxPayment.Common.TaxPayement;

namespace TaxPayment.Controllers.Calculate
{
    public class CalulateController : Controller
    {
        public IActionResult PremiumCalulator()
        {
            return View();
        }
        
        
        public IActionResult TaxCalulator()
        {
            return View();
        }
    }
}
