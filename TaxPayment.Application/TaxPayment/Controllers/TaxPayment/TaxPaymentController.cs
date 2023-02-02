using Microsoft.AspNetCore.Mvc;
using TaxPayment.Common.TaxPayement;
using TaxPaymet.Business.TaxPayement;

namespace TaxPayment.Controllers.TaxPayment
{
    public class TaxPaymentController : Controller
    {
        private ITaxPayementBusiness _taxPayementBusiness;
        public TaxPaymentController(ITaxPayementBusiness taxPayementBusiness)
        {
            _taxPayementBusiness = taxPayementBusiness;
        }
        public IActionResult Index()
        {
            var param = new TaxPayementParam
            {
                Flag = "GetGridDetailList",
            };
            var response = _taxPayementBusiness.GetGridDetailList(param);
            return View(response);
        }
        public IActionResult Payemnt()
        {
            return View();
        }
        public IActionResult VerifyTaxPayementDetail(TaxPayementParam param)
        {
            param.Flag = "VerifyTaxPayementDetail";
            param.VerifiedBy = "admin";
            var response = _taxPayementBusiness.ManageTaxPayementDetail(param);
            return RedirectToAction("Index");
        }
        public IActionResult ApproveTaxPayementDetail(TaxPayementParam param)
        {
            param.Flag = "ApproveTaxPayementDetail";
            param.VerifiedBy = "admin";
            var response = _taxPayementBusiness.ManageTaxPayementDetail(param);
            return RedirectToAction("Index");
        }
        public IActionResult ViewTaxPayementDetail(string Id)
        {
            var param = new TaxPayementParam
            {
                Flag = "ViewTaxPayementDetail",
            };
            var response = _taxPayementBusiness.GetTaxPayementDetail(param);
            return RedirectToAction("Index");
        }
    }
}
