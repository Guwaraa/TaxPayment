using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxPayment.Common.Premium
{
    public class PremiumDetails
    {
        public string RowId { get; set; }
        public string RowNum { get; set; }
        public string Flag { get; set; }
        public string VechicleCategory { get; set; }
        public string FiscalYear { get; set; }
        public string Province { get; set; }
        public string InsuranceCompany { get; set; }
        public string InsuranceRate { get; set; }
        public string Status { get; set; }
    }
   
    public class PremiumDetailsParam
    {
        public string Flag { get; set; }
        public string VechicleCategory { get; set; }
        public string FiscalYear { get; set; }
        public string Province { get; set; }
        public string InsuranceCompany { get; set; }
        public string InsuranceRate { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }
}
