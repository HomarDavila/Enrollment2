using Common;
using Common.Logging;
using Domain.Entity_Models.Identity;
using Infraestructure.Context;
using Infraestructure.Repositories;
using Mehdime.Entity;
using Service.Interfaces;
using System;

namespace Service.Implementations
{
    public class OptionTypeService : IOptionTypeService
    {


        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<OptionType, ApplicationDbContext> repository;
        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }

        public OptionTypeService(
           IDbContextScopeFactory _dbContextScopeFactory,
           IRepository<OptionType, ApplicationDbContext> _repository,
           IConfigurationLib _config
       )
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _repository;
            config = _config;
        }

        public EResponseBase<OptionType> Get()
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<OptionType> result = new EResponseBase<OptionType>();
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
                result = new UtilitariesResponse<OptionType>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<OptionType> Get(int id)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<OptionType> result = new EResponseBase<OptionType>();
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
                result = new UtilitariesResponse<OptionType>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<OptionType> InsertOrUpdate(OptionType model)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<OptionType> rh = new EResponseBase<OptionType>();
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
                rh = new UtilitariesResponse<OptionType>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<OptionType> Delete(int id)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<OptionType> rh = new EResponseBase<OptionType>();
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
                rh = new UtilitariesResponse<OptionType>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<OptionType> Delete(OptionType model)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<OptionType> rh = new EResponseBase<OptionType>();
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
                rh = new UtilitariesResponse<OptionType>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<OptionType> Disabled(int id, bool enabled)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<OptionType> rh = new EResponseBase<OptionType>();
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
                rh = new UtilitariesResponse<OptionType>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<OptionType> GetByCode(string code)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<OptionType> result = new EResponseBase<OptionType>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(code, printDebug: true);
                    result = repository.Find(x => x.Code == code);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<OptionType>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }


    }
}
