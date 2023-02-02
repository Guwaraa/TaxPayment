using Microsoft.AspNetCore.Mvc;
using TaxPayment.Common.KYCDetail;
using TaxPayment.Common.VechicleRegistration;
using TaxPaymet.Business.Registration;

namespace TaxPayment.Controllers.VechicleRegistration
{
    public class VechicleRegistrationController : Controller
    {
        private IRegistrationBusiness _registrationBusiness;
        public VechicleRegistrationController(IRegistrationBusiness registrationBusiness)
        {
            _registrationBusiness = registrationBusiness;
        }
        public IActionResult Index()
        {
            var param = new
            {
                Flag = "GetGridDetailList",
            };
            var resposne = _registrationBusiness.GetRequiredDetailList(param);
            return View(resposne);
        }
        public IActionResult ManageVechicleRegistration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddVechicleRegistration(RegistrationViewModel model)
        {
            if (model.FrontPageImage == null || model.VechicleInfoImage == null || model.TaxPaidImage == null)
            {
                return Content("file not selected");
            }
            var param = new RegistrationPram();
            #region FileUpload 
            var folderName = HttpContext.Session.GetString("UserName") + "_" + model.KYCCode + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            //Views_Shared__FrontLayout Image
            param.FrontPageImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", folderName, model.FrontPageImage.FileName);
            var directory = Path.GetDirectoryName(param.FrontPageImagePath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var stream = new FileStream(param.FrontPageImagePath, FileMode.Create))
            {
                await model.FrontPageImage.CopyToAsync(stream);
            }


            param.VechicleInfoImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", folderName, model.VechicleInfoImage.FileName);
             directory = Path.GetDirectoryName(param.VechicleInfoImagePath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var stream = new FileStream(param.VechicleInfoImagePath, FileMode.Create))
            {
                await model.VechicleInfoImage.CopyToAsync(stream);
            }


            param.TaxPaidImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", folderName, model.TaxPaidImage.FileName);
             directory = Path.GetDirectoryName(param.TaxPaidImagePath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var stream = new FileStream(param.TaxPaidImagePath, FileMode.Create))
            {
                await model.TaxPaidImage.CopyToAsync(stream);
            }
            #endregion
            param.Flag = "AddVechicleRegistration";
            param.KYCCode = model.KYCCode;
            param.VechicleType = model.VechicleType;
            param.VechicleNumber = model.VechicleNumber;
            param.RegisteredDate = model.RegisteredDate;
            param.Ownername = model.Ownername;
            param.CompanyName = model.CompanyName;
            param.VechicleModel = model.VechicleModel;
            param.DateOfModification = model.DateOfModification;
            param.VechiclePower = model.VechiclePower;
            param.Color = model.Color;
            param.EngineNumber = model.EngineNumber;
            param.LastTaxPaidDateFrom = model.LastTaxPaidDateFrom;
            param.LastTaxPaidDateTo = model.LastTaxPaidDateTo;
            var response = _registrationBusiness.ManageRegistrationDetail(param);
            return RedirectToPage("Index");
        }
        public IActionResult UpdateVechicleRegistration(string UserId)
        {
            var param = new
            {
                UserId = UserId,
                Flag = "GetRegisteredDetails",
            };
            var response = _registrationBusiness.GetRegisteredDetails(param);
            return View("ManageVechicleRegistration",response);
        }
    }
}
