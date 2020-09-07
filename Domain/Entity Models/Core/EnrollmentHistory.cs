using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity_Models
{
    public class EnrollmentHistory : IAuditEntity, IEquatable<EnrollmentHistory>
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        public Member Member { get; set; }
        public int FamilyId { get; set; }
        public string SSN { get; set; }
        public string Suffix { get; set; }
        public string MPI { get; set; }
        public string MPIShort { get; set; }
        public string MPIContactMember { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? CertificationDate { get; set; }
        public string PlanType { get; set; }
        public string PlanVersion { get; set; }
        public string MemberPrimaryCenter { get; set; }
        public string MedicaidIndicator { get; set; }
        public string MedicareIndicator { get; set; }
        public string HICNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool EstadoEncuesta { get; set; }
        public bool? MCOChange { get; set; }
        public bool? PCPChange { get; set; }
        public bool? PMGChange { get; set; }

        public int? MCOId { get; set; }
        [ForeignKey("MCOId")]
        public ManagedCareOrganization MCO { get; set; }
        public string MCOModifiedSource { get; set; }
        public string MCOModifiedBy { get; set; }
        public DateTime? MCOModifiedDate { get; set; }
        public DateTime? MCOEffectiveDate { get; set; }

        public int? PMGId { get; set; }
        [ForeignKey("PMGId")]
        public PrimaryMedicalGroup PMG { get; set; }
        public string PMGModifiedSource { get; set; }
        public string PMGModifiedBy { get; set; }
        public DateTime? PMGModifiedDate { get; set; }
        public DateTime? PMGEffectiveDate { get; set; }


        public int? PCPId { get; set; }
        [ForeignKey("PCPId")]
        public PersonPrimaryCarePhysician PCP { get; set; }
        public string PCPModifiedSource { get; set; }
        public string PCPModifiedBy { get; set; }
        public DateTime? PCPModifiedDate { get; set; }
        public DateTime? PCPEffectiveDate { get; set; }



        public string TranId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
        public string filePDF { get; set; }

        public int? PreviusPmg { get; set; }
        public int? PreviusPcp { get; set; }
        public int? PreviusMco { get; set; }
        public string ChangeReason { get; set; }
        public int? JustCauseReasonId { get; set; }
        public string JustCauseComment { get; set; }

        public int? StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status Status { get; set; }

        // Important: Used to operate the distinct linq method
        public bool Equals(EnrollmentHistory other)
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
            EnrollmentHistory other = obj as EnrollmentHistory;
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
        public DateTime? Fecha { get; set; }
        [NotMapped]
        public int TotalPMG { get; set; }
        [NotMapped]
        public int TotalMCO { get; set; }
        [NotMapped]
        public int TotalPCP { get; set; }
    }
}
