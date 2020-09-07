using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Custom_Models
{
    public class PcpRequestV1
    {
        public string PcpFullName { get; set; }
        public string NPI { get; set; }
        public int? SpecialityId { get; set; }
        public int? PmgId { get; set; }
        public bool ShowForChangeEnrollmentProcess { get; set; }
        public List<int> lst_McoId { get; set; }
        public int? MunicipalityId { get; set; }
    }
}
