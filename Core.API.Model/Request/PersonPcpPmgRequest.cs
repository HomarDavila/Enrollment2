using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Custom_Models
{
    public class PersonPcpPmgRequest
    {
        public int PersonId { get; set; }
        public int McoId { get; set; }
        public int PcpId { get; set; }
        public int PmgId { get; set; }
        public int Origin { get; set; }
        public string UserName { get; set; }
    }
}

