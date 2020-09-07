//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EnrollmentSurvey.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EnrollmentHistories
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EnrollmentHistories()
        {
            this.Puntuacion = new HashSet<Puntuacion>();
        }
    
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int FamilyId { get; set; }
        public string SSN { get; set; }
        public string Suffix { get; set; }
        public string MPI { get; set; }
        public string MPIshort { get; set; }
        public string MPIContactMember { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<System.DateTime> CertificationDate { get; set; }
        public string PlanType { get; set; }
        public string PlanVersion { get; set; }
        public string MemberPrimaryCenter { get; set; }
        public string MedicaidIndicator { get; set; }
        public string MedicareIndicator { get; set; }
        public string HICNumber { get; set; }
        public Nullable<int> MCOId { get; set; }
        public string MCOModifiedSource { get; set; }
        public string MCOModifiedBy { get; set; }
        public Nullable<System.DateTime> MCOModifiedDate { get; set; }
        public Nullable<System.DateTime> MCOEffectiveDate { get; set; }
        public Nullable<int> PMGId { get; set; }
        public string PMGModifiedSource { get; set; }
        public string PMGModifiedBy { get; set; }
        public Nullable<System.DateTime> PMGModifiedDate { get; set; }
        public Nullable<System.DateTime> PMGEffectiveDate { get; set; }
        public Nullable<int> PCPId { get; set; }
        public string PCPModifiedSource { get; set; }
        public string PCPModifiedBy { get; set; }
        public Nullable<System.DateTime> PCPModifiedDate { get; set; }
        public Nullable<System.DateTime> PCPEffectiveDate { get; set; }
        public string TranId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public bool Enabled { get; set; }
        public string filePDF { get; set; }
        public Nullable<int> PreviusPmg { get; set; }
        public Nullable<int> PreviusPcp { get; set; }
        public Nullable<int> PreviusMco { get; set; }
        public string ChangeReason { get; set; }
        public string State { get; set; }
        public Nullable<int> JustCauseReasonId { get; set; }
        public Nullable<int> StatusId { get; set; }
        public string Phone { get; set; }
        public Nullable<bool> EstadoEncuesta { get; set; }
        public Nullable<bool> MCOChange { get; set; }
        public Nullable<bool> PCPChange { get; set; }
        public Nullable<bool> PMGChange { get; set; }
        public string JustCauseComment { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Puntuacion> Puntuacion { get; set; }
    }
}