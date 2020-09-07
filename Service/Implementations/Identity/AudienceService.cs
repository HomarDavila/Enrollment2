using Common;
using Common.Logging;
using Domain.Entity_Models.Identity;
using Infraestructure.Context;
using Infraestructure.Repositories;
using Mehdime.Entity;
using Service.Interfaces;
using System;
using System.Security.Cryptography;

namespace Service.Implementations
{
    public class AudienceService : IAudienceService
    {

        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<Audience, ApplicationDbContext> repository;
        private readonly IConfigurationLib config;
        public ICustomLog Logger { get; set; }
        public ITransaction Transaction { get; set; }

        public AudienceService(
         IDbContextScopeFactory _dbContextScopeFactory,
         IRepository<Audience, ApplicationDbContext> _repository,
         IConfigurationLib _config
         )
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _repository;
            config = _config;
        }


        public EResponseBase<Audience> Register(string name, string base64)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            using (log4net.NDC.Push(Logger.Header))
            {
                EResponseBase<Audience> rh = new EResponseBase<Audience>();
                try
                {
                    using (IDbContextScope ctx = dbContextScopeFactory.Create())
                    {
                        Logger.Print_InitMethod();
                        string clientId = Guid.NewGuid().ToString("N");
                        byte[] key = new byte[32];
                        RNGCryptoServiceProvider.Create().GetBytes(key);
                        string base64Secret = base64;
                        Audience model = new Audience { Id = clientId, Base64Secret = base64Secret, Name = name };
                        Logger.Print_Request(model, printDebug: true);
                        rh = repository.Insert(model);
                        ctx.SaveChanges();
                        Logger.Print_Response(rh, printDebug: true);
                        Logger.Print_EndMethod();
                    }
                }
                catch (Exception e)
                {
                    rh = new UtilitariesResponse<Audience>(config).setResponseBaseForException(e);
                    Logger.Error(e.Message);
                }

                return rh;
            }
        }

        public EResponseBase<Audience> Get(string clientId)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            using (log4net.NDC.Push(Logger.Header))
            {
                EResponseBase<Audience> result = new EResponseBase<Audience>();
                try
                {
                    using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                    {
                        result = repository.FirstOrDefault(filter: x => x.Id == clientId);
                    }
                }
                catch (Exception e)
                {
                    result = new UtilitariesResponse<Audience>(config).setResponseBaseForException(e);
                    Logger.Error(e.Message);
                }

                return result;
            }

        }


    }
}
