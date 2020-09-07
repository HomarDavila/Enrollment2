using Audit.Core;
using EnrollmentSystemWebApp.Helpers;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EnrollmentSystemWebApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
            //UpdateData();
            //Audit.Core.Configuration.Setup()
            // .UseSqlServer(config => config.ConnectionString(new CustomConfigurationLib().ConnectionString)
            // .Schema("dbo")
            //     .TableName("Event")
            //     .IdColumnName("EventId")
            //     .JsonColumnName("Data")
            //     .LastUpdatedColumnName("LastUpdatedDate"));
        }
    }
}
