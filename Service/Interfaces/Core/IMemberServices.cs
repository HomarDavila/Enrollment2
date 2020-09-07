using Common;
using Common.Logging;
using Domain.Custom_Models;
using Domain.Entity_Models;
using System;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IMemberServices
    {
        EResponseBase<Member> Get();
        EResponseBase<Member> Get(int memberId);
        EResponseBase<MemberCustomModel> GetEmail(int memberId);
        EResponseBase<MemberCustomModelV2> Get(string MPI, string Last4SSN, DateTime? DateOfBirth, string FirstName, string FirtLastName, string SecondLastName);
        EResponseBase<Member> Get(string Last4SSN, DateTime DateOfBirth, string ZipCode, string FirstLastName, string SecondLastName, string FirstName);
        EResponseBase<Member> GetMembersById(int memberId);
        EResponseBase<EnrollmentHistory> GetEnrollmentChangeHistory(int memberId);
        Task<EResponseBase<EnrollmentHistory>> SendSms(int memberId,string  phone);
        EResponseBase<EnrollmentHistory> GetEnrollmentHistory(DateTime? fecini, DateTime? fecfin);
        EResponseBase<EnrollmentPeriod> SendEnrPeriod(DateTime? fecini, DateTime? fecfin);
        EResponseBase<EnrollmentPeriod> GetEnrPeriod();
        EResponseBase<Member> ChangeEnrollment(int memberId, int? mcoId, int? pmgId, int? pcpId, int? ppcpId, bool permission, int justCause, DataSource origin, string userName, string justCauseComment, string phone, string email, bool ignoreValidationRules = false);
        EResponseBase<Member> ChangeEnrollmentReject(int memberId, int? IdHistories, int? IdStatus, int? mcoId, int? pmgId, int? pcpId, int? ppcpId, bool permission, int justCause, DataSource origin, string userName, string justCauseComment, bool ignoreValidationRules = false);
        bool ChangeEnrollmentEnabled(int memberId, bool inEnrollmentPeriod, bool ignoreValidationRules, DateTime initOpenEnrollmentForActives, DateTime endOpenEnrollmentForActives,ref bool isAvailableForChange, ref string reason, ref string reasonEN, ref int code, bool isconsuler = false);
        bool ChangeEnrollmentEnabledJustCause(int memberId, ref bool isAvailableForChange, ref string reason, ref string reasonEN, ref int code);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}

