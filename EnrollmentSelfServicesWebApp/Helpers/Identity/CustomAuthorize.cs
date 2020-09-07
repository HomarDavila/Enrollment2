using Common;
using Common.AspNet.Logging;
using Common.HttpHelpers;
using Common.Logging;
using EnrrolmentSelfServicesWebApp.Helpers.Http;
using EnrrolmentSelfServicesWebApp.Proxy;
using Security.API.Model.Response;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EnrrolmentSelfServicesWebApp.Helpers.Identity
{
    public class CustomAuthorize
    {
        private ICustomLog logger;
        private CustomConfigurationLib config;
        private ProxySecurityAPI proxySecurityAPI;

        public CustomAuthorize()
        {
            config = new CustomConfigurationLib();
            proxySecurityAPI = new ProxySecurityAPI();
            logger = new CustomLog4Net();
        }

        private static CustomAuthorize obj;

        public static CustomAuthorize getInstance()
        {
            if (obj == null)
                obj = new CustomAuthorize();
            return obj;
        }

        public bool IsAutenticado(System.Web.HttpContextBase httpContext, string Opcion, string Application)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            using (log4net.NDC.Push(AuditUserDataHelpers.AuditUserData()))
            {
                try
                {
                    EResponseBase<TokenResponse> token = (EResponseBase<TokenResponse>)httpContext.Session[config.SessionToken];
                    EResponseBase<User_Response_v1> userName = (EResponseBase<User_Response_v1>)HttpContext.Current.Session[config.SessionUser];

                    //El token es nulo
                    if (token == null || userName == null) { return InvalidatedSession(config); }
                    if (userName.objeto == null) { return InvalidatedSession(config); }

                    //No se consiguio el token
                    if (token.Code != config.CodigoExito) { logger.Info(String.Format("Authorize[{0},{1}] - Token must be not null", Opcion, userName)); return InvalidatedSession(config); }

                    //Validando usuario
                    EResponseBase<User_Response_v1> user = Task.Run(() => proxySecurityAPI.GetUserByName(config, userName.objeto.UserName, token.objeto.AccessToken)).Result;
                    if (user.Code != config.CodigoExito) { logger.Info(String.Format("Authorize[{0},{1}] - User not found", Opcion, userName)); return InvalidatedSession(config); }
                    if (user.objeto == null) { logger.Info(String.Format("Authorize[{0},{1}] - User not found", Opcion, userName)); return InvalidatedSession(config); }

                    //Validar si existe la opcion
                    EResponseBase<OptionRol_Response_v2> options = Task.Run(() => proxySecurityAPI.GetOptionsByUserId(config, user.objeto.Id, token.objeto.AccessToken)).Result;
                    if (options.Code != config.CodigoExito) { logger.Info(String.Format("Authorize[{0},{1}] - User doesnt have options assigned", Opcion, userName)); return InvalidatedSession(config); }

                    //Validar si tiene permiso para la opcion deseada   
                    bool optionFounds = options.listado.Where(x => x.Option.Code == Opcion && x.Application.Code == Application).Any();
                    if (!optionFounds) { logger.Info(String.Format("Authorize[{0},{1}] - User doesnt have the option ({2}) assigned", Opcion, userName, Opcion)); return InvalidatedSession(config); }

                    return true;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return InvalidatedSession(config);
                }
            }
        }

        private static bool InvalidatedSession(CustomConfigurationLib config)
        {
            HttpContext.Current.Session[config.SessionToken] = null;
            HttpContext.Current.Session[config.SessionUser] = null;
            return false;
        }
        private void InitializeLogger(Transaction transaction)
        {
            logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, transaction);
        }
    }
}
