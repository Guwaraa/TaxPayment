using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxPayment.Common.VechicleRegistration
{
    public class RegistrationViewModel
    {
        public string KYCCode { get; set; }
        public string VechicleType { get; set; }
        public string VechicleNumber { get; set; }
        public string RegisteredDate { get; set; }
        public string Ownername { get; set; }
        public string CompanyName { get; set; }
        public string VechicleModel { get; set; }
        public string DateOfModification { get; set; }
        public string VechiclePower { get; set; }
        public string Color { get; set; }
        public string EngineNumber { get; set; }
        public string LastTaxPaidDateFrom { get; set; }
        public string LastTaxPaidDateTo { get; set; }
        public IFormFile FrontPageImage { get; set; }
        public IFormFile VechicleInfoImage { get; set; }
        public IFormFile TaxPaidImage { get; set; }
        public string FrontPageImagePath { get; set; }
        public string VechicleInfoImagePath { get; set; }
        public string TaxPaidImagePath { get; set; }
    }
}
