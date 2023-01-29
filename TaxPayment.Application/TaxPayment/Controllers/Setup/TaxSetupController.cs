using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaxPayment.Common.TaxSetup;
using TaxPaymet.Business.Setup.TaxSetup;

namespace TaxPayment.Controllers.Setup
{
    public class TaxSetupController : Controller
    {
        private ITaxSetupBusiness _taxSetupBusiness;
        public TaxSetupController(ITaxSetupBusiness taxSetupBusiness)
        {
            _taxSetupBusiness = taxSetupBusiness;
        }
        public IActionResult Index()
        {
            var param = new
            {
                Flag = "GetRequiredDetailList",
            };
            var response = _taxSetupBusiness.GetRequiredDetailList(param);
            return View(response);
        }
        public IActionResult ManageTaxSetup()
        {
            var taxDetail = new TaxSetupViewModel();
            return View(taxDetail);
        }
        [HttpPost]
        public IActionResult AddTaxSetup(TaxSetupParam taxSetupParam)
        {
            taxSetupParam.Flag = "AddTaxSetupDetails";
            taxSetupParam.CreatedBy = "admin";
            var response = _taxSetupBusiness.ManageTaxSetupDetails(taxSetupParam);
            return RedirectToAction("Index");
        }
        public IActionResult UpdateTaxSetup(string RowId)
        {
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UpdateTaxSetup(TaxSetupViewModel taxSetupViewModel)
        {
            return RedirectToAction("Index");
        }
        public IActionResult UpdateTaxSetupStatus(string RowId)
        {
            return RedirectToAction("Index");
        }
    }
}
