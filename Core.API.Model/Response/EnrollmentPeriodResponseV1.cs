using Core.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Custom_Models
{
    public class EnrollmentPeriodResponseV1
    {
        public int Id { get; set; }

        public DateTime? PeriodIni { get; set; }
        public DateTime? PeriodFin { get; set; }


        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }

    }
}
