using Common;
using Common.AspNet.Logging;
using Common.HttpHelpers;
using Common.Logging;
using EnrollmentSystemWebApp.Helpers.Http;
using EnrollmentSystemWebApp.Proxy;
using Newtonsoft.Json;
using Security.API.Model.Response;
using System;
using System.Threading.Tasks;
using System.Web;


namespace EnrollmentSystemWebApp.Helpers.Identity
{
    public class LoginCoreActions
    {
        private static LoginCoreActions obj;
        private CustomConfigurationLib config;
        private ICustomLog logger;

        public LoginCoreActions()
        {
            config = new CustomConfigurationLib();
            logger = new CustomLog4Net();
        }

        public static LoginCoreActions getInstance()
        {
            if (obj == null)
                obj = new LoginCoreActions();
            return obj;
        }

        //Core Methods
        public EResponseBase<TokenResponse> Login(string userName, string password, string clientID, Boolean CrearSession = true)
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            using (log4net.NDC.Push(AuditUserDataHelpers.AuditUserData()))
            {
                try
                {
                    EResponseBase<TokenResponse> tokenResponse = Task.Run(() => new ProxySecurityAPI().GetToken(config, userName, password, clientID)).Result;
                    if (tokenResponse.Code == config.CodigoExito && CrearSession)
                    {
                        HttpContext.Current.Session[config.SessionToken] = tokenResponse;
                        EResponseBase<User_Response_v1> user = Task.Run(() => new ProxySecurityAPI().GetUserByName(config, userName, tokenResponse.objeto.AccessToken)).Result;
                        HttpContext.Current.Session[config.SessionUser] = user;
                                              
                    }
                    return tokenResponse;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<TokenResponse>(config).setResponseBaseForException(ex);
                }
            }
        }
        public EResponseBase<SimpleEntity> Logout()
        {
            Transaction transaction = string.Empty.GetTransaction();
            InitializeLogger(transaction);
            using (log4net.NDC.Push(AuditUserDataHelpers.AuditUserData()))
            {
                try
                {
                    HttpContext.Current.Session[config.SessionToken] = null;
                    HttpContext.Current.Session[config.SessionUser] = null;
                    return new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForOK();
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForException(ex);
                }
            }
        }

        private void InitializeLogger(Transaction transaction)
        {
            logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, transaction);
        }
    }
}