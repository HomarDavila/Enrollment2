using Common;
using Common.AspNet.Logging;
using Common.HttpHelpers;
using Common.Logging;
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
    public class SettingsController : Controller
    {
        private CustomConfigurationLib config;
        private ICustomLog logger;
        private ProxyCoreAPI proxyCoreAPI;

        public SettingsController()
        {
            config = new CustomConfigurationLib();
            logger = new CustomLog4Net();
            proxyCoreAPI = new ProxyCoreAPI();
        }

        //[MustBeAuthenticated(Opcion = "HomeSelfServices", ValidarAutorizacion = true)]
        public ActionResult MyAccount()
        {
            ViewBag.urlGov = config.url_Gov;
            ViewBag.urlVital = config.url_Vital;
            ViewBag.urlASES = config.url_ASES;
            ViewBag.UserFullName = string.Empty;
            ViewBag.MemberId = string.Empty;
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            if (user != null)
            {
                if (user.objeto != null)
                {
                    User_Response_v1 userT = user.objeto;
                    ViewBag.UserFullName = $"{VerifyNullString(userT.FirstName).ToUpper()} {VerifyNullString(userT.LastName1).ToUpper()} {VerifyNullString(userT.LastName2).ToUpper()}";
                    ViewBag.MemberId = userT.MemberId;
                    ViewBag.UserId = userT.Id;
                }
            }
            return View();
        }

        private void InitializeLogger(Transaction transaction)
        {
            logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, transaction);
        }

        private string VerifyNullString(string text)
        {
            if (text == null)
            {
                return "";
            }
            return text;
        }
    }
}