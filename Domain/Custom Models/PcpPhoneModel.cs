using Domain.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Custom_Models
{
    public class PcpPhoneModel
    {
        public int? PcpId { get; set; }        
        public List<string> Phones { get; set; }
        public List<string> Faxes { get; set; }        
    }
}
