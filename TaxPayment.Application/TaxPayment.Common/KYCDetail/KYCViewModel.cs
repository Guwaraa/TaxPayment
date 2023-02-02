using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TaxPayment.Common.KYCDetail
{
    public class KYCViewModel
    {
        public string RowId { get; set; }
        public string UserId { get; set; }
        public string KYCCode { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string ApprovedRemarks { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string CurrentAddress { get; set; }
        public string ParmanentAddress { get; set; }
        public string ContactNumber { get; set; }
        public string ApproveVerify { get; set; }
        public string CitizenshipNumber { get; set; }
        public string Remarks { get; set; }
        public string Gender { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string BackImagePath { get; set; }
        public string FrontImagePath { get; set; }
        public string VerifiedRemarks { get; set; }
        public string RejectedRemarks { get; set; }
        public string RejectedBy { get; set; }
        public IFormFile FrontImage { get; set; }
        public IFormFile BackImage { get; set; }
        public List<SelectListItem> GenderList { get; set; }
    }
}
