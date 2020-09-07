using Common;
using Common.Logging;
using Domain.Entity_Models.Identity;
using Infraestructure.Context;
using Infraestructure.Repositories;
using Mehdime.Entity;
using Newtonsoft.Json;
using Service.Interfaces;
using System;

namespace Service.Implementations
{
    public class OptionService : IOptionService
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<Option, ApplicationDbContext> repository;
        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }

        public OptionService(
           IDbContextScopeFactory _dbContextScopeFactory,
           IRepository<Option, ApplicationDbContext> _repository,
           IConfigurationLib _config
       )
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _repository;
            config = _config;
        }

        public EResponseBase<Option> Get()
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Option> result = new EResponseBase<Option>();
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
                result = new UtilitariesResponse<Option>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<Option> Get(int id)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Option> result = new EResponseBase<Option>();
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
                result = new UtilitariesResponse<Option>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<Option> InsertOrUpdate(Option model)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Option> rh = new EResponseBase<Option>();
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
                rh = new UtilitariesResponse<Option>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<Option> Delete(int id)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Option> rh = new EResponseBase<Option>();
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
                rh = new UtilitariesResponse<Option>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<Option> Delete(Option model)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Option> rh = new EResponseBase<Option>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    string dataRequest = JsonConvert.SerializeObject(model);
                    Logger.Print_Request(dataRequest, printDebug: true);
                    rh = repository.Delete(model);
                    ctx.SaveChanges();
                    string dataResponse = JsonConvert.SerializeObject(rh);
                    Logger.Print_Response(dataResponse, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                rh = new UtilitariesResponse<Option>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<Option> Disabled(int id, bool enabled)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Option> rh = new EResponseBase<Option>();
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
                rh = new UtilitariesResponse<Option>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<Option> GetByCode(string code)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Option> result = new EResponseBase<Option>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(code, printDebug: true);
                    result = repository.Find(x => x.Code == code);
                    string dataResponse = JsonConvert.SerializeObject(result);
                    Logger.Print_Response(dataResponse, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<Option>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<Option> GetByTypeId(int typeId)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Option> result = new EResponseBase<Option>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(typeId, printDebug: true);
                    result = repository.Find(x => x.OptionTypeId == typeId);
                    string dataResponse = JsonConvert.SerializeObject(result);
                    Logger.Print_Response(dataResponse, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<Option>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }


    }
}
