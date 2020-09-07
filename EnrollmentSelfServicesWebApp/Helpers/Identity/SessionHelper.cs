using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnrrolmentSelfServicesWebApp.Helpers.Identity
{
    public class SessionHelper
    {
        public static bool ExistUserInSession()
        {
            CustomConfigurationLib config = new CustomConfigurationLib();
            bool exist = false;
            if (HttpContext.Current.Session[config.SessionToken] != null) exist = true;
            return exist;
        }
    }
}