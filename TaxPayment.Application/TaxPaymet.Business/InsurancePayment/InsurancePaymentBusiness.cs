using ISolutionVersionNext.Shared.GridHelpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxPayment.Common.InsurancePayment;
using TaxPayment.Common.KYCDetail;
using TaxPayment.Common.Premium;
using TaxPayment.Common.SystemResponse;
using TaxPayment.Repository.GenericRepository;

namespace TaxPaymet.Business.InsurancePayment
{
    public class InsurancePaymentBusiness : IInsurancePaymentBusiness
    {
        private IGenericRepository _genericRepository;
        private readonly string StoreProcedureName = "Setup.Proc_INSURANCEPAYMENTMANAGEMENT";
        public InsurancePaymentBusiness(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public SystemResponse ManageInsurancePaymentDetail(InsurancePaymentParam param)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public InsurancePayementViewModel GetInsurancePaymentDetail(InsurancePaymentParam param)
        {
            var response = _genericRepository.ManageDataWithSingleObject<InsurancePayementViewModel>(StoreProcedureName, param);
            return response;
        }
        public List<InsurancePayementDetails> GetGridDetailList(InsurancePaymentParam param)
        {
            var response = _genericRepository.ManageDataWithListObject<InsurancePayementDetails>(StoreProcedureName, param);
            return response;
        }



        public InsurancePayementViewModel GetRequiredDetails(object param)
        {
            
            var response = _genericRepository.ManageDataWithMultipleSelectListItemOBJ(StoreProcedureName, param);
            var premiumList = new InsurancePayementViewModel
            {
                VechicleCategoryList = response[0],
                ProvinceList = response[1],
                InsuranceCompanyList = response[2]
            };
            return premiumList;
        }
        public async Task<List<InsurancePayementDetails>> GetKycLists(GridParam gridParam)
        {
            List<Task<InsurancePayementDetails>> agentTypeLists = new List<Task<InsurancePayementDetails>>();
            var details = _genericRepository.ManageDataWithListObject<InsurancePayementDetails>(StoreProcedureName, gridParam);
            foreach (InsurancePayementDetails agentType in details)
            {
                agentTypeLists.Add(Task.Run(() => KycDetailGridManagement(agentType)));
            }
            var results = await Task.WhenAll(agentTypeLists);
            return results.ToList();
        }
        private InsurancePayementDetails KycDetailGridManagement(InsurancePayementDetails kycDetail)
        {
            var rowId = kycDetail.RowId;
            StringBuilder stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(kycDetail.VerifiedBy) && !string.IsNullOrEmpty(kycDetail.ApprovedBy))
            {
                kycDetail.Status = "<span class=\"label label-success\">Approve Success</span>";
                stringBuilder.Append(" <a href='" + "/InsurancePayment/ViewInsurancePaymentDetail/" + rowId + "' class='btn btn-sm btn-info btn-round' title='View Insurance Payment Details'><i class='bx bx-search-alt'></i></a>");
            }
            if (string.IsNullOrEmpty(kycDetail.VerifiedBy))
            {
                var Status = kycDetail.Status == "R" ? "<span class=\"label label-danger\">Rejected</span>" : "<span class=\"label label-warning\">Verification Pending</span>";
                if (kycDetail.Status == "R")
                {
                    stringBuilder.Append("<a href='" + "/InsurancePayment/ViewInsurancePaymentDetail/" + rowId + "' class='btn btn-sm btn-link btn-round' title='View Insurance Payment Details'><i class='bx bx-search-alt'></i></a>");
                }
                else
                {
                    stringBuilder.Append(" <a href='" + "/InsurancePayment/VerifyInsurancePaymentDetail/" + rowId + "' class='btn btn-sm btn-info btn-round' title='Verify Insurance Payment Details'><i class='bx bx-check></i></a>");
                }
                kycDetail.Status = Status;
            }
            if (!string.IsNullOrEmpty(kycDetail.VerifiedBy) && string.IsNullOrEmpty(kycDetail.ApprovedBy))
            {
                kycDetail.Status = "<span class=\"label label-warning\">Approve Pending</span>";
                stringBuilder.Append(" <a href='" + "/InsurancePayment/ApproveInsurancePaymentDetail/" + rowId + "' class='btn btn-sm btn-info btn-round' title='Approve Insurance Payment Details'><i class='bx bx-check'></i></a>");
            }
            kycDetail.Action = stringBuilder.ToString();
            return kycDetail;
        }
        public SystemResponse ManageKYCDetail(InsurancePaymentParam param)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public InsurancePayementViewModel VerifyKycDetails(InsurancePaymentParam doctorparam)
        {
            return _genericRepository.ManageDataWithListObject<InsurancePayementViewModel>(StoreProcedureName, doctorparam)[0];
        }
    }
}
