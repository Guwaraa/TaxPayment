using ISolutionVersionNext.UtilityHelpers.Alert;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using TaxPayment.Common.Premium;
using TaxPayment.Common.TaxSetup;
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
            var PremiumList = _premiumBusiness.GetRequiredDetails();
            if (id == null)
            {
                return View(PremiumList);
            }
            var premiumDetailsParam = new PremiumViewModel
            {
                Flag = "GetRequiredPremiumDetails"
            };
            var response = _premiumBusiness.GetPremiumUpdateDetails(premiumDetailsParam);
            response.VechicleCategoryList = PremiumList.VechicleCategoryList;
            response.FiscalYearList = PremiumList.FiscalYearList;
            response.ProvinceList = PremiumList.ProvinceList;
            response.InsuranceCompanyList = PremiumList.InsuranceCompanyList;
            return View(response);
        }
        [HttpPost]
        public IActionResult AddPremiumSetup(PremiumDetailsParam premiumSetupParam)
        {
            premiumSetupParam.Flag = "AddPremiumDetails";
            premiumSetupParam.CreatedBy = User.Identity.Name;
            var response = _premiumBusiness.ManagePremiumSetupDetails(premiumSetupParam);
            return RedirectToAction("Index").WithAlertMessage(response.Code, response.Message); ;
        }
    
        [HttpPost]
        public IActionResult UpdatePremiumSetup(PremiumDetailsParam premiumSetupParam)
        {
            premiumSetupParam.Flag = "UpdatePremiumSetupDetails";
            premiumSetupParam.ModifiedBy = User.Identity.Name;
            var response = _premiumBusiness.ManagePremiumSetupDetails(premiumSetupParam);
            return RedirectToAction("Index").WithAlertMessage(response.Code, response.Message); ;
        }
    }
}
