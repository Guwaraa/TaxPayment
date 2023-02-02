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
            return RedirectToPage("Index","User");

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
                Flag = "ViewKYCDetail",
            };
            var response = _kYCDetailBusiness.GetKYCDetail(param);
            return RedirectToAction("Index");
        }
    }
}
