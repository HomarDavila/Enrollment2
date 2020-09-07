using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Entity_Models
{
    public class EnrollmentStatistics : IAuditEntity, IEquatable<EnrollmentStatistics>
    {
        public int Id { get; set; }

        public int TypeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }

        [NotMapped]
        public int Type1Count { get; set; }
        [NotMapped]
        public int Type2Count { get; set; }
        [NotMapped]
        public int Type3Count { get; set; }
        [NotMapped]
        public int Type4Count { get; set; }
        [NotMapped]
        public DateTime? StartDate { get; set; }
        [NotMapped]
        public DateTime? EndDate { get; set; }
        [NotMapped]
        public string CreatedOnFormated { get; set; }


        public bool Equals(EnrollmentStatistics other)
        {
            if (other == null) return false;
            if (!Id.Equals(other.Id)) return false;
            return true;
        }
    }
}
