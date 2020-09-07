using Common;
using Common.Logging;
using Domain.Entity_Models.Identity;
using Infraestructure.Context;
using Infraestructure.Repositories;
using Mehdime.Entity;
using Service.Interfaces;
using Service.Interfaces.Identity;
using System;
using System.Linq;

namespace Service.Implementations.Identity
{
    public class ApplicationService : IApplicationService
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<Application, ApplicationDbContext> repository;
        private readonly IConfigurationLib config;
        public ICustomLog Logger { get; set; }
        public ITransaction Transaction { get; set; }


        public ApplicationService(
           IDbContextScopeFactory _dbContextScopeFactory,
           IRepository<Application, ApplicationDbContext> _repository,
           IConfigurationLib _config
       )
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _repository;
            config = _config;
        }


        public EResponseBase<Application> Get()
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Application> result = new EResponseBase<Application>();
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
                result = new UtilitariesResponse<Application>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<Application> Get(int id)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Application> result = new EResponseBase<Application>();

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
                result = new UtilitariesResponse<Application>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<Application> InsertOrUpdate(Application model)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Application> rh = new EResponseBase<Application>();
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
                rh = new UtilitariesResponse<Application>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<Application> Delete(int id)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Application> rh = new EResponseBase<Application>();
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
                rh = new UtilitariesResponse<Application>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<Application> Delete(Application model)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Application> rh = new EResponseBase<Application>();
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
                rh = new UtilitariesResponse<Application>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<Application> Disabled(int id, bool enabled)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Application> rh = new EResponseBase<Application>();
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
                rh = new UtilitariesResponse<Application>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }


        public EResponseBase<Application> GetByFilters(string searchText)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Application> result = new EResponseBase<Application>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(searchText, printDebug: true);
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        IQueryable<Application> resultTemp = repository.FindWithoutEResponse();
                        resultTemp.Where(x => x.Name.ToUpper() == searchText.ToUpper() || x.Code == searchText.ToUpper() || x.URL == searchText.ToUpper());
                        result = new UtilitariesResponse<Application>(config).setResponseBaseForList(resultTemp);
                    }
                    else result = new UtilitariesResponse<Application>(config).setResponseBaseForParameterNoValid();
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<Application>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<Application> GetByFilters(string name, string url, string code)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Application> result = new EResponseBase<Application>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(String.Format("name : {0}, code: {1}, description: {2}", name, code, url), printDebug: true);
                    if (String.IsNullOrEmpty(name) && String.IsNullOrEmpty(code) && String.IsNullOrEmpty(url))
                        result = new UtilitariesResponse<Application>(config).setResponseBaseForParameterNoValid();
                    else
                    {
                        IQueryable<Application> resultTemp = repository.FindWithoutEResponse();
                        if (!String.IsNullOrEmpty(name)) resultTemp.Where(x => x.Name == name);
                        if (!String.IsNullOrEmpty(code)) resultTemp.Where(x => x.Code == code);
                        if (!String.IsNullOrEmpty(url)) resultTemp.Where(x => x.URL == url);
                        result = new UtilitariesResponse<Application>(config).setResponseBaseForList(resultTemp);
                    }
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<Application>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }



    }
}
