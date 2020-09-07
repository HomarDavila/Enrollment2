using Common;
using Common.Logging;
using Domain.Entity_Models;
using Infraestructure.Context;
using Infraestructure.Repositories;
using Mehdime.Entity;
using Service.Helpers;
using Service.Interfaces;
using System;
using System.Linq;

namespace Service.Implementations
{
    public class PmgServices : IPmgServices
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<PrimaryMedicalGroup, ApplicationDbContext> repository;
        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }

        public PmgServices(IDbContextScopeFactory _dbContextScopeFactory,
                           IRepository<PrimaryMedicalGroup, ApplicationDbContext> _repository,
                           IConfigurationLib _config)
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _repository;
            config = _config;
        }

        public EResponseBase<PrimaryMedicalGroup> Get(bool ShowForChangeEnrollmentProcess)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<PrimaryMedicalGroup> result = new EResponseBase<PrimaryMedicalGroup>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    if (ShowForChangeEnrollmentProcess)
                    {
                        result = repository.Find(null, x => x.Where(z => z.Enabled == true && z.PmgCode != null).OrderBy(y => y.PmgName)); //CustomConfigurationLib.PMGNoIdentificado
                    }
                    else
                    {
                        result = repository.Find(null, x => x.Where(z => z.Enabled == true).OrderBy(y => y.PmgName));
                    }
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<PrimaryMedicalGroup>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }

    }
}
