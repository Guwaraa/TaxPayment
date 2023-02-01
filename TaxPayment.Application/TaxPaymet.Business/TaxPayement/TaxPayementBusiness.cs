using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxPayment.Common.SystemResponse;
using TaxPayment.Repository.GenericRepository;
using TaxPayment.Common.TaxPayement;

namespace TaxPaymet.Business.TaxPayement
{
    public class TaxPayementBusiness : ITaxPayementBusiness
    {
        private IGenericRepository _genericRepository;
        private readonly string StoreProcedureName = "Proc_TAXPAYEMENTDETAILMANAGEMENT";
        public TaxPayementBusiness(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public SystemResponse ManageTaxPayementDetail(TaxPayementParam param)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public TaxPayementViewModel GetTaxPayementDetail(TaxPayementParam param)
        {
            var response = _genericRepository.ManageDataWithSingleObject<TaxPayementViewModel>(StoreProcedureName, param);
            return response;
        }
        public List<TaxPayementDetails> GetGridDetailList(TaxPayementParam param)
        {
            var response = _genericRepository.ManageDataWithListObject<TaxPayementDetails>(StoreProcedureName, param);
            return response;
        }
    }
}
