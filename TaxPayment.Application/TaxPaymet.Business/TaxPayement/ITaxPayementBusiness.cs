using TaxPayment.Common.SystemResponse;
using TaxPayment.Common.TaxPayement;

namespace TaxPaymet.Business.TaxPayement
{
    public interface ITaxPayementBusiness
    {
        SystemResponse ManageTaxPayementDetail(TaxPayementParam param);
        TaxPayementViewModel GetTaxPayementDetail(TaxPayementParam param);
        List<TaxPayementDetails> GetGridDetailList(TaxPayementParam param);
    }
}
