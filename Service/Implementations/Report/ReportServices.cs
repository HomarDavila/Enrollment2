using Common;
using Common.Logging;
using Domain.Entity_Models;
using Infraestructure.Context;
using Infraestructure.Repositories;
using Mehdime.Entity;
using Service.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;

namespace Service.Implementations
{
    public class ReportServices : IReportServices
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<EnrollmentHistory, ApplicationDbContext> repositoryEnrollmentHistory;

        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }

        public ReportServices(IDbContextScopeFactory _dbContextScopeFactory,
                              IRepository<EnrollmentHistory, ApplicationDbContext> _EnrollmentHistory,
                              IConfigurationLib _config)
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repositoryEnrollmentHistory = _EnrollmentHistory;
            config = _config;

        }

        public EResponseBase<EnrollmentHistory> GetReportInscripciones(DateTime? pFecha, string pInscripcion)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<EnrollmentHistory> result = new EResponseBase<EnrollmentHistory>();
            try
            {

                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    ApplicationDbContext context = ctx.DbContexts.Get<ApplicationDbContext>();

                    EnrollmentHistory oResult = new EnrollmentHistory();

                    //var context = new ApplicationDbContext();
                    //var query = context.EnrollmentHistories
                    //    .Where(x => x.Enabled == true);

                    IQueryable<EnrollmentHistory> query = from History in context.EnrollmentHistories
                                                          where (History.PMGModifiedDate == DbFunctions.TruncateTime(pFecha) && History.PMGModifiedSource == pInscripcion && History.Enabled == true)
                                                          select History;

                    System.Collections.Generic.List<EnrollmentHistory> lst = query.ToList();

                    oResult.Fecha = pFecha;
                    oResult.TotalPMG = lst.Count();

                    query = from History in context.EnrollmentHistories
                            where (History.MCOModifiedDate == DbFunctions.TruncateTime(pFecha) && History.MCOModifiedSource == pInscripcion)
                            select History;

                    lst = query.ToList();

                    //lst = query.GroupBy(x => x.MCOEffectiveDate)
                    //    .Select(g => new EnrollmentHistory() { Fecha = g.Key, TotalMCO = g.Count() })
                    //    .ToList();

                    oResult.TotalMCO = lst.Count();

                    query = from History in context.EnrollmentHistories
                            where (History.PCPModifiedDate == DbFunctions.TruncateTime(pFecha) && History.PCPModifiedSource == pInscripcion)
                            select History;

                    lst = query.ToList();

                    oResult.TotalPCP = lst.Count();

                    //var query = from History in context.EnrollmentHistories
                    //            where (History.CreatedOn == pFecha && (History.PMGModifiedSource == pInscripcion || History.MCOModifiedSource == pInscripcion || History.PCPModifiedSource == pInscripcion))
                    //            group History by History.CreatedOn into NewHistory
                    //            let TotPMG = NewHistory.Count(m => m.PMGModifiedSource != "")
                    //            let TotMCO = NewHistory.Count(m => m.MCOModifiedSource != "")
                    //            let TotPCP = NewHistory.Count(m => m.PCPModifiedSource != "")
                    //            select new EnrollmentHistory() { Fecha = NewHistory.Key, TotalPMG = TotPMG, TotalMCO = TotMCO, TotalPCP = TotPCP };

                    //var objeto = query.FirstOrDefault();

                    //var query = from History in context.EnrollmentHistories
                    //            where (History.CreatedOn == pFecha && (History.PMGModifiedSource == pInscripcion || History.MCOModifiedSource == pInscripcion || History.PCPModifiedSource == pInscripcion))
                    //            group History by History.CreatedOn into NewHistory
                    //            select new EnrollmentHistory() { Fecha = NewHistory.Key, TotalPMG = NewHistory.Count(), TotalMCO = NewHistory.Count(), TotalPCP = NewHistory.Count() };

                    //var objeto = query.FirstOrDefault();

                    //var query = context.EnrollmentHistories.Where(x => x.CreatedOn == pFecha && x.PMGModifiedSource == pInscripcion)
                    //    .GroupBy(x => x.CreatedOn)
                    //    .Select(g => new EnrollmentHistory() { Fecha = g.Key, TotalPMG = g.Count() });

                    //var objeto = query.FirstOrDefault();

                    result = new UtilitariesResponse<EnrollmentHistory>(config).setResponseBaseForObj(oResult);

                    //var lst = query.ToList();

                    //result = new UtilitariesResponse<EnrollmentHistory>(config).setResponseBaseForList(lst);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<EnrollmentHistory>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }

        public EResponseBase<EnrollmentHistory> GetReportJustaCausa(DateTime? pFecha, string pInscripcion)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<EnrollmentHistory> result = new EResponseBase<EnrollmentHistory>();
            try
            {

                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    ApplicationDbContext context = ctx.DbContexts.Get<ApplicationDbContext>();

                    EnrollmentHistory oResult = new EnrollmentHistory();

                    IQueryable<EnrollmentHistory> query = from History in context.EnrollmentHistories
                                                          where (History.PMGModifiedDate == DbFunctions.TruncateTime(pFecha) && History.PMGModifiedSource == pInscripcion && History.JustCauseReasonId != null && History.Enabled == true)
                                                          select History;

                    System.Collections.Generic.List<EnrollmentHistory> lst = query.ToList();

                    oResult.Fecha = pFecha;
                    oResult.TotalPMG = lst.Count();

                    query = from History in context.EnrollmentHistories
                            where (History.MCOModifiedDate == DbFunctions.TruncateTime(pFecha) && History.MCOModifiedSource == pInscripcion && History.JustCauseReasonId != null)
                            select History;

                    lst = query.ToList();

                    oResult.TotalMCO = lst.Count();

                    query = from History in context.EnrollmentHistories
                            where (History.PCPModifiedDate == DbFunctions.TruncateTime(pFecha) && History.PCPModifiedSource == pInscripcion && History.JustCauseReasonId != null)
                            select History;

                    lst = query.ToList();

                    oResult.TotalPCP = lst.Count();

                    result = new UtilitariesResponse<EnrollmentHistory>(config).setResponseBaseForObj(oResult);

                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<EnrollmentHistory>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }
    }
}
