using ISolutionVersionNext.Shared.GridHelpers;
using ISolutionVersionNext.UtilityHelpers.Alert;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using TaxPayment.Common.KYCDetail;
using TaxPayment.Common.Premium;
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
            var agentType = await _kYCDetailBusiness.GetKycLists(agentTypeDetails);
            var agentTypeLists = new HtmlGrid<KYCDetails>();
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
        public IActionResult ManageKycDetail()
        {
            var response = new KYCViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddKycDetail(KYCViewModel param)
        {
            #region FileUpload
            if (param.FrontImage == null || param.BackImage == null)
            {
                return Content("file not selected");
            }

            var folderName = HttpContext.Session.GetString("UserName") + "_" + param.UserId + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            //Views_Shared__FrontLayout Image
            param.FrontImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", folderName, param.FrontImage.FileName);
            var directory = Path.GetDirectoryName(param.FrontImagePath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var stream = new FileStream(param.FrontImagePath, FileMode.Create))
            {
                await param.FrontImage.CopyToAsync(stream);
            }
            //backimage
            param.BackImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", folderName, param.BackImage.FileName);
            directory = Path.GetDirectoryName(param.BackImagePath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var stream = new FileStream(param.BackImagePath, FileMode.Create))
            {
                await param.BackImage.CopyToAsync(stream);
            }
            #endregion
            var parameter = new KYCParam
            {
                Flag = "AddKYCDetails",
                UserId = param.UserId,
                RowId = param.RowId,
                KYCCode = param.KYCCode,
                FirstName = param.FirstName,
                MiddleName = param.MiddleName,
                LastName = param.LastName,
                DateOfBirth = param.DateOfBirth,
                CurrentAddress = param.CurrentAddress,
                ParmanentAddress = param.ParmanentAddress,
                ContactNumber = param.ContactNumber,
                CitizenshipNumber = param.CitizenshipNumber,
                Gender = param.Gender,
                CreatedBy = param.CreatedBy,
                ModifiedBy = param.ModifiedBy,
                FrontImagePath = param.FrontImagePath,
                BackImagePath = param.BackImagePath
            };
            var response = _kYCDetailBusiness.ManageKYCDetail(parameter);
            return RedirectToPage("Index", "User");
        }

        public IActionResult VerifyKYCDetail(string id)
        {
            if (id == null)
                return RedirectToAction("Index").WithAlertMessage("111", "Could not perform Verify task.");
            KYCParam doctorDetails = new KYCParam()
            {
                RowId = id,
                Flag = "GetRequiredKycDetails"
            };
            var response = _kYCDetailBusiness.VerifyKycDetails(doctorDetails);
            return View(response);
        }
        [HttpPost]
        public IActionResult VerifyKYCDetail(KYCViewModel doctorViewModels)
        {
            var doctorParam = new KYCParam();
            if (!string.IsNullOrEmpty(doctorViewModels.ApproveVerify))
            {
                doctorParam.Flag = "VerifyKYCDetail";
                doctorParam.VerifiedBy = User.Identity.Name;
                doctorParam.VerifiedRemarks = doctorViewModels.Remarks;
                doctorParam.RowId = doctorViewModels.RowId;
            }
            else
            {
                doctorParam.Flag = "RejectKYCDetail";
                doctorParam.RejectedBy = User.Identity.Name;
                doctorParam.RejectedRemarks = doctorViewModels.Remarks;
                doctorParam.RowId = doctorViewModels.RowId;

            }
            var response = _kYCDetailBusiness.ManageKYCDetail(doctorParam);
            return RedirectToAction("Index").WithAlertMessage(response.Code, response.Message);
        }
        public IActionResult ApproveKYCDetail(string id)
        {
            if (id == null)
                return RedirectToAction("Index").WithAlertMessage("111", "Could not perform Verify task.");
            KYCParam doctorDetails = new KYCParam()
            {
                RowId = id,
                Flag = "GetRequiredKycDetails"
            };
            var response = _kYCDetailBusiness.VerifyKycDetails(doctorDetails);
            return View(response);
        }
        [HttpPost]
        public IActionResult ApproveKYCDetail(KYCViewModel doctorViewModels)
        {
            var doctorParam = new KYCParam();
            if (!string.IsNullOrEmpty(doctorViewModels.ApproveVerify))
            {
                doctorParam.Flag = "ApproveKYCDetail";
                doctorParam.ApprovedBy = User.Identity.Name;
                doctorParam.ApprovedRemarks = doctorViewModels.Remarks;
                doctorParam.RowId = doctorViewModels.RowId;

            }
            else
            {
                doctorParam.Flag = "RejectKYCDetail";
                doctorParam.RejectedBy = User.Identity.Name;
                doctorParam.RejectedRemarks = doctorViewModels.Remarks;
                doctorParam.RowId = doctorViewModels.RowId;

            }
            var response = _kYCDetailBusiness.ManageKYCDetail(doctorParam);
            return RedirectToAction("Index").WithAlertMessage(response.Code, response.Message);
        }
        public IActionResult ViewKYCDetail(string id)
        {
            if (id == null)
                return RedirectToAction("Index").WithAlertMessage("111", "Could not perform View KYC Details task.");
            KYCParam doctorDetails = new KYCParam()
            {
                RowId = id,
                Flag = "GetRequiredKycDetails"
            };
            var response = _kYCDetailBusiness.VerifyKycDetails(doctorDetails);
            return View(response);
        }
    }
}
