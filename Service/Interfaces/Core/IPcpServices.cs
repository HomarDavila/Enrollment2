using Common;
using Common.HttpHelpers;
using Common.Logging;
using Core.API.Model.Response;
using Domain.Custom_Models;
using Domain.Entity_Models;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IPcpServices
    {
        EResponseBase<PersonPrimaryCarePhysician> Get(bool ShowForChangeEnrollmentProcess);
        EResponseBase<PrimaryCarePhysicianCustomModel> Get(string PcpFullName, string NPI, int? SpecialityId, int? PmgId, bool ShowForChangeEnrollmentProcess, List<int> McoIds);
        EResponseBase<PrimaryCarePhysician> Get(int PCPId);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
        EResponseBase<PrimaryCarePhysicianCustomModel> GetByFiltersToList(string PcpFullName, string NPI, int? SpecialityId, int? PmgId, bool ShowForChangeEnrollmentProcess, List<int> McoIds, int? MunicipalityId);
    }
}
