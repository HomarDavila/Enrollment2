using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.Model.Response
{
    public class SpecialityResponseV2
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? Enabled { get; set; }
        public string Nombre { get; set; }
        public bool? ShowItOnChangeEnrollmentProcess { get; set; }
    }
}
