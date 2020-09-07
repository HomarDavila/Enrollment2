using Domain.Entity_Models;
using Domain.Entity_Models.Identity;
using Infraestructure.Context;
using Mehdime.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Service
{
    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(IUserStore<User, int> store)
       : base(store)
        {
        }
        public void changeDBContext(Infraestructure.Context.ApplicationDbContext dbcontext)
        {
            Store = new UserStore<User, Role, int, UserLogin, UserRol, UserClaim>(dbcontext);
        }
    }

    public class UserManagerService : IUserManagerService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        public UserManagerService(IDbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }
        public ApplicationUserManager Create()
        {
            using (IDbContextScope ctx = _dbContextScopeFactory.Create())
            {
                UserStore<User, Role, int, UserLogin, UserRol, UserClaim> userStore = new UserStore<User, Role, int, UserLogin, UserRol, UserClaim>(ctx.DbContexts.Get<ApplicationDbContext>());
                ApplicationUserManager manager = new ApplicationUserManager(userStore);
                return manager;
            }
        }
    }

    public interface IUserManagerService
    {
        ApplicationUserManager Create();
    }

}
