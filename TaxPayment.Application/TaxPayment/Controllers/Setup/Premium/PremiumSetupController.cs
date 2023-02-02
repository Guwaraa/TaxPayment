using ISolutionVersionNext.Shared.GridHelpers;
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
            return View();
        }
        [HttpPost]
        public async Task<string> GetGridDetails(GridDetails param)
        {
            var agentTypeDetails = new GridParam
            {
                DisplayLength = param.length,
                DisplayStart = param.start,
                SortDir = param.order[0].dir,
                SortCol = param.order[0].column,
                Flag = "GetRequiredDetailList",
                Search = param.search.value,
                UserName = User.Identity.Name,
            };
            var agentType = await _premiumBusiness.GetPremiumSetupLists(agentTypeDetails);
            var agentTypeLists = new HtmlGrid<PremiumDetails>();
            agentTypeLists.aaData = agentType;
            var firstDefault = agentType.FirstOrDefault();
            if (firstDefault != null)
            {
                agentTypeLists.iTotalDisplayRecords = Convert.ToInt32(firstDefault.FilterCount);
                agentTypeLists.iTotalRecords = Convert.ToInt32(firstDefault.FilterCount);
            }
            var result = JsonConvert.SerializeObject(agentTypeLists);
            return result;
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
                RowId= id,
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
            return RedirectToAction("Index").WithAlertMessage(response.Code, response.Message); 
        }
        public IActionResult UpdatePremiumSetupStatus(string RowId)
        {
            var premiumSetupParam = new PremiumDetailsParam
            {
                Flag = "UpdatePremiumSetupStatus",
                RowId =RowId,
            };
            var response = _premiumBusiness.ManagePremiumSetupDetails(premiumSetupParam);
            return RedirectToAction("Index").WithAlertMessage(response.Code, response.Message);

        }
    }
}
