using Common;
using Service.Helpers;
using System.Web;
using System.Web.Mvc;

namespace Service.Config
{
    public class CustomAutenticacionUsuarioAttribute : SistemaSeguridad.AuthLib.AutenticacionUsuarioAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (CustomConfigurationLib.ValidarAutorizacion)
            {
                return base.AuthorizeCore(httpContext);
            }

            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CustomConfigurationLib.ValidarAutorizacion)
            {
                base.OnAuthorization(filterContext);
            }
        }
    }
}
