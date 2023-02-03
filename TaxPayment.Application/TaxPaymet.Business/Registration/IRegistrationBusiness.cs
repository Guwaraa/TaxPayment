using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxPayment.Common.SystemResponse;
using TaxPayment.Common.VechicleRegistration;

namespace TaxPaymet.Business.Registration
{
    public interface IRegistrationBusiness
    {
        SystemResponse ManageRegistrationDetail(RegistrationPram pram);
        List<RegistrationDetails> GetRequiredDetailList(object param);
        RegistrationViewModel GetRegisteredDetails(object param);
    }
}
