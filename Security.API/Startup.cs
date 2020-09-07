using AutoMapper;
using Common;
using Domain.Custom_Models;
using Domain.Entity_Models;
using Domain.Entity_Models.Identity;
using log4net;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using Security.API;
using Security.API.App_Start;
using Security.API.Helpers;
using Security.API.Model.Request;
using Security.API.Model.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]
namespace Security.API
{
    public partial class Startup
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Startup));
        public void Configuration(IAppBuilder app)
        {

            GlobalConfiguration.Configure(WebApiConfig.Register);
            app.UseCors(CorsOptions.AllowAll);
            //app.UseWebApi(GlobalConfiguration.Configuration);
            log4net.Config.XmlConfigurator.Configure();
            ConfigureAuth(app);
            ConfigureClient(app);
            Mapper.Initialize(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.CreateMap(typeof(EResponseBase<>), typeof(EResponseBase<>));
                cfg.CreateMap<Application_Request_v1, Application>();
                cfg.CreateMap<Option_Request_v1, Option>();
                cfg.CreateMap<OptionRol_Request_v1, OptionRol>();
                cfg.CreateMap<Rol_Request_v1, Role>();
                cfg.CreateMap<User_Request_v1, User>();
                cfg.CreateMap<UserRol_Request_v1, UserRol>();
                cfg.CreateMap<Application, Application_Response_v1>();
                cfg.CreateMap<Option, Option_Response_v1>();
                cfg.CreateMap<OptionRol, OptionRol_Response_v1>();
                cfg.CreateMap<Role, Rol_Response_v1>();
                cfg.CreateMap<User, User_Response_v1>();
                cfg.CreateMap<User, User_Response_v2>();
                cfg.CreateMap<UserRol, UserRol_Response_v1>();
                cfg.CreateMap<Audience, Audience_Response_v1>();
                cfg.CreateMap<SecurityAnswer_Request_v1, SecurityAnswer>();
                cfg.CreateMap<SecurityAnswer, SecurityQuestion_Response_v1>();
            });

        }
    }
}
