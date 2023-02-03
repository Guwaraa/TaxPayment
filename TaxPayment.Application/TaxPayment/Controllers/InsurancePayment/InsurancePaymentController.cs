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

        public IActionResult Payment()
        {
            var param = new
            {
                Flag = "GetRequiredDetail",
                Userid = HttpContext.Session.GetString("UserId")
            };
            var response = _insurancePaymentBusiness.GetRequiredDetails(param);
            return View(response);
        }
        public IActionResult UserPayment()
        {
            var param = new InsurancePaymentParam
            {
                Flag = "GetRequiredDetailList",
                UserId = HttpContext.Session.GetString("UserId"),
            };
            var response = _insurancePaymentBusiness.GetGridDetailList(param);
            return View(response);
        }
        [HttpPost]
        public IActionResult AddInsurancePayemnt(InsurancePaymentParam param)
        {
            param.Flag = "AddInsurancePayemnt";
            param.UserId = HttpContext.Session.GetString("UserId");
            var response = _insurancePaymentBusiness.ManageInsurancePaymentDetail(param);
            return View();
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
