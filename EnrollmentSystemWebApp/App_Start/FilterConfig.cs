using Service.Config;
using System.Web;
using System.Web.Mvc;

namespace EnrollmentSystemWebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new CustomAutenticacionUsuarioAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
