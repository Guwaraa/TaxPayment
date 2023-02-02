using ISolutionVersionNext.Shared.GridHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxPayment.Common.Premium;
using TaxPayment.Common.SystemResponse;

namespace TaxPaymet.Business.Setup.PremiumSetup
{
    public interface IPremiumBusiness
    {
        List<PremiumDetails> GetRequiredDetailList(object param);
        SystemResponse ManagePremiumSetupDetails(PremiumDetailsParam param);
        PremiumViewModel GetRequiredDetails();
        PremiumViewModel GetPremiumUpdateDetails(PremiumViewModel premiumParam);
        Task<List<PremiumDetails>> GetPremiumSetupLists(GridParam gridParam);
    }
}
