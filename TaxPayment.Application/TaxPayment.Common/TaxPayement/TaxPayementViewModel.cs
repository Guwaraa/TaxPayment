using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxPayment.Common.TaxPayement
{
    public class TaxPayementViewModel
    {
        public string Flag { get; set; }
        public string RowId { get; set; }
        public string RowNum { get; set; }
        public string BankCode { get; set; }
        public string VerifiedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string Action { get; set; }
        public string ApproveVerify { get; set; }
        public string FilterCount { get; set; }
        public string ApprovedRemarks { get; set; }
        public string RejectedRemarks { get; set; }
        public string RejectedBy { get; set; }
        public string VerifiedRemarks { get; set; }
        public string KYCCode { get; set; }
        public string Province { get; set; }
        public string VechiclePower { get; set; }
        public string VechicleCategory { get; set; }
        public List<SelectListItem> VechicleCategoryList { get; set; }
        public string VechicleNo { get; set; }
        public string TaxRate { get; set; }
        public string PaidDate { get; set; }
        public string LastDueDate { get; set; }
        public string LateFeeAmount { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

    }
}
