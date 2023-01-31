using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxPayment.Common.InsurancePayment;
using TaxPayment.Common.SystemResponse;

namespace TaxPaymet.Business.InsurancePayment
{
    public interface IInsurancePaymentBusiness
    {
        List<InsurancePayementDetails> GetGridDetailList(InsurancePaymentParam param);
        InsurancePayementViewModel GetInsurancePaymentDetail(InsurancePaymentParam param);
        SystemResponse ManageInsurancePaymentDetail(InsurancePaymentParam param);
    }
}
