using Common;
using Common.Logging;
using Domain.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IReportServices
    {
        EResponseBase<EnrollmentHistory> GetReportInscripciones(DateTime? pFecha, string pInscripcion);
        EResponseBase<EnrollmentHistory> GetReportJustaCausa(DateTime? pFecha, string pInscripcion);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
