using Autofac;
using Autofac.Integration.WebApi;
using Common.Logging;
using LightInject;
using Security.API.Controllers;
using Service.Config;
using System.Net;
using System.Reflection;
using System.Web.Http;

namespace Security.API.App_Start
{
    public class DependencyContainer
    {
        public static IContainer BuildContainer(HttpConfiguration configuration)
        {
            ContainerBuilder builder = new ContainerBuilder();

            //Register all services.
            builder.RegisterApiControllers(typeof(MailController).GetTypeInfo().Assembly);
            builder.RegisterApiControllers(typeof(SecurityQuestionController).GetTypeInfo().Assembly);
            builder.RegisterApiControllers(typeof(OptionController).GetTypeInfo().Assembly);
            builder.RegisterApiControllers(typeof(OptionRolController).GetTypeInfo().Assembly);
            builder.RegisterApiControllers(typeof(RolController).GetTypeInfo().Assembly);
            builder.RegisterApiControllers(typeof(UserController).GetTypeInfo().Assembly);
            builder.RegisterApiControllers(typeof(UserRolController).GetTypeInfo().Assembly);
            builder.RegisterWebApiFilterProvider(configuration);
            IContainer container = builder.Build();
            return container;
        }
    }
}