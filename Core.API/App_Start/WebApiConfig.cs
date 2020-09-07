using Autofac.Integration.WebApi;
using Core.API.App_Start;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Core.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.DependencyResolver = new AutofacWebApiDependencyResolver(DependencyContainer.BuildContainer(config));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                   name: "Index",
                   routeTemplate: "",
                   defaults: new { controller = "Swagger", action = "Index" }
               );

            config.Routes.MapHttpRoute(
                 name: "Error404",
                 routeTemplate: "{*url}",
                 defaults: new { controller = "NotFound", action = "ErrorNotFound" }
              );

            JsonMediaTypeFormatter jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
