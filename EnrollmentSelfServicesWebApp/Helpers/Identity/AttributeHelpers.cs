using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EnrrolmentSelfServicesWebApp.Helpers.Identity
{

    // Si no estamos logeado, regresamos al login
    public class RedirectIfNoAuthenticatedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!SessionHelper.ExistUserInSession())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = new CustomConfigurationLib().NotAuthorizedController,
                    action = new CustomConfigurationLib().NotAuthorizedAction
                }));
            }
        }
    }

    // Si estamos logeado ya no podemos acceder a la página de Login
    public class AlreadyAuthenticatedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (SessionHelper.ExistUserInSession())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = new CustomConfigurationLib().HomeController,
                    action = new CustomConfigurationLib().HomeAction
                }));
            }
        }
    }

    //Borrando cache
    public class NoCache : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnResultExecuting(filterContext);
        }
    }

    //Base para la authorizacion
    public class MustBeAuthenticatedAttribute : AuthorizeAttribute
    {

        public Boolean ValidarAutorizacion { get; set; }

        public string Opcion
        {
            get;
            set;
        }

        public MustBeAuthenticatedAttribute() : base()
        {

        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            CustomConfigurationLib config = new CustomConfigurationLib();
            if (config.ValidarAutorizacion)
            {
                List<string> atributos = filterContext.ActionDescriptor.GetCustomAttributes(true).Select(x => x.ToString().ToLower()).ToList();
                atributos.AddRange(filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(true).Select(x => x.ToString().ToLower()));
                if (!atributos.Contains("AllowAnonymous") && !atributos.Contains("AllowAnonymousAttribute"))
                {
                    base.OnAuthorization(filterContext);
                }
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            CustomConfigurationLib config = new CustomConfigurationLib();
            if (config.ValidarAutorizacion)
            {
                CustomAuthorize auth = new CustomAuthorize();
                return auth.IsAutenticado(httpContext, Opcion, config.ApplicationCode);
            }

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new
            {
                Controller = new CustomConfigurationLib().NotAuthorizedController,
                Action = new CustomConfigurationLib().NotAuthorizedAction
            }));
        }


    }
}