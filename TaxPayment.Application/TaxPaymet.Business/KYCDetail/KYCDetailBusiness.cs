using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxPayment.Common.KYCDetail;
using TaxPayment.Common.SystemResponse;
using TaxPayment.Repository.GenericRepository;

namespace TaxPaymet.Business.KYCDetail
{
    public class KYCDetailBusiness : IKYCDetailBusiness
    {
        private IGenericRepository _genericRepository;
        private readonly string StoreProcedureName = "Proc_KYCDETAILMANAGEMENT";
        public KYCDetailBusiness(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public SystemResponse ManageKYCDetail(KYCParam param)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public KYCViewModel GetKYCDetail(KYCParam param)
        {
            var response = _genericRepository.ManageDataWithSingleObject<KYCViewModel>(StoreProcedureName, param);
            return response;
        }
        public List<KYCDetails> GetGridDetailList(KYCParam param)
        {
            var response = _genericRepository.ManageDataWithListObject<KYCDetails>(StoreProcedureName, param);
            return response;
        }
    }
}
