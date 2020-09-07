using Common;
using Common.Logging;
using Domain.Entity_Models;
using Infraestructure.Context;
using Infraestructure.Repositories;
using Mehdime.Entity;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class ManagedCareOrganizationServices : IManagedCareOrganizationServices
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<ManagedCareOrganization, ApplicationDbContext> repository;
        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }


        public ManagedCareOrganizationServices(IDbContextScopeFactory _dbContextScopeFactory,
                              IRepository<ManagedCareOrganization, ApplicationDbContext> _repository,
                              IConfigurationLib _config)
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _repository;
            config = _config;

        }
        public EResponseBase<ManagedCareOrganization> Get()
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<ManagedCareOrganization> result = new EResponseBase<ManagedCareOrganization>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    result = repository.Find(x => x.Enabled == true, x => x.OrderBy(y => y.CarrierName));
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
