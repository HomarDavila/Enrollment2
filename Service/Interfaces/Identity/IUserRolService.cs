using Common;
using Common.Logging;
using Domain.Entity_Models.Identity;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IUserRolService
    {
        EResponseBase<UserRol> Get();
        EResponseBase<UserRol> GetByExecutorIds(List<int> rolExecutorIds);
        EResponseBase<UserRol> Get(int id);
        EResponseBase<UserRol> InsertOrUpdate(UserRol model);
        EResponseBase<UserRol> Delete(UserRol model);
        EResponseBase<UserRol> Delete(int id);
        EResponseBase<UserRol> Disabled(int id, bool enabled);


        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
