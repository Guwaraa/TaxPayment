using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxPayment.Common.InsurancePayment;
using TaxPayment.Common.Premium;
using TaxPayment.Common.SystemResponse;
using TaxPayment.Repository.GenericRepository;

namespace TaxPaymet.Business.InsurancePayment
{
    public class InsurancePaymentBusiness : IInsurancePaymentBusiness
    {
        private IGenericRepository _genericRepository;
        private readonly string StoreProcedureName = "Proc_INSURANCEPAYMENTMANAGEMENT";
        public InsurancePaymentBusiness(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public SystemResponse ManageInsurancePaymentDetail(InsurancePaymentParam param)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public InsurancePayementViewModel GetInsurancePaymentDetail(InsurancePaymentParam param)
        {
            var response = _genericRepository.ManageDataWithSingleObject<InsurancePayementViewModel>(StoreProcedureName, param);
            return response;
        }
        public List<InsurancePayementDetails> GetGridDetailList(InsurancePaymentParam param)
        {
            var response = _genericRepository.ManageDataWithListObject<InsurancePayementDetails>(StoreProcedureName, param);
            return response;
        }

        public InsurancePayementViewModel GetRequiredDetails()
        {
            var flag = "GetRequiredList";
            var response = _genericRepository.ManageDataWithMultipleSelectListItem(StoreProcedureName, flag);
            var premiumList = new PremiumViewModel
            {
                VechicleCategoryList = response[0],
                FiscalYearList = response[1],
                ProvinceList = response[2],
                InsuranceCompanyList = response[3]
            };
            return premiumList;
        }
    }
}
