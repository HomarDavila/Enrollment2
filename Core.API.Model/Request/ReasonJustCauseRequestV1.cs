using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.Model.Request
{
    public class ReasonJustCauseRequestV1
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public string Razon { get; set; }
        public string Description { get; set; }
        public string Descripcion { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool Enabled { get; set; }
    }
}
