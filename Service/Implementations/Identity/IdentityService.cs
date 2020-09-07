using Common;
using Common.HttpHelpers;
using Common.Logging;
using Domain.Custom_Models;
using Domain.Entity_Models;
using Domain.Entity_Models.Identity;
using Infraestructure.Repositories;
using Mehdime.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using Service.Interfaces;
using Service.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Security;

namespace Service.Implementations
{
    public class IdentityService : IIdentityService
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<User, Infraestructure.Context.ApplicationDbContext> repository;
        private readonly IRepository<Role, Infraestructure.Context.ApplicationDbContext> repository2;
        private readonly IRepository<OptionRol, Infraestructure.Context.ApplicationDbContext> repository3;
        private readonly IRepository<UserRol, Infraestructure.Context.ApplicationDbContext> repository4;
        private readonly IRepository<Application, Infraestructure.Context.ApplicationDbContext> repository6;
        private readonly IRepository<Option, Infraestructure.Context.ApplicationDbContext> repository7;
        public ApplicationUserManager _userManager { get; set; }

        public IConfigurationLib config { get; set; }
        public ICustomLog Logger { get; set; }
        public ITransaction Transaction { get; set; }
        public ProxyCoreAPI proxyCoreAPI;

        public IdentityService(
           IDbContextScopeFactory _dbContextScopeFactory,
           IRepository<User, Infraestructure.Context.ApplicationDbContext> _repository,
           IRepository<Role, Infraestructure.Context.ApplicationDbContext> _repository2,
           IRepository<OptionRol, Infraestructure.Context.ApplicationDbContext> _repository3,
           IRepository<UserRol, Infraestructure.Context.ApplicationDbContext> _repository4,
           IRepository<Application, Infraestructure.Context.ApplicationDbContext> _repository6,
           IRepository<Option, Infraestructure.Context.ApplicationDbContext> _repository7,
           IConfigurationLib _config
       )
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _repository;
            repository2 = _repository2;
            repository3 = _repository3;
            repository4 = _repository4;
            repository6 = _repository6;
            repository7 = _repository7;
            proxyCoreAPI = new ProxyCoreAPI();
            config = _config;
        }

        public bool ExistUser(string user)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            using (log4net.NDC.Push(Logger.Header))
            {
                try
                {
                    User userResponse = null;
                    using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                    {
                        Logger.Print_InitMethod();
                        Logger.Print_Request(String.Format("User: {0}", user), printDebug: true);
                        userResponse = repository.FirstOrDefaultWithoutEResponse(filter: x => x.UserName.ToLower().Trim() == user.ToLower().Trim()
                                                                                               && x.Enabled == true);
                        if (userResponse == null) return false;
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e.Message);
                    return false;
                }
                finally
                {
                    Logger.Print_EndMethod();
                }
            }
        }

        public EResponseBase<Role> GetRolesByUserId(int userId, string applicationCode)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Role> result = new EResponseBase<Role>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    Application applicationResponse = repository6.FirstOrDefaultWithoutEResponse(filter: x => x.Code == applicationCode);
                    if (applicationResponse == null) result = new UtilitariesResponse<Role>(config).setResponseBaseForNoDataFound();
                    else
                    {
                        IQueryable<UserRol> userRolResponse = repository4.FindWithoutEResponse(filter: x => x.UserId == userId && x.ApplicationId == applicationResponse.Id);
                        if (userRolResponse == null) result = new UtilitariesResponse<Role>(config).setResponseBaseForNoDataFound();
                        else if (!userRolResponse.Any()) result = new UtilitariesResponse<Role>(config).setResponseBaseForNoDataFound();
                        else
                        {
                            IQueryable<Role> roles = from userRol in userRolResponse
                                                     join rol in repository2.FindWithoutEResponse()
                                                     on userRol.RoleId equals rol.Id
                                                     select rol;
                            result = new UtilitariesResponse<Role>(config).setResponseBaseForList(roles);
                            result.IsOK = true;
                        }
                    }
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<Role>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public bool HavePermissions(string optionCode, int rolId, string applicationCode)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    string dataRequest = JsonConvert.SerializeObject(null);
                    Logger.Print_Request(dataRequest, printDebug: true);
                    Application applicationResponse = repository6.FirstOrDefaultWithoutEResponse(filter: x => x.Code == applicationCode);
                    if (applicationResponse == null) return false;
                    if (!applicationResponse.Enabled.Value) return false;
                    Option optionResponse = repository7.FirstOrDefaultWithoutEResponse(filter: x => x.Code == optionCode);
                    if (optionResponse == null) return false;
                    if (!optionResponse.Enabled.Value) return false;
                    List<OptionRol> optionRolResponse = repository3.FindWithoutEResponse(filter: x => x.RolId == rolId && x.ApplicationId == applicationResponse.Id && x.OptionId == optionResponse.Id).ToList();
                    if (optionRolResponse == null) return false;
                    else return true;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return false;
            }
            finally
            {
                Logger.Print_EndMethod();
            }
        }

        public async Task<EResponseBase<SimpleEntity>> Register(User user)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            using (log4net.NDC.Push(Logger.Header))
            {
                EResponseBase<SimpleEntity> rh = new EResponseBase<SimpleEntity>();
                List<string> errorAnswers = new List<string>();
                try
                {
                    using (IDbContextScope ctx = dbContextScopeFactory.Create())
                    {
                        Logger.Print_InitMethod();
                        Logger.Print_Request(user, printDebug: true);
                        _userManager.changeDBContext(ctx.DbContexts.Get<Infraestructure.Context.ApplicationDbContext>());
                        rh = NotExistUserInLocalDB(user);
                        if (rh.Code == config.CodigoExito)
                        {
                            //var responseExistMember = Task.Run(() => ExistUserInMembersDB(user, null)).Result;

                            EResponseBase<MemberResponseV3> responseExistMember = await ExistUserInMembersDB(user, null);
                            if (responseExistMember.Code == config.CodigoExito)
                            {
                                if (responseExistMember.objeto != null)
                                {
                                    if (responseExistMember.objeto.MCOId > 0)
                                    {
                                        user.MemberId = responseExistMember.objeto.Id;
                                        user.MPI = responseExistMember.objeto.MPIShort;

                                        if (responseExistMember.objeto.CountOfMembers == 1)
                                        {
                                            if (user.Roles != null)
                                            {
                                                UserRol userRol = user.Roles.FirstOrDefault();
                                                int ApplicationId = userRol.ApplicationId;


                                                rh = await CreateAccountCore(user);

                                                if (rh.Code == config.CodigoExito)
                                                {
                                                    UserRol userRolInserted = user.Roles.FirstOrDefault();
                                                    userRolInserted.ApplicationId = ApplicationId;
                                                    userRolInserted.Enabled = true;
                                                    userRolInserted.CreatedOn = DateTime.Today;
                                                    repository4.InsertOrUpdate(userRolInserted, userRolInserted.Id);
                                                    ctx.SaveChanges();
                                                }
                                            }
                                            else rh = new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForWithouRoles();

                                        }
                                        else if (responseExistMember.objeto.CountOfMembers == 0) rh = new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForUserNoExist();
                                        else rh = new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForMoreThanOneUserFound();
                                    }
                                    else
                                    {
                                        rh = new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForUserNoExist();
                                    }
                                }
                                else
                                {
                                    rh = new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForUserNoExist();
                                }
                            }
                            else
                            {
                                rh = new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForUserNoExist();
                            }
                        }
                        Logger.Print_Response(rh, printDebug: true);
                        Logger.Print_EndMethod();
                    }
                }
                catch (Exception e)
                {
                    rh = new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForException(e);
                    Logger.Error(e.Message);
                }
                return rh;
            }
        }

        public async Task<EResponseBase<User>> ResetPassword(string userName, string newPassword)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            using (log4net.NDC.Push(Logger.Header))
            {
                EResponseBase<User> rh = new EResponseBase<User>();
                try
                {
                    using (IDbContextScope ctx = dbContextScopeFactory.Create())
                    {
                        Logger.Print_InitMethod();
                        Logger.Print_Request($"userName: {userName}", printDebug: true);
                        EResponseBase<User> userResponse = repository.Find(filter: x => x.UserName.ToLower().Trim() == userName.ToLower().Trim());
                        if (userResponse.Code == config.CodigoExito)
                        {
                            User user = userResponse.listado.FirstOrDefault();
                            rh = ValidateUser(user.Id);
                            if (rh.Code == config.CodigoExito)
                            {
                                _userManager.changeDBContext(ctx.DbContexts.Get<Infraestructure.Context.ApplicationDbContext>());
                                string code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                                IdentityResult result = await _userManager.ResetPasswordAsync(user.Id, code, newPassword);
                                var _user = _userManager.FindById(user.Id);
                                _user.HasDefaultCredentials = true;
                                _userManager.Update(_user);
                                User userTempResponse = repository.FirstOrDefaultWithoutEResponse(x => x.Id == user.Id);
                                if (!result.Succeeded) rh = new UtilitariesResponse<User>(config).setResponseBaseForValidationExceptionString(result.Errors.ToList());
                                else
                                {
                                    userTempResponse.PassWithoutEncrypt = newPassword;
                                    rh = new UtilitariesResponse<User>(config).setResponseBaseForObj(userTempResponse);
                                }

                            }
                        }
                        else
                        {
                            rh.Code = userResponse.Code;
                            rh.Message = userResponse.Message;
                            rh.FunctionalErrors = userResponse.FunctionalErrors;
                            rh.MessageEN = userResponse.MessageEN;
                            rh.TechnicalErrors = userResponse.TechnicalErrors;
                        }
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
        }

        public async Task<EResponseBase<User>> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            using (log4net.NDC.Push(Logger.Header))
            {
                EResponseBase<User> rh = new EResponseBase<User>();

                try
                {
                    using (IDbContextScope ctx = dbContextScopeFactory.Create())
                    {

                        Logger.Print_InitMethod();
                        Logger.Print_Request(userId, printDebug: true);
                        rh = ValidateUser(userId);
                        if (rh.Code == config.CodigoExito)
                        {
                            _userManager.changeDBContext(ctx.DbContexts.Get<Infraestructure.Context.ApplicationDbContext>());
                            IdentityResult result = await _userManager.ChangePasswordAsync(userId, oldPassword, newPassword);
                            var _user = _userManager.FindById(userId);
                            _user.HasDefaultCredentials = false;
                            _userManager.Update(_user);
                            User userTempResponse = repository.FirstOrDefaultWithoutEResponse(x => x.Id == userId);
                            if (!result.Succeeded) rh = new UtilitariesResponse<User>(config).setResponseBaseForValidationExceptionString(result.Errors.ToList());
                            else rh = new UtilitariesResponse<User>(config).setResponseBaseForObj(userTempResponse);
                        }
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
        }
        public async Task<EResponseBase<User>> ChangePasswordExternal(string userName, string mpi, string newPassword)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            using (log4net.NDC.Push(Logger.Header))
            {
                EResponseBase<User> rh = new EResponseBase<User>();

                try
                {
                    using (IDbContextScope ctx = dbContextScopeFactory.Create())
                    {

                        Logger.Print_InitMethod();
                        Logger.Print_Request(userName, printDebug: true);
                        rh = ValidateUserByName(userName);
                        if (rh.Code == config.CodigoExito)
                        {
                            _userManager.changeDBContext(ctx.DbContexts.Get<Infraestructure.Context.ApplicationDbContext>());

                            var _user = await _userManager.FindByNameAsync(userName);
                            if (_user.MPI != mpi)
                            {
                                rh = new UtilitariesResponse<User>(config).setResponseBaseForObj(_user);
                            }
                            else
                            {
                                await _userManager.RemovePasswordAsync(_user.Id);
                                var result = await _userManager.AddPasswordAsync(_user.Id, newPassword);
                                _user.HasDefaultCredentials = false;
                                _user.CreatedBy = "ImportedRegistered";
                                await _userManager.UpdateAsync(_user);
                                //User userTempResponse = repository.FirstOrDefaultWithoutEResponse(x => x.Id == userId);
                                if (!result.Succeeded)
                                    rh = new UtilitariesResponse<User>(config).setResponseBaseForValidationExceptionString(result.Errors.ToList());
                                else
                                    rh = new UtilitariesResponse<User>(config).setResponseBaseForObj(_user);
                            }
                        }
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
        }

        public EResponseBase<SimpleEntity> UnlockUser(int userId)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<SimpleEntity> rh = new EResponseBase<SimpleEntity>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(userId, printDebug: true);
                    User user = new User { Id = userId };
                    user.AccessFailedCount = 0;
                    user.LockoutEnabled = false;
                    user.LockoutEndDateUtc = null;
                    repository.DbContext.Entry(user).Property("AccessFailedCount").IsModified = true;
                    repository.DbContext.Entry(user).Property("LockoutEnabled").IsModified = true;
                    repository.DbContext.Entry(user).Property("LockoutEndDateUtc").IsModified = true;
                    ctx.SaveChanges();
                    rh = new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForOK(new SimpleEntity() { Id = userId });
                    Logger.Print_Response(rh, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                rh = new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<User> UpdatePhone(int userId, string phoneNumber)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<User> rh = new EResponseBase<User>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(String.Format(" userId: {0}  phoneNumber: {1}", userId, phoneNumber), printDebug: true);
                    User user = new User { Id = userId };
                    rh = ValidateUser(userId);
                    if (rh.Code == config.CodigoExito)
                    {
                        user.PhoneNumber = phoneNumber;
                        repository.DbContext.Set<User>().Attach(user);
                        user.PhoneNumber = phoneNumber;
                        repository.DbContext.Entry(user).Property("PhoneNumber").IsModified = true;
                        ctx.SaveChanges();
                        User userTempResponse = repository.FirstOrDefaultWithoutEResponse(x => x.Id == user.Id);
                        rh = new UtilitariesResponse<User>(config).setResponseBaseForObj(userTempResponse);
                    }
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

        public EResponseBase<User> UpdateEmail(int userId, string email)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<User> rh = new EResponseBase<User>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(String.Format(" userId: {0}  email: {1}", userId, email), printDebug: true);
                    User user = new User { Id = userId };
                    rh = ValidateUser(userId);
                    if (rh.Code == config.CodigoExito)
                    {
                        repository.DbContext.Set<User>().Attach(user);
                        user.Email = email;
                        repository.DbContext.Entry(user).Property("Email").IsModified = true;
                        ctx.SaveChanges();
                        User userTempResponse = repository.FirstOrDefaultWithoutEResponse(x => x.Id == user.Id);
                        rh = new UtilitariesResponse<User>(config).setResponseBaseForObj(userTempResponse);
                    }
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

        public EResponseBase<User> UpdatePersonalData(int userId, string email, string phoneNumber, string name, string firstLastName, string secondLastName, string identifyNumber, bool isAdminitrador)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<User> rh = new EResponseBase<User>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(String.Format(" userId: {0}  email: {1}", userId, email), printDebug: true);
                    User user = new User { Id = userId };
                    rh = ValidateUser(userId);
                    if (rh.Code == config.CodigoExito)
                    {
                        repository.DbContext.Set<User>().Attach(user);
                        user.Email = email;
                        user.LastName1 = firstLastName;
                        user.LastName2 = secondLastName;
                        user.SSNLast4 = identifyNumber;
                        user.FirstName = name;
                        user.PhoneNumber = phoneNumber;
                        user.IsAdministrator = isAdminitrador;
                        repository.DbContext.Entry(user).Property("Email").IsModified = true;
                        repository.DbContext.Entry(user).Property("FirstLastName").IsModified = true;
                        repository.DbContext.Entry(user).Property("SecondLastName").IsModified = true;
                        repository.DbContext.Entry(user).Property("IdentifyNumber").IsModified = true;
                        repository.DbContext.Entry(user).Property("Name").IsModified = true;
                        repository.DbContext.Entry(user).Property("PhoneNumber").IsModified = true;
                        repository.DbContext.Entry(user).Property("IsAdministrator").IsModified = true;
                        ctx.SaveChanges();
                        User userTempResponse = repository.FirstOrDefaultWithoutEResponse(x => x.Id == user.Id);
                        rh = new UtilitariesResponse<User>(config).setResponseBaseForObj(userTempResponse);
                        Logger.Print_Response(rh, printDebug: true);
                        Logger.Print_EndMethod();
                    }
                }
            }
            catch (Exception e)
            {
                rh = new UtilitariesResponse<User>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<OptionRol> GetOptionsByUserId(int userId, string applicationCode)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<OptionRol> result = new EResponseBase<OptionRol>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    Application applicationResponse = repository6.FirstOrDefaultWithoutEResponse(filter: x => x.Code == applicationCode);
                    if (applicationResponse == null) result = new UtilitariesResponse<OptionRol>(config).setResponseBaseForNoDataFound();
                    else
                    {
                        IQueryable<OptionRol> userRolResponse = repository3.FindWithoutEResponse(x => x.ApplicationId == applicationResponse.Id, null, x => x.Application, x => x.Option, x => x.Rol);
                        result = new UtilitariesResponse<OptionRol>(config).setResponseBaseForList(userRolResponse);
                    }
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<OptionRol>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public async Task<User> Find(string userName, string password)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            using (log4net.NDC.Push(Logger.Header))
            {
                try
                {
                    using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                    {
                        _userManager.changeDBContext(ctx.DbContexts.Get<Infraestructure.Context.ApplicationDbContext>());
                        User user = await _userManager.FindAsync(userName, password);
                        return user;
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e.Message);
                    return null;
                }
            }

        }

        public async Task<User> Login(string userName, string password)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            using (log4net.NDC.Push(Logger.Header))
            {
                //User user = null;
                try
                {
                    using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                    {
                        _userManager.changeDBContext(ctx.DbContexts.Get<Infraestructure.Context.ApplicationDbContext>());

                        User user = await _userManager.FindByNameAsync(userName);
                        if (user.CreatedBy == "Imported")
                            return user;
                        //if (user != null)
                        //{
                        //user = new User { UserName = userName };
                        user = await _userManager.FindAsync(userName, password);
                        //if (!isLoginOk) user = null;
                        //}
                        return user;
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e.Message);
                    return null;
                }
            }

        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(User user, string authenticationType)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            using (log4net.NDC.Push(Logger.Header))
            {
                try
                {
                    using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                    {
                        _userManager.changeDBContext(ctx.DbContexts.Get<Infraestructure.Context.ApplicationDbContext>());
                        ClaimsIdentity userIdentityResponse = await _userManager.CreateIdentityAsync(user, authenticationType);
                        return userIdentityResponse;
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e.Message);
                    return null;
                }
            }
        }

        public EResponseBase<User> ValidateUser(int userId)
        {
            EResponseBase<User> rh = new EResponseBase<User>();
            EResponseBase<User> user = repository.FirstOrDefault(x => x.Id == userId);
            if (user.Code != config.CodigoExito)
                rh = new UtilitariesResponse<User>(config).setResponseBaseForUserNoExist();
            else
            {
                if (user.objeto == null)
                    rh = new UtilitariesResponse<User>(config).setResponseBaseForUserNoExist();
                else
                {
                    if (!user.objeto.Enabled.HasValue)
                        if (!user.objeto.Enabled.Value)
                            rh = new UtilitariesResponse<User>(config).setResponseBaseForUserNoEnabled();
                        else
                            rh = new UtilitariesResponse<User>(config).setResponseBaseForOK();
                    else rh = new UtilitariesResponse<User>(config).setResponseBaseForOK();
                }
            }
            return rh;
        }
        public EResponseBase<User> ValidateUserByName(string userName)
        {
            EResponseBase<User> rh = new EResponseBase<User>();
            EResponseBase<User> user = repository.FirstOrDefault(x => x.UserName == userName);
            if (user.Code != config.CodigoExito)
                rh = new UtilitariesResponse<User>(config).setResponseBaseForUserNoExist();
            else
            {
                if (user.objeto == null)
                    rh = new UtilitariesResponse<User>(config).setResponseBaseForUserNoExist();
                else
                {
                    if (!user.objeto.Enabled.HasValue)
                        if (!user.objeto.Enabled.Value)
                            rh = new UtilitariesResponse<User>(config).setResponseBaseForUserNoEnabled();
                        else
                            rh = new UtilitariesResponse<User>(config).setResponseBaseForOK();
                    else rh = new UtilitariesResponse<User>(config).setResponseBaseForOK();
                }
            }
            return rh;
        }

        private async Task<EResponseBase<SimpleEntity>> CreateAccountCore(User model)
        {
            EResponseBase<SimpleEntity> rh = new EResponseBase<SimpleEntity>();

            IdentityResult result = await _userManager.CreateAsync(model, model.PassWithoutEncrypt);

            if (result.Succeeded)
            {
                //Encriptar Password
                //    IdentityResult result2 = await _userManager.AddPasswordAsync(model.Id, model.PassWithoutEncrypt);                
                rh = new UtilitariesResponse<SimpleEntity>(config).
                    setResponseBaseForOK(new SimpleEntity() { Id = model.Id });
            }
            else
            {
                rh = new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForValidationExceptionString(result.Errors.ToList());
            }
            return rh;
        }

        private EResponseBase<SimpleEntity> NotExistUserInLocalDB(User model)
        {
            EResponseBase<SimpleEntity> rh = new EResponseBase<SimpleEntity>();
            bool existUser = ExistUser(model.UserName);
            bool existUser2 = ExistUser2(model.ZipCode, model.SSNLast4, model.DateOfBirth.Value);

            if (existUser || existUser2)
            {
                rh = new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForUserExist();
            }
            else
            {
                rh = new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForOK();
            }


            return rh;
        }

        private bool ExistUser2(string zipCode, string sSNLast4, DateTime dateOfBirth)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            using (log4net.NDC.Push(Logger.Header))
            {
                try
                {
                    User userResponse = null;
                    using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                    {
                        Logger.Print_InitMethod();
                        Logger.Print_Request(null, printDebug: true);
                        userResponse = repository.FirstOrDefaultWithoutEResponse(filter: x => (x.ZipCode.ToLower().Trim() == zipCode.ToLower().Trim()
                                                                                                 && x.SSNLast4.ToLower().Trim() == sSNLast4.ToLower().Trim()
                                                                                                 && x.DateOfBirth == dateOfBirth && x.Enabled == true
                                                                                              ));
                        if (userResponse == null) return false;
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e.Message);
                    return false;
                }
                finally
                {
                    Logger.Print_EndMethod();
                }
            }
        }

        private async Task<EResponseBase<MemberResponseV3>> ExistUserInMembersDB(User model, string token)
        {
            Transaction transaction = string.Empty.GetTransaction();
            MemberRequestV3 request = new MemberRequestV3()
            {
                Last4SSN = model.SSNLast4,
                DateOfBirth = model.DateOfBirth.Value,
                ZipCode = model.ZipCode,
                FirstLastName = model.LastName1,
                SecondLastName = model.LastName2,
                FirstName = model.FirstName
            };
            EResponseBase<MemberResponseV3> existUser = await proxyCoreAPI.GetPeopleByFilters(transaction, Logger, config, token, request);
            return existUser;
        }

    }
}
