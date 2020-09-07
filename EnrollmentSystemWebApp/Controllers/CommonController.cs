using Audit.Mvc;
using Common;
using Common.AspNet.Logging;
using Common.HttpHelpers;
using Common.Logging;
using Core.API.Model;
using Core.API.Model.Request;
using Core.API.Model.Response;
using Domain.Custom_Models;
using EnrollmentSystemWebApp.Helpers;
using EnrollmentSystemWebApp.Proxy;
using Security.API.Model.Response;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnrollmentSystemWebApp.Controllers
{
    //[Audit(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = false, IncludeRequestBody = false)]
    [Authorize]
    public class CommonController : Controller
    {
        private CustomConfigurationLib config;
        private ICustomLog logger;
        private ProxyCoreAPI proxyCoreAPI;
        private ProxySecurityAPI proxySecurityAPI;

        public CommonController()
        {
            config = new CustomConfigurationLib();
            logger = new CustomLog4Net();
            proxyCoreAPI = new ProxyCoreAPI();
            proxySecurityAPI = new ProxySecurityAPI();
        }

        public async Task<ActionResult> GetAllMco()
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            bool showEnrollmentProcess = true;
            EResponseBase<McoResponseV1> response = await proxyCoreAPI.GetMcos(transaction, logger, config, null, showEnrollmentProcess);
            int recordsTotal = 0;
            IEnumerable<McoResponseV1> records = null;
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
        public async Task<ActionResult> GetPcpWithFilters(PcpRequestV1 entityRequest)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<PrimaryCarePhysicianCustomModel> response = await proxyCoreAPI.GetPcpWithFilters(transaction, logger, config, null, entityRequest);
            int recordsTotal = 0;
            IEnumerable<PrimaryCarePhysicianCustomModel> records = null;
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
        public async Task<ActionResult> GetPcpWithFiltersToList(PcpRequestV1 entityRequest)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<PcpResponseV2> response = await proxyCoreAPI.GetPcpWithFiltersToList(transaction, logger, config, null, entityRequest);
            int recordsTotal = 0;
            IEnumerable<PcpResponseV2> records = null;
            if (response.Code == config.CodigoExito)
            {
                recordsTotal = response.listado.Count();
                records = response.listado.ToList();
            }
            var language = "";
            if (SessionHelper.Language == "en")
                language = response.MessageEN;
            else
                language = response.Message;

            var result = new
            {
                code = response.Code,
                message = language,
                recordsTotal = recordsTotal,
                records = records
            };
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public async Task<ActionResult> ChangePersonMcoEnabled(int MemberId)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<EnrollmentResponseV2> response = await proxyCoreAPI.ChangePersonMcoEnabled(transaction, logger, config, null, MemberId);
            EnrollmentResponseV2 record = new EnrollmentResponseV2();
            record = response.objeto;
            Session[config.ChangePersonMcoEnabled] = record.Enabled;
            var result = new
            {
                code = response.Code,
                message = response.Message,
                records = record
            };
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public async Task<ActionResult> ChangeEnrollmentEnabledJustCause(int MemberId)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<EnrollmentResponseV2> response = await proxyCoreAPI.ChangeEnrollmentEnabledJustCause(transaction, logger, config, null, MemberId);
            EnrollmentResponseV2 record = new EnrollmentResponseV2();
            record = response.objeto;
            Session[config.ChangeEnrollmentEnabledJustCause] = record.Enabled;
            var result = new
            {
                code = response.Code,
                message = response.Message,
                records = record
            };
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public async Task<ActionResult> GetAllSpeciality()
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<SpecialityResponseV1> response = await proxyCoreAPI.GetSpecialities(transaction, logger, config, null);
            int recordsTotal = 0;
            IEnumerable<SpecialityResponseV1> records = null;
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
        public async Task<ActionResult> GetAllSpecialityPCP(int PCPId)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            PmgRequestV1 request = new PmgRequestV1()
            {
                PCPId = PCPId,
                ShowForChangeEnrollmentProcess = true
            };
            EResponseBase<SpecialityResponseV1> response = await proxyCoreAPI.PostSpecialitiesPCPId(transaction, logger, config, null, request);
            int recordsTotal = 0;
            IEnumerable<SpecialityResponseV1> records = null;
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

        public async Task<ActionResult> GetAllPmgPCP(int PCPId)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            PmgRequestV1 request = new PmgRequestV1()
            {
                PCPId = PCPId,
                ShowForChangeEnrollmentProcess = true
            };
            EResponseBase<PmgResponseV1> response = await proxyCoreAPI.PostAllPmgPCP(transaction, logger, config, null, request);
            int recordsTotal = 0;
            IEnumerable<PmgResponseV1> records = null;
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

        public async Task<ActionResult> GetAllPcp()
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<PersonPcpResponseV1> response = await proxyCoreAPI.GetPcps(transaction, logger, config, null);
            int recordsTotal = 0;
            IEnumerable<PersonPcpResponseV1> records = null;
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

        public async Task<ActionResult> GetAllPmg()
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            bool ShowForChangeEnrollmentProcess = true;
            EResponseBase<PmgResponseV1> response = await proxyCoreAPI.GetPmgs(transaction, logger, config, null, ShowForChangeEnrollmentProcess);
            int recordsTotal = 0;
            IEnumerable<PmgResponseV1> records = null;
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
        public async Task<ActionResult> GetEnrollmentHistoryByPersonId(int PersonId)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<EnrollmentHistoryResponseV1> response = await proxyCoreAPI.GetEnrollmentHistoryByPersonId(transaction, logger, config, null, PersonId);

            int recordsTotal = 0;
            IEnumerable<EnrollmentHistoryResponseV1> records = null;
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
        public async Task<ActionResult> SendConfirmationEmail(EmailRequestV1 entityRequest)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);

            object result;
            string email1 = string.Empty;
            string email2 = string.Empty;
            string emails = string.Empty;
            //EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            EResponseBase<MemberResponseV4> user = await proxyCoreAPI.GetEmailPeopleById(transaction, logger, config, null, entityRequest.MemberID);
            //if (user != null)
            //{

            if (user.objeto != null || !string.IsNullOrEmpty(entityRequest.Email))
            {
                MemberResponseV4 userT = user.objeto;

                if (user.objeto != null)
                    email1 = userT.Email;

                if (!string.IsNullOrEmpty(entityRequest.Email))
                    email2 = entityRequest.Email;

                if (!string.IsNullOrEmpty(email1) && !string.IsNullOrEmpty(email2))
                {
                    entityRequest.Email = string.Concat(email2, ";", email1);
                }
                else
                {
                    if (!string.IsNullOrEmpty(email1))
                    {
                        entityRequest.Email = email1;
                    }
                    if (!string.IsNullOrEmpty(email2))
                    {
                        entityRequest.Email = email2;
                    }
                }

                if (!string.IsNullOrEmpty(entityRequest.Email))
                {

                    EResponseBase<Common.HttpHelpers.SimpleEntity> response = await proxySecurityAPI.SendConfirmationEmail(config, entityRequest);
                    result = new
                    {
                        code = response.Code,
                        message = response.Message
                    };
                }
                else
                {
                    result = new
                    {
                        code = 0,
                        message = ""
                    };
                }
            }
            else
            {
                result = new
                {
                    code = 0,
                    message = ""
                };
            }
            //}
            //else
            //{
            //    result = new
            //    {
            //        code = -200,
            //        message = ""
            //    };
            //}

            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public async Task<ActionResult> CreatePDF(PcpPmgMcoRequestV1 entityRequest)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<PDFResponseV1> response = await proxyCoreAPI.CreatePDF(transaction, logger, config, null, entityRequest);
            var result = new
            {
                code = response.Code,
                message = response.Message,
                filePDF = response.objeto.filePDF
                //recordsTotal = recordsTotal,
                //records = records
            };
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public JsonResult ChangeLanguage(string language)
        {
            string newLanguage;
            string newCulture;

            if (language == AppConstants.EnglishLanguage)
            {
                newLanguage = AppConstants.EnglishLanguage;
                newCulture = AppConstants.EnglishCulture;
            }
            else
            {
                newLanguage = AppConstants.DefaultLanguage;
                newCulture = AppConstants.DefaultCulture;
            }

            SessionHelper.Language = newLanguage;
            SessionHelper.Culture = newCulture;

            bool result = true;
            string message = "No se pudo cambiar de Idioma";
            string url = Request.UrlReferrer.ToString();

            return Json(new { result = result, message = message, url = url }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SendSms(int PersonId, string phone)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            phone = phone.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
            EResponseBase<EnrollmentHistoryResponseV1> response = await proxyCoreAPI.SendSms(transaction, logger, config, null, PersonId, phone);
            var result = new
            {
                code = response.Code,
                message = response.Message
            };
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public async Task<ActionResult> SendLinkQuitz(EmailRequestV1 entityRequest)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);

            object result;
            EResponseBase<MemberResponseV4> user = await proxyCoreAPI.GetEmailPeopleById(transaction, logger, config, null, entityRequest.MemberID);
            if (user != null)
            {
                if (user.objeto != null)
                {
                    MemberResponseV4 userT = user.objeto;


                    entityRequest.NameTo = string.Concat(userT.FirstName.ToUpper(), " ", userT.FirstLastName.ToUpper(), " ", userT.SecondLastName.ToUpper());
                    if (entityRequest.Contact == true)
                    {
                        entityRequest.Email = string.Concat(entityRequest.Email, ";", userT.Email);
                    }
                    else
                    {
                        entityRequest.Email = userT.Email;
                    }


                    if (!string.IsNullOrEmpty(entityRequest.Email))
                    {
                        entityRequest.EnrollmentHistoryID = (int)Session[config.SessionEnrollmentHistory];
                        EResponseBase<SimpleEntity> response = await proxySecurityAPI.SendLinkQuitz(config, entityRequest);
                        result = new
                        {
                            code = response.Code,
                            message = response.Message
                        };
                    }
                    else
                    {
                        result = new
                        {
                            code = 0,
                            message = ""
                        };
                    }
                }
                else
                {
                    result = new
                    {
                        code = -200,
                        message = "Falta usuario para enviar"
                    };
                }
            }
            else
            {
                result = new
                {
                    code = -200,
                    message = "No envió datos"
                };
            }

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