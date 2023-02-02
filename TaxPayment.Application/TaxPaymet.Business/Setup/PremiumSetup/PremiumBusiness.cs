using ISolutionVersionNext.Shared.GridHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxPayment.Common.Premium;
using TaxPayment.Common.SystemResponse;
using TaxPayment.Common.TaxSetup;
using TaxPayment.Repository.GenericRepository;

namespace TaxPaymet.Business.Setup.PremiumSetup
{
    public class PremiumBusiness : IPremiumBusiness
    {
        private IGenericRepository _genericRepository;
        private string StoredProcedureName = "Setup.PROC_PREMIUMSETUPMANAGEMENT";
        public PremiumBusiness(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<List<PremiumDetails>> GetPremiumSetupLists(GridParam gridParam)
        {
            List<Task<PremiumDetails>> agentTypeLists = new List<Task<PremiumDetails>>();
            var details = _genericRepository.ManageDataWithListObject<PremiumDetails>(StoredProcedureName, gridParam);
            foreach (PremiumDetails agentType in details)
            {
                agentTypeLists.Add(Task.Run(() => PremiumSetupGridManagement(agentType)));
            }
            var results = await Task.WhenAll(agentTypeLists);
            return results.ToList();
        }
        private PremiumDetails PremiumSetupGridManagement(PremiumDetails agentType)
        {
            var rowId = agentType.RowId;
            agentType.Status = agentType.Status == "A" ? "<i class=\"bx-bx-check-circle-alt mdi-18px text-success\" title='Active'></i>" : "<i class=\"mdi mdi-close-circle mdi-18px text-danger\" title='Inactive'></i>";
            StringBuilder actionDetails = new StringBuilder();
           
                actionDetails.Append("<a href='" + "/PremiumSetup/ManagePremiumSetup/" + rowId + "' class='btn btn-sm btn-link btn-round' title='Edit Premium Setup'><i class='bx bx-edit-alt'></i></a>");
                actionDetails.Append(" <a href='" + "/PremiumSetup/UpdatePremiumSetupStatus/" + rowId + "' class='btn btn-sm btn-success btn-round confirmation' title='Change Status'><i class='mdi mdi-lock-reset'></i></a>");
            agentType.Action = actionDetails.ToString();
            return agentType;
        }
        public List<PremiumDetails> GetRequiredDetailList(object param)
        {
            var details = _genericRepository.ManageDataWithListObject<PremiumDetails>(StoredProcedureName, param);
            return details;
        }
        public SystemResponse ManagePremiumSetupDetails(PremiumDetailsParam param)
        {
            var response = _genericRepository.ManageData(StoredProcedureName, param);
            return response;
        }
        public PremiumViewModel GetRequiredDetails()
        {
            var flag = "GetRequiredList";
            var response = _genericRepository.ManageDataWithMultipleSelectListItem(StoredProcedureName, flag);
            var premiumList = new PremiumViewModel
            {
                VechicleCategoryList = response[0],
                FiscalYearList = response[1],
                ProvinceList = response[2],
                InsuranceCompanyList = response[3]
            };
            return premiumList;
        }
        public PremiumViewModel GetPremiumUpdateDetails(PremiumViewModel premiumParam)
        {
            return _genericRepository.ManageDataWithListObject<PremiumViewModel>(StoredProcedureName, premiumParam)[0];
        }
    }
}
