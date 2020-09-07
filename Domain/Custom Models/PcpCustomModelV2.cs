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
    public class PcpCustomModelV2
    {
        public int? PersonId { get; set; }
        public int? SpecialityId { get; set; }
        public PersonPrimaryCarePhysician Person { get; set; }
        public Speciality Speciality { get; set; }
        public List<PrimaryCarePhysicianCustomModel> Data { get; set; }
    }
}
