using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TaxPayment.Common.TaxSetup
{
    public class TaxSetupDetails
    {
        public string RowId { get; set; }
        public string RowNum { get; set; }
        public string TaxCode { get; set; }
        public string VechicleCategory { get; set; }
        public string FiscalYear { get; set; }
        public string Province { get; set; }
        public string CCFrom { get; set; }
        public string CCTo { get; set; }
        public string TaxRate { get; set; }
        public string Status { get; set; }
    }
    public class TaxSetupParam
    {
        public string Flag { get; set; }
        public string TaxCode { get; set; }
        public string VechicleCategory { get; set; }
        public string FiscalYear { get; set; }
        public string Province { get; set; }
        public string TaxSetupUploadJson { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
