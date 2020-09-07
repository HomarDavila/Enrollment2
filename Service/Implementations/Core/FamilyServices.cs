using Common;
using Common.Logging;
using Domain.Custom_Models;
using Domain.Entity_Models;
using Infraestructure.Context;
using Infraestructure.Repositories;
using Mehdime.Entity;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Service.Implementations
{
    public class FamilyServices : IFamilyServices
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<Family, ApplicationDbContext> repository;
        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }

        public FamilyServices(IDbContextScopeFactory _dbContextScopeFactory,
                           IRepository<Family, ApplicationDbContext> _repository,
                           IConfigurationLib _config)
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _repository;
            config = _config;
        }


        public EResponseBase<Family> Get()
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Family> result = new EResponseBase<Family>();
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
                result = new UtilitariesResponse<Family>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }

        public EResponseBase<Family> Get(int FamilyId)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Family> result = new EResponseBase<Family>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    result = repository.Find(x => x.Id == FamilyId, null, x => x.Members);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<Family>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }
    }
}
