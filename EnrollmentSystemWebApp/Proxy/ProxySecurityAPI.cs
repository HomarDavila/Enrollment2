using Common;
using Common.HttpHelpers;
using Common.Logging;
using Common.Proxy;
using Core.API.Model;
using EnrollmentSystemWebApp.Helpers;
using Newtonsoft.Json;
using Security.API.Model.Request;
using Security.API.Model.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnrollmentSystemWebApp.Proxy
{
    public class ProxySecurityAPI
    {

        public async Task<EResponseBase<TokenResponse>> GetToken(CustomConfigurationLib config, string userName, string password, string clientID)
        {
            using (CustomProxyREST<TokenResponse> service = new CustomProxyREST<TokenResponse>(config))
            {
                string transactionId = string.Empty;
                string dataRequest = JsonConvert.SerializeObject(userName);
                List<KeyValuePair<string, string>> securityData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", config.Grant_type),
                    new KeyValuePair<string, string>("username", userName),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("client_Id", clientID)
                };
                EResponseBase<TokenResponse> response = await service.GetToken(config.SecurityAPI_URLBase,
                                                      config.SecurityAPI_TokenPrefix,
                                                      transactionId.GetTransaction().Id,
                                                      AppConstants.Web,
                                                      config.SecurityAPI_IgnoreSSL,
                                                      config.SecurityAPI_Timeout,
                                                      securityData);
                string dataResponse = JsonConvert.SerializeObject(response);
                return response;
            };
        }

        public async Task<EResponseBase<User_Response_v1>> GetUserByName(CustomConfigurationLib config, string userName, string token)
        {
            using (CustomProxyREST<User_Request_Filters_v10, User_Response_v1> service = new CustomProxyREST<User_Request_Filters_v10, User_Response_v1>(config))
            {
                string transaction = string.Empty;
                string dataRequest = JsonConvert.SerializeObject(null);
                EResponseBase<User_Response_v1> response = await service.Post(config.SecurityAPI_URLBase,
                                                 config.SecurityApi_ServicePreffix,
                                                 config.SecurityAPI_UserController,
                                                 config.SecurityAPI_User_GetByNameAction,
                                                 config.TokenType,
                                                 token,
                                                 transaction.GetTransaction().Id,
                                                 AppConstants.Web,
                                                 config.SecurityAPI_IgnoreSSL,
                                                 config.SecurityAPI_Timeout,
                                                 new User_Request_Filters_v10() { UserName = userName });
                string dataResponse = JsonConvert.SerializeObject(response);
                return response;
            };
        }

        public async Task<EResponseBase<OptionRol_Response_v2>> GetOptionsByUserId(CustomConfigurationLib config, int userId, string token)
        {
            using (CustomProxyREST<User_Request_Filters_v9, OptionRol_Response_v2> service = new CustomProxyREST<User_Request_Filters_v9, OptionRol_Response_v2>(config))
            {
                string transaction = string.Empty;
                string dataRequest = JsonConvert.SerializeObject(null);
                EResponseBase<OptionRol_Response_v2> response = await service.Post(config.SecurityAPI_URLBase,
                                                 config.SecurityAPI_ServicePrefix,
                                                 config.SecurityAPI_IdentityController,
                                                 config.SecurityAPI_Identity_GetOptionsByUserIdAction,
                                                 config.TokenType,
                                                 token,
                                                 transaction.GetTransaction().Id,
                                                 AppConstants.Web,
                                                 config.SecurityAPI_IgnoreSSL,
                                                 config.SecurityAPI_Timeout,
                                                 new User_Request_Filters_v9() { UserId = userId, ApplicationCode = config.ApplicationCode });
                string dataResponse = JsonConvert.SerializeObject(response);
                return response;
            };
        }

        public async Task<EResponseBase<SecurityQuestion_Response_v1>> GetSecurityQuestions(CustomConfigurationLib config)
        {
            using (CustomProxyREST<SecurityQuestion_Response_v1> service = new CustomProxyREST<SecurityQuestion_Response_v1>(config))
            {
                string transaction = string.Empty;
                string dataRequest = JsonConvert.SerializeObject(null);
                EResponseBase<SecurityQuestion_Response_v1> response = await service.Get(config.SecurityAPI_URLBase,
                                                 config.SecurityAPI_ServicePrefix,
                                                 config.SecurityAPI_SecurityQuestionController,
                                                 config.SecurityAPI_SecurityQuestion_GetAction,
                                                 null,
                                                 null,
                                                 transaction.GetTransaction().Id,
                                                 AppConstants.Web,
                                                 config.SecurityAPI_IgnoreSSL,
                                                 config.SecurityAPI_Timeout);
                string dataResponse = JsonConvert.SerializeObject(response);
                return response;
            };
        }

        public async Task<EResponseBase<SimpleEntity>> Register(CustomConfigurationLib config, User_Request_v1 request)
        {
            using (CustomProxyREST<User_Request_v1, SimpleEntity> service = new CustomProxyREST<User_Request_v1, SimpleEntity>(config))
            {
                string transaction = string.Empty;
                string dataRequest = JsonConvert.SerializeObject(null);
                EResponseBase<SimpleEntity> response = await service.Post(config.SecurityAPI_URLBase,
                                                 config.SecurityAPI_ServicePrefix,
                                                 config.SecurityAPI_IdentityController,
                                                 config.SecurityAPI_Identity_RegisterAction,
                                                 null,
                                                 null,
                                                 transaction.GetTransaction().Id,
                                                 AppConstants.Web,
                                                 config.SecurityAPI_IgnoreSSL,
                                                 config.SecurityAPI_Timeout,
                                                 request);
                string dataResponse = JsonConvert.SerializeObject(response);
                return response;
            };
        }

        public async Task<EResponseBase<User_Response_v2>> ResetPassword(CustomConfigurationLib config, User_Request_Filters_v8 request)
        {
            using (CustomProxyREST<User_Request_Filters_v8, User_Response_v2> service = new CustomProxyREST<User_Request_Filters_v8, User_Response_v2>(config))
            {
                string transaction = string.Empty;
                string dataRequest = JsonConvert.SerializeObject(null);
                EResponseBase<User_Response_v2> response = await service.Post(config.SecurityAPI_URLBase,
                                                 config.SecurityAPI_ServicePrefix,
                                                 config.SecurityAPI_IdentityController,
                                                 config.SecurityAPI_Identity_ResetPasswordAction,
                                                 null,
                                                 null,
                                                 transaction.GetTransaction().Id,
                                                 AppConstants.Web,
                                                 config.SecurityAPI_IgnoreSSL,
                                                 config.SecurityAPI_Timeout,
                                                 request);
                string dataResponse = JsonConvert.SerializeObject(response);
                return response;
            };
        }

        public async Task<EResponseBase<User_Response_v1>> ChangePassword(CustomConfigurationLib config, string token, User_Request_Filters_v4 request)
        {
            using (CustomProxyREST<User_Request_Filters_v4, User_Response_v1> service = new CustomProxyREST<User_Request_Filters_v4, User_Response_v1>(config))
            {
                string transaction = string.Empty;
                string dataRequest = JsonConvert.SerializeObject(null);
                EResponseBase<User_Response_v1> response = await service.Post(config.SecurityAPI_URLBase,
                                                 config.SecurityAPI_ServicePrefix,
                                                 config.SecurityAPI_IdentityController,
                                                 config.SecurityAPI_Identity_ChangePasswordAction,
                                                 config.TokenType,
                                                 token,
                                                 transaction.GetTransaction().Id,
                                                 AppConstants.Web,
                                                 config.SecurityAPI_IgnoreSSL,
                                                 config.SecurityAPI_Timeout,
                                                 request);
                string dataResponse = JsonConvert.SerializeObject(response);
                return response;
            };
        }

        public async Task<EResponseBase<UserRol_Response_v1>> RegisterUserRol(CustomConfigurationLib config, UserRol_Request_v1 request)
        {
            using (CustomProxyREST<UserRol_Request_v1, UserRol_Response_v1> service = new CustomProxyREST<UserRol_Request_v1, UserRol_Response_v1>(config))
            {
                string transaction = string.Empty;
                string dataRequest = JsonConvert.SerializeObject(null);
                EResponseBase<UserRol_Response_v1> response = await service.Post(config.SecurityAPI_URLBase,
                                                 config.SecurityAPI_ServicePrefix,
                                                 config.SecurityAPI_UserRolController,
                                                 config.SecurityAPI_UserRol_RegisterOrUpdateAction,
                                                 null,
                                                 null,
                                                 transaction.GetTransaction().Id,
                                                 AppConstants.Web,
                                                 config.SecurityAPI_IgnoreSSL,
                                                 config.SecurityAPI_Timeout,
                                                 request);
                string dataResponse = JsonConvert.SerializeObject(response);
                return response;
            };
        }

        public async Task<EResponseBase<SimpleEntity>> SendResetPasswordEmail(CustomConfigurationLib config, Mail_Request_v1 request)
        {
            using (CustomProxyREST<Mail_Request_v1, SimpleEntity> service = new CustomProxyREST<Mail_Request_v1, SimpleEntity>(config))
            {
                string transaction = string.Empty;
                string dataRequest = JsonConvert.SerializeObject(null);
                EResponseBase<SimpleEntity> response = await service.Post(config.SecurityAPI_URLBase,
                                                 config.SecurityAPI_ServicePrefix,
                                                 config.SecurityAPI_MailController,
                                                 config.SecurityAPI_Mail_SendResetPasswordEmailAction,
                                                 null,
                                                 null,
                                                 transaction.GetTransaction().Id,
                                                 AppConstants.Web,
                                                 config.SecurityAPI_IgnoreSSL,
                                                 config.SecurityAPI_Timeout,
                                                 request);
                string dataResponse = JsonConvert.SerializeObject(response);
                return response;
            };
        }

        public async Task<EResponseBase<SimpleEntity>> SendConfirmationEmail(CustomConfigurationLib config, EmailRequestV1 request)
        {
            using (CustomProxyREST<EmailRequestV1, SimpleEntity> service = new CustomProxyREST<EmailRequestV1, SimpleEntity>(config))
            {
                string transaction = string.Empty;
                string dataRequest = JsonConvert.SerializeObject(null);
                EResponseBase<SimpleEntity> response = await service.Post(config.SecurityAPI_URLBase,
                                                 config.SecurityAPI_ServicePrefix,
                                                 config.SecurityAPI_MailController,
                                                 config.SecurityAPI_Mail_SendConfirmationEmail,
                                                 null,
                                                 null,
                                                 transaction.GetTransaction().Id,
                                                 AppConstants.Web,
                                                 config.SecurityAPI_IgnoreSSL,
                                                 config.SecurityAPI_Timeout,
                                                 request);
                string dataResponse = JsonConvert.SerializeObject(response);
                return response;
            };
        }

        public async Task<EResponseBase<SimpleEntity>> SendLinkQuitz(CustomConfigurationLib config, EmailRequestV1 request)
        {
            using (CustomProxyREST<EmailRequestV1, SimpleEntity> service = new CustomProxyREST<EmailRequestV1, SimpleEntity>(config))
            {
                string transaction = string.Empty;
                string dataRequest = JsonConvert.SerializeObject(null);
                EResponseBase<SimpleEntity> response = await service.Post(config.SecurityAPI_URLBase,
                                                 config.SecurityAPI_ServicePrefix,
                                                 config.SecurityAPI_MailController,
                                                 config.SecurityAPI_Mail_SendLinkQuitz,
                                                 null,
                                                 null,
                                                 transaction.GetTransaction().Id,
                                                 AppConstants.Web,
                                                 config.SecurityAPI_IgnoreSSL,
                                                 config.SecurityAPI_Timeout,
                                                 request);
                string dataResponse = JsonConvert.SerializeObject(response);
                return response;
            };
        }

        //public async Task<EResponseBase<SimpleEntity>> SendConfirmationEmail(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, EmailRequestV1 request)
        //{
        //    using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
        //    {
        //        using (CustomProxyREST<SimpleEntity> service = new CustomProxyREST<SimpleEntity>(config))
        //        {

        //            logger.Print_InitMethod();
        //            string dataRequest;
        //            dataRequest = request.Email + "/" + request.NameTo + "/" + request.NameFile;

        //            logger.Info(string.Format("URL: {0} {1}",
        //                ProxyBase.GetURL(config.SecurityAPI_URLBase,
        //                config.SecurityApi_ServicePreffix,
        //                config.SecurityAPI_MailController,
        //                config.SecurityAPI_Mail_SendConfirmationEmail,
        //                dataRequest),
        //                "(GET)"));

        //            logger.Print_Request(dataRequest);
        //            EResponseBase<SimpleEntity> response = await service.Get(
        //                config.SecurityAPI_URLBase,
        //                config.SecurityApi_ServicePreffix,
        //                config.SecurityAPI_MailController,
        //                config.SecurityAPI_Mail_SendConfirmationEmail,
        //                null,
        //                null,
        //                transaction.Id,
        //                AppConstants.Web,
        //                config.SecurityAPI_IgnoreSSL,
        //                config.SecurityAPI_Timeout,
        //                dataRequest);
        //            logger.Print_Response(response);
        //            logger.Print_EndMethod();
        //            return response;
        //        };
        //    }
        //}
    }
}