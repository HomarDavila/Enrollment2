using Common;
using System.Web;

namespace EnrrolmentSelfServicesWebApp.Helpers.Http
{
    public class AuditUserDataHelpers
    {
        public static string AuditUserData()
        {
            string userData = string.Empty;
            try
            {
                userData = "Origen: [" + AppConstants.Web + "] Browser: [" + HttpContext.Current.Request.Browser.Type + "]";
            }
            catch
            {
                userData = "Origen: [" + AppConstants.Web + "] Browser: [" + AppConstants.Unknow + "]";
            }
            return userData;
        }
    }

}
