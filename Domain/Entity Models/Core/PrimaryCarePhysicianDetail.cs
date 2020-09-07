
using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity_Models
{
    public class PrimaryCarePhysicianDetail : IAuditEntity, IEquatable<PrimaryCarePhysicianDetail>
    {
        public int Id { get; set; }
        //public int? PrimaryCarePhysicianId { get; set; }
        public int? PcpPmgMcoId { get; set; }
        [ForeignKey("PcpPmgMcoId")]
        public PcpPmgMco PCPPMGCMCO { get; set; }
        public int? MunicipalityId { get; set; }
        [ForeignKey("MunicipalityId")]
        public Municipality Municipality { get; set; }
        public string Phone { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
        // Important: Used to operate the distinct linq method
        public bool Equals(PrimaryCarePhysicianDetail other)
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
            PrimaryCarePhysicianDetail other = obj as PrimaryCarePhysicianDetail;
            if (other != null)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

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
            get { return (AmountOfLivesEnrolled >= Capacity); }
        }
    }
}
