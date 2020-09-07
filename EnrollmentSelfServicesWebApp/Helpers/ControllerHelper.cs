using Common;
using Common.HttpHelpers;
using EnrrolmentSelfServicesWebApp.Helpers;
using EnrrolmentSelfServicesWebApp.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EnrollmentSelfServicesWebApp.Helpers
{
    public static class ControllerHelper
    {
        public static async Task<bool> ValidateUserIdRequest(this Controller controller, int id)
        {
            var config = new CustomConfigurationLib();
            ProxySecurityAPI proxy = new ProxySecurityAPI();
            EResponseBase<TokenResponse> token = (EResponseBase<TokenResponse>)controller.Session[config.SessionToken];
            var user = await proxy.GetUserByName(config, token.objeto.UserName, token.objeto.AccessToken);
            return user.objeto.Id == id;
        }
    }
}