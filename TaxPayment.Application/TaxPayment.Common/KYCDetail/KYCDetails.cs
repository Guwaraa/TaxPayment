using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxPayment.Common.KYCDetail
{
    public class KYCDetails
    {
    }
    public class KYCParam
    {
        public string Flag { get; set; }
        public string VerifiedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string Remarks { get; set; }
    }
}
