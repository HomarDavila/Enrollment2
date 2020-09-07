
using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity_Models
{
    public class ManagedCareOrganization : IAuditEntity, IEquatable<ManagedCareOrganization>
    {
        public int Id { get; set; }
        public string CarrierCode { get; set; }
        public string CarrierName { get; set; }
        public string LogoURL { get; set; }
        public int? Capacity { get; set; }
        public int? AmountOfLivesEnrolled { get; set; }

        [NotMapped]
        public int AmountOfLivesPending
        {
            get
            {
                if (Capacity.HasValue && AmountOfLivesEnrolled.HasValue)
                    return Capacity.Value - AmountOfLivesEnrolled.Value;
                else return 0;
            }
        }

        [NotMapped]
        public bool OverCapacity
        {
            get { return (AmountOfLivesPending <= 0); }
        }

        public string NPI { get; set; }
        public string EIN { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
        // Important: Used to operate the distinct linq method
        public bool Equals(ManagedCareOrganization other)
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
            ManagedCareOrganization other = obj as ManagedCareOrganization;
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
