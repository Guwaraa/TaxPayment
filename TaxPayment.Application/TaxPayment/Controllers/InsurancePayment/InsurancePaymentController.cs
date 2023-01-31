using Microsoft.AspNetCore.Mvc;
using TaxPayment.Common.InsurancePayment;
using TaxPaymet.Business.InsurancePayment;

namespace TaxPayment.Controllers.InsurancePayment
{
    public class InsurancePaymentController : Controller
    {
        private IInsurancePaymentBusiness _insurancePaymentBusiness;
        public InsurancePaymentController(IInsurancePaymentBusiness insurancePaymentBusiness)
        {
            _insurancePaymentBusiness = insurancePaymentBusiness;
        }
        public IActionResult Index()
        {
            var param = new InsurancePaymentParam
            {
                Flag = "GetGridDetailList",
            };
            var response = _insurancePaymentBusiness.GetGridDetailList(param);
            return View(response);
        }
        public IActionResult VerifyInsurancePaymentDetail(InsurancePaymentParam param)
        {
            param.Flag = "VerifyInsurancePaymentDetail";
            param.VerifiedBy = "admin";
            var response = _insurancePaymentBusiness.ManageInsurancePaymentDetail(param);
            return RedirectToAction("Index");
        }
        public IActionResult ApproveInsurancePaymentDetail(InsurancePaymentParam param)
        {
            param.Flag = "ApproveInsurancePaymentDetail";
            param.VerifiedBy = "admin";
            var response = _insurancePaymentBusiness.ManageInsurancePaymentDetail(param);
            return RedirectToAction("Index");
        }
        public IActionResult ViewInsurancePaymentDetail(string Id)
        {
            var param = new InsurancePaymentParam
            {
                Flag = "ViewInsurancePaymentDetail",
            };
            var response = _insurancePaymentBusiness.GetInsurancePaymentDetail(param);
            return RedirectToAction("Index");
        }
    }
}
