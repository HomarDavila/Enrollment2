using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.Model
{
    public class PcpPmgMcoRequestV1
    {
        public int PcpId { get; set; }
        public int PmgId { get; set; }
        public int McoId { get; set; }
        public int MemberId { get; set; }
    }
}
