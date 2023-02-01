using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxPayment.Common.KYCDetail;
using TaxPayment.Common.SystemResponse;

namespace TaxPaymet.Business.KYCDetail
{
    public interface IKYCDetailBusiness
    {
        SystemResponse ManageKYCDetail(KYCParam param);
        KYCViewModel GetKYCDetail(KYCParam param);
        List<KYCDetails> GetGridDetailList(KYCParam param);
    }
}
