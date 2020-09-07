using Common;
using Common.Logging;
using Domain.Custom_Models;
using Domain.Entity_Models;
using Infraestructure.Context;
using Mehdime.Entity;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class PrimaryCarePhysicianDetailServices : IPrimaryCarePhysicianDetailServices
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }

        public PrimaryCarePhysicianDetailServices(IDbContextScopeFactory _dbContextScopeFactory,
                           IConfigurationLib _config)
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            config = _config;
        }

        public EResponseBase<PrimaryCarePhysicianDetailCustomModel> GetByFiltersToList(int? PersonId, int? SpecialityId, int? PmgId, int? MunicipalityId)
        {
            MunicipalityId = 0;
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<PrimaryCarePhysicianDetailCustomModel> result = new EResponseBase<PrimaryCarePhysicianDetailCustomModel>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    ApplicationDbContext context = ctx.DbContexts.Get<ApplicationDbContext>();
                    IQueryable<PrimaryCarePhysicianDetail> query = from Pcp in context.PrimaryCarePhysicianDetails
                                                                   select Pcp;
                    //query = query.Include(x => x.Municipality);
                    query = query.Include(x => x.PCPPMGCMCO);
                    query = query.Where(x => x.PCPPMGCMCO.PCP.PersonId == PersonId);
                    query = query.Where(x => x.PCPPMGCMCO.PCP.SpecialityId == SpecialityId);
                    if (MunicipalityId.HasValue) if (MunicipalityId != 0) query = query.Where(x => x.MunicipalityId == MunicipalityId);
                    if (PmgId.HasValue) if (PmgId != 0) query = query.Where(x => x.PCPPMGCMCO.PMG.Id == PmgId);

                    //var response = (from p in query
                    //                group p by new { PrimaryCarePhysicianDetailID = p.Id, p.PCPPMGCMCO.Id, p.PCPPMGCMCO.PCP.PersonId , p.PCPPMGCMCO.McoId } into g
                    //                select new
                    //                {
                    //                    Id = g.Key == null ? 0 : g.Key.PrimaryCarePhysicianDetailID//,
                    //                    //PersonId = g.Key.PersonId
                    //                });

                    var response = (from p in query
                                    group p by new { PrimaryCarePhysicianDetailID = p.Id } into g
                                    select new
                                    {
                                        Id = g.Key == null ? 0 : g.Key.PrimaryCarePhysicianDetailID
                                    });

                    var responseList = (from obj in response
                                        join pcp in context.PrimaryCarePhysicianDetails.Include(x => x.Municipality)
                                        on obj.Id equals pcp.Id
                                        select new PrimaryCarePhysicianDetailCustomModel()
                                        {
                                            Id = pcp.Id,
                                            AddressLineOne = pcp.AddressLineOne,
                                            AddressLineTwo = pcp.AddressLineTwo,
                                            Phone = pcp.Phone,
                                            Enabled = pcp.Enabled,
                                            Municipality = pcp.Municipality,
                                            MunicipalityId = pcp.MunicipalityId,
                                            City = pcp.City,
                                            ZipCode = pcp.ZipCode,
                                            State = pcp.State,
                                            AvailableManagedCareOrganizations = (from MCOgs in (from MCOs in context.ManagedCareOrganizations
                                                                                 join PPMs in context.PcpPmgMcos on MCOs.Id equals PPMs.McoId
                                                                                 join PCPs in context.PrimaryCarePhysicians on PPMs.PrimaryCarePhysicianId equals PCPs.Id
                                                                                 where PCPs.PersonId == pcp.PCPPMGCMCO.PCP.PersonId
                                                                                 && PCPs.SpecialityId == pcp.PCPPMGCMCO.PCP.SpecialityId
                                                                                 && PPMs.PmgId == pcp.PCPPMGCMCO.PmgId
                                                                                                group PPMs by new { PPMs.PrimaryCarePhysicianId, PPMs.McoId } into g
                                                                                                select new
                                                                                                {
                                                                                                    IdMco = g.Key == null ? 0 : g.Key.McoId
                                                                                                })
                                                                                 join MCO2s in context.ManagedCareOrganizations on MCOgs.IdMco equals MCO2s.Id
                                                                                 select new ManagedCareOrganizationsCustomModel()
                                                                                 {
                                                                                     Id = MCO2s.Id,
                                                                                     CarrierName = MCO2s.CarrierName
                                                                                 })
                                        });
                    
                    var uniqueUsers = responseList.ToList();
                    result = new UtilitariesResponse<PrimaryCarePhysicianDetailCustomModel>(config).setResponseBaseForList(uniqueUsers);
                    Logger.Print_Response(result);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<PrimaryCarePhysicianDetailCustomModel>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }
    }
}
