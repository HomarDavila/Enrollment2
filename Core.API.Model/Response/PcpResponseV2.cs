using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.Model.Response
{
    public class PcpResponseV2
    {
        public int Id { get; set; }
        public bool? Enabled { get; set; }
        public PersonPcpResponseV2 Person { get; set; }
        public int? PersonId { get; set; }
        public int? SpecialityId { get; set; }
        public SpecialityResponseV2 Speciality { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public ICollection<McoResponseV2> AvailableManagedCareOrganizations { get; set; }
        public ICollection<PmgResponseV2> AvailablePrimaryMedicalGroups { get; set; }
    }
}
