using Audit.Mvc;
using Common;
using Common.Logging;
using EnrollmentSystemWebApp.Helpers;
using Security.API.Model.Response;
using Service.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnrollmentSystemWebApp.Controllers
{
    [Authorize]    
    public class HomeController : Controller
    {
        private CustomConfigurationLib config;
        public HomeController()
        {
            config = new CustomConfigurationLib();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            ViewBag.Usuario = user.objeto.FirstName;
            ViewBag.listOptions = user.objeto.listOptions;
            ViewBag.BusquedaProveedores = config.SelftService_BusquedaProveedores;
            return View();
        }

        public ActionResult AdditionalResource()
        {
            EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)Session[config.SessionUser];
            ViewBag.Usuario = user.objeto.FirstName;
            ViewBag.listOptions = user.objeto.listOptions;
            return View();
        }


    }
}
