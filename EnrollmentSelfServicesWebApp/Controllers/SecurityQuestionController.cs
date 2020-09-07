using Common.AspNet.Logging;
using Common.Logging;
using EnrrolmentSelfServicesWebApp.Helpers;
using EnrrolmentSelfServicesWebApp.Helpers.Http;
using EnrrolmentSelfServicesWebApp.Proxy;
using Security.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EnrollmentSelfServicesWebApp.Controllers
{
    public class SecurityQuestionController : Controller
    {
        private CustomConfigurationLib config;
        private ICustomLog logger;

        public SecurityQuestionController()
        {
            config = new CustomConfigurationLib();
            logger = new CustomLog4Net();
        }

        [HttpGet]
        public ActionResult Get(string searchTerm)
        {
            ProxySecurityAPI proxy = new ProxySecurityAPI();
            Transaction transaction = string.Empty.GetTransaction();
            List<SecurityQuestion_Response_v1> list = new List<SecurityQuestion_Response_v1>();
            InitializeLogger(transaction);
            using (log4net.NDC.Push(AuditUserDataHelpers.AuditUserData()))
            {
                Common.EResponseBase<SecurityQuestion_Response_v1> response = Task.Run(() => proxy.GetSecurityQuestions(config)).Result;
                if (response.Code == config.CodigoExito)
                {
                    if (!String.IsNullOrEmpty(searchTerm))
                    {
                        List<SecurityQuestion_Response_v1> listTemp = response.listado.ToList();
                        list = response.listado.ToList().Where(x => x.QuestionES.Trim().ToLower().Contains(searchTerm.Trim().ToLower())).ToList();
                    }
                    else
                    {
                        list = response.listado.ToList();
                    }
                }


                var result = new
                {
                    Total = list.Count(),
                    Results = list
                };
                return new JsonResult
                {
                    Data = result,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }


        private void InitializeLogger(Transaction transaction)
        {
            logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, transaction);
        }
    }
}