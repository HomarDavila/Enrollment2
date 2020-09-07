using Core.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Custom_Models
{
    public class MemberResponseV1
    {
        
        public int Id { get; set; }
        public int EnrollmentHistoryID { get; set; }
        public int FamilyId { get; set; }
        public FamilyResponseV3 Family { get; set; }
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
        public string MemberFullName
        {
            get { return $"{FirstName} {FirstLastName} {SecondLastName}"; }
        }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? CertificationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string PlanType { get; set; }
        public string PlanVersion { get; set; }
        public string MemberPrimaryCenter { get; set; }
        public string MedicaidIndicator { get; set; }
        public string MedicareIndicator { get; set; }
        public string HICNumber { get; set; }
        public int? MCOId { get; set; }
        public McoResponseV1 MCO { get; set; }
        public DateTime? MCOEffectiveDate { get; set; }
        public int? PMGId { get; set; }
        public PmgResponseV1 PMG { get; set; }
        public DateTime? PMGEffectiveDate { get; set; }
        public int? PCPId { get; set; }
        public PersonPcpResponseV1 PCP { get; set; }
        public DateTime? PCPEffectiveDate { get; set; }
        public string TranId { get; set; }
        public string Elegibility
        {
            get
            {
                if (TranId!=null)
                {
                    if (TranId.Trim().ToUpper().Equals("E"))
                    {
                        return "Elegible";
                    }
                    if (TranId.Trim().ToUpper().Equals("I"))
                    {
                        return "Inelegible";
                    }

                }
               
                return string.Empty;
            }
        }
        public bool IsAvailableForChange { get; set; }
        public string Reason { get; set; }
        public string ReasonEN { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
    }
}
