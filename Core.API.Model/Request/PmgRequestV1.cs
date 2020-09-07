using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.Model.Request
{
    public class PmgRequestV1
    {
        public int PCPId { get; set; }
        public bool ShowForChangeEnrollmentProcess { get; set; }
    }
}
