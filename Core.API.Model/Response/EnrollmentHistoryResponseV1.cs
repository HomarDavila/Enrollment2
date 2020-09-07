using Core.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Custom_Models
{
    public class EnrollmentHistoryResponseV1
    {

        public int Id { get; set; }
        public int MemberId { get; set; }
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

        public int? MCOId { get; set; }
        public McoResponseV1 MCO { get; set; }
        public string MCOModifiedSource { get; set; }
        public string MCOModifiedBy { get; set; }
        public DateTime? MCOModifiedDate { get; set; }
        public DateTime? MCOEffectiveDate { get; set; }

        public int? PMGId { get; set; }
        public PmgResponseV1 PMG { get; set; }
        public string PMGModifiedSource { get; set; }
        public string PMGModifiedBy { get; set; }
        public DateTime? PMGModifiedDate { get; set; }
        public DateTime? PMGEffectiveDate { get; set; }


        public int? PCPId { get; set; }
        public PersonPcpResponseV1 PCP { get; set; }
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
        public int? StatusId { get; set; }
        public StatusResponseV1 Status { get; set; }
    }
}
