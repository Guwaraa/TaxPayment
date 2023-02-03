using ISolutionVersionNext.Shared.GridHelpers;
using ISolutionVersionNext.UtilityHelpers.Alert;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaxPayment.Common.KYCDetail;
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
            var agentType = await _taxPayementBusiness.GetTaxPaymentLists(agentTypeDetails);
            var agentTypeLists = new HtmlGrid<TaxPayementDetails>();
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
        public IActionResult Payemnt()
        {
            return View();
        }
        public IActionResult VerifyTaxPayment(string id)
        {
            if (id == null)
                return RedirectToAction("Index").WithAlertMessage("111", "Could not perform Verify task.");
            TaxPayementParam doctorDetails = new TaxPayementParam()
            {
                RowId = id,
                Flag = "GetRequiredDetails"
            };
            var response = _taxPayementBusiness.VerifyTaxPayment(doctorDetails);
            return View(response);
        }
        [HttpPost]
        public IActionResult VerifyTaxPayment(TaxPayementViewModel doctorViewModels)
        {
            var doctorParam = new TaxPayementParam();
            if (!string.IsNullOrEmpty(doctorViewModels.ApproveVerify))
            {
                doctorParam.Flag = "VerifyTaxPayment";
                doctorParam.VerifiedBy = User.Identity.Name;
                doctorParam.VerifiedRemarks = doctorViewModels.Remarks;
                doctorParam.RowId = doctorViewModels.RowId;
            }
            else
            {
                doctorParam.Flag = "RejectTaxPayment";
                doctorParam.RejectedBy = User.Identity.Name;
                doctorParam.RejectedRemarks = doctorViewModels.Remarks;
                doctorParam.RowId = doctorViewModels.RowId;

            }
            var response = _taxPayementBusiness.ManageTaxPayment(doctorParam);
            return RedirectToAction("Index").WithAlertMessage(response.Code, response.Message);
        }
        public IActionResult ApproveTaxPayment(string id)
        {
            if (id == null)
                return RedirectToAction("Index").WithAlertMessage("111", "Could not perform Verify task.");
            TaxPayementParam doctorDetails = new TaxPayementParam()
            {
                RowId = id,
                Flag = "GetRequiredDetails"
            };
            var response = _taxPayementBusiness.VerifyTaxPayment(doctorDetails);
            return View(response);
        }
        [HttpPost]
        public IActionResult ApproveTaxPayment(TaxPayementViewModel doctorViewModels)
        {
            var doctorParam = new TaxPayementParam();
            if (!string.IsNullOrEmpty(doctorViewModels.ApproveVerify))
            {
                doctorParam.Flag = "ApproveTaxPayment";
                doctorParam.ApprovedBy = User.Identity.Name;
                doctorParam.ApprovedRemarks = doctorViewModels.Remarks;
                doctorParam.RowId = doctorViewModels.RowId;

            }
            else
            {
                doctorParam.Flag = "RejectTaxPayment";
                doctorParam.RejectedBy = User.Identity.Name;
                doctorParam.RejectedRemarks = doctorViewModels.Remarks;
                doctorParam.RowId = doctorViewModels.RowId;

            }
            var response = _taxPayementBusiness.ManageTaxPayment(doctorParam);
            return RedirectToAction("Index").WithAlertMessage(response.Code, response.Message);
        }
        public IActionResult ViewTaxPayment(string id)
        {
            if (id == null)
                return RedirectToAction("Index").WithAlertMessage("111", "Could not perform View KYC Details task.");
            TaxPayementParam doctorDetails = new TaxPayementParam()
            {
                RowId = id,
                Flag = "GetRequiredDetails"
            };
            var response = _taxPayementBusiness.VerifyTaxPayment(doctorDetails);
            return View(response);
        }
    }
}
