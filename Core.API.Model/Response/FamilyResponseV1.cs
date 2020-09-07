using Core.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Custom_Models
{
    public class FamilyResponseV1
    {
        public int Id { get; set; }
        public string FamilyCode { get; set; }
        public string ApplicationNumber { get; set; }
        public string SSN { get; set; }
        public string Suffix { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactFirstLastName { get; set; }
        public string ContactSecondLastName { get; set; }
        public string ContactMiddleName { get; set; }
        public string Region { get; set; }
        public string Municipality { get; set; }
        public string Facility { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? CertificationDate { get; set; }
        public string MailAddressLineOne { get; set; }
        public string MailAddressLineTwo { get; set; }
        public string MailAddressZipCode { get; set; }
        public string MailAddressZip4 { get; set; }
        public string ResidenceAddressLineOne { get; set; }
        public string ResidenceAddressLineTwo { get; set; }
        public string ResidenceAddressZipCode { get; set; }
        public string ResidenceAddressZip4 { get; set; }
        public string Phone { get; set; }
        public string TranId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
        public ICollection<MemberResponseV1> Members { get; set; }
    }
}
