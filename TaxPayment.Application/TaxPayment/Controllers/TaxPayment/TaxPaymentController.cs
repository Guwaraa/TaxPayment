using ISolutionVersionNext.Shared.GridHelpers;
using ISolutionVersionNext.UtilityHelpers.Alert;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaxPayment.Common.InsurancePayment;
using TaxPayment.Common.KYCDetail;
using TaxPayment.Common.SystemResponse;
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
        public IActionResult UserPayment()
        {
            var param = new TaxPayementParam
            {
                Flag = "GetGridDetailList",
                UserId = HttpContext.Session.GetString("UserId"),
            };
            var response = _taxPayementBusiness.GetGridDetailList(param);
            return View(response);
        }
        [HttpPost]
        public IActionResult AddUserPayment(TaxPayementParam taxPayementParam)
        {
            taxPayementParam.Flag = "AddTaxPayemnt";
            taxPayementParam.UserId = HttpContext.Session.GetString("UserId");
            var response = _taxPayementBusiness.ManageTaxPayementDetail(taxPayementParam);
            return RedirectToAction("UserPayment");
        }
        public IActionResult VerifyTaxPayment(string id)
        {
            TaxPayementParam doctorDetails = new TaxPayementParam()
            {
                RowId = id,
                Flag = "GetRequiredDetails"
            };
            //var response = _taxPayementBusiness.VerifyTaxPayment(doctorDetails);
            var response = new TaxPayementViewModel();
            response.BankCode = "1010101";
            response.Province = "Province 1";
            response.VechiclePower = "150";
            response.VechicleCategory = "Bike";
            response.VechicleNo = "4708";
            response.TaxRate = "5000";
            response.TaxRate = "2023/02/03";
            response.LastDueDate = "2024/02/03";
            response.Action = "1";
            return View(response);
        }
        public IActionResult TaxPayment(string id)
        {
            TaxPayementParam doctorDetails = new TaxPayementParam()
            {
                RowId = id,
                Flag = "GetRequiredDetails"
            };
            //var response = _taxPayementBusiness.VerifyTaxPayment(doctorDetails);
            var response = new TaxPayementViewModel();
            response.BankCode = "1010101";
            response.Province = "Province 1";
            response.VechiclePower = "150";
            response.VechicleCategory = "Bike";
            response.VechicleNo = "4708";
            response.TaxRate = "5000";
            response.TaxRate = "2023/02/03";
            response.LastDueDate = "2024/02/03";
            return View(response);
        }
        [HttpPost]
        public IActionResult VerifyTaxPayment(TaxPayementViewModel doctorViewModels)
        {
            //var doctorParam = new TaxPayementParam();
            //if (!string.IsNullOrEmpty(doctorViewModels.ApproveVerify))
            //{
            //    doctorParam.Flag = "VerifyTaxPayment";
            //    doctorParam.VerifiedBy = User.Identity.Name;
            //    doctorParam.VerifiedRemarks = doctorViewModels.Remarks;
            //    doctorParam.RowId = doctorViewModels.RowId;
            //}
            //else
            //{
            //    doctorParam.Flag = "RejectTaxPayment";
            //    doctorParam.RejectedBy = User.Identity.Name;
            //    doctorParam.RejectedRemarks = doctorViewModels.Remarks;
            //    doctorParam.RowId = doctorViewModels.RowId;

            //}
            //var response = _taxPayementBusiness.ManageTaxPayment(doctorParam);
            var response = new SystemResponse();
            response.Code = "000";
            response.Message = "Tax verified Sucessful";
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

            TaxPayementParam doctorDetails = new TaxPayementParam()
            {
                RowId = "1",
                Flag = "GetRequiredDetails"
            };
            var response = _taxPayementBusiness.VerifyTaxPayment(doctorDetails);
            return View(response);
        }
    }
}
