using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnrollmentPrincipalWebApp.Helpers
{
    public class RequestHelpers
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