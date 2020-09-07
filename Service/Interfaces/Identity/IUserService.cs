using Common;
using Common.Logging;
using Domain.Entity_Models;

namespace Service.Interfaces
{
    public interface IUserService
    {
        EResponseBase<User> Get();
        EResponseBase<User> Get(int id);
        EResponseBase<User> GetByUserName(string username);
        EResponseBase<User> GetByEmail(string email);
        EResponseBase<User> GetByPhone(string phone);
        EResponseBase<User> InsertOrUpdate(User model);
        EResponseBase<User> Delete(int id);
        EResponseBase<User> Delete(User model);
        EResponseBase<User> Disabled(int id, bool enabled);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
