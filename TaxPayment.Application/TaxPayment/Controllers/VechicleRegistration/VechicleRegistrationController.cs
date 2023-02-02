using Microsoft.AspNetCore.Mvc;

namespace TaxPayment.Controllers.VechicleRegistration
{
    public class VechicleRegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ManageVechicleRegistration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddVechicleRegistration(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Content("file not selected");
            }

            var folderName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", folderName, file.FileName);
            var directory = Path.GetDirectoryName(path);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return RedirectToPage("Index");
        }
    }
}
