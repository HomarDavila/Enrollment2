using Common;
using Common.Logging;
using Domain.Entity_Models.Identity;

namespace Service.Interfaces.Identity
{
    public interface IApplicationService
    {
        EResponseBase<Application> Get();
        EResponseBase<Application> Get(int id);
        EResponseBase<Application> GetByFilters(string searchText);
        EResponseBase<Application> GetByFilters(string name, string url, string code);

        EResponseBase<Application> InsertOrUpdate(Application model);
        EResponseBase<Application> Delete(int id);
        EResponseBase<Application> Delete(Application model);
        EResponseBase<Application> Disabled(int id, bool enabled);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
