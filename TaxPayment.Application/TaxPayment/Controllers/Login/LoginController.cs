using Microsoft.AspNetCore.Mvc;
using TaxPayment.Common.User;
using TaxPaymet.Business.Login;

namespace TaxPayment.Controllers.Login
{
    public class LoginController : Controller
    {
        private ILoginBusiness _loginBusiness;
        public LoginController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }
        public IActionResult Login()
        {
         
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserParam param)
        {
            param.Flag = "CheckUserName";
             var response = _loginBusiness.CheckUserName(param);
            if(response.Code=="000")
            {
                HttpContext.Session.SetString("UserId", response.Data);
                HttpContext.Session.SetString("UserName", response.Extras);
                //ViewBag["UserId"] =response.Data;
                //ViewBag["UserName"] = response.Extras;
                return RedirectToAction("Index","User");
               
            }
            return View("Login");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserParam userParam)
        {
            userParam.Flag = "RegiterDetail";
            var response = _loginBusiness.RegiterDetail(userParam);
            return RedirectToAction("Login", response);
        }
    }
}
