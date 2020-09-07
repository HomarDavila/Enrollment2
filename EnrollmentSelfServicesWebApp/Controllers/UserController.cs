using Common;
using Common.AspNet.Logging;
using Common.Enums;
using Common.HttpHelpers;
using Common.Logging;
using EnrollmentSelfServicesWebApp.Helpers;
using EnrrolmentSelfServicesWebApp.Helpers;
using EnrrolmentSelfServicesWebApp.Helpers.Http;
using EnrrolmentSelfServicesWebApp.Helpers.Identity;
using EnrrolmentSelfServicesWebApp.Proxy;
using Security.API.Model.Request;
using Security.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EnrollmentSelfServicesWebApp.Controllers
{
    public class UserController : Controller
    {
        private CustomConfigurationLib config;
        private ICustomLog logger;

        public UserController()
        {
            config = new CustomConfigurationLib();
            logger = new CustomLog4Net();
        }

        // GET: User
        [AlreadyAuthenticated]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.urlGov = config.url_Gov;
            ViewBag.urlVital = config.url_Vital;
            ViewBag.urlASES = config.url_ASES;
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            ViewBag.urlGov = config.url_Gov;
            ViewBag.urlVital = config.url_Vital;
            ViewBag.urlASES = config.url_ASES;
            return View();
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.urlGov = config.url_Gov;
            ViewBag.urlVital = config.url_Vital;
            ViewBag.urlASES = config.url_ASES;
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            if (user != null)
            {
                if (user.objeto != null)
                {
                    User_Response_v1 userT = user.objeto;
                    ViewBag.UserId = userT.Id;
                }
            }
            return View();
        }
        public ActionResult ChangePasswordExternal()
        {
            ViewBag.urlGov = config.url_Gov;
            ViewBag.urlVital = config.url_Vital;
            ViewBag.urlASES = config.url_ASES;
            //EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            //if (user != null)
            //{
            //    if (user.objeto != null)
            //    {
            //        User_Response_v1 userT = user.objeto;
            //        ViewBag.UserId = userT.Id;
            //    }
            //}
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ChangePasswordExternal(User_Request_Filters_v12 request, string ReturnUrl = "")
        {
            ProxySecurityAPI proxy = new ProxySecurityAPI();
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            using (log4net.NDC.Push(AuditUserDataHelpers.AuditUserData()))
            {
                logger.Print_InitMethod();
                try
                {
                    if (ModelState.IsValid)
                    {
                        logger.Print_Request(request);
                        EResponseBase<User_Response_v1> userResponse = await proxy.ChangePasswordExternal(config, request);
                        logger.Print_Response(userResponse);
                        if (request.MPI != userResponse.objeto.MPI)
                            return Json(new { code = config.CodigoMPIInCorrect, message = config.MensajeMPIInCorrectES, messageEN = config.MensajeMPIInCorrectEN }, JsonRequestBehavior.AllowGet);

                            return Json(new { code = userResponse.Code, message = userResponse.Message, messageEN = userResponse.MessageEN }, JsonRequestBehavior.AllowGet);
                    }
                    else return Json(new { code = config.CodigoParametrosNoValido, message = config.MensajeParametrosNoValidoES, messageEN = config.MensajeParametrosNoValidoEN }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);

                    return Json(new { code = config.CodigoErrorNoEspecificado, message = config.MensajeErrorNoEspecificadoES, messageEN = config.MensajeErrorNoEspecificadoEN }, JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    logger.Print_EndMethod();
                    proxy = null;
                }
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User_Request_Filters_v11 request)
        {
            ProxySecurityAPI proxy = new ProxySecurityAPI();

            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            using (log4net.NDC.Push(AuditUserDataHelpers.AuditUserData()))
            {
                logger.Print_InitMethod();
                try
                {
                    if (ModelState.IsValid)
                    {
                        logger.Print_Request(request.UserName);
                        EResponseBase<Common.HttpHelpers.TokenResponse> response = LoginCoreActions.getInstance().Login(request.UserName, request.Password, config.AudienceId);
                        logger.Print_Response(response);
                        var hasDefaultCredentials = false;
                        var isImported = false;
                        if (response.Code == 0)
                        {
                            //if (response.objeto.CreatedBy == "Imported")
                            //{
                            //    return RedirectToAction("ChangePasswordExternal", "User");
                            //}
                            ReportsController ctrl = new ReportsController();
                            _ = ctrl.InsertStatistic(3);
                            //var user = await proxy.GetUserByName(config, request.UserName, response.objeto.AccessToken);
                            //var user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
                            //hasDefaultCredentials = user.objeto.HasDefaultCredentials;
                            isImported = response.objeto.CreatedBy == "Imported";
                            if (!isImported)
                            {
                                hasDefaultCredentials = Convert.ToBoolean(response.objeto.HasDefaultCredentials);
                                FormsAuthentication.SetAuthCookie(request.UserName, false);
                                var authTicket = new FormsAuthenticationTicket(1, request.UserName, DateTime.Now, DateTime.Now.AddMinutes(response.objeto.ExpiresIn), false, "User");
                                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                                HttpContext.Response.Cookies.Add(authCookie);
                            }
                        }

                        return Json(new
                        {
                            code = response.Code,
                            message = messageWithFunctionalErrors(response),
                            messageEN = messageENWithFunctionalErrors(response),
                            hasDefaultCredentials = hasDefaultCredentials,
                            isImported = isImported
                        });
                    }
                    else
                    {
                        return Json(new { code = config.CodigoParametrosNoValido, message = config.MensajeParametrosNoValidoES, messageEN = config.MensajeParametrosNoValidoEN });
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return Json(new { code = config.CodigoErrorNoEspecificado, message = config.MensajeErrorNoEspecificadoES, messageEN = config.MensajeErrorNoEspecificadoEN });
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(User_Request_v1 request, string ReturnUrl = "")
        {
            ProxySecurityAPI proxy = new ProxySecurityAPI();
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            using (log4net.NDC.Push(AuditUserDataHelpers.AuditUserData()))
            {
                logger.Print_InitMethod();
                try
                {
                    if (ModelState.IsValid)
                    {
                        List<UserRol_Request_v1> Roles = new List<UserRol_Request_v1>();
                        Roles.Add(new UserRol_Request_v1
                        {
                            RoleId = (int)EnumRol.Role.UserEnrollmentSelfService,
                            CreatedBy = AppConstants.System,
                            ApplicationId = (int)EnumRol.Application.EnrollmentSelfService
                        });
                        request.Roles = Roles;
                        request.CreatedBy = AppConstants.System;
                        request.CreatedOn = DateTime.Now;
                        logger.Print_Request(request);
                        //Registrando usuario
                        EResponseBase<Common.HttpHelpers.SimpleEntity> response = Task.Run(() => proxy.Register(config, request)).Result;
                        logger.Print_Response(response);

                        //Registrando por defecto como rol usuario
                        if (response.Code == config.CodigoExito)
                        {
                            if (response.objeto != null)
                            {
                                EResponseBase<UserRol_Response_v1> response2 = Task.Run(() => proxy.RegisterUserRol(config, new UserRol_Request_v1() { CreatedBy = "System", CreatedOn = DateTime.Now, Enabled = true, RoleId = config.RoleByDefault, UserId = response.objeto.Id })).Result;
                            }
                        }

                        return Json(new { code = response.Code, message = response.Message, messageEN = response.MessageEN });
                    }
                    else return Json(new { result = config.CodigoParametrosNoValido, message = config.MensajeParametrosNoValidoES, messageEN = config.MensajeParametrosNoValidoEN }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return Json(new { result = config.CodigoErrorNoEspecificado, message = config.MensajeErrorNoEspecificadoES, messageEN = config.MensajeErrorNoEspecificadoEN }, JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    logger.Print_EndMethod();
                    proxy = null;
                }

            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(User_Request_Filters_v8 request, string ReturnUrl = "")
        {
            ProxySecurityAPI proxy = new ProxySecurityAPI();
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            using (log4net.NDC.Push(AuditUserDataHelpers.AuditUserData()))
            {
                logger.Print_InitMethod();
                try
                {
                    if (ModelState.IsValid)
                    {
                        logger.Print_Request(request);
                        EResponseBase<User_Response_v2> response = await proxy.ResetPassword(config, request);
                        if (response.Code == config.CodigoExito)
                        {
                            if (response.objeto != null)
                            {
                                await proxy.SendResetPasswordEmail(config,
                                    new Mail_Request_v1() { Subject = config.SubjectChangePasswordProcess, EmailTo = request.UserName, Password = response.objeto.PassWithoutEncrypt, UserName = request.UserName, UserFullName = $"{response.objeto.FirstName}  {response.objeto.LastName1} {response.objeto.LastName2}", Language = request.Language });
                            }
                        }
                        logger.Print_Response(response);
                        return Json(new { code = response.Code, message = response.Message, messageEN = response.MessageEN });
                    }
                    else return Json(new { code = config.CodigoParametrosNoValido, message = config.MensajeParametrosNoValidoES, messageEN = config.MensajeParametrosNoValidoEN });
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return Json(new { code = config.CodigoErrorNoEspecificado, message = config.MensajeErrorNoEspecificadoES, messageEN = config.MensajeErrorNoEspecificadoEN });
                }
                finally
                {
                    logger.Print_EndMethod();
                    proxy = null;
                }
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ChangePassword(User_Request_Filters_v4 request, string ReturnUrl = "")
        {
            ProxySecurityAPI proxy = new ProxySecurityAPI();
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            using (log4net.NDC.Push(AuditUserDataHelpers.AuditUserData()))
            {
                logger.Print_InitMethod();
                try
                {
                    var isvalidUserIdRequest = await this.ValidateUserIdRequest(request.Id);
                    if (ModelState.IsValid && isvalidUserIdRequest)
                    {
                        logger.Print_Request(request);
                        EResponseBase<TokenResponse> token = (EResponseBase<TokenResponse>)Session[config.SessionToken];
                        EResponseBase<User_Response_v1> response = await proxy.ChangePassword(config, token.objeto.AccessToken, request);
                        if (response.Code == config.CodigoExito)
                        {
                            if (response.objeto != null)
                            {
                                await proxy.SendResetPasswordEmail(config,
                                    new Mail_Request_v1()
                                    {
                                        Subject = config.SubjectChangePasswordProcess,
                                        EmailTo = response.objeto.UserName,
                                        Password = null,
                                        UserName = response.objeto.UserName,
                                        UserFullName = $"{response.objeto.FirstName}  {response.objeto.LastName1} {response.objeto.LastName2}",
                                        Language = "es"
                                    });
                            }
                        }
                        logger.Print_Response(response);
                        return Json(new { code = response.Code, message = response.Message, messageEN = response.MessageEN }, JsonRequestBehavior.AllowGet);
                    }
                    else return Json(new { code = config.CodigoParametrosNoValido, message = config.MensajeParametrosNoValidoES, messageEN = config.MensajeParametrosNoValidoEN }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);

                    return Json(new { code = config.CodigoErrorNoEspecificado, message = config.MensajeErrorNoEspecificadoES, messageEN = config.MensajeErrorNoEspecificadoEN }, JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    logger.Print_EndMethod();
                    proxy = null;
                }
            }
        }

        [AllowAnonymous]
        public ActionResult Logout(string url)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            using (log4net.NDC.Push(AuditUserDataHelpers.AuditUserData()))
            {
                logger.Print_InitMethod();
                try
                {
                    EResponseBase<Common.HttpHelpers.SimpleEntity> response = LoginCoreActions.getInstance().Logout();
                    FormsAuthentication.SignOut();
                    logger.Print_Response(response);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
            if (string.IsNullOrEmpty(url))
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                return Redirect(url);
            }
        }

        private void InitializeLogger(Transaction transaction)
        {
            logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, transaction);
        }

        private string messageWithFunctionalErrors(Common.EResponseBase<Common.HttpHelpers.TokenResponse> response)
        {
            string customMessageES = string.Empty;
            customMessageES = customMessageES + response.Message + "<br>";
            if (response.FunctionalErrors != null)
            {
                foreach (Common.HttpHelpers.SimpleEntity functionalError in response.FunctionalErrors)
                {
                    customMessageES = customMessageES + functionalError.NameES + "<br>";
                }
            }

            return customMessageES;
        }
        private string messageENWithFunctionalErrors(Common.EResponseBase<Common.HttpHelpers.TokenResponse> response)
        {
            string customMessageEN = string.Empty;
            customMessageEN = customMessageEN + response.MessageEN + "<br>";
            if (response.FunctionalErrors != null)
            {
                foreach (Common.HttpHelpers.SimpleEntity functionalError in response.FunctionalErrors)
                {
                    customMessageEN = customMessageEN + functionalError.NameEN + "<br>";
                }
            }
            return customMessageEN;
        }

        [HttpPost]
        public async Task<ActionResult> GetId(int UserId)
        {
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            ProxySecurityAPI proxy = new ProxySecurityAPI();
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            EResponseBase<User_Response_v1> response = await proxy.GetId(transaction, logger, config, null, UserId);
            User_Response_v1 records = null;
            if (response.Code == config.CodigoExito)
            {
                records = new User_Response_v1();
                if (response.objeto != null)
                {
                    response.objeto.FirstName = string.IsNullOrEmpty(response.objeto.FirstName) ? string.Empty : response.objeto.FirstName.ToUpper();
                    response.objeto.LastName1 = string.IsNullOrEmpty(response.objeto.LastName1) ? string.Empty : response.objeto.LastName1.ToUpper();
                    response.objeto.LastName2 = string.IsNullOrEmpty(response.objeto.LastName2) ? string.Empty : response.objeto.LastName2.ToUpper();
                }
                records = response.objeto;
                return Json(new
                {
                    code = response.Code,
                    message = response.Message,
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
                    records = records
                }, JsonRequestBehavior.AllowGet
                );
            }
        }

        [HttpPost]
        public async Task<ActionResult> SetUser(User_Request_v1 entityRequest)
        {
            ProxySecurityAPI proxy = new ProxySecurityAPI();
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            var isvalidUserIdRequest = await this.ValidateUserIdRequest(entityRequest.Id);
            if (!isvalidUserIdRequest)
            {
                return Json(new { code = config.CodigoParametrosNoValido, message = config.MensajeParametrosNoValidoES, messageEN = config.MensajeParametrosNoValidoEN }, JsonRequestBehavior.AllowGet);
            }

            EResponseBase<User_Response_v1> response = await proxy.SetUser(transaction, logger, config, null, entityRequest);

            var result = new
            {
                code = response.Code,
                message = response.Message,
            };
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

    }
}