using Common.AspNet.Logging;
using Common.Logging;
using Core.API.Model.Response;
using EnrollmentSystemWebApp.Helpers;
using EnrollmentSystemWebApp.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EnrollmentSystemWebApp.Controllers
{
    [Authorize]
    public class ReasonJustCauseController : Controller
    {
        private CustomConfigurationLib config;
        private ICustomLog logger;
        private ProxyCoreAPI proxyCoreAPI;
        private ProxySecurityAPI proxySecurityAPI;
        // GET: ReasonJustCause
        public ReasonJustCauseController()
        {
            config = new CustomConfigurationLib();
            logger = new CustomLog4Net();
            proxyCoreAPI = new ProxyCoreAPI();
            proxySecurityAPI = new ProxySecurityAPI();
        }

        public async Task<ActionResult> GetReasonJustCause()
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            Common.EResponseBase<ReasonJustCauseResponseV1> response = await proxyCoreAPI.GetReasonJustCause(transaction, logger, config, null);

            int recordsTotal = 0;
            IEnumerable<ReasonJustCauseResponseV1> records = null;
            if (response.Code == config.CodigoExito)
            {
                recordsTotal = response.listado.Count();
                records = response.listado.ToList();
            }
            var result = new
            {
                code = response.Code,
                message = response.Message,
                recordsTotal = recordsTotal,
                records = records
            };
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public async Task<ActionResult> GetReasonJustCauseByID(int ReasonJustCauseId)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            Common.EResponseBase<ReasonJustCauseResponseV1> response = await proxyCoreAPI.GetReasonJustCauseById(transaction, logger, config, null, ReasonJustCauseId);
            var result = new
            {
                code = response.Code,
                message = response.Message,
                objeto = response.objeto
            };
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        private void InitializeLogger(Transaction transaction)
        {
            logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, transaction);
        }
    }
}