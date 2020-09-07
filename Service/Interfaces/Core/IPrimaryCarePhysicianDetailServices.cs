using Common;
using Common.Logging;
using Domain.Custom_Models;

namespace Service.Interfaces
{
    public interface IPrimaryCarePhysicianDetailServices
    {
        EResponseBase<PrimaryCarePhysicianDetailCustomModel> GetByFiltersToList(int? PersonId, int? SpecialityId, int? PmgId, int? MunicipalityId);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
