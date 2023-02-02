using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxPayment.Common.SystemResponse;
using TaxPayment.Common.User;
using TaxPayment.Repository.GenericRepository;

namespace TaxPaymet.Business.Login
{
    public class LoginBusiness : ILoginBusiness
    {
        private IGenericRepository _genericRepository;
        private readonly string StoreProcedureName = "";
        public LoginBusiness(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public SystemResponse RegiterDetail(UserParam userParam)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, userParam);
            return response;
        }
        public SystemResponse CheckUserName(UserParam userParam)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, userParam);
            return response;
        }

    }
}
