using Common.AspNet.Logging;
using Common.Logging;
using Domain.Custom_Models;
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
    public class ReportsController : Controller
    {
        private CustomConfigurationLib config;
        private ICustomLog logger;
        private ProxyCoreAPI proxyCoreAPI;

        public ReportsController()
        {
            config = new CustomConfigurationLib();
            logger = new CustomLog4Net();
            proxyCoreAPI = new ProxyCoreAPI();
        }

        private void InitializeLogger(Transaction transaction)
        {
            logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, transaction);
        }

        [HttpPost]
        public async Task<ActionResult> GetReportsWeb(StatisticsRequestV1 request)
        {

            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            Common.EResponseBase<Core.API.Model.Response.StatisticsResponseV1> response = await proxyCoreAPI.GetReportsWeb(transaction, logger, config, null, request);
            int recordsTotal = 0;
            List<EnrollmentPeriodResponseV1> records = null;
            if (response.Code == config.CodigoExito)
            {
                return Json(response);
            }
            else
            {
                return Json(new
                {
                    code = response.Code,
                    message = response.Message,
                    recordsTotal = recordsTotal,
                    records = records
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> InsertStatistic(int typeId)
        {
            StatisticsRequestV1 request = new StatisticsRequestV1
            {
                TypeId = typeId
            };

            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            Common.EResponseBase<Core.API.Model.Response.StatisticsResponseV1> response = await proxyCoreAPI.InsertStatistic(transaction, logger, config, null, request);
            int recordsTotal = 0;
            List<EnrollmentPeriodResponseV1> records = null;
            if (response.Code == config.CodigoExito)
            {
                return Json(response);
            }
            else
            {
                return Json(new
                {
                    code = response.Code,
                    message = response.Message,
                    recordsTotal = recordsTotal,
                    records = records
                });
            }
        }
    }
}