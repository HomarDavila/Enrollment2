using Autofac.Integration.WebApi;
using LightInject;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Security.API.App_Start;
using Service.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;

namespace Security.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            SwaggerConfig.Register();
            // Web API configuration and services
            config.DependencyResolver = new AutofacWebApiDependencyResolver(DependencyContainer.BuildContainer(config));

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

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
