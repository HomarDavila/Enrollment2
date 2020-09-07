using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Custom_Models
{
    public class OverCapacityResponseV1
    {
        public List<McoOverCapacity> lstMcoOverCapacity;
        public List<PmgOverCapacity> lstPmgOverCapacity;
        public List<PcpOverCapacity> lstPcpOverCapacity;

        public class McoOverCapacity
        {
            public int IdMco { get; set; }
            public bool OverCapacityMco { get; set; }
        }
        public class PmgOverCapacity
        {
            public int IdPmg { get; set; }
            public bool OverCapacityPmg { get; set; }
        }
        public class PcpOverCapacity
        {
            public int IdPcp { get; set; }
            public bool OverCapacityPcp { get; set; }
        }
    }
}
