using Common;
using Common.HttpHelpers;
using Common.Logging;
using Domain.Custom_Models;
using Domain.Entity_Models;

namespace Service.Interfaces
{
    public interface IMcoServices
    {
        EResponseBase<ManagedCareOrganization> Get(bool showEnrollmentProcess);
        EResponseBase<ManagedCareOrganization> Get(int McoId);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
