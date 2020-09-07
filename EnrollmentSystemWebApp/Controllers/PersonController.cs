using Audit.Mvc;
using Common;
using Common.AspNet.Logging;
using Common.Logging;
using Core.API.Model.Response;
using Domain.Custom_Models;
using EnrollmentSystemWebApp.Helpers;
using EnrollmentSystemWebApp.Proxy;
using Org.BouncyCastle.Asn1;
using Security.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnrollmentSystemWebApp.Controllers
{
    //[Audit(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = false, IncludeRequestBody = false)]
    [Authorize]
    public class PersonController : Controller
    {
        private CustomConfigurationLib config;
        private ICustomLog logger;
        private ProxyCoreAPI proxyCoreAPI;

        public PersonController()
        {
            config = new CustomConfigurationLib();
            logger = new CustomLog4Net();
            proxyCoreAPI = new ProxyCoreAPI();
        }

        // GET: Person
        public ActionResult SearchPerson()
        {
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            ViewBag.Usuario = user.objeto.FirstName;
            ViewBag.listOptions = user.objeto.listOptions;
            return View();
        }
        public ActionResult ConfigPeriod()
        {
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            ViewBag.Usuario = user.objeto.FirstName;
            ViewBag.listOptions = user.objeto.listOptions;
            return View();
        }

        public ActionResult ExportCsv()
        {
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            ViewBag.Usuario = user.objeto.FirstName;
            ViewBag.listOptions = user.objeto.listOptions;
            return View();
        }
        public ActionResult Settings()
        {
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            ViewBag.Usuario = user.objeto.FirstName;
            ViewBag.listOptions = user.objeto.listOptions;
            return View();
        }
        public ActionResult Reports()
        {
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            ViewBag.Usuario = user.objeto.FirstName;
            ViewBag.listOptions = user.objeto.listOptions;
            return View();
        }

        public ActionResult ReportsWeb()
        {
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            ViewBag.Usuario = user.objeto.FirstName;
            ViewBag.listOptions = user.objeto.listOptions;
            return View();
        }

        public ActionResult ReportsSystem()
        {
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            ViewBag.Usuario = user.objeto.FirstName;
            ViewBag.listOptions = user.objeto.listOptions;
            return View();
        }
        public ActionResult ReportsEnrollment()
        {
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            ViewBag.Usuario = user.objeto.FirstName;
            ViewBag.listOptions = user.objeto.listOptions;
            return View();
        }
        public ActionResult ReportsDisenrollment()
        {
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            ViewBag.Usuario = user.objeto.FirstName;
            ViewBag.listOptions = user.objeto.listOptions;
            return View();
        }
        public ActionResult ReportsJustcause()
        {
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            ViewBag.Usuario = user.objeto.FirstName;
            ViewBag.listOptions = user.objeto.listOptions;
            return View();
        }

        public ActionResult ReportsCallCenter()
        {
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            ViewBag.Usuario = user.objeto.FirstName;
            ViewBag.listOptions = user.objeto.listOptions;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetPeopleByFilters(MemberRequestV2 request)
        {

            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            Common.EResponseBase<MemberResponseV6> response = await proxyCoreAPI.GetPeopleByFilters(transaction, logger, config, null, request);
            int recordsTotal = 0;
            List<MemberResponseV6> records = null;
            if (response.Code == config.CodigoExito)
            {
                records = new List<MemberResponseV6>();

                records = response.listado.ToList();
                recordsTotal = records.Count;
                return Json(new
                {
                    code = response.Code,
                    message = response.Message,
                    recordsTotal = recordsTotal,
                    records = records
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
                    records = records
                });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult>  GetByFilters(string Last4SSN="", string DateOfBirth= "")
        {

            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            string url = string.Empty;

            MemberRequestV2 request = new MemberRequestV2
            {
                Last4SSN = Last4SSN,
                DateOfBirth =DateOfBirth=="" ? DateTime.MinValue : Convert.ToDateTime( DateOfBirth)
            };
            Common.EResponseBase<MemberResponseV6> response = await proxyCoreAPI.GetPeopleByFilters(transaction, logger, config, null, request);

            if (response.listado != null && response.listado.ToList().Count > 0)
                url =string.Concat(config.urlMembers, response.listado.FirstOrDefault().Id.ToString());           
             
            return Json(new{url = url }, JsonRequestBehavior.AllowGet);

        }
       

        [HttpPost]
        public async Task<ActionResult> ChangePersonMco(MemberRequestV1 request)
        {
            bool isEnabled = (request.IsJustCause) ? (bool)Session[config.ChangeEnrollmentEnabledJustCause] : (bool)Session[config.ChangePersonMcoEnabled];
            if (isEnabled)
            {
                Transaction transaction = string.Empty.GetTransaction();
                InitializeLogger(transaction);
                EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
                request.UserName = user.objeto.FirstName;
                Common.EResponseBase<MemberResponseV1> response = await proxyCoreAPI.ChangePersonMco(transaction, logger, config, null, request);
                int recordsTotal = 0;
                MemberResponseV1 record = null;
                //TODO: HOMAR Quitar
                //response.Code = 0;
                if (response.Code == config.CodigoExito)
                {
                    record = new MemberResponseV1();
                    record = response.objeto;
                    Session[config.SessionEnrollmentHistory] = record.EnrollmentHistoryID;
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
            else
            {
                return Json(new
                {
                    code = -1000
                });
            }
        }
        [HttpPost]
        public async Task<ActionResult> ChangePersonMcoReject(MemberRequestV1 request)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            Common.EResponseBase<MemberResponseV1> response = await proxyCoreAPI.ChangePersonMcoReject(transaction, logger, config, null, request);
            int recordsTotal = 0;
            MemberResponseV1 record = null;
            //TODO: HOMAR Quitar
            //response.Code = 0;
            if (response.Code == config.CodigoExito)
            {
                record = new MemberResponseV1();
                record = response.objeto;
                Session[config.SessionEnrollmentHistory] = record.EnrollmentHistoryID;
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