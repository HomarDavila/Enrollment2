using Common.AspNet.Logging;
using Common.Logging;
using Core.API.Model;
using Core.API.Model.Response;
using EnrollmentSystemWebApp.Helpers;
using EnrollmentSystemWebApp.Proxy;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnrollmentSystemWebApp.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private CustomConfigurationLib config;
        private ICustomLog logger;
        private ProxyCoreAPI proxyCoreAPI;
        private ProxySecurityAPI proxySecurityAPI;
        // GET: File
        public FileController()
        {
            config = new CustomConfigurationLib();
            logger = new CustomLog4Net();
            proxyCoreAPI = new ProxyCoreAPI();
            proxySecurityAPI = new ProxySecurityAPI();
        }

        [HttpPost]
        public async Task<ActionResult> GetFile(int FileId)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            Common.EResponseBase<FileResponseV1> response = await proxyCoreAPI.GetFile(transaction, logger, config, null, FileId);

            int recordsTotal = 0;
            IEnumerable<FileResponseV1> records = null;
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
        public async Task<ActionResult> GetEnrollmentFiles(int MemberId)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            Common.EResponseBase<FileResponseV1> response = await proxyCoreAPI.GetEnrollmentFiles(transaction, logger, config, null, MemberId);

            int recordsTotal = 0;
            IEnumerable<FileResponseV1> records = null;
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
        public async Task<ActionResult> DisabledEnrollmentFiles(FileRequestV1 entityRequest)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            Common.EResponseBase<FileResponseV1> response = await proxyCoreAPI.DisabledEnrollmentFiles(transaction, logger, config, null, entityRequest);
            //int recordsTotal = 0;
            //IEnumerable<FileResponseV1> records = null;
            //if (response.Code == config.CodigoExito)
            //{
            //    recordsTotal = response.listado.Count();
            //    records = response.listado.ToList();
            //}
            var result = new
            {
                code = response.Code,
                message = response.Message,
                //recordsTotal = recordsTotal,
                //records = records
            };
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public async Task<ActionResult> SetEnrollmentFiles(FileRequestV1 entityRequest)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            Common.EResponseBase<FileResponseV1> response = await proxyCoreAPI.SetEnrollmentFiles(transaction, logger, config, null, entityRequest);
            //int recordsTotal = 0;

            //IEnumerable<FileResponseV1> records = null;
            //if (response.Code == config.CodigoExito)
            //{
            //    recordsTotal = response.listado.Count();
            //    records = response.listado.ToList();
            //}
            var result = new
            {
                code = response.Code,
                message = response.Message,
                //recordsTotal = recordsTotal,
                //records = records
            };
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public async Task<ActionResult> GetValidateFiles()
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);

            await Task.Delay(1);
            var result = new
            {
                code = 0,
                message = "Ok",
                TamanioMaximo = config.TamanioMaximoEnrollmentFile,
                ExtensionValid = config.ExtensionValidEnrollmentFile,
                Path = config.PathEnrollmentFile
            };

            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public async Task<ActionResult> GetPDF(FileRequestV1 request)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            Common.EResponseBase<FileResponseV1> response = await proxyCoreAPI.GetPDF(transaction, logger, config, null, request);

            var result = new
            {
                code = response.Code,
                message = response.Message,
                content = response.objeto.Content
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