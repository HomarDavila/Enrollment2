using Domain.Entity_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Custom_Models
{
    [NotMapped]
    public class PrimaryCarePhysicianCustomModel : PrimaryCarePhysician
    {
        //public IEnumerable<PcpPmgMco> AvailableManagedCareOrganizations { get; set; }
        public IEnumerable<ManagedCareOrganizationsCustomModel> AvailableManagedCareOrganizations { get; set; }
        public IEnumerable<PrimaryMedicalGroupCustomModel> AvailablePrimaryMedicalGroups { get; set; }
        //public Gender Gender { get; set; }
        //public int? GenderId { get; set; }
    }
}
