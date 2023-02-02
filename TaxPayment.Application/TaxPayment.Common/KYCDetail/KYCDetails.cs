using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxPayment.Common.KYCDetail
{
    public class KYCDetails
    {
        public string KYCCode { get; set; }
        public string UserId { get; set; }
        public string RowNum { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        public string CurrentAddress { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string VerifiedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string RejectedBy { get; set; }
    }
    public class KYCParam
    {
        public string Flag { get; set; }
        public string UserId { get; set; }
        public string RowId { get; set; }
        public string KYCCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string CurrentAddress { get; set; }
        public string ParmanentAddress { get; set; }
        public string ContactNumber { get; set; }
        public string CitizenshipNumber { get; set; }
        public string Gender { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string FrontImagePath { get; set; }
        public string BackImagePath { get; set; }
        public string VerifiedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string Remarks { get; set; }
    }
}
