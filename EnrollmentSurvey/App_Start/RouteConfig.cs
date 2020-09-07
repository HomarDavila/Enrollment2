using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EnrollmentSurvey
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "ENCUESTA",
                url: "c{id}",
                defaults: new { controller = "Encuesta", action = "Index", id = "id", cultura = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Encuesta", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
