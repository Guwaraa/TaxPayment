using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxPayment.Common.SystemResponse;
using TaxPayment.Common.TaxSetup;
using TaxPayment.Repository.GenericRepository;

namespace TaxPaymet.Business.Setup.TaxSetup
{
    public class TaxSetupBusiness : ITaxSetupBusiness
    {
        private IGenericRepository _genericRepository;
        private string StoredProcedureName = "Setup.PROC_TAXSETUPMANAGEMENT";
        public TaxSetupBusiness(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public SystemResponse ManageTaxSetupDetails(TaxSetupParam param)
        {
            var response = _genericRepository.ManageData(StoredProcedureName, param);
            return response;
        }
        public List<TaxSetupDetails> GetRequiredDetailList(object param)
        {
            var details = _genericRepository.ManageDataWithListObject<TaxSetupDetails>(StoredProcedureName, param);
            return details;
        }
        public TaxSetupViewModel GetTaxSetupDetails(object param)
        {
            var response = _genericRepository.ManageDataWithListObjectMultiple<TaxSetupViewModel, TaxSetupValueDetail>(StoredProcedureName, param);
            var taxDetail = new TaxSetupViewModel();
            taxDetail = ((List<TaxSetupViewModel>)response[0])[0];
            taxDetail.TaxSetupUploadJson = JsonConvert.SerializeObject((List<TaxSetupValueDetail>)response[1]);
            return taxDetail;
            
        }
    }
}
