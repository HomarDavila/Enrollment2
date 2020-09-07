using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Custom_Models
{
    public class ApplicationMemberResponse
    {
        public int ApplicationId { get; set; }
        public string ApplicationNumber { get; set; }
        public int? ContactId { get; set; }
        public string ContactFullName { get; set; }
        public string ContactMPI { get; set; }
        public int ApplicationMemberId { get; set; }
        public int PersonId { get; set; }
        public string MPI { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string ElegibityES { get; set; }
        public string ElegibityEN { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }        
        public int? McoId { get; set; }
        public string CarrierName { get; set; }        
        public string PcpNPI { get; set; }
        public string PcpFullName { get; set; }
        public string PmgTaxId { get; set; }
        public string PmgName { get; set; }
        public bool? IsAvailableForChange { get; set; }
        public string Reason { get; set; }
    }
}
