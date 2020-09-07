using Common;
using Common.HttpHelpers;
using Common.Logging;
using Domain.Entity_Models;
using Domain.Entity_Models.Identity;
using Infraestructure.Context;
using Infraestructure.Repositories;
using Mehdime.Entity;
using Newtonsoft.Json;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Implementations
{
    public class UserRolService : IUserRolService
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<UserRol, ApplicationDbContext> repository;
        private readonly IRepository<User, Infraestructure.Context.ApplicationDbContext> repository2;
        private readonly IRepository<Application, Infraestructure.Context.ApplicationDbContext> repository3;
        private readonly IRepository<Role, Infraestructure.Context.ApplicationDbContext> repository4;
        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }

        public UserRolService(
         IDbContextScopeFactory _dbContextScopeFactory,
         IRepository<UserRol, ApplicationDbContext> _repository,
         IRepository<User, ApplicationDbContext> _repository2,
         IRepository<Application, ApplicationDbContext> _repository3,
         IRepository<Role, ApplicationDbContext> _repository4,
         IConfigurationLib _config
        )
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _repository;
            repository2 = _repository2;
            repository3 = _repository3;
            repository4 = _repository4;
            config = _config;
        }

        public EResponseBase<UserRol> Delete(UserRol model)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<UserRol> rh = new EResponseBase<UserRol>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    string dataRequest = JsonConvert.SerializeObject(model);
                    Logger.Print_Request(dataRequest, printDebug: true);
                    UserRol entityToDelete = repository.SingleOrDefaultWithoutEResponse(filter: x => (x.ApplicationId == model.ApplicationId && x.RoleId == model.RoleId && x.UserId == model.UserId));
                    rh = repository.Delete(entityToDelete);
                    ctx.SaveChanges();
                    string dataResponse = JsonConvert.SerializeObject(rh);
                    Logger.Print_Response(dataResponse, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                rh = new UtilitariesResponse<UserRol>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }
            return rh;
        }

        public EResponseBase<UserRol> InsertOrUpdate(UserRol model)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<UserRol> rh = new EResponseBase<UserRol>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(model, printDebug: true);
                    rh = repository.InsertOrUpdate(model, model.Id);
                    ctx.SaveChanges();
                    Logger.Print_Response(rh, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                rh = new UtilitariesResponse<UserRol>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<UserRol> Get()
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<UserRol> result = new EResponseBase<UserRol>();
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
                result = new UtilitariesResponse<UserRol>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<UserRol> GetByExecutorIds(List<int> rolExecutorIds)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<UserRol> result = new EResponseBase<UserRol>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(rolExecutorIds, printDebug: true);
                    foreach (int id in rolExecutorIds)
                    {
                        if (result.listado == null)
                            result = repository.Find(x => x.Id == id);
                        else
                            result.listado.ToList().AddRange(repository.Find(x => x.Id == id).listado.ToList());
                    }
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<UserRol>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<UserRol> Get(int id)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<UserRol> result = new EResponseBase<UserRol>();
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
                result = new UtilitariesResponse<UserRol>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<UserRol> Delete(int id)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<UserRol> rh = new EResponseBase<UserRol>();
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
                rh = new UtilitariesResponse<UserRol>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<UserRol> Disabled(int id, bool enabled)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<UserRol> rh = new EResponseBase<UserRol>();
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
                rh = new UtilitariesResponse<UserRol>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }


        private List<string> validateInsert(List<UserRol> model, List<string> errorList)
        {
            foreach (UserRol optionRol in model)
            {
                errorList = isEnabledRol(optionRol.RoleId, errorList);
                errorList = isEnabledApplication(optionRol.ApplicationId, errorList);
                errorList = isEnabledUser(optionRol.UserId, errorList);
            }
            return errorList;
        }

        private List<string> isEnabledUser(int userId, List<string> errorList)
        {
            EResponseBase<User> response = repository2.FirstOrDefault(x => x.Id == userId);
            if (response.Code != config.CodigoExito)
                errorList.Add(String.Format("User {0} not found", userId));
            else
            {
                if (response.objeto == null)
                    errorList.Add(String.Format("User {0} not found", userId));
                else
                {
                    if (!response.objeto.Enabled.Value)
                        errorList.Add(String.Format("User {0} is not active", userId));
                }
            }

            return errorList;
        }

        private List<string> isEnabledApplication(int applicationId, List<string> errorList)
        {
            EResponseBase<Application> response = repository3.FirstOrDefault(x => x.Id == applicationId);
            if (response.Code != config.CodigoExito)
                errorList.Add(String.Format("Application {0} not found", applicationId));
            else
            {
                if (response.objeto == null)
                    errorList.Add(String.Format("Application {0} not found", applicationId));
                else
                {
                    if (!response.objeto.Enabled.Value)
                        errorList.Add(String.Format("Application {0} is not active", applicationId));
                }
            }

            return errorList;
        }

        private List<string> isEnabledRol(int rolId, List<string> errorList)
        {
            EResponseBase<User> response = repository2.FirstOrDefault(x => x.Id == rolId);
            if (response.Code != config.CodigoExito)
                errorList.Add(String.Format("Rol {0} not found", rolId));
            else
            {
                if (response.objeto == null)
                    errorList.Add(String.Format("Rol {0} not found", rolId));
                else
                {
                    if (!response.objeto.Enabled.Value)
                        errorList.Add(String.Format("Rol {0} is not active", rolId));
                }
            }

            return errorList;
        }
    }
}
