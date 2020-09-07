using Common;
using Common.Logging;
using Domain.Entity_Models;

using Infraestructure.Context;
using Infraestructure.Repositories;
using Mehdime.Entity;
using Service.Interfaces;
using System;
using System.Collections.Generic;

namespace Service.Implementations
{
    public class ConfigurationDetailService : IConfigurationDetailService
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<ConfigurationDetail, ApplicationDbContext> repository;
        private readonly IRepository<Configuration, ApplicationDbContext> repository2;
        private readonly IConfigurationLib config;
        public ICustomLog Logger { get; set; }
        public ITransaction Transaction { get; set; }

        public ConfigurationDetailService(
           IDbContextScopeFactory _dbContextScopeFactory,
           IRepository<ConfigurationDetail, ApplicationDbContext> _Repository,
           IRepository<Configuration, ApplicationDbContext> _Repository2,
           IConfigurationLib _config
       )
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _Repository;
            repository2 = _Repository2;
            config = _config;
        }

        public EResponseBase<ConfigurationDetail> Get()
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<ConfigurationDetail> result = new EResponseBase<ConfigurationDetail>();
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
                result = new UtilitariesResponse<ConfigurationDetail>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<ConfigurationDetail> Get(int id)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<ConfigurationDetail> result = new EResponseBase<ConfigurationDetail>();

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
                result = new UtilitariesResponse<ConfigurationDetail>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<ConfigurationDetail> InsertOrUpdate(ConfigurationDetail model)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<ConfigurationDetail> rh = new EResponseBase<ConfigurationDetail>();
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
                rh = new UtilitariesResponse<ConfigurationDetail>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<ConfigurationDetail> InsertOrUpdate(List<ConfigurationDetail> listModel)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<ConfigurationDetail> rh = new EResponseBase<ConfigurationDetail>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(listModel, printDebug: true);
                    foreach (ConfigurationDetail model in listModel)
                    {
                        rh = repository.InsertOrUpdate(model, model.Id);
                    };
                    rh.listado = listModel;
                    ctx.SaveChanges();
                    Logger.Print_Response(rh, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                rh = new UtilitariesResponse<ConfigurationDetail>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<ConfigurationDetail> Delete(int id)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<ConfigurationDetail> rh = new EResponseBase<ConfigurationDetail>();
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
                rh = new UtilitariesResponse<ConfigurationDetail>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<ConfigurationDetail> Delete(ConfigurationDetail model)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<ConfigurationDetail> rh = new EResponseBase<ConfigurationDetail>();
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
                rh = new UtilitariesResponse<ConfigurationDetail>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<ConfigurationDetail> Disabled(int id, bool enabled)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<ConfigurationDetail> rh = new EResponseBase<ConfigurationDetail>();
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
                rh = new UtilitariesResponse<ConfigurationDetail>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<ConfigurationDetail> GetByCode(string Code)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<ConfigurationDetail> result = new EResponseBase<ConfigurationDetail>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(Code, printDebug: true);
                    result = repository.Find(x => x.Code == Code);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<ConfigurationDetail>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<ConfigurationDetail> GetByConfigurationId(int configurationId)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<ConfigurationDetail> result = new EResponseBase<ConfigurationDetail>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(configurationId, printDebug: true);
                    result = repository.Find(x => x.ConfigurationId == configurationId);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<ConfigurationDetail>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<ConfigurationDetail> GetByConfigurationCode(string configurationCode)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<ConfigurationDetail> result = new EResponseBase<ConfigurationDetail>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(configurationCode, printDebug: true);
                    Configuration resultTemp = repository2.SingleOrDefaultWithoutEResponse(x => x.Code.ToUpper().Trim() == configurationCode.ToUpper().Trim());
                    if (resultTemp != null) result = repository.Find(x => x.ConfigurationId == resultTemp.Id);
                    else result = new UtilitariesResponse<ConfigurationDetail>(config).setResponseBaseForNoDataFound();
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<ConfigurationDetail>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }


    }
}
