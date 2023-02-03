using ISolutionVersionNext.Shared.GridHelpers;
using TaxPayment.Common.KYCDetail;
using TaxPayment.Common.SystemResponse;
using TaxPayment.Common.TaxPayement;

namespace TaxPaymet.Business.TaxPayement
{
    public interface ITaxPayementBusiness
    {
        SystemResponse ManageTaxPayementDetail(TaxPayementParam param);
        TaxPayementViewModel GetTaxPayementDetail(TaxPayementParam param);
        List<TaxPayementDetails> GetGridDetailList(TaxPayementParam param);
        Task<List<TaxPayementDetails>> GetTaxPaymentLists(GridParam gridParam);
        TaxPayementViewModel VerifyTaxPayment(TaxPayementParam doctorparam);
        SystemResponse ManageTaxPayment(TaxPayementParam param);
    }
}
