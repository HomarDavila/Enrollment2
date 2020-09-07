using Domain.Custom_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.Model.Response
{
    public class ReportResponseV1
    {
        public DateTime? Fecha { get; set; }
        public int TotalPMG { get; set; }
        public int TotalMCO { get; set; }
        public int TotalPCP { get; set; }
    }
}