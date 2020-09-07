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
    public class PrimaryCarePhysicianDetailCustomModel : PrimaryCarePhysicianDetail
    {
        public IEnumerable<ManagedCareOrganizationsCustomModel> AvailableManagedCareOrganizations { get; set; }
    }
}
