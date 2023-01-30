using Microsoft.AspNetCore.Mvc;

namespace TaxPayment.Controllers.TaxPayment
{
    public class TaxPaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
