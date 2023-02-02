using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxPayment.Common.Premium
{
    public class PremiumViewModel
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
        public string FilterCount { get; set; }
        public List<SelectListItem> VechicleCategoryList { get; set; }
        public List<SelectListItem> FiscalYearList { get; set; }
        public List<SelectListItem> ProvinceList { get; set; }
        public List<SelectListItem> InsuranceCompanyList { get; set; }


    }
}
