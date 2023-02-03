using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxPayment.Common.SystemResponse;
using TaxPayment.Repository.GenericRepository;
using TaxPayment.Common.TaxPayement;
using ISolutionVersionNext.Shared.GridHelpers;
using TaxPayment.Common.KYCDetail;

namespace TaxPaymet.Business.TaxPayement
{
    public class TaxPayementBusiness : ITaxPayementBusiness
    {
        private IGenericRepository _genericRepository;
        private readonly string StoreProcedureName = "Setup.Proc_TAXPAYMENTMANAGEMENT";
        public TaxPayementBusiness(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public SystemResponse ManageTaxPayementDetail(TaxPayementParam param)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public TaxPayementViewModel GetTaxPayementDetail(TaxPayementParam param)
        {
            var response = _genericRepository.ManageDataWithSingleObject<TaxPayementViewModel>(StoreProcedureName, param);
            return response;
        }
        public List<TaxPayementDetails> GetGridDetailList(TaxPayementParam param)
        {
            var response = _genericRepository.ManageDataWithListObject<TaxPayementDetails>(StoreProcedureName, param);
            return response;
        }
        public async Task<List<TaxPayementDetails>> GetTaxPaymentLists(GridParam gridParam)
        {
            List<Task<TaxPayementDetails>> agentTypeLists = new List<Task<TaxPayementDetails>>();
            var details = _genericRepository.ManageDataWithListObject<TaxPayementDetails>(StoreProcedureName, gridParam);
            foreach (TaxPayementDetails agentType in details)
            {
                agentTypeLists.Add(Task.Run(() => KycDetailGridManagement(agentType)));
            }
            var results = await Task.WhenAll(agentTypeLists);
            return results.ToList();
        }
        private TaxPayementDetails KycDetailGridManagement(TaxPayementDetails kycDetail)
        {
            var rowId = kycDetail.RowId;
            StringBuilder stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(kycDetail.VerifiedBy) && !string.IsNullOrEmpty(kycDetail.ApprovedBy))
            {
                kycDetail.Status = "<span class=\"label label-success\">Approve Success</span>";
                stringBuilder.Append(" <a href='" + "/TaxPayment/ViewTaxPayment/" + rowId + "' class='btn btn-sm btn-info btn-round' title='View Tax Detail'><i class='bx bx-search-alt'></i></a>");
            }
            if (string.IsNullOrEmpty(kycDetail.VerifiedBy))
            {
                var Status = kycDetail.Status == "R" ? "<span class=\"label label-danger\">Rejected</span>" : "<span class=\"label label-warning\">Verification Pending</span>";
                if (kycDetail.Status == "R")
                {
                    stringBuilder.Append("<a href='" + "/TaxPayment/ViewTaxPayment/" + rowId + "' class='btn btn-sm btn-link btn-round' title='View Tax Detail'><i class='bx bx-search-alt'></i></a>");
                }
                else
                {
                    stringBuilder.Append(" <a href='" + "/TaxPayment/VerifyTaxPayment/" + rowId + "' class='btn btn-sm btn-info btn-round' title='Verify Tax Detail'><i class='bx bx-check'></i></a>");
                }
                kycDetail.Status = Status;
            }
            if (!string.IsNullOrEmpty(kycDetail.VerifiedBy) && string.IsNullOrEmpty(kycDetail.ApprovedBy))
            {
                kycDetail.Status = "<span class=\"label label-warning\">Approve Pending</span>";
                stringBuilder.Append(" <a href='" + "/TaxPayment/ApproveTaxPayment/" + rowId + "' class='btn btn-sm btn-info btn-round' title='Approve Tax Detail'><i class='bx bx-check'></i></a>");
            }
            kycDetail.Action = stringBuilder.ToString();
            return kycDetail;
        }
        public SystemResponse ManageTaxPayment(TaxPayementParam param)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public TaxPayementViewModel VerifyTaxPayment(TaxPayementParam doctorparam)
        {
            return _genericRepository.ManageDataWithListObject<TaxPayementViewModel>(StoreProcedureName, doctorparam)[0];
        }
    }
}
