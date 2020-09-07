using Common;
using Common.Logging;
using Domain.Custom_Models;
using Domain.Entity_Models;
using Domain.Entity_Models.Identity;
using Infraestructure.Context;
using Infraestructure.Repositories;
using Mehdime.Entity;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<User, Infraestructure.Context.ApplicationDbContext> repository;
        private readonly IRepository<Role, Infraestructure.Context.ApplicationDbContext> RoleRepository;
        private readonly IRepository<UserRol, Infraestructure.Context.ApplicationDbContext> UserRoleRepository;
        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }

        public UserService(
           IDbContextScopeFactory _dbContextScopeFactory,
           IRepository<User, Infraestructure.Context.ApplicationDbContext> _repository,
           IRepository<Role, Infraestructure.Context.ApplicationDbContext> _roleRepository,
           IRepository<UserRol, Infraestructure.Context.ApplicationDbContext> _userRoleRepository,
           IConfigurationLib _config
       )
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _repository;
            RoleRepository = _roleRepository;
            UserRoleRepository = _userRoleRepository;
            config = _config;
        }

        public EResponseBase<User> Get()
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<User> result = new EResponseBase<User>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    result = repository.Find();
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<User>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<User> Get(int id)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<User> result = new EResponseBase<User>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(id, printDebug: true);
                    result = repository.SingleOrDefault(x => x.Id == id);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<User>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<User> InsertOrUpdate(User model)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<User> rh = new EResponseBase<User>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(model, printDebug: true);
                    EResponseBase<User> userlist = repository.Find(x => x.Id == model.Id);
                    User user = userlist.listado.FirstOrDefault();
                    if (user != null)
                    {
                        //user.UserName = user.UserName;
                        //model.ZipCode = user.ZipCode;
                        //model.IsAdministrator = user.IsAdministrator;
                        //model.PasswordHash = user.PasswordHash;
                        //model.SecurityStamp = user.SecurityStamp;
                        //model.TwoFactorEnabled = user.TwoFactorEnabled;
                        //model.LockoutEndDateUtc = user.LockoutEndDateUtc;
                        user.FirstName = string.IsNullOrEmpty(model.FirstName) ? user.FirstName : model.FirstName;
                        user.LastName1 = string.IsNullOrEmpty(model.LastName1) ? user.LastName1 : model.LastName1;
                        user.LastName2 = string.IsNullOrEmpty(model.LastName2) ? user.LastName2 : model.LastName2;
                        user.DateOfBirth = (model.DateOfBirth == null) ? user.DateOfBirth : model.DateOfBirth;
                        user.Email = string.IsNullOrEmpty(model.Email) ? user.Email : model.Email;
                        user.Email2 = string.IsNullOrEmpty(model.Email2) ? user.Email2 : model.Email2;
                        user.EmailConfirmed = model.EmailConfirmed;
                        user.PhoneNumber = string.IsNullOrEmpty(model.PhoneNumber) ? user.PhoneNumber : model.PhoneNumber;
                        user.PhoneNumber2 = string.IsNullOrEmpty(model.PhoneNumber2) ? user.PhoneNumber2 : model.PhoneNumber2;
                        user.PhoneNumberConfirmed = model.PhoneNumberConfirmed;
                        user.LockoutEnabled = model.LockoutEnabled;
                        user.AccessFailedCount = model.AccessFailedCount;
                        user.SSNLast4 = string.IsNullOrEmpty(model.SSNLast4) ? user.SSNLast4 : model.SSNLast4;
                        user.OptIn = model.OptIn;
                        user.MPI = string.IsNullOrEmpty(model.MPI) ? user.MPI : model.MPI;
                        //rh = repository.InsertOrUpdate(model, model.Id);
                        rh = repository.Update(user);
                    }
                    else
                    {
                        rh = repository.Insert(model);
                    }
                    ctx.SaveChanges();
                    Logger.Print_Response(rh, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                rh = new UtilitariesResponse<User>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<User> Delete(int id)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<User> rh = new EResponseBase<User>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(id, printDebug: true);
                    rh = repository.Delete(id);
                    ctx.SaveChanges();
                    Logger.Print_Response(rh, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                rh = new UtilitariesResponse<User>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<User> Delete(User model)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<User> rh = new EResponseBase<User>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(model, printDebug: true);
                    rh = repository.Delete(model);
                    ctx.SaveChanges();
                    Logger.Print_Response(rh, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                rh = new UtilitariesResponse<User>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<User> Disabled(int id, bool enabled)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<User> rh = new EResponseBase<User>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(String.Format("id: {0},  enabled: {1}", id, enabled), printDebug: true);
                    rh = repository.ChangeEnabled(id, enabled);
                    ctx.SaveChanges();
                    Logger.Print_Response(rh, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                rh = new UtilitariesResponse<User>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<User> GetByUserName(string username)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<User> result = new EResponseBase<User>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(username, printDebug: true);

                    ApplicationDbContext context = ctx.DbContexts.Get<ApplicationDbContext>();

                    IQueryable<User> queryUser = repository.FindWithoutEResponse(x => x.UserName == username);
                    User usuario = queryUser.FirstOrDefault();
                    int RoleId = usuario.Roles.FirstOrDefault().RoleId;


                    IQueryable<Role> queryRoles = RoleRepository.
                        FindWithoutEResponse(x => x.Id == RoleId);

                    IQueryable<OptionCustomModel> queryOptions = (from opt in context.Options
                                                       join opr in context.OptionRols on opt.Id equals opr.OptionId
                                                       join rlu in context.UserRols on opr.RolId equals rlu.RoleId
                                                       join rol in context.Roles on rlu.RoleId equals rol.Id
                                                       where rlu.UserId == usuario.Id
                                                      && opt.Enabled == true
                                                      && rlu.Enabled == true
                                                      && rol.Enabled == true
                                                       select new OptionCustomModel()
                                                       {
                                                           Id = opt.Id,
                                                           Code = opt.Code,
                                                           Name = opt.Name,
                                                           Url = opt.URL
                                                       });

                    IQueryable<Member> queryMembers = context.Members.Include(x => x.PCP)
                                           .Include(x => x.PMG)
                                           .Include(x => x.MCO)
                                           .Where(x => x.Id == usuario.MemberId);


                    usuario.listRoles = queryRoles.ToList();
                    usuario.listOptions = queryOptions.ToList();
                    usuario.Member = queryMembers.FirstOrDefault();

                    result.objeto = usuario;
                    //Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<User>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<User> GetByEmail(string email)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<User> result = new EResponseBase<User>();

            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(email, printDebug: true);
                    result = repository.Find(x => x.Email == email);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<User>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<User> GetByPhone(string phone)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<User> result = new EResponseBase<User>();

            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(phone, printDebug: true);
                    result = repository.Find(x => x.PhoneNumber == phone);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<User>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }





    }
}
