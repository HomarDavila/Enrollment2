using Common;
using Common.AspNet.Logging;
using Common.Logging;
using Core.API.Model.Response;
using EnrrolmentSelfServicesWebApp.Helpers;
using EnrrolmentSelfServicesWebApp.Proxy;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnrollmentSelfServicesWebApp.Controllers
{
    public class MunicipalityController : Controller
    {
        private CustomConfigurationLib config;
        private ICustomLog logger;
        private ProxyCoreAPI proxyCoreAPI;
        private ProxySecurityAPI proxySecurityAPI;

        public MunicipalityController()
        {
            config = new CustomConfigurationLib();
            logger = new CustomLog4Net();
            proxyCoreAPI = new ProxyCoreAPI();
            proxySecurityAPI = new ProxySecurityAPI();
        }

        public async Task<ActionResult> Get()
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<MunicipalityResponseV1> response = await proxyCoreAPI.GetMunicipalitys(transaction, logger, config, null);
            int recordsTotal = 0;
            IEnumerable<MunicipalityResponseV1> records = null;
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

        private void InitializeLogger(Transaction transaction)
        {
            logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, transaction);
        }

    }
}