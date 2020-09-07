using Common;
using Common.Logging;
using Domain.Entity_Models;
using Infraestructure.Context;
using Infraestructure.Repositories;
using Mehdime.Entity;
using Service.Helpers;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class PrimaryMedicalGroupServices : IPrimaryMedicalGroupServices
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<PrimaryMedicalGroup, ApplicationDbContext> repository;
        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }
        public PrimaryMedicalGroupServices(IDbContextScopeFactory _dbContextScopeFactory,
                              IRepository<PrimaryMedicalGroup, ApplicationDbContext> _repository,
                              IConfigurationLib _config)
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _repository;
            config = _config;

        }

        public EResponseBase<PrimaryMedicalGroup> GetByPCPId(int PCPId, bool ShowForChangeEnrollmentProcess)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<PrimaryMedicalGroup> result = new EResponseBase<PrimaryMedicalGroup>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    ApplicationDbContext context = ctx.DbContexts.Get<ApplicationDbContext>();
                    IQueryable<PrimaryMedicalGroup> query;
                    if (ShowForChangeEnrollmentProcess)
                    {
                        query = from PrimaryCarePhysician in context.PrimaryCarePhysicians
                                join PersonPrimaryCarePhysician in context.PersonPrimaryCarePhysicians on PrimaryCarePhysician.PersonId equals PersonPrimaryCarePhysician.Id
                                join PcpPmgMco in context.PcpPmgMcos on PrimaryCarePhysician.Id equals PcpPmgMco.PrimaryCarePhysicianId
                                join PrimaryMedicalGroup in context.PrimaryMedicalGroups on PcpPmgMco.PmgId equals PrimaryMedicalGroup.Id
                                where PersonPrimaryCarePhysician.Id == PCPId
                                && PrimaryMedicalGroup.PmgCode != null //CustomConfigurationLib.PMGNoIdentificado
                                && PcpPmgMco.Enabled==true
                                select PrimaryMedicalGroup;
                    }
                    else
                    {
                        query = from PrimaryCarePhysician in context.PrimaryCarePhysicians
                                join PersonPrimaryCarePhysician in context.PersonPrimaryCarePhysicians on PrimaryCarePhysician.PersonId equals PersonPrimaryCarePhysician.Id
                                join PcpPmgMco in context.PcpPmgMcos on PrimaryCarePhysician.Id equals PcpPmgMco.PrimaryCarePhysicianId
                                join PrimaryMedicalGroup in context.PrimaryMedicalGroups on PcpPmgMco.PmgId equals PrimaryMedicalGroup.Id
                                where PersonPrimaryCarePhysician.Id == PCPId
                                && PcpPmgMco.Enabled == true
                                select PrimaryMedicalGroup;
                    }


                    List<PrimaryMedicalGroup> list = query.Distinct().ToList();

                    result = new UtilitariesResponse<PrimaryMedicalGroup>(config).setResponseBaseForList(list);

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
