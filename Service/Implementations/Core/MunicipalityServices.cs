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
    public class MunicipalityServices : IMunicipalityServices
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<Municipality, ApplicationDbContext> repository;
        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }


        public MunicipalityServices(IDbContextScopeFactory _dbContextScopeFactory,
                              IRepository<Municipality, ApplicationDbContext> _repository,
                              IConfigurationLib _config)
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _repository;
            config = _config;

        }
        public EResponseBase<Municipality> Get()
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Municipality> result = new EResponseBase<Municipality>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    result = repository.Find(x => x.Enabled == true, x => x.OrderBy(y => y.Name));
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<Municipality>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }
    }
}
