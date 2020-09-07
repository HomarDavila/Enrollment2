using Audit.Mvc;
using Common.AspNet.Logging;
using Common.Logging;
using Core.API.Model.Request;
using Core.API.Model.Response;
using Domain.Custom_Models;
using EnrollmentSystemWebApp.Helpers;
using EnrollmentSystemWebApp.Proxy;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnrollmentSystemWebApp.Controllers
{
    //[Audit(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = false, IncludeRequestBody = false)]
    [Authorize]
    public class ReportController : Controller
    {
        private CustomConfigurationLib config;
        private ICustomLog logger;
        private ProxyCoreAPI proxyCoreAPI;

        public ReportController()
        {
            config = new CustomConfigurationLib();
            logger = new CustomLog4Net();
            proxyCoreAPI = new ProxyCoreAPI();
        }

        [HttpPost]
        public async Task<ActionResult> GetReportInscripciones(ReportRequestV1 request)
        {

            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            Common.EResponseBase<ReportResponseV1> response = await proxyCoreAPI.GetReportInscripciones(transaction, logger, config, null, request);
            int recordsTotal = 0;
            //List<ReportResponseV1> records = null;
            ReportResponseV1 record = null;
            if (response.Code == config.CodigoExito)
            {
                //records = new List<ReportResponseV1>();

                //records = response.listado.ToList();
                //recordsTotal = records.Count;
                record = new ReportResponseV1();

                record = response.objeto;
                recordsTotal = (record != null ? 1 : 0);
                return Json(new
                {
                    code = response.Code,
                    message = response.Message,
                    recordsTotal = recordsTotal,
                    records = record
                }
                );
            }
            else
            {
                return Json(new
                {
                    code = response.Code,
                    message = response.Message,
                    recordsTotal = recordsTotal,
                    records = record
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> GetReportJustaCausa(ReportRequestV1 request)
        {

            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            Common.EResponseBase<ReportResponseV1> response = await proxyCoreAPI.GetReportJustaCausa(transaction, logger, config, null, request);
            int recordsTotal = 0;
            ReportResponseV1 record = null;
            if (response.Code == config.CodigoExito)
            {
                record = new ReportResponseV1();

                record = response.objeto;
                recordsTotal = (record != null ? 1 : 0);
                return Json(new
                {
                    code = response.Code,
                    message = response.Message,
                    recordsTotal = recordsTotal,
                    records = record
                }
                );
            }
            else
            {
                return Json(new
                {
                    code = response.Code,
                    message = response.Message,
                    recordsTotal = recordsTotal,
                    records = record
                });
            }
        }

        private void InitializeLogger(Transaction transaction)
        {
            logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, transaction);
        }
    }
}