using Common;
using Common.Logging;
using Domain.Entity_Models;
using Infraestructure.Context;
using Infraestructure.Repositories;
using Mehdime.Entity;
using Service.Interfaces;
using System;
using System.Linq;

namespace Service.Implementations
{
    public class ReasonJustCauseServices : IReasonJustCauseServices
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<ReasonJustCause, ApplicationDbContext> repository;
        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }


        public ReasonJustCauseServices(IDbContextScopeFactory _dbContextScopeFactory,
                              IRepository<ReasonJustCause, ApplicationDbContext> _repository,
                              IConfigurationLib _config)
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _repository;
            config = _config;

        }
        public EResponseBase<ReasonJustCause> Get()
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<ReasonJustCause> result = new EResponseBase<ReasonJustCause>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    result = repository.Find(x => x.Enabled == true, x => x.OrderBy(y => y.Reason));
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<ReasonJustCause>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }

        public EResponseBase<ReasonJustCause> GetReasonJustCauseByID(int ReasonJustCauseId)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<ReasonJustCause> result = new EResponseBase<ReasonJustCause>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    ReasonJustCause objeto = repository.SingleOrDefaultWithoutEResponse(x => x.Id == ReasonJustCauseId);
                    result = new UtilitariesResponse<ReasonJustCause>(config).setResponseBaseForObj(objeto);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<ReasonJustCause>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }

        public EResponseBase<ReasonJustCause> Disabled(int id, bool enabled)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<ReasonJustCause> rh = new EResponseBase<ReasonJustCause>();

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
                rh = new UtilitariesResponse<ReasonJustCause>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }
        public EResponseBase<ReasonJustCause> InsertOrUpdate(ReasonJustCause model)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<ReasonJustCause> rh = new EResponseBase<ReasonJustCause>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(model, printDebug: true);
                    model.CreatedOn = DateTime.Today;
                    rh = repository.InsertOrUpdate(model, model.Id);
                    ctx.SaveChanges();
                    Logger.Print_Response(rh, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                rh = new UtilitariesResponse<ReasonJustCause>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }
    }
}
