using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.Model.Request
{
    public class ReportRequestV1
    {
        public DateTime? Fecha { get; set; }
        public string Inscripcion { get; set; }
    }
}
