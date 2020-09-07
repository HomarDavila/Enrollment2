using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.Model
{
    public class PrimaryCarePhysicianDetailRequestV1
    {
        public int? PersonId { get; set; }
        public int? SpecialityId { get; set; }
        public int? PmgId { get; set; }
        public int? MunicipalityId { get; set; }
    }
}
