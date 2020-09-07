using AutoMapper;
using Common;
using Common.AspNet.Logging;
using Common.Logging;
using Core.API.Model;
using Domain.Custom_Models;
using EnrollmentSystemWebApp.Helpers;
using EnrollmentSystemWebApp.Proxy;
using Security.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
//using OfficeOpenXml;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnrollmentSystemWebApp.Controllers
{
    [Authorize]
    public class EnrollmentController : Controller
    {
        private CustomConfigurationLib config;
        private ICustomLog logger;
        private ProxyCoreAPI proxyCoreAPI;

        public EnrollmentController()
        {
            config = new CustomConfigurationLib();
            logger = new CustomLog4Net();
            proxyCoreAPI = new ProxyCoreAPI();
        }

        // GET: Enrollment
        public ActionResult Enrollment(int ApplicationMemberID)
        {
            try
            {
                TempData["ApplicationMemberID"] = ApplicationMemberID;
                TempData["NotApllyJustCauseId"] = config.NotApllyJustCauseId;
                EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
                List<Rol_Response_v1> roles = user.objeto.listRoles;
                ViewBag.Usuario = user.objeto.FirstName;
                ViewBag.listOptions = user.objeto.listOptions;
                bool permission = false;
                roles.ForEach(x =>
                {
                    if (x.Id == int.Parse(config.Role_Consuler) || x.Id == int.Parse(config.Role_CallCenter) || x.Id == int.Parse(config.Role_AdministradorSystem))
                    {
                        permission = true;
                    }
                });
                ViewBag.permission = permission.ToString().ToLower();
                ViewBag.language = Session[SessionHelper.LANGUAGE_SESSION_KEY];
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "User");
            }

        }

        public ActionResult Rejects()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetMembersByApplicationMemberID(int ApplicationMemberID)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<MemberResponseV1> response = await proxyCoreAPI.GetPeopleById(transaction, logger, config, null, ApplicationMemberID);
            int recordsTotal = 0;
            //Para las pruebas (Quitar despues)
            response.objeto.IsAvailableForChange = true;
            //Quitar el IsAvailableForChange
            List<MemberResponseV1> records = null;
            if (response.Code == config.CodigoExito)
            {
                records = new List<MemberResponseV1>
                {
                    response.objeto
                };
                recordsTotal = records.Count();
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

        [HttpPost]
        public async Task<ActionResult> GetOnlyRejects()
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<EnrollmentHistoryResponseV2> response = await proxyCoreAPI.GetOnlyRejects(transaction, logger, config, null);
            int recordsTotal = 0;
            List<EnrollmentHistoryResponseV2> records = null;
            if (response.Code == config.CodigoExito)
            {
                records = response.listado.ToList();
                recordsTotal = records.Count();
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

        [HttpPost]
        public async Task<ActionResult> GethistoryByFilters(MemberRequestV1 request)
        {

            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<EnrollmentHistoryResponseV1> response = await proxyCoreAPI.GetEnrollmentHistory(transaction, logger, config, null, request);
            int recordsTotal = 0;
            List<EnrollmentHistoryResponseV1> records = null;
            if (response.Code == config.CodigoExito)
            {
                records = new List<EnrollmentHistoryResponseV1>();

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


        [HttpPost]
        public async Task<ActionResult> SendPEnrollmenteriod(MemberRequestV1 request)
        {

            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<EnrollmentHistoryResponseV1> response = await proxyCoreAPI.SendEnrollmentPeriod(transaction, logger, config, null, request);
            int recordsTotal = 0;
            List<EnrollmentHistoryResponseV1> records = null;
            if (response.Code == config.CodigoExito)
            {
                records = new List<EnrollmentHistoryResponseV1>();


                return Json(new
                {
                    code = response.Code,
                    message = response.Message,
                    recordsTotal = recordsTotal
                }
                );
            }
            else
            {
                return Json(new
                {
                    code = response.Code,
                    message = response.Message,
                    recordsTotal = recordsTotal
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> GetEnrollmentPeriod(MemberRequestV1 request)
        {

            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<EnrollmentPeriodResponseV1> response = await proxyCoreAPI.GetEnrollmentPeriod(transaction, logger, config, null, request);
            int recordsTotal = 0;
            List<EnrollmentPeriodResponseV1> records = null;
            if (response.Code == config.CodigoExito)
            {
                records = new List<EnrollmentPeriodResponseV1>();

                //TODO: HOMAR Quitar
                //response.objeto.Enabled = true;

                recordsTotal = records.Count;
                return Json(new
                {
                    code = response.Code,
                    message = response.Message,
                    recordsTotal = recordsTotal,
                    records = response.objeto
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

        [HttpPost]
        public async Task<ActionResult> SendStatistics(MemberRequestV1 request)
        {

            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<EnrollmentHistoryResponseV1> response = await proxyCoreAPI.SendStatistics(transaction, logger, config, null, request);
            int recordsTotal = 0;
            List<EnrollmentPeriodResponseV1> records = null;
            if (response.Code == config.CodigoExito)
            {
                records = new List<EnrollmentPeriodResponseV1>();



                recordsTotal = records.Count;
                return Json(new
                {
                    code = response.Code,
                    message = response.Message,
                    recordsTotal = recordsTotal,
                    records = response.objeto
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

        [HttpPost]
        public async Task<ActionResult> GetStatistics(MemberRequestV1 request)
        {

            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<EnrollmentHistoryResponseV1> response = await proxyCoreAPI.SendStatistics(transaction, logger, config, null, request);
            int recordsTotal = 0;
            List<EnrollmentPeriodResponseV1> records = null;
            if (response.Code == config.CodigoExito)
            {
                records = new List<EnrollmentPeriodResponseV1>();



                recordsTotal = records.Count;
                return Json(new
                {
                    code = response.Code,
                    message = response.Message,
                    recordsTotal = recordsTotal,
                    records = response.objeto
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

        [HttpPost]
        public async Task<ActionResult> GetEnrollmentStatistics(MemberRequestV1 request)
        {

            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<EnrollmentHistoryResponseV1> response = await proxyCoreAPI.SendStatistics(transaction, logger, config, null, request);
            int recordsTotal = 0;
            List<EnrollmentPeriodResponseV1> records = null;
            if (response.Code == config.CodigoExito)
            {
                records = new List<EnrollmentPeriodResponseV1>();



                recordsTotal = records.Count;
                return Json(new
                {
                    code = response.Code,
                    message = response.Message,
                    recordsTotal = recordsTotal,
                    records = response.objeto
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



        public async Task<ActionResult> GetAllOverCapacity()
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<OverCapacityResponseV1> response = await proxyCoreAPI.GetAllOverCapacity(transaction, logger, config, null);
            int recordsTotalPmg = 0;
            int recordsTotalPcp = 0;
            int recordsTotalMco = 0;
            IEnumerable<OverCapacityResponseV1.PmgOverCapacity> recordsPmg = null;
            IEnumerable<OverCapacityResponseV1.McoOverCapacity> recordsMco = null;
            IEnumerable<OverCapacityResponseV1.PcpOverCapacity> recordsPcp = null;
            if (response.Code == config.CodigoExito)
            {
                recordsTotalPmg = response.objeto.lstPmgOverCapacity.Count();
                recordsPmg = response.objeto.lstPmgOverCapacity.ToList();
                recordsTotalMco = response.objeto.lstMcoOverCapacity.Count();
                recordsMco = response.objeto.lstMcoOverCapacity.ToList();
                recordsTotalPcp = response.objeto.lstPcpOverCapacity.Count();
                recordsPcp = response.objeto.lstPcpOverCapacity.ToList();
            }
            var result = new
            {
                code = response.Code,
                message = response.Message,
                recordsTotalPmg = recordsTotalPmg,
                recordsPmg = recordsPmg,
                recordsTotalMco = recordsTotalMco,
                recordsMco = recordsMco,
                recordsTotalPcp = recordsTotalPcp,
                recordsPcp = recordsPcp
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