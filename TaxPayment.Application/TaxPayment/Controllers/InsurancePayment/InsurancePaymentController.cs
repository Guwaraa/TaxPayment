using ISolutionVersionNext.Shared.GridHelpers;
using ISolutionVersionNext.UtilityHelpers.Alert;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaxPayment.Common.InsurancePayment;
using TaxPayment.Common.KYCDetail;
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
                Flag = "GetGridDetailList",
                Search = param.search.value,
                UserName = User.Identity.Name,
            };
            var agentType = await _insurancePaymentBusiness.GetKycLists(agentTypeDetails);
            var agentTypeLists = new HtmlGrid<InsurancePayementDetails>();
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
        public IActionResult VerifyInsurancePaymentDetail(string id)
        {
            if (id == null)
                return RedirectToAction("Index").WithAlertMessage("111", "Could not perform Verify task.");
            InsurancePaymentParam doctorDetails = new InsurancePaymentParam()
            {
                RowId = id,
                Flag = "GetRequiredDetails"
            };
            var response = _insurancePaymentBusiness.VerifyKycDetails(doctorDetails);
            return View(response);
        }
        [HttpPost]
        public IActionResult VerifyInsurancePaymentDetail(InsurancePayementViewModel doctorViewModels)
        {
            var doctorParam = new InsurancePaymentParam();
            if (!string.IsNullOrEmpty(doctorViewModels.ApproveVerify))
            {
                doctorParam.Flag = "VerifyInsuranceDetail";
                doctorParam.VerifiedBy = User.Identity.Name;
                doctorParam.VerifiedRemarks = doctorViewModels.Remarks;
                doctorParam.RowId = doctorViewModels.RowId;
            }
            else
            {
                doctorParam.Flag = "RejectInsuranceDetail";
                doctorParam.RejectedBy = User.Identity.Name;
                doctorParam.RejectedRemarks = doctorViewModels.Remarks;
                doctorParam.RowId = doctorViewModels.RowId;

            }
            var response = _insurancePaymentBusiness.ManageKYCDetail(doctorParam);
            return RedirectToAction("Index").WithAlertMessage(response.Code, response.Message);
        }
        public IActionResult ApproveInsurancePaymentDetail(string id)
        {
            if (id == null)
                return RedirectToAction("Index").WithAlertMessage("111", "Could not perform Verify task.");
            InsurancePaymentParam doctorDetails = new InsurancePaymentParam()
            {
                RowId = id,
                Flag = "GetRequiredDetails"
            };
            var response = _insurancePaymentBusiness.VerifyKycDetails(doctorDetails);
            return View(response);
        }
        [HttpPost]
        public IActionResult ApproveInsurancePaymentDetail(InsurancePayementViewModel doctorViewModels)
        {
            var doctorParam = new InsurancePaymentParam();
            if (!string.IsNullOrEmpty(doctorViewModels.ApproveVerify))
            {
                doctorParam.Flag = "ApproveInsuranceDetail";
                doctorParam.ApprovedBy = User.Identity.Name;
                doctorParam.ApprovedRemarks = doctorViewModels.Remarks;
                doctorParam.RowId = doctorViewModels.RowId;

            }
            else
            {
                doctorParam.Flag = "RejectInsuranceDetail";
                doctorParam.RejectedBy = User.Identity.Name;
                doctorParam.RejectedRemarks = doctorViewModels.Remarks;
                doctorParam.RowId = doctorViewModels.RowId;

            }
            var response = _insurancePaymentBusiness.ManageKYCDetail(doctorParam);
            return RedirectToAction("Index").WithAlertMessage(response.Code, response.Message);
        }
        public IActionResult ViewInsurancePaymentDetail(string id)
        {
            if (id == null)
                return RedirectToAction("Index").WithAlertMessage("111", "Could not perform View KYC Details task.");
            InsurancePaymentParam doctorDetails = new InsurancePaymentParam()
            {
                RowId = id,
                Flag = "GetRequiredDetails"
            };
            var response = _insurancePaymentBusiness.VerifyKycDetails(doctorDetails);
            return View(response);
        }
    }
}
