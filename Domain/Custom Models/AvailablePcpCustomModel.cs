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
    public class AvailablePcpCustomModel
    {
        public int McoId { get; set; }
        public ManagedCareOrganization MCO { get; set; }
        public int? PmgId { get; set; }
        public PrimaryMedicalGroup PMG { get; set; }
        public int? PcpId { get; set; }
        public PrimaryCarePhysician PCP { get; set; }
        public bool IsAvailable { get; set; }
    }
}
