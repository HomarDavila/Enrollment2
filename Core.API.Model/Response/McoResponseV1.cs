using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Custom_Models
{
    public class McoResponseV1
    {
        public int Id { get; set; }
        public string CarrierCode { get; set; }
        public string CarrierName { get; set; }
        public string LogoURL { get; set; }
        public int? Capacity { get; set; }
        public int? AmountOfLivesEnrolled { get; set; }
        public int AmountOfLivesPending { get; set; }
        public bool OverCapacity { get; set; }
        public string NPI { get; set; }
        public string EIN { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
    }
}
