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
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Service.Implementations
{
    public class PcpServices : IPcpServices
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<PersonPrimaryCarePhysician, ApplicationDbContext> repository;
        private readonly IRepository<PrimaryCarePhysician, ApplicationDbContext> repository2;
        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }
        public object Person { get; private set; }

        public PcpServices(IDbContextScopeFactory _dbContextScopeFactory,
                           IRepository<PersonPrimaryCarePhysician, ApplicationDbContext> _repository,
                           IRepository<PrimaryCarePhysician, ApplicationDbContext> _repository2,
                           IConfigurationLib _config)
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository2 = _repository2;
            repository = _repository;
            config = _config;
        }

        public EResponseBase<PersonPrimaryCarePhysician> Get(bool ShowForChangeEnrollmentProcess)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<PersonPrimaryCarePhysician> result = new EResponseBase<PersonPrimaryCarePhysician>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    ApplicationDbContext context = ctx.DbContexts.Get<ApplicationDbContext>();
                    List<PersonPrimaryCarePhysician> list = null;
                    IQueryable<PrimaryCarePhysician> result2;
                    if (ShowForChangeEnrollmentProcess) result2 = repository2.FindWithoutEResponse(x => x.Speciality.ShowItOnChangeEnrollmentProcess == true, x => x.OrderBy(y => y.Person.FullName), x => x.Person, x => x.Speciality);
                    else result2 = repository2.FindWithoutEResponse(null, x => x.OrderBy(y => y.Person.FullName), x => x.Person);

                    //if (ShowForChangeEnrollmentProcess) result2 = repository2.FindWithoutEResponse(x => x.Speciality.ShowItOnChangeEnrollmentProcess == true, x => x.OrderBy(y => y.Person.FullName), x => x.Person, x => x.Speciality).Where(x => x.Id == x.Person.Id && x.AmountOfLivesEnrolled <= x.Capacity);
                    //else result2 = repository2.FindWithoutEResponse(null, x => x.OrderBy(y => y.Person.FullName), x => x.Person).Where(x => x.Id == x.Person.Id && x.AmountOfLivesEnrolled <= x.Capacity);

                    if (result2 != null)
                    {
                        if (result2.Any())
                        {
                            //list = (from obj in result2
                            //        select obj.Person).ToList();
                            List<PrimaryCarePhysician> listPCP = (from obj in result2
                                                                  select obj).ToList();
                            //listPCP.ForEach(cc => cc.Person.OverCapacity = cc.OverCapacity);
                            //listPCP.ForEach(cc => cc.Person.PcpId = cc.Id);
                            list = new List<PersonPrimaryCarePhysician>();
                            foreach (PrimaryCarePhysician item in listPCP)
                            {
                                list.Add(item.Person);
                            }
                        }
                    }

                    //IQueryable<PrimaryCarePhysician> query = from PrimaryCarePhysician in context.PrimaryCarePhysicians
                    //                                         select PrimaryCarePhysician;
                    //query = query.Include(x => x.Speciality);
                    //query = query.Include(x => x.Person);
                    //query = query.Where(x => x.AmountOfLivesEnrolled <= x.Capacity && x.Speciality.ShowItOnChangeEnrollmentProcess == true);

                    //List<PrimaryCarePhysician> listPCP = (from p in query
                    //                                      select new PrimaryCarePhysician()
                    //                                      {
                    //                                          Person = p.Person
                    //                                      }).ToList();

                    //list = new List<PersonPrimaryCarePhysician>();
                    //foreach (PrimaryCarePhysician item in listPCP)
                    //{
                    //    list.Add(item.Person);
                    //}

                    result = new UtilitariesResponse<PersonPrimaryCarePhysician>(config).setResponseBaseForList(list);


                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<PersonPrimaryCarePhysician>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }

        public EResponseBase<PrimaryCarePhysicianCustomModel> Get(string PcpFullName, string NPI, int? SpecialityId, int? PmgId, bool ShowForChangeEnrollmentProcess, List<int> McoIds)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<PrimaryCarePhysicianCustomModel> result = new EResponseBase<PrimaryCarePhysicianCustomModel>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    ApplicationDbContext context = ctx.DbContexts.Get<ApplicationDbContext>();
                    IQueryable<PcpPmgMco> query = from Pcp in context.PcpPmgMcos.Where(x=>x.Enabled==true)
                                                  select Pcp;
                    //query = query.Include(x => x.PCP.Municipality);
                    query = query.Include(x => x.PCP.Speciality);
                    query = query.Include(x => x.PCP.Person);
                    query = query.Include(x => x.Details);
                    query = query.Include(x => x.PCP.Person.Gender);
                    query = query.Include(x => x.PMG);
                    if (ShowForChangeEnrollmentProcess) query = query.Where(x => x.PCP.Speciality.ShowItOnChangeEnrollmentProcess == true);
                    if (!String.IsNullOrEmpty(PcpFullName)) query = query.Where(x => x.PCP.Person.FullName.Trim().ToUpper().Contains(PcpFullName.Trim().ToUpper()));
                    if (!String.IsNullOrEmpty(NPI)) query = query.Where(x => x.PCP.Person.NPI.Trim().ToUpper().Contains(NPI.Trim().ToUpper()));
                    if (SpecialityId.HasValue) if (SpecialityId != 0) query = query.Where(x => x.PCP.SpecialityId == SpecialityId);
                    if (PmgId.HasValue)
                    {
                        if (PmgId != 0)
                        {
                            query = query.Where(x => x.PmgId == PmgId);
                        }
                    }
                    if (McoIds != null)
                    {
                        if (McoIds.Any())
                        {
                            //foreach (int mcoId in McoIds)
                            //{
                            //    query = query.Where(x => x.McoId == mcoId);
                            //});
                            if (McoIds.Count < 5)
                                for (int y = McoIds.Count; y < 5; y++)
                                {
                                    McoIds.Add(0);
                                }
                            int value_1 = McoIds[0];
                            int value_2 = McoIds[1];
                            int value_3 = McoIds[2];
                            int value_4 = McoIds[3];
                            int value_5 = McoIds[4];
                            query = query.Where(x => x.McoId == value_1 ||
                                                     x.McoId == value_2 ||
                                                     x.McoId == value_3 ||
                                                     x.McoId == value_4 ||
                                                     x.McoId == value_5
                                                     );
                        }
                    }

                    var response = (from p in query
                                    group p by p.PrimaryCarePhysicianId into g
                                    select new
                                    {
                                        Id = g.Key == null ? 0 : g.Key.Value,
                                        AvailableManagedCareOrganizations = g.Select(c => c)//.Select(c => c.PMG)
                                    });

                    var response2 = (from p in query
                                     group p by p.PrimaryCarePhysicianId into g
                                     select new
                                     {
                                         Id = g.Key == null ? 0 : g.Key.Value,
                                         AvailableManagedCareOrganizations = g.Select(c => c)//.Select(c => c.PMG)
                                     });

                    List<PrimaryCarePhysicianCustomModel> responseList = (from obj in response
                                                                          join pcp in context.PrimaryCarePhysicians.Include(x => x.Person).Include(x => x.Speciality) on obj.Id equals pcp.Id
                                                                          select new PrimaryCarePhysicianCustomModel()
                                                                          {
                                                                              Id = pcp.Id,
                                                                              //AddressLineOne = pcp.AddressLineOne,
                                                                              //AddressLineTwo = pcp.AddressLineTwo,
                                                                              //AmountOfLivesEnrolled = pcp.AmountOfLivesEnrolled,
                                                                              //Capacity = pcp.Capacity,
                                                                              //City = pcp.City,
                                                                              CreatedBy = pcp.CreatedBy,
                                                                              CreatedOn = pcp.CreatedOn,
                                                                              Enabled = pcp.Enabled,
                                                                              //Details = pcp.Details,
                                                                              //Municipality = pcp.Municipality,
                                                                              //MunicipalityId = pcp.MunicipalityId,
                                                                              Person = pcp.Person,
                                                                              PersonId = pcp.PersonId,
                                                                              Speciality = pcp.Speciality,
                                                                              SpecialityId = pcp.SpecialityId,
                                                                              //Gender = pcp.Person.Gender,
                                                                              //GenderId = pcp.Person.GenderId,
                                                                              //State = pcp.State,
                                                                              UpdatedBy = pcp.UpdatedBy,
                                                                              UpdatedOn = pcp.UpdatedOn//,
                                                                              //ZipCode = pcp.ZipCode//,
                                                                              //AvailableManagedCareOrganizations = obj.AvailableManagedCareOrganizations
                                                                          })
                                        .ToList();

                    result = new UtilitariesResponse<PrimaryCarePhysicianCustomModel>(config).setResponseBaseForList(responseList);
                    Logger.Print_Response(result);
                    Logger.Print_EndMethod();
                }

            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<PrimaryCarePhysicianCustomModel>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        public EResponseBase<PrimaryCarePhysician> Get(int PcpId)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<PrimaryCarePhysician> result = new EResponseBase<PrimaryCarePhysician>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    //result = repository2.Find(x => x.Id == PcpId, null, x => x.Municipality, x => x.Speciality, x => x.Person, x => x.Details);
                    //result = repository2.Find(x => x.Id == PcpId, null, x => x.Municipality, x => x.Speciality, x => x.Person);
                    result = repository2.Find(x => x.Id == PcpId, null, x => x.Speciality, x => x.Person);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<PrimaryCarePhysician>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }

        public EResponseBase<PrimaryCarePhysicianCustomModel> GetByFiltersToList(string PcpFullName, string NPI, int? SpecialityId, int? PmgId, bool ShowForChangeEnrollmentProcess, List<int> McoIds, int? MunicipalityId)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<PrimaryCarePhysicianCustomModel> result = new EResponseBase<PrimaryCarePhysicianCustomModel>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    ApplicationDbContext context = ctx.DbContexts.Get<ApplicationDbContext>();
                    IQueryable<PcpPmgMco> query = from Pcp in context.PcpPmgMcos.Where(x=>x.Enabled==true)
                                                  select Pcp;                    
                    query = query.Include(x => x.PCP.Speciality);
                    query = query.Include(x => x.PCP.Person);
                    query = query.Include(x => x.PMG);
                    if (MunicipalityId != null)
                    {
                        query = query.Include(x => x.Details);
                        query = query.Where(x => x.Details.Any(y => y.MunicipalityId == MunicipalityId));
                    }


                    if (ShowForChangeEnrollmentProcess) query = query.Where(x => x.PCP.Speciality.ShowItOnChangeEnrollmentProcess == true);
                    if (ShowForChangeEnrollmentProcess) query = query.Where(x => x.PMG.PmgCode != null); //CustomConfigurationLib.PMGNoIdentificado
                    if (!string.IsNullOrEmpty(PcpFullName))
                    {
                        int nameCount = query.Where(x => x.PCP.Person.FullName.Trim().ToUpper().Contains(PcpFullName.Trim().ToUpper())).Count();
                        if (nameCount > 0)
                        {
                            query = query.Where(x => x.PCP.Person.FullName.Trim().ToUpper().Contains(PcpFullName.Trim().ToUpper()));
                        }
                        else
                        {
                            string[] names = PcpFullName.ToUpper().Trim().Replace(",", "").Split(' '); //BARNECET LUGO, OSVALDO
                            List<string> namesList = names.ToList();
                            //for (int i = 0; i < namesList.Count(); i++)
                            //{
                            //    if (namesList[i].Length<=2)
                            //    {
                            //        namesList.RemoveAt(i);
                            //    }
                            //}
                            query = query.Where(x => namesList.All(a => x.PCP.Person.FullName.Trim().ToUpper().Contains(a)));
                        }
                    }
                    if (!string.IsNullOrEmpty(NPI)) query = query.Where(x => x.PCP.Person.NPI == NPI);
                    if (SpecialityId.HasValue && SpecialityId != 0) query = query.Where(x => x.PCP.SpecialityId == SpecialityId);
                    if (PmgId.HasValue)
                    {
                        if (PmgId != 0)
                        {
                            query = query.Where(x => x.PmgId == PmgId);
                        }
                    }

                    if (McoIds != null)
                    {
                        if (McoIds.Any())
                        {
                            query = query.Where(x => McoIds.Contains(x.McoId));
                        }
                    }
                    if (ShowForChangeEnrollmentProcess) query = query.Where(x => x.McoId != 3);

                    var response = (from p in query
                                    group p by new { p.PCP.PersonId, p.PCP.SpecialityId } into g
                                    select new
                                    {
                                        IdPerson = g.Key == null ? 0 : g.Key.PersonId,
                                        IdSpeciality = g.Key.SpecialityId
                                    });
                    
                    if (ShowForChangeEnrollmentProcess)
                    {
                        var query3 = (from obj in response
                                            join pcp in context.PrimaryCarePhysicians.Include(x => x.Person)
                                            on new { PersonId = obj.IdPerson, SpecialityId = obj.IdSpeciality } equals new { PersonId = pcp.PersonId, SpecialityId = pcp.SpecialityId }
                                            select new PrimaryCarePhysicianCustomModel()
                                            {
                                                Id = pcp.Id,
                                                Enabled = pcp.Enabled,
                                                Person = pcp.Person,
                                                PersonId = pcp.PersonId,
                                                Speciality = pcp.Speciality,
                                                SpecialityId = pcp.SpecialityId,
                                                AvailableManagedCareOrganizations = (from MCOgs in (from MCOs in context.ManagedCareOrganizations
                                                                                                    join PPMs in context.PcpPmgMcos.Where(x => x.Enabled == true) on MCOs.Id equals PPMs.McoId
                                                                                                    join PMGs in context.PrimaryMedicalGroups on PPMs.PmgId equals PMGs.Id
                                                                                                    join PCPs in context.PrimaryCarePhysicians on PPMs.PrimaryCarePhysicianId equals PCPs.Id
                                                                                                    where PCPs.PersonId == pcp.PersonId
                                                                                                    && PCPs.SpecialityId == pcp.SpecialityId
                                                                                                    && PMGs.PmgCode != null //CustomConfigurationLib.PMGNoIdentificado
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
                                                                                     }),

                                                AvailablePrimaryMedicalGroups = (from PMGgs in (from PMGs in context.PrimaryMedicalGroups
                                                                                                join PPMs in context.PcpPmgMcos.Where(x=>x.Enabled==true) on PMGs.Id equals PPMs.PmgId
                                                                                                join PCPs in context.PrimaryCarePhysicians on PPMs.PrimaryCarePhysicianId equals PCPs.Id
                                                                                                where PCPs.PersonId == pcp.PersonId
                                                                                                && PCPs.SpecialityId == pcp.SpecialityId
                                                                                                && PCPs.Id == pcp.Id
                                                                                                && PMGs.PmgCode != null //CustomConfigurationLib.PMGNoIdentificado
                                                                                                group PPMs by new { PPMs.PrimaryCarePhysicianId, PPMs.PmgId } into g
                                                                                                select new
                                                                                                {
                                                                                                    IdPmg = g.Key == null ? 0 : g.Key.PmgId
                                                                                                })
                                                                                 join PMG2s in context.PrimaryMedicalGroups on PMGgs.IdPmg equals PMG2s.Id
                                                                                 select new PrimaryMedicalGroupCustomModel()
                                                                                 {
                                                                                     Id = PMG2s.Id,
                                                                                     PmgName = PMG2s.PmgName
                                                                                 })
                                            }).Take(200);
                        //Los primeros 200 aumentan por el inner join por eso lo volví a forzar
                        var responseList = query3.OrderBy(x => x.Person.FullName).Take(200).ToList();

                        result = new UtilitariesResponse<PrimaryCarePhysicianCustomModel>(config).setResponseBaseForList(responseList);
                    }
                    else
                    {
                        var query2 = (from obj in response
                                      join pcp in context.PrimaryCarePhysicians.Include(x => x.Person)
                                      on new { PersonId = obj.IdPerson, SpecialityId = obj.IdSpeciality } equals new { PersonId = pcp.PersonId, SpecialityId = pcp.SpecialityId }
                                      select new PrimaryCarePhysicianCustomModel()
                                      {
                                          Id = pcp.Id,
                                          Enabled = pcp.Enabled,
                                          Person = pcp.Person,
                                          PersonId = pcp.PersonId,
                                          Speciality = pcp.Speciality,
                                          SpecialityId = pcp.SpecialityId,
                                          AvailableManagedCareOrganizations = (from MCOgs in (from MCOs in context.ManagedCareOrganizations
                                                                                              join PPMs in context.PcpPmgMcos.Where(x => x.Enabled == true) on MCOs.Id equals PPMs.McoId
                                                                                              join PCPs in context.PrimaryCarePhysicians on PPMs.PrimaryCarePhysicianId equals PCPs.Id
                                                                                              where PCPs.PersonId == pcp.PersonId
                                                                                              && PCPs.SpecialityId == pcp.SpecialityId
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
                                                                               }),

                                          AvailablePrimaryMedicalGroups = (from PMGgs in (from PMGs in context.PrimaryMedicalGroups
                                                                                          join PPMs in context.PcpPmgMcos.Where(x=>x.Enabled==true) on PMGs.Id equals PPMs.PmgId
                                                                                          join PCPs in context.PrimaryCarePhysicians on PPMs.PrimaryCarePhysicianId equals PCPs.Id
                                                                                          where PCPs.PersonId == pcp.PersonId
                                                                                          && PCPs.SpecialityId == pcp.SpecialityId
                                                                                          && PCPs.Id == pcp.Id
                                                                                          group PPMs by new { PPMs.PrimaryCarePhysicianId, PPMs.PmgId } into g
                                                                                          select new
                                                                                          {
                                                                                              IdPmg = g.Key == null ? 0 : g.Key.PmgId
                                                                                          })
                                                                           join PMG2s in context.PrimaryMedicalGroups on PMGgs.IdPmg equals PMG2s.Id
                                                                           select new PrimaryMedicalGroupCustomModel()
                                                                           {
                                                                               Id = PMG2s.Id,
                                                                               PmgName = PMG2s.PmgName
                                                                           })
                                      }).Take(200);
                        //Los primeros 200 aumentan por el inner join por eso lo volví a forzar
                        var responseList2 = query2.OrderBy(x => x.Person.FullName).Take(200).ToList();
                        result = new UtilitariesResponse<PrimaryCarePhysicianCustomModel>(config).setResponseBaseForList(responseList2);
                    }


                    Logger.Print_Response(result);
                    Logger.Print_EndMethod();
                }

            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<PrimaryCarePhysicianCustomModel>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }

        //public EResponseBase<PcpCustomModelV2> GetByFiltersToList2(string PcpFullName, string NPI, int? SpecialityId, int? PmgId, bool ShowForChangeEnrollmentProcess, List<int> McoIds)
        //{
        //    Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
        //    EResponseBase<PcpCustomModelV2> result = new EResponseBase<PcpCustomModelV2>();
        //    try
        //    {
        //        using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
        //        {
        //            Logger.Print_InitMethod();
        //            Logger.Print_Request(null, printDebug: true);
        //            ApplicationDbContext context = ctx.DbContexts.Get<ApplicationDbContext>();
        //            IQueryable<PcpPmgMco> query = from Pcp in context.PcpPmgMcos
        //                                          select Pcp;
        //            query = query.Include(x => x.PCP.Municipality);
        //            query = query.Include(x => x.PCP.Speciality);
        //            query = query.Include(x => x.PCP.Person);
        //            query = query.Include(x => x.Details);
        //            query = query.Include(x => x.PCP.Person.Gender);
        //            query = query.Include(x => x.PMG);
        //            if (ShowForChangeEnrollmentProcess) query = query.Where(x => x.PCP.Speciality.ShowItOnChangeEnrollmentProcess == true);
        //            if (!String.IsNullOrEmpty(PcpFullName)) query = query.Where(x => x.PCP.Person.FullName.Trim().ToUpper().Contains(PcpFullName.Trim().ToUpper()));
        //            //if (!String.IsNullOrEmpty(NPI)) query = query.Where(x => x.PCP.Person.NPI.Trim().ToUpper().Contains(NPI.Trim().ToUpper()));
        //            //if (SpecialityId.HasValue && SpecialityId != 0) query = query.Where(x => x.PCP.SpecialityId == SpecialityId);
        //            if (PmgId.HasValue)
        //            {
        //                if (PmgId != 0)
        //                {
        //                    query = query.Where(x => x.PmgId == PmgId);
        //                }
        //            }

        //            if (McoIds != null)
        //            {
        //                if (McoIds.Any())
        //                {
        //                    if (McoIds.Count < 5)
        //                        for (int y = McoIds.Count; y < 5; y++)
        //                        {
        //                            McoIds.Add(0);
        //                        }
        //                    int value_1 = McoIds[0];
        //                    int value_2 = McoIds[1];
        //                    int value_3 = McoIds[2];
        //                    int value_4 = McoIds[3];
        //                    int value_5 = McoIds[4];
        //                    query = query.Where(x => x.McoId == value_1 ||
        //                                             x.McoId == value_2 ||
        //                                             x.McoId == value_3 ||
        //                                             x.McoId == value_4 ||
        //                                             x.McoId == value_5
        //                                             );
        //                }
        //            }

        //            var response = (from p in query
        //                            group p by p.PrimaryCarePhysicianId into g
        //                            select new
        //                            {
        //                                Id = g.Key == null ? 0 : g.Key.Value,
        //                                AvailableManagedCareOrganizations = g.Select(c => c)
        //                            });

        //            //List<PrimaryCarePhysicianCustomModel> responseList = (from obj in response
        //            var responseList = (from obj in response
        //                                join pcp in context.PrimaryCarePhysicians.Include(x => x.Person).
        //                                Include(x => x.Speciality)
        //                                on obj.Id equals pcp.Id
        //                                select new PrimaryCarePhysicianCustomModel()
        //                                {
        //                                    Id = pcp.Id,
        //                                    //AddressLineOne = pcp.AddressLineOne,
        //                                    //AddressLineTwo = pcp.AddressLineTwo,
        //                                    AmountOfLivesEnrolled = pcp.AmountOfLivesEnrolled,
        //                                    Capacity = pcp.Capacity,
        //                                    City = pcp.City,
        //                                    CreatedBy = pcp.CreatedBy,
        //                                    CreatedOn = pcp.CreatedOn,
        //                                    Enabled = pcp.Enabled,
        //                                    //Details = pcp.Details,
        //                                    Municipality = pcp.Municipality,
        //                                    MunicipalityId = pcp.MunicipalityId,
        //                                    Person = pcp.Person,
        //                                    PersonId = pcp.PersonId,
        //                                    Speciality = pcp.Speciality,
        //                                    SpecialityId = pcp.SpecialityId,
        //                                    //Gender = pcp.Person == null ? new Gender { Id = 1, Name = "Male" } : pcp.Person.Gender,
        //                                    //GenderId = pcp.Person == null ? 1  : pcp.Person.GenderId,
        //                                    Gender = pcp.Person.Gender,
        //                                    GenderId = pcp.Person.GenderId,
        //                                    State = pcp.State,
        //                                    UpdatedBy = pcp.UpdatedBy,
        //                                    UpdatedOn = pcp.UpdatedOn,
        //                                    ZipCode = pcp.ZipCode,
        //                                    AvailableManagedCareOrganizations = obj.AvailableManagedCareOrganizations,


        //                                });
        //            //.ToList();                    
        //            // if (!String.IsNullOrEmpty(PcpFullName)) responseList = responseList.Where(x => x.Person.FullName.Trim().ToUpper().Contains(PcpFullName.Trim().ToUpper()));
        //            if (SpecialityId.HasValue && SpecialityId != 0) responseList = responseList.Where(x => x.SpecialityId == SpecialityId);

        //            //var r1 = response.ToList();
        //            //List<PcpCustomModelV2> uniqueUsers = responseList
        //            var uniqueUsers = responseList
        //                .GroupBy(x => new { x.PersonId, x.SpecialityId })
        //                .Select(x => new PcpCustomModelV2()
        //                {
        //                    PersonId = x.Key.PersonId,
        //                    SpecialityId = x.Key.SpecialityId,
        //                    Person = x.Select(y => y.Person).FirstOrDefault(),
        //                    Speciality = x.Select(y => y.Speciality).FirstOrDefault(),
        //                    Data = x.ToList()
        //                }).Take(100);
        //            //.ToList();

        //            //if (response.listado != null) response.listado = response.listado.Any() ? response.listado.Take(100).ToList() : response.listado;
        //            result = new UtilitariesResponse<PcpCustomModelV2>(config).setResponseBaseForList(uniqueUsers);
        //            Logger.Print_Response(result);
        //            Logger.Print_EndMethod();
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        result = new UtilitariesResponse<PcpCustomModelV2>(config).setResponseBaseForException(e);
        //        Logger.Error(e.Message);
        //    }

        //    return result;
        //}
    }
}
