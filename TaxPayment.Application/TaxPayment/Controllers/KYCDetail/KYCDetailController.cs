using Microsoft.AspNetCore.Mvc;
using TaxPayment.Common.KYCDetail;
using TaxPaymet.Business.KYCDetail;

namespace TaxPayment.Controllers.KYCDetail
{
    public class KYCDetailController : Controller
    {
        private IKYCDetailBusiness _kYCDetailBusiness;
        public KYCDetailController(IKYCDetailBusiness kYCDetailBusiness)
        {
            _kYCDetailBusiness = kYCDetailBusiness;
        }
        public IActionResult Index()
        {
            var param = new KYCParam
            {
                Flag = "GetGridDetailList",
            };
            var response = _kYCDetailBusiness.GetGridDetailList(param);
            return View(response);
        }
        public IActionResult VerifyKYCDetail(KYCParam param)
        {
            param.Flag = "VerifyKYCDetail";
            param.VerifiedBy = "admin";
            var response = _kYCDetailBusiness.ManageKYCDetail(param);
            return RedirectToAction("Index");
        }
        public IActionResult ApproveKYCDetail(KYCParam param)
        {
            param.Flag = "ApproveKYCDetail";
            param.VerifiedBy = "admin";
            var response = _kYCDetailBusiness.ManageKYCDetail(param);
            return RedirectToAction("Index");
        }
        public IActionResult ViewKYCDetail(string Id)
        {
            var param = new KYCParam
            {
                Flag="ViewKYCDetail",
            };
            var response = _kYCDetailBusiness.GetKYCDetail(param);
            return RedirectToAction("Index");
        }
    }
}
