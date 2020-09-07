using Common;
using Common.Logging;
using Domain.Custom_Models;
using Domain.Entity_Models;

namespace Service.Interfaces
{
    public interface IPrimaryMedicalGroupServices
    {
        EResponseBase<PrimaryMedicalGroup> GetByPCPId(int PCPId, bool ShowForChangeEnrollmentProcess);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
