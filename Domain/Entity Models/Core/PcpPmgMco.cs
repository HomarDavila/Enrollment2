using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity_Models
{
    public class PcpPmgMco : IAuditEntity, IEquatable<PcpPmgMco>
    {
        public int Id { get; set; }
        public int? PrimaryCarePhysicianId { get; set; }
        [ForeignKey("PrimaryCarePhysicianId")]
        public PrimaryCarePhysician PCP { get; set; }
        public int? PmgId { get; set; }
        [ForeignKey("PmgId")]
        public PrimaryMedicalGroup PMG { get; set; }
        public int McoId { get; set; }
        [ForeignKey("McoId")]
        public ManagedCareOrganization MCO { get; set; }
        public ICollection<PrimaryCarePhysicianDetail> Details { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
        // Important: Used to operate the distinct linq method
        public bool Equals(PcpPmgMco other)
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
            PcpPmgMco other = obj as PcpPmgMco;
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
