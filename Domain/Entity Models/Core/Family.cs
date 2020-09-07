using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity_Models
{
    public class Family : IAuditEntity, IEquatable<Family>
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
        public string ContactFullName
        {
            get { return $"{ContactFirstName} {ContactFirstLastName} {ContactSecondLastName}"; }
        }

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
        [JsonIgnore]
        public ICollection<Member> Members { get; set; }

        // Important: Used to operate the distinct linq method
        public bool Equals(Family other)
        {
            if (other == null) return false;
            if (!Id.Equals(other.Id)) return false;
            return true;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + Id.GetHashCode();
                return hash;
            }
        }
        public override bool Equals(object obj)
        {
            Family other = obj as Family;
            if (other != null)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }
    }
}
