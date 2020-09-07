using Common;
using Common.Logging;
using Domain.Custom_Models;
using Domain.Entity_Models;
using Infraestructure.Context;
using Infraestructure.Repositories;
using Mehdime.Entity;
using Service.Helpers;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;

namespace Service.Implementations
{
    public class PcpPmgMcoServices : IPcpPmgMcoServices
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<PcpPmgMco, ApplicationDbContext> repository;
        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }
        public PcpPmgMcoServices(IDbContextScopeFactory _dbContextScopeFactory,
                              IRepository<PcpPmgMco, ApplicationDbContext> _repository,
                              IConfigurationLib _config)
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _repository;
            config = _config;

        }

        public EResponseBase<PcpPmgMco> GetPcpPmgMco(int McoId, int PmgId, int PcpId)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<PcpPmgMco> result = new EResponseBase<PcpPmgMco>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    //result = repository.Find(x => x.Id == x.Id, null, x => x.MCO, x => x.PMG, x => x.PCP);
                    ApplicationDbContext context = ctx.DbContexts.Get<ApplicationDbContext>();
                    IQueryable<PcpPmgMco> query = from PcpPmgMco in context.PcpPmgMcos
                                                  where
                                                   (
                                                     (PcpPmgMco.McoId == McoId)
                                                     && (PcpPmgMco.PmgId == PmgId)
                                                     && (PcpPmgMco.PrimaryCarePhysicianId == PcpId)
                                                    )
                                                  select PcpPmgMco;
                    List<PcpPmgMco> list = query.Include(i => i.PCP)
                                    .Include(i => i.PMG)
                                    .Include(i => i.MCO)
                                    .Include(i => i.PCP.Person)
                                    .Distinct()
                                    .ToList();
                    result = new UtilitariesResponse<PcpPmgMco>(config).setResponseBaseForList(list);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<PcpPmgMco>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }
    }
}
