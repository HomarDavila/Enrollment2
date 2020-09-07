using Common;
using Common.Logging;
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
    public class SpecialityServices : ISpecialityServices
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<Speciality, ApplicationDbContext> repository;
        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }

        public SpecialityServices(IDbContextScopeFactory _dbContextScopeFactory,
                                  IRepository<Speciality, ApplicationDbContext> _repository,
                                  IConfigurationLib _config)
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _repository;
            config = _config;
        }

        public EResponseBase<Speciality> Get()
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Speciality> result = new EResponseBase<Speciality>();
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
                result = new UtilitariesResponse<Speciality>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }
        public EResponseBase<Speciality> Get(bool showItOnChangeEnrollmentProcess)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Speciality> result = new EResponseBase<Speciality>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    result = repository.Find(x => x.ShowItOnChangeEnrollmentProcess == showItOnChangeEnrollmentProcess, y => y.OrderBy(z => z.Name));
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<Speciality>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }

        public EResponseBase<Speciality> GetByPCPId(int PCPId, bool showItOnChangeEnrollmentProcess)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Speciality> result = new EResponseBase<Speciality>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    ApplicationDbContext context = ctx.DbContexts.Get<ApplicationDbContext>();
                    IQueryable<Speciality> query;
                    if (showItOnChangeEnrollmentProcess)
                    {
                        query = from PrimaryCarePhysician in context.PrimaryCarePhysicians
                                join PersonPrimaryCarePhysician in context.PersonPrimaryCarePhysicians on PrimaryCarePhysician.PersonId equals PersonPrimaryCarePhysician.Id
                                join Speciality in context.Specialities on PrimaryCarePhysician.SpecialityId equals Speciality.Id
                                where PersonPrimaryCarePhysician.Id == PCPId
                                && Speciality.ShowItOnChangeEnrollmentProcess == showItOnChangeEnrollmentProcess
                                select Speciality;
                    }
                    else
                    {
                        query = from PrimaryCarePhysician in context.PrimaryCarePhysicians
                                join PersonPrimaryCarePhysician in context.PersonPrimaryCarePhysicians on PrimaryCarePhysician.PersonId equals PersonPrimaryCarePhysician.Id
                                join Speciality in context.Specialities on PrimaryCarePhysician.SpecialityId equals Speciality.Id
                                where PersonPrimaryCarePhysician.Id == PCPId
                                select Speciality;
                    }
                  
                    List<Speciality> list = query.Distinct().ToList();

                    result = new UtilitariesResponse<Speciality>(config).setResponseBaseForList(list);

                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<Speciality>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }


    }
}
