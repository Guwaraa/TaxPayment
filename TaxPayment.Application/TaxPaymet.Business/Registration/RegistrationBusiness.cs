using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxPayment.Common.Premium;
using TaxPayment.Common.SystemResponse;
using TaxPayment.Common.VechicleRegistration;
using TaxPayment.Repository.GenericRepository;

namespace TaxPaymet.Business.Registration
{
    public class RegistrationBusiness : IRegistrationBusiness 
    {
        private IGenericRepository _genericRepository;
        private readonly string StoreProcedureName = "Setting.Proc_RegistrationManagement";
        public RegistrationBusiness(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public SystemResponse ManageRegistrationDetail(RegistrationPram pram)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, pram);
            return response;
        }

        public List<RegistrationDetails> GetRequiredDetailList(object param)
        {
            var details = _genericRepository.ManageDataWithListObject<RegistrationDetails>(StoreProcedureName, param);
            return details;
        }
        public RegistrationViewModel GetRegisteredDetails(object param)
        {
            var response = _genericRepository.ManageDataWithSingleObject<RegistrationViewModel>(StoreProcedureName, param);
            return response;
        }
    }
}
