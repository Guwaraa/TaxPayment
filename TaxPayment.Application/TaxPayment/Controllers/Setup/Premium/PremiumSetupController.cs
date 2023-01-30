using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using TaxPayment.Common.Premium;
using TaxPaymet.Business.Setup.PremiumSetup;

namespace TaxPayment.Controllers.Setup.Premium
{
    public class PremiumSetupController : Controller
    {
        private IPremiumBusiness _premiumBusiness;

        public PremiumSetupController(IPremiumBusiness premiumBusiness)
        {
            _premiumBusiness = premiumBusiness;
        }
        public IActionResult Index()
        {
            var param = new
            {
                Flag = "GetRequiredDetailList",
            };
            var response = _premiumBusiness.GetRequiredDetailList(param);
            return View(response);
        }
        public IActionResult ManagePremiumSetup(string id)
        {
            //var PremiumList = _premiumBusiness.GetRequiredDetails();
            //if (id == null)
            //{
            //    return View(PremiumList);
            //}
            //var premiumDetailsParam = new PremiumDetailsParam
            //{
            //    Flag = "GetRequiredSubGroupDetails"
            //};
            //var response = _premiumBusiness.GetSubGroupUpdateDetails(premiumDetailsParam);
            //response.PremiumList = PremiumList.PremiumList;

            return View();
        }
        [HttpPost]
        public IActionResult AddPremiumSetup(PremiumDetailsParam premiumSetupParam)
        {
            premiumSetupParam.Flag = "AddPremiumDetails";
            premiumSetupParam.CreatedBy = User.Identity.Name;
            var response = _premiumBusiness.ManagePremiumSetupDetails(premiumSetupParam);
            return RedirectToAction("Index");
        }
    }
}
