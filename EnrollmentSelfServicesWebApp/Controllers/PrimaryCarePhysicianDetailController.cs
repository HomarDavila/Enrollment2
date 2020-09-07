using Common;
using Common.AspNet.Logging;
using Common.Logging;
using Core.API.Model;
using Domain.Custom_Models;
using EnrrolmentSelfServicesWebApp.Helpers;
using EnrrolmentSelfServicesWebApp.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EnrollmentSelfServicesWebApp.Controllers
{
    public class PrimaryCarePhysicianDetailController : Controller
    {
        private CustomConfigurationLib config;
        private ICustomLog logger;
        private ProxyCoreAPI proxyCoreAPI;
        private ProxySecurityAPI proxySecurityAPI;

        public PrimaryCarePhysicianDetailController()
        {
            config = new CustomConfigurationLib();
            logger = new CustomLog4Net();
            proxyCoreAPI = new ProxyCoreAPI();
            proxySecurityAPI = new ProxySecurityAPI();
        }

        [HttpPost]
        public async Task<ActionResult> GetPrimaryCarePhysicianDetailWithFiltersToList(PrimaryCarePhysicianDetailRequestV1 entityRequest)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<PrimaryCarePhysicianDetailCustomModel> response = await proxyCoreAPI.GetPrimaryCarePhysicianDetailWithFiltersToList(transaction, logger, config, null, entityRequest);
            int recordsTotal = 0;
            IEnumerable<PrimaryCarePhysicianDetailCustomModel> records = null;
            if (response.Code == config.CodigoExito)
            {
                recordsTotal = response.listado.Count();
                records = response.listado.ToList();
            }
            string language = SessionHelper.Language;
            string mensaje = "";
            if (language == "en")
                mensaje = response.MessageEN;
            else
                mensaje = response.Message;
            var result = new
                {
                    code = response.Code,
                    message = mensaje,
                    recordsTotal = recordsTotal,
                    records = records
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