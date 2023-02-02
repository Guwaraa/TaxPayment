using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxPayment.Common.Premium;
using TaxPayment.Common.SystemResponse;
using TaxPayment.Common.TaxSetup;
using TaxPayment.Repository.GenericRepository;

namespace TaxPaymet.Business.Setup.PremiumSetup
{
    public class PremiumBusiness : IPremiumBusiness
    {
        private IGenericRepository _genericRepository;
        private string StoredProcedureName = "Setup.PROC_PREMIUMSETUPMANAGEMENT";
        public PremiumBusiness(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public List<PremiumDetails> GetRequiredDetailList(object param)
        {
            var details = _genericRepository.ManageDataWithListObject<PremiumDetails>(StoredProcedureName, param);
            return details;
        }
        public SystemResponse ManagePremiumSetupDetails(PremiumDetailsParam param)
        {
            var response = _genericRepository.ManageData(StoredProcedureName, param);
            return response;
        }
        public PremiumViewModel GetRequiredDetails()
        {
            var flag = "GetRequiredList";
            var response = _genericRepository.ManageDataWithMultipleSelectListItem(StoredProcedureName, flag);
            var premiumList = new PremiumViewModel
            {
                VechicleCategoryList = response[0],
                FiscalYearList = response[1],
                ProvinceList = response[2],
                InsuranceCompanyList = response[3]
            };
            return premiumList;
        }
        public PremiumViewModel GetPremiumUpdateDetails(PremiumViewModel premiumParam)
        {
            return _genericRepository.ManageDataWithListObject<PremiumViewModel>(StoredProcedureName, premiumParam)[0];
        }
    }
}
