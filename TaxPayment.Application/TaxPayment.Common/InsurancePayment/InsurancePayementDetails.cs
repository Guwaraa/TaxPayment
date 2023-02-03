using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxPayment.Common.InsurancePayment
{
    public class InsurancePayementDetails
    {
        public string KYCCode { get; set; }
        public string RowNum { get; set; }
        public string Province { get; set; }
        public string VechicleCategory { get; set; }
        public string CompanyName { get; set; }
        public string InsuranceRate { get; set; }
        public string PaidDate { get; set; }
        public string VerifiedBy { get; set; }
        public string ApprovedBy { get; set; }
    }
    public class InsurancePaymentParam
    {
        public string Flag { get; set; }
        public string UserId { get; set; }
        public string KYCCode { get; set; }
        public string Province { get; set; }
        public string VechicleCategory { get; set; }
        public string CompanyName { get; set; }
        public string PaidDate { get; set; }
        public string VechicleNo { get; set; }
        public string BankCode { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string VerifiedBy { get; set; }
        public string VerifiedRemarks { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedRemarks { get; set; }
    }
}
