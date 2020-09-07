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

namespace Service.Implementations
{
    public class ReportsServices : IReportsServices
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<EnrollmentStatistics, ApplicationDbContext> repository;
        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }

        public ReportsServices(IDbContextScopeFactory _dbContextScopeFactory,
                                  IRepository<EnrollmentStatistics, ApplicationDbContext> _repository,
                                  IConfigurationLib _config)
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _repository;
            config = _config;
        }

        public EResponseBase<EnrollmentStatistics> GetReportsWeb(EnrollmentStatistics request)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<EnrollmentStatistics> result = new EResponseBase<EnrollmentStatistics>();
            List<EnrollmentStatistics> list = new List<EnrollmentStatistics>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);

                    ApplicationDbContext context = ctx.DbContexts.Get<ApplicationDbContext>();
                    IQueryable<EnrollmentStatistics> query = context.EnrollmentStatistics.Where(x => x.Enabled == true);
                    if (request?.StartDate != null) query = query.Where(x => x.CreatedOn >= request.StartDate);
                    if (request?.EndDate != null) query = query.Where(x => x.CreatedOn <= request.EndDate);
                    if (query.Count() > 0)
                    {
                        var group = query
                            .GroupBy(x => x.CreatedOn)
                            .Select(x => new
                            {
                                CreatedOn = x.Key,
                                Type1Count = x.Count(y => y.TypeId == 1),
                                Type2Count = x.Count(y => y.TypeId == 2),
                                Type3Count = x.Count(y => y.TypeId == 3),
                                Type4Count = x.Count(y => y.TypeId == 4)
                            })
                            .OrderBy(x => x.CreatedOn);

                        foreach (var item in group)
                        {
                            EnrollmentStatistics statistics = new EnrollmentStatistics
                            {
                                CreatedOnFormated = item.CreatedOn?.ToString("yyyy/MM/dd"),
                                Type1Count = item.Type1Count,
                                Type2Count = item.Type2Count,
                                Type3Count = item.Type3Count,
                                Type4Count = item.Type4Count
                            };
                            list.Add(statistics);
                        }
                    }


                    result = new UtilitariesResponse<EnrollmentStatistics>(config).setResponseBaseForList(list);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<EnrollmentStatistics>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }

        public EResponseBase<EnrollmentStatistics> InsertStatistic(EnrollmentStatistics request)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<EnrollmentStatistics> result = new EResponseBase<EnrollmentStatistics>();
            EnrollmentStatistics model = new EnrollmentStatistics();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {

                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);

                    model.CreatedOn = DateTime.Today;
                    model.TypeId = request.TypeId;
                    model.Enabled = true;
                    EResponseBase<EnrollmentStatistics> statistic = repository.Insert(model);
                    ctx.SaveChanges();

                    result = statistic;
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<EnrollmentStatistics>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }
    }
}
