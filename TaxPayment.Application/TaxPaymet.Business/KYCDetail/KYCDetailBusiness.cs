using ISolutionVersionNext.Shared.GridHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxPayment.Common.KYCDetail;
using TaxPayment.Common.Premium;
using TaxPayment.Common.SystemResponse;
using TaxPayment.Repository.GenericRepository;

namespace TaxPaymet.Business.KYCDetail
{
    public class KYCDetailBusiness : IKYCDetailBusiness
    {
        private IGenericRepository _genericRepository;
        private readonly string StoreProcedureName = "Setup.Proc_KYCDETAILMANAGEMENT";
        public KYCDetailBusiness(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<List<KYCDetails>> GetKycLists(GridParam gridParam)
        {
            List<Task<KYCDetails>> agentTypeLists = new List<Task<KYCDetails>>();
            var details = _genericRepository.ManageDataWithListObject<KYCDetails>(StoreProcedureName, gridParam);
            foreach (KYCDetails agentType in details)
            {
                agentTypeLists.Add(Task.Run(() => KycDetailGridManagement(agentType)));
            }
            var results = await Task.WhenAll(agentTypeLists);
            return results.ToList();
        }
        private KYCDetails KycDetailGridManagement(KYCDetails kycDetail)
        {
            var rowId = kycDetail.RowId;
            StringBuilder stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(kycDetail.VerifiedBy) && !string.IsNullOrEmpty(kycDetail.ApprovedBy))
            {
                kycDetail.Status = "<span class=\"label label-success\">Approve Success</span>";
                    stringBuilder.Append(" <a href='" + "/KYCDetail/ViewKYCDetail/" + rowId + "' class='btn btn-sm btn-info btn-round' title='View KYC Details'><i class='bx bxs-check-circle'></i></a>");
            }
            if (string.IsNullOrEmpty(kycDetail.VerifiedBy))
            {
                var Status = kycDetail.Status == "R" ? "<span class=\"label label-danger\">Rejected</span>" : "<span class=\"label label-warning\">Verification Pending</span>";
                if (kycDetail.Status == "R")
                {
                        stringBuilder.Append("<a href='" + "/KYCDetail/ViewKYCDetail/" + rowId + "' class='btn btn-sm btn-link btn-round' title='View KYC Details'><i class='mdi mdi-eye'></i></a>");
                }
                else
                {
                        stringBuilder.Append(" <a href='" + "/KYCDetail/VerifyKYCDetail/" + rowId + "' class='btn btn-sm btn-info btn-round' title='Verify KYC Details'><i class='mdi mdi-eye'></i></a>");
                }
                kycDetail.Status = Status;
            }
            if (!string.IsNullOrEmpty(kycDetail.VerifiedBy) && string.IsNullOrEmpty(kycDetail.ApprovedBy))
            {
                kycDetail.Status = "<span class=\"label label-warning\">Approve Pending</span>";
                    stringBuilder.Append(" <a href='" + "/KYCDetail/ApproveKYCDetail/" + rowId + "' class='btn btn-sm btn-info btn-round' title='Approve KYC Details'><i class='mdi mdi-eye'></i></a>");
            }
            kycDetail.Action = stringBuilder.ToString();
            return kycDetail;
        }
        public SystemResponse ManageKYCDetail(KYCParam param)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public KYCViewModel VerifyKycDetails(KYCParam doctorparam)
        {
            return _genericRepository.ManageDataWithListObject<KYCViewModel>(StoreProcedureName, doctorparam)[0];
        }
        public KYCViewModel GetKYCDetail(KYCParam param)
        {
            var response = _genericRepository.ManageDataWithSingleObject<KYCViewModel>(StoreProcedureName, param);
            return response;
        }
        public List<KYCDetails> GetGridDetailList(KYCParam param)
        {
            var response = _genericRepository.ManageDataWithListObject<KYCDetails>(StoreProcedureName, param);
            return response;
        }
    }
}
