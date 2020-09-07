using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity_Models
{
    public class Member : IAuditEntity, IEquatable<Member>
    {
        public int Id { get; set; }
        public int FamilyId { get; set; }
        [ForeignKey("FamilyId")]
        public Family Family { get; set; }
        public string SSN { get; set; }
        public string Last4SSN { get; set; }
        public string Suffix { get; set; }
        public string MPI { get; set; }
        public string MPIShort { get; set; }
        public string MPIContactMember { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string FullName
        {
            get { return $"{FirstName} {FirstLastName} {SecondLastName}"; }
        }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? CertificationDate { get; set; }
        public DateTime? NewCertificationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string PlanType { get; set; }
        public string PlanVersion { get; set; }
        public string MemberPrimaryCenter { get; set; }
        public string MedicaidIndicator { get; set; }
        public string MedicareIndicator { get; set; }
        public string HICNumber { get; set; }
        public int? MCOId { get; set; }
        [ForeignKey("MCOId")]
        public ManagedCareOrganization MCO { get; set; }
        public DateTime? MCOEffectiveDate { get; set; }
        public int? PMGId { get; set; }
        [ForeignKey("PMGId")]
        public PrimaryMedicalGroup PMG { get; set; }
        public DateTime? PMGEffectiveDate { get; set; }
        public int? PCPId { get; set; }
        [ForeignKey("PCPId")]
        public PersonPrimaryCarePhysician PCP { get; set; }
        public int? GenderId { get; set; }
        [ForeignKey("GenderId")]
        public Gender Gender { get; set; }
        public DateTime? PCPEffectiveDate { get; set; }
        [NotMapped]
        public bool IsAvailableForChange { get; set; }
        [NotMapped]
        public string Reason { get; set; }
        [NotMapped]        
        public string ReasonEN { get; set; }
        public string TranId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }

        [NotMapped]
        public int EnrollmentHistoryID { get; set; }
        // Important: Used to operate the distinct linq method
        public bool Equals(Member other)
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
            Member other = obj as Member;
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
