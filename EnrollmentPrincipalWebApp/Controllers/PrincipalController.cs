using Common;
using Common.AspNet.Logging;
using Common.HttpHelpers;
using Common.Logging;
using EnrollmentPrincipalWebApp.Helpers;
using EnrollmentPrincipalWebApp.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc; 
using Common.Logging;
using Common;
using Common.HttpHelpers;
using EnrollmentPrincipalWebApp.Helpers;
using Common.AspNet.Logging;
using System;
using EnrollmentPrincipalWebApp.Proxy;
using System.Configuration;

namespace EnrollmentPrincipalWebApp.Controllers
{

    public class PrincipalController : Controller
    {
        CustomConfigurationLib config;
        // Pages in Spanish 
        //Test Homar Dávila
        //Test Homar Dávila 2
        //Test Homar Dávila 3
        public PrincipalController()
        {
            config = new CustomConfigurationLib();
        }

        public ActionResult Index()
        {
            ReportsController ctrl = new ReportsController();
            _ = ctrl.InsertStatistic(2);
            ViewBag.SelfServices = config.SelftService_SelftServices;
            ViewBag.Logos = true;
            ViewBag.BusquedaProveedores = config.SelftService_BusquedaProveedores;
            ViewBag.UrlCensos = config.UrlCensos;
            ViewBag.ModalEnabled = config.ModalEnabled;
            ViewBag.ModalTitle = config.ModalTitleES;
            ViewBag.ModalMsg = config.ModalMsgES;
            ViewBag.ModalMsg2 = config.ModalMsg2ES;
            return View();
        }

        public  ActionResult IndexNA()
        {
            ReportsController ctrl = new ReportsController();
            _ = ctrl.InsertStatistic(2);
            ViewBag.SelfServices = config.SelftService_SelftServices;
            ViewBag.Logos = true;
            ViewBag.BusquedaProveedores = config.SelftService_BusquedaProveedores;
            ViewBag.UrlCensos = config.UrlCensos;
            ViewBag.ModalEnabled = config.ModalEnabled;
            ViewBag.ModalTitle = config.ModalTitleEN;
            ViewBag.ModalMsg = config.ModalMsgEN;
            ViewBag.ModalMsg2 = config.ModalMsg2EN;
            return View();
        }

        public ActionResult PanoramaGeneral()
        {
            ViewBag.SelfServices = config.SelftService_SelftServices;
            return View();
        }

        public ActionResult PuntosDestacadosDetalles()
        {
            ViewBag.SelfServices = config.SelftService_SelftServices;
            return View();
        }

        public ActionResult BusquedaProveedores()
        {
            ViewBag.BusquedaProveedores = config.SelftService_BusquedaProveedores;
            ViewBag.SelfServices = config.SelftService_SelftServices;
            //"http://52.20.56.7/EnrollmentSelfServices/Home/IndexNA/";
            return View();
        }

        public ActionResult UbicacionOficinas()
        {
            ViewBag.SelfServices = config.SelftService_SelftServices;
            return View();
        }

        public ActionResult CambiarMco()
        {
            ViewBag.SelfServices = config.SelftService_SelftServices;

            return View();
        }

        public ActionResult HacerCita()
        {
            ViewBag.SelfServices = config.SelftService_SelftServices;
            ViewBag.HacerCita = config.Url_HacerCitas;
            //ViewBag.HacerCita = WebConfigurationManager.AppSettings["HacerCita"].ToString();
            //"http://3.226.62.144/VitalQ.citas";
            return View();
        }
        public ActionResult ContactaAseguradora()
        {
            ViewBag.SelfServices = config.SelftService_SelftServices;
            return View();
        }

        // Pages in English 

        public ActionResult English()
        {
            ViewBag.SelfServices = config.SelftService_SelftServices;
            return View();
        }

        public ActionResult Overview()
        {
            ViewBag.SelfServices = config.SelftService_SelftServices;
            return View();
        }

        public ActionResult DetailHighlights()
        {
            ViewBag.SelfServices = config.SelftService_SelftServices;
            return View();
        }

        public ActionResult ProviderSearch()
        {
            ViewBag.BusquedaProveedores = config.SelftService_BusquedaProveedores;
            ViewBag.SelfServices = config.SelftService_SelftServices;
            return View();
        }

        public ActionResult OfficeLocations()
        {
            ViewBag.SelfServices = config.SelftService_SelftServices;
            return View();
        }

        public ActionResult ChangeMco()
        {
            ViewBag.SelfServices = config.SelftService_SelftServices;
            return View();
        }

        public ActionResult MakeAppointment()
        {
            ViewBag.SelfServices = config.SelftService_SelftServices;
            ViewBag.HacerCita = config.Url_HacerCitas;
            return View();
        }
        public ActionResult ContactInsurer()
        {
            ViewBag.SelfServices = config.SelftService_SelftServices;
            return View();
        }

    }

}
