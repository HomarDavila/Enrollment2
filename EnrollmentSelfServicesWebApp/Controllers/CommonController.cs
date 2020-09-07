using Common;
using Common.AspNet.Logging;
using Common.HttpHelpers;
using Common.Logging;
using Core.API.Model;
using Core.API.Model.Request;
using Core.API.Model.Response;
using Domain.Custom_Models;
using EnrrolmentSelfServicesWebApp.Helpers;
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
    public class CommonController : Controller
    {
        private CustomConfigurationLib config;
        private ICustomLog logger;
        private ProxyCoreAPI proxyCoreAPI;
        private ProxySecurityAPI proxySecurityAPI;
        private IConfigurationLib configuration;

        public CommonController()
        {
            config = new CustomConfigurationLib();
            logger = new CustomLog4Net();
            proxyCoreAPI = new ProxyCoreAPI();
            proxySecurityAPI = new ProxySecurityAPI();
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

        [HttpGet]
        public async Task<ActionResult> GetPeopleByMemberId()
        {
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<MemberResponseV2> response = await proxyCoreAPI.GetMembersById(transaction, logger, config, null, user.objeto.MemberId);
            int recordsTotal = 0;
            List<MemberResponseV2> records = null;
            if (response.Code == config.CodigoExito)
            {
                records = new List<MemberResponseV2>();

                records = response.listado.ToList();
                recordsTotal = records.Count;
                return Json(new
                {
                    code = response.Code,
                    message = response.Message,
                    recordsTotal = recordsTotal,
                    records = records
                }, JsonRequestBehavior.AllowGet
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
                }, JsonRequestBehavior.AllowGet
                );
            }
        }

        [HttpPost]
        public async Task<ActionResult> GetPcpWithFilters(PcpRequestV1 entityRequest)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<PcpResponseV1> response = await proxyCoreAPI.GetPcpWithFilters(transaction, logger, config, null, entityRequest);
            int recordsTotal = 0;
            IEnumerable<PcpResponseV1> records = null;
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

        public async Task<ActionResult> GetAllPmg(bool ShowForChangeEnrollmentProcess)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
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

        [HttpGet]
        //public async Task<ActionResult> GetAllSpeciality(bool ShowForChangeEnrollmentProcess, string pValor)
        public async Task<ActionResult> GetAllSpeciality(bool ShowForChangeEnrollmentProcess)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<SpecialityResponseV1> response = await proxyCoreAPI.GetSpecialities(transaction, logger, config, null, ShowForChangeEnrollmentProcess);
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

        [HttpPost]
        public async Task<ActionResult> GetAllSpecialityPCP(PmgRequestV1 request)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<SpecialityResponseV1> response = await proxyCoreAPI.GetSpecialitiesPCPId(transaction, logger, config, null, request);
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

        [HttpPost]
        public async Task<ActionResult> GetAllPmgPCP(PmgRequestV1 request)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<PmgResponseV1> response = await proxyCoreAPI.GetAllPmgPCP(transaction, logger, config, null, request);
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
        public async Task<ActionResult> ChangePersonMco(MemberRequestV1 request)
        {
            bool isEnabled = (bool)(Session[config.ChangePersonMcoEnabled] ?? false);
            if (isEnabled)
            {
                Transaction transaction = string.Empty.GetTransaction();
                InitializeLogger(transaction);
                EResponseBase<MemberResponseV1> response = await proxyCoreAPI.ChangePersonMco(transaction, logger, config, null, request);
                int recordsTotal = 0;
                MemberResponseV1 record = null;
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

        [HttpGet]
        public async Task<ActionResult> ChangePersonMcoEnabled(int memberId=0)
        {
            //EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            //memberId = 42;                            
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<EnrollmentResponseV2> response = await proxyCoreAPI.ChangePersonMcoEnabled(transaction, logger, config, null, memberId);
            EnrollmentResponseV2 record = new EnrollmentResponseV2();
            
            //if (response.Code == config.CodigoExito)
            //{
            //record = new EnrollmentResponseV2();
            record = response.objeto;
            Session[config.ChangePersonMcoEnabled] = record.Enabled;
            //}
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

        [HttpPost]
        public async Task<ActionResult> SendConfirmationEmail(EmailRequestV1 entityRequest)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);

            object result;
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            if (user != null)
            {
                if (user.objeto != null)
                {
                    User_Response_v1 userT = user.objeto;


                    entityRequest.NameTo = string.Concat(userT.FirstName.ToUpper(), " ", userT.LastName1.ToUpper(), " ", userT.LastName2.ToUpper());
                    
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

                        EResponseBase<SimpleEntity> response = await proxySecurityAPI.SendConfirmationEmail(config, entityRequest);
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

        [HttpPost]
        public async Task<ActionResult> SendUserRegisterEmail(EmailRequestV1 entityRequest)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);

            object result;


            if (!string.IsNullOrEmpty(entityRequest.Email))
            {
                EResponseBase<SimpleEntity> response = await proxySecurityAPI.SendUserRegister(config, entityRequest);
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
                    code = -200,
                    message = "Falta Ingresar email"
                };
            }



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
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            if (user != null)
            {
                if (user.objeto != null)
                {
                    User_Response_v1 userT = user.objeto;


                    entityRequest.NameTo = string.Concat(userT.FirstName.ToUpper(), " ", userT.LastName1.ToUpper(), " ", userT.LastName2.ToUpper());
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


        [HttpPost]
        public async Task<ActionResult> GetPDF(FileRequestV1 request)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<FileResponseV1> response = await proxyCoreAPI.GetPDF(transaction, logger, config, null, request);

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
        public async Task<ActionResult> SendSms(int PersonId, string phone)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            phone = phone.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
            EResponseBase<EnrollmentHistoryResponseV1> response = await proxyCoreAPI.SendSms(transaction, logger, config, null, PersonId, phone);

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
        public async Task<ActionResult> GetEnrPeriod(MemberRequestV1 request)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<EnrollmentPeriodResponseV1> response = await proxyCoreAPI.GetEnrPeriod(transaction, logger, config, null, request);

            return new JsonResult
            {
                Data = response,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        private void InitializeLogger(Transaction transaction)
        {
            logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, transaction);
        }

    }
}