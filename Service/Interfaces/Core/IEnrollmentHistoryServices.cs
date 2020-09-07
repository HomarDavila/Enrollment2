using Common;
using Common.Logging;
using Domain.Custom_Models;
using Domain.Entity_Models;
using System;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEnrollmentHistoryServices
    {
        EResponseBase<EnrollmentHistory> Get();
        EResponseBase<EnrollmentHistory> Get(int EnrollmentHistoryId);
        EResponseBase<EnrollmentHistory> GetOnlyRejects();
        EResponseBase<Member> ChangeEnrollmentForCorrection(int memberId, int? mcoId, int? pmgId, int? pcpId, int? ppcpId, bool permission, int justCause, DataSource origin, string userName, int EnrollmentHistoryId, bool ignoreValidationRules = false);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}

