using Common;
using Common.Logging;
using Domain.Custom_Models;
using Domain.Entity_Models;

namespace Service.Interfaces
{
    public interface ISpecialityServices
    {
        EResponseBase<Speciality> Get();
        EResponseBase<Speciality> Get(bool ShowForChangeEnrollmentProcess = false);
        EResponseBase<Speciality> GetByPCPId(int PCPId, bool ShowForChangeEnrollmentProcess);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
