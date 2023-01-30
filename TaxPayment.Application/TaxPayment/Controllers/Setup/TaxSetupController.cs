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
        public IActionResult UpdateTaxSetup(string TaxCode)
        {
            var param = new
            {
                Flag = "GetTaxSetupDetails",
                TaxCode = TaxCode
            };
            var response = _taxSetupBusiness.GetTaxSetupDetails(param);
            return View("ManageTaxSetup",response);
        }
        [HttpPost]
        public IActionResult UpdateTaxSetup(TaxSetupParam taxSetupParam)
        {
            taxSetupParam.Flag = "UpdateTaxSetupDetails";
            taxSetupParam.ModifiedBy = "admin";
            var response = _taxSetupBusiness.ManageTaxSetupDetails(taxSetupParam);
            return RedirectToAction("Index");
        }
        public IActionResult UpdateTaxSetupStatus(string RowId)
        {
            return RedirectToAction("Index");
        }
    }
}
