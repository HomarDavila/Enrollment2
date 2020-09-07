using Common;
using Common.Logging;
using Domain.Entity_Models;

namespace Service.Interfaces
{
    public interface IRolService
    {
        EResponseBase<Role> Get();
        EResponseBase<Role> Get(int id);
        EResponseBase<Role> InsertOrUpdate(Role model);
        EResponseBase<Role> Delete(int id);
        EResponseBase<Role> Delete(Role model);
        EResponseBase<Role> Disabled(int id, bool enabled);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
