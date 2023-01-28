using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaxPayment.Common.TaxSetup;
using TaxPaymet.Business.Setup.TaxSetup;

namespace TaxPayment.Controllers.Setup
{
    public class TaxSetupController : Controller
    {
        private ITaxSetupBusiness _taxSetupBusiness;
        private IMapper _mapper;
        public TaxSetupController(ITaxSetupBusiness taxSetupBusiness,IMapper mapper)
        {
            _taxSetupBusiness = taxSetupBusiness;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var param = new
            {
                Flag = "GetRequiredDetailList",
            };
            var response = _taxSetupBusiness.GetRequiredDetailList(param);
            return View(response);
        }
        public IActionResult ManageTaxSetup()
        {
            var taxDetail = new TaxSetupViewModel();
            return View(taxDetail);
        }
        [HttpPost]
        public IActionResult AddTaxSetup(TaxSetupViewModel taxSetupViewModel)
        {
            var taxdetail = _mapper.Map<TaxSetupParam>(taxSetupViewModel);
            return RedirectToAction("Index");
        }
        public IActionResult UpdateTaxSetup(string RowId)
        {
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UpdateTaxSetup(TaxSetupViewModel taxSetupViewModel)
        {
            return RedirectToAction("Index");
        }
        public IActionResult UpdateTaxSetupStatus(string RowId)
        {
            return RedirectToAction("Index");
        }
    }
}
