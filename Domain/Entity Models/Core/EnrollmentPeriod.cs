using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity_Models
{
    public class EnrollmentPeriod : IAuditEntity, IEquatable<EnrollmentPeriod>
    {
        public int Id { get; set; }

        public DateTime? PeriodIni { get; set; }
        public DateTime? PeriodFin { get; set; }


        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }



        public bool Equals(EnrollmentPeriod other)
        {
            if (other == null) return false;
            if (!Id.Equals(other.Id)) return false;
            return true;
        }
    }
}
