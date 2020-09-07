
using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity_Models
{
    public class PersonPrimaryCarePhysician : IAuditEntity, IEquatable<PersonPrimaryCarePhysician>
    {
        public int Id { get; set; }
        public string FederalTaxId { get; set; }
        public string NPI { get; set; }
        public string FullName
        {
            get; set; // get { return $"{FirstName} {MiddleName} {FirstLastName}"; }
        }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
        // Important: Used to operate the distinct linq method
        public bool Equals(PersonPrimaryCarePhysician other)
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
            PersonPrimaryCarePhysician other = obj as PersonPrimaryCarePhysician;
            if (other != null)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }
        public int? GenderId { get; set; }
        [ForeignKey("GenderId")]
        public Gender Gender { get; set; }

        [NotMapped]
        public bool OverCapacity { get; set; }
        [NotMapped]
        public int PcpId { get; set; }
    }
}
