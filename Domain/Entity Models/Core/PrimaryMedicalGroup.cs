
using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity_Models
{
    public class PrimaryMedicalGroup : IAuditEntity, IEquatable<PrimaryMedicalGroup>
    {
        public int Id { get; set; }
        public string PmgCode { get; set; }
        public string PmgName { get; set; }
        public string PmgFederalTaxId { get; set; }
        public string NPI { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
        // Important: Used to operate the distinct linq method
        public bool Equals(PrimaryMedicalGroup other)
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
            PrimaryMedicalGroup other = obj as PrimaryMedicalGroup;
            if (other != null)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        [NotMapped]
        public bool OverCapacity
        {
            get { return false; }
        }
    }
}
