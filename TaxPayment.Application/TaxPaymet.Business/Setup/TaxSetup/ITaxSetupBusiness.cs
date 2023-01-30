using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxPayment.Common.SystemResponse;
using TaxPayment.Common.TaxSetup;

namespace TaxPaymet.Business.Setup.TaxSetup
{
    public interface ITaxSetupBusiness
    {
        SystemResponse ManageTaxSetupDetails(TaxSetupParam param);
        List<TaxSetupDetails> GetRequiredDetailList(object param);
        TaxSetupViewModel GetTaxSetupDetails(object param);
    }
}
