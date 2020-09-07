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
    public class McoServices : IMcoServices
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<ManagedCareOrganization, ApplicationDbContext> repository;
        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }

        public McoServices(IDbContextScopeFactory _dbContextScopeFactory,
                           IRepository<ManagedCareOrganization, ApplicationDbContext> _repository,
                           IConfigurationLib _config)
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _repository;
            config = _config;
        }

        public EResponseBase<ManagedCareOrganization> Get(bool showEnrollmentProcess)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<ManagedCareOrganization> result = new EResponseBase<ManagedCareOrganization>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    if (!showEnrollmentProcess) result = repository.Find(x => x.Enabled == true, x => x.OrderBy(y => y.CarrierName));
                    else result = repository.Find(x => x.Enabled == true && x.CarrierCode != "11", x => x.OrderBy(y => y.CarrierName));
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<ManagedCareOrganization>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }

        public EResponseBase<ManagedCareOrganization> Get(int McoId)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<ManagedCareOrganization> result = new EResponseBase<ManagedCareOrganization>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    result = repository.Find(x => x.Id == McoId);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<ManagedCareOrganization>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }

    }
}
