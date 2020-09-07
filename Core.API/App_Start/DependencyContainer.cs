using Autofac;
using Autofac.Integration.WebApi;
using Core.API.Controllers;
using System.Net;
using System.Reflection;
using System.Web.Http;

namespace Core.API.App_Start
{
    public class DependencyContainer
    {
        public static IContainer BuildContainer(HttpConfiguration configuration)
        {
            ContainerBuilder builder = new ContainerBuilder();

            //Register all services.
            builder.RegisterApiControllers(typeof(NotFoundController).GetTypeInfo().Assembly);
            builder.RegisterWebApiFilterProvider(configuration);

            IContainer container = builder.Build();
            return container;
        }
    }
}