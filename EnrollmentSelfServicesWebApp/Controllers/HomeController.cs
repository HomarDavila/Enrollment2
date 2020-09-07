using Common;
using Common.AspNet.Logging;
using Common.HttpHelpers;
using Common.Logging;
using Core.API.Model.Response;
using Domain.Custom_Models;
using EnrrolmentSelfServicesWebApp.Helpers;
using EnrrolmentSelfServicesWebApp.Helpers.Http;
using EnrrolmentSelfServicesWebApp.Helpers.Identity;
using EnrrolmentSelfServicesWebApp.Proxy;
using Security.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnrollmentSelfServicesWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private CustomConfigurationLib config;
        private ProxyCoreAPI proxyCoreAPI;
        private ICustomLog logger;

        public HomeController()
        {
            config = new CustomConfigurationLib();
            logger = new CustomLog4Net();
            proxyCoreAPI = new ProxyCoreAPI();
        }

        //[MustBeAuthenticated(Opcion = "HomeSelfServices", ValidarAutorizacion = true)]
        public ActionResult Index()
        {

            ViewBag.urlGov = config.url_Gov;
            ViewBag.urlVital = config.url_Vital;
            ViewBag.urlASES = config.url_ASES;
            ViewBag.UserFullName = string.Empty;
            ViewBag.MemberId = string.Empty;
            ViewBag.Name = string.Empty;

            ReportsController ctrl = new ReportsController();
            _ = ctrl.InsertStatistic(1);
            if (Session[config.SessionUser] != null && (bool)(Session[config.ChangePersonMcoEnabled] ?? false))
            {
                EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
                if (user != null)
                {
                    if (user.objeto != null)
                    {
                        User_Response_v1 userT = user.objeto;
                        ViewBag.UserFullName = $"{userT.FirstName} {userT.LastName1} {userT.LastName2}";
                        ViewBag.MemberId = userT.MemberId;
                        ViewBag.Name = userT.FirstName;
                    }
                }
                else
                {
                    return RedirectToAction("Logout", "User");
                }
            }
            else
            {
                return RedirectToAction("Logout", "User");
            }


            return View();
        }

        public async Task<ActionResult> Welcome()
        {
            ViewBag.urlGov = config.url_Gov;
            ViewBag.urlVital = config.url_Vital;
            ViewBag.urlASES = config.url_ASES;
            ViewBag.UserFullName = string.Empty;
            ViewBag.MemberId = string.Empty;
            if (Session[config.SessionUser] != null)
            {
                EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
                if (user != null)
                {
                    if (user.objeto != null)
                    {
                        User_Response_v1 userT = user.objeto;
                        ViewBag.UserFullName = $"{userT.FirstName} {userT.LastName1} {userT.LastName2}";
                        ViewBag.MemberId = userT.MemberId;

                        // se cae al invocar el metodo, por eso lo hago asi 
                        Transaction transaction = string.Empty.GetTransaction();
                        InitializeLogger(transaction);
                        EResponseBase<EnrollmentResponseV2> response = await proxyCoreAPI.ChangePersonMcoEnabled(transaction, logger, config, null, userT.MemberId);
                        EnrollmentResponseV2 record = new EnrollmentResponseV2();
                        record = response.objeto;
                        Session[config.ChangePersonMcoEnabled] = record.Enabled;
                        ViewBag.ChangePersonMcoEnabled = Session[config.ChangePersonMcoEnabled];
                    }
                    return View(user.objeto);
                }
                else
                {
                    return RedirectToAction("Logout", "User");
                }
            }
            else
            {
                return RedirectToAction("Logout", "User");
            }
        }

        public ActionResult Tools()
        {
            ViewBag.urlGov = config.url_Gov;
            ViewBag.urlVital = config.url_Vital;
            ViewBag.urlASES = config.url_ASES;
            return View();
        }

        [AllowAnonymous]
        public ActionResult IndexNA()
        {
            ViewBag.urlGov = config.url_Gov;
            ViewBag.urlVital = config.url_Vital;
            ViewBag.urlASES = config.url_ASES;
            ViewBag.UserFullName = string.Empty;
            ViewBag.MemberId = string.Empty;
            ViewBag.IsNA = true;
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            if (user != null)
            {
                if (user.objeto != null)
                {
                    User_Response_v1 userT = user.objeto;
                    ViewBag.UserFullName = $"{userT.FirstName} {userT.LastName1} {userT.LastName2}";
                    ViewBag.MemberId = userT.MemberId;
                }
            }
            ReportsController ctrl = new ReportsController();
            _ = ctrl.InsertStatistic(1);
            return View();
        }

        private void InitializeLogger(Transaction transaction)
        {
            logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, transaction);
        }
    }
}