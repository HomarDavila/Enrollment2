using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.Model.Response
{
    public class SpecialityResponseV1
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public bool? ShowItOnChangeEnrollmentProcess { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
        public string Nombre { get; set; }
    }
}
