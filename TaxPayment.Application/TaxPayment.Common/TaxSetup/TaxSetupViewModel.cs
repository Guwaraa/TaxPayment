﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxPayment.Common.TaxSetup
{
    public class TaxSetupViewModel
    {
        public string RowId { get; set; }
        public string VechicleCategory { get; set; }
        public string FiscalYear { get; set; }
        public string Province { get; set; }
        public string TaxSetupUploadJson { get; set; }
    }
    public class TaxSetupValueDetail
    {
        public string CCFrom { get; set; }
        public string CCTo { get; set; }
        public string TaxRate { get; set; }
    }
}
