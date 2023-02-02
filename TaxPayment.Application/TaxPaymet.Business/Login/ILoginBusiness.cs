using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxPayment.Common.SystemResponse;
using TaxPayment.Common.User;

namespace TaxPaymet.Business.Login
{
    public interface ILoginBusiness
    {
        SystemResponse CheckUserName(UserParam userParam);
        SystemResponse RegiterDetail(UserParam userParam);
    }
}
