using Common.AspNet.Logging;
using Common.Logging;
using Domain.Custom_Models;
using EnrollmentSystemWebApp.Helpers;
using EnrollmentSystemWebApp.Helpers.Http;
using EnrollmentSystemWebApp.Helpers.Identity;
using Security.API.Model.Request;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EnrollmentSystemWebApp.Controllers
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
        public ActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            return View();
        }

        //[MustBeAuthenticated]
        //[AllowAnonymous]
        //public ActionResult ChangePassword()
        //{
        //    return View();
        //}

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User_Request_Filters_v11 request)
        {
            Common.Logging.Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            using (log4net.NDC.Push(AuditUserDataHelpers.AuditUserData()))
            {
                logger.Print_InitMethod();
                try
                {
                    if (ModelState.IsValid)
                    {
                        logger.Print_Request(request.UserName);
                        Common.EResponseBase<Common.HttpHelpers.TokenResponse> response = LoginCoreActions.getInstance().Login(request.UserName, request.Password, config.AudienceId);
                        logger.Print_Response(response);

                        if (response.Code == 0)
                        {
                            ReportsController ctrl = new ReportsController();
                            _ = ctrl.InsertStatistic(4);
                            FormsAuthentication.SetAuthCookie(request.UserName, false);
                            var authTicket = new FormsAuthenticationTicket(1, request.UserName, DateTime.Now, DateTime.Now.AddMinutes(response.objeto.ExpiresIn + 1), false, "User");
                            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                            HttpContext.Response.Cookies.Add(authCookie);

                        }

                        return Json(new { code = response.Code, message = messageWithFunctionalErrors(response), messageEN = messageENWithFunctionalErrors(response) });
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

        //[HttpPost]
        //[AllowAnonymous]
        //public ActionResult Register(User_Request_v1 request, string ReturnUrl = "")
        //{
        //    ProxySecurityAPI proxy = new ProxySecurityAPI();
        //    Transaction transaction = string.Empty.GetTransaction();
        //    InitializeLogger(transaction);
        //    using (log4net.NDC.Push(AuditUserDataHelpers.AuditUserData()))
        //    {
        //        logger.Print_InitMethod();
        //        try
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                request.CreatedBy = "System";
        //                request.CreatedOn = DateTime.Now;
        //                logger.Print_Request(request);
        //                //Registrando usuario
        //                var response = Task.Run(() => proxy.Register(config, request)).Result;
        //                logger.Print_Response(response);

        //                //Registrando por defecto como rol usuario
        //                if (response.Code == config.CodigoExito)
        //                {
        //                    if (response.objeto != null)
        //                    {
        //                        var response2 = Task.Run(() => proxy.RegisterUserRol(config, new UserRol_Request_v1() { CreatedBy = "System", CreatedOn = DateTime.Now, Enabled = true, RoleId = config.RoleByDefault, UserId = response.objeto.Id })).Result;
        //                    }
        //                }

        //                return Json(new { code = response.Code, message = response.Message, messageEN = response.MessageEN });
        //            }
        //            else return Json(new { result = config.CodigoParametrosNoValido, message = config.MensajeParametrosNoValidoES, messageEN = config.MensajeParametrosNoValidoEN }, JsonRequestBehavior.AllowGet);
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Error(ex);
        //            return Json(new { result = config.CodigoErrorNoEspecificado, message = config.MensajeErrorNoEspecificadoES, messageEN = config.MensajeErrorNoEspecificadoEN }, JsonRequestBehavior.AllowGet);
        //        }
        //        finally
        //        {
        //            logger.Print_EndMethod();
        //            proxy = null;
        //        }

        //    }
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<ActionResult> ResetPassword(User_Request_Filters_v8 request, string ReturnUrl = "")
        //{
        //    ProxySecurityAPI proxy = new ProxySecurityAPI();
        //    Transaction transaction = string.Empty.GetTransaction();
        //    InitializeLogger(transaction);
        //    using (log4net.NDC.Push(AuditUserDataHelpers.AuditUserData()))
        //    {
        //        logger.Print_InitMethod();
        //        try
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                logger.Print_Request(request);
        //                var response = await proxy.ResetPassword(config, request);
        //                if (response.Code == config.CodigoExito)
        //                {
        //                    if (response.objeto != null)
        //                    {
        //                        await proxy.SendResetPasswordEmail(config, new Mail_Request_v1() { Subject = config.SubjectChangePasswordProcess, EmailTo = request.UserName, Password = response.objeto.PassWithoutEncrypt, UserName = request.UserName, UserFullName = $"{response.objeto.FirstName}  {response.objeto.LastName1} {response.objeto.LastName2}" });
        //                    }
        //                }
        //                logger.Print_Response(response);
        //                return Json(new { code = response.Code, message = response.Message, messageEN = response.MessageEN });
        //            }
        //            else return Json(new { code = config.CodigoParametrosNoValido, message = config.MensajeParametrosNoValidoES, messageEN = config.MensajeParametrosNoValidoEN });
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Error(ex);
        //            return Json(new { code = config.CodigoErrorNoEspecificado, message = config.MensajeErrorNoEspecificadoES, messageEN = config.MensajeErrorNoEspecificadoEN });
        //        }
        //        finally
        //        {
        //            logger.Print_EndMethod();
        //            proxy = null;
        //        }
        //    }
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //public JsonResult ChangePassword(User_Request_Filters_v4 request, string ReturnUrl = "")
        //{
        //    ProxySecurityAPI proxy = new ProxySecurityAPI();
        //    Transaction transaction = string.Empty.GetTransaction();
        //    InitializeLogger(transaction);
        //    using (log4net.NDC.Push(AuditUserDataHelpers.AuditUserData()))
        //    {
        //        logger.Print_InitMethod();
        //        try
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                logger.Print_Request(request);
        //                var token = Session[config.SessionToken];
        //                var userResponse = Task.Run(() => proxy.ChangePassword(config, token.ToString(), request)).Result;
        //                logger.Print_Response(userResponse);
        //                return Json(new { result = userResponse.Code, message = userResponse.Message, messageEN = userResponse.MessageEN }, JsonRequestBehavior.AllowGet);
        //            }
        //            else return Json(new { result = config.CodigoParametrosNoValido, message = config.MensajeParametrosNoValidoES, messageEN = config.MensajeParametrosNoValidoEN }, JsonRequestBehavior.AllowGet);
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Error(ex);
        //            return Json(new { result = config.CodigoErrorNoEspecificado, message = config.MensajeErrorNoEspecificadoES, messageEN = config.MensajeErrorNoEspecificadoEN }, JsonRequestBehavior.AllowGet);
        //        }
        //        finally
        //        {
        //            logger.Print_EndMethod();
        //            proxy = null;
        //        }
        //    }
        //}

        [AllowAnonymous]
        public ActionResult Logout()
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            using (log4net.NDC.Push(AuditUserDataHelpers.AuditUserData()))
            {
                logger.Print_InitMethod();
                try
                {
                    Common.EResponseBase<Common.HttpHelpers.SimpleEntity> response = LoginCoreActions.getInstance().Logout();
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
            return RedirectToAction("Login", "User");
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

    }
}