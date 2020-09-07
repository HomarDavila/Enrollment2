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
    public interface IReasonJustCauseServices
    {
        EResponseBase<ReasonJustCause> Get();
        EResponseBase<ReasonJustCause> GetReasonJustCauseByID(int reasonJustCauseId);
        EResponseBase<ReasonJustCause> Disabled(int id, bool enabled);
        EResponseBase<ReasonJustCause> InsertOrUpdate(ReasonJustCause model);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
