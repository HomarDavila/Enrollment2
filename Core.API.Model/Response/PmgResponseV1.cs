using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.Model.Response
{
    public class PmgResponseV1
    {
        public int Id { get; set; }
        public string PmgCode { get; set; }
        public string PmgName { get; set; }
        public string PmgFederalTaxId { get; set; }
        public string NPI { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
    }
}
