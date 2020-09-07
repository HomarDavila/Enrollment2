using Domain.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Custom_Models
{
    public class StatisticsRequestV1 : EnrollmentStatistics
    {
        public int MemberId { get; set; }
        public int? McoId { get; set; }
        public int? PmgId { get; set; }
        public int? PcpId { get; set; }

        public DateTime? fecini { get; set; }
        public DateTime? fecfin { get; set; }


        public DataSource Origin { get; set; }
        public string UserName { get; set; }
        public bool IgnoreValidationRules { get; set; }
    }
}
