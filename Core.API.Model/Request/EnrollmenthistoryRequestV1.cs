using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Custom_Models
{
    public class EnrollmentHistoryRequestV1
    {
        public int MemberId { get; set; }
        public int? McoId { get; set; }
        public int? PmgId { get; set; }
        public int? PcpId { get; set; }
        public int? PpcpId { get; set; }

        public DateTime? fecini { get; set; }
        public DateTime? fecfin { get; set; }


        public DataSource Origin { get; set; }
        public string UserName { get; set; }
        public bool IgnoreValidationRules { get; set; }
        public string FilePDF { get; set; }
        public bool Permission { get; set; }
        public bool IsJustCause { get; set; }
        public int JustCause { get; set; }
        public int EnrollmentHistoryId { get; set; }
    }
}

