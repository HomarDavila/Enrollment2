using Domain.Entity_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Custom_Models
{ 
    [NotMapped]
    public class PcpCustomModel : PrimaryCarePhysician
    {        
        public List<string> Phones { get; set; }
        public List<string> Faxes { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public int? McoId { get; set; }
        public int? PmgId { get; set; }        
        public List<AvailablePcpCustomModel> AvailableOptions { get; set; }
    }
}
