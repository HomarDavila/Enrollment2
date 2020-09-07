using Audit.WebApi;
using Common;
using Common.Generic.HttpHelpers;
using Common.HttpHelpers;
using Common.Logging;
using Renci.SshNet;
using Security.API.Helpers;
using Security.API.Model.Request;
using Service.DependecyInjection;
using Service.Interfaces;
using Sms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;

namespace Security.API.Controllers
{
    [AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
    [RoutePrefix("api/Mail/v1")]
    public class MailController : ApiController
    {
        private readonly IMailService mailServices = DependencyFactory.GetInstance<IMailService>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;

        public MailController()
        {
            mailServices = DependencyFactory.GetInstance<IMailService>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        [Route("SendConfirmationEmail")]
        [HttpPost]
        public EResponseBase<SimpleEntity> SendConfirmationEmail([FromBody] Mail_Request_v2 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    EResponseBase<SimpleEntity> response = null;
                    logger.Print_Request($"emailTo: {request.Email}, contactFullName: {request.NameTo}");
                    List<string> contentToReplace = new List<string>
                    {
                        $"contactFullName%{request.NameTo}"
                    };

                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(CustomConfigurationLib.TemplateSendConfirmationMail)))
                    {
                        string bodyHTML = reader.ReadToEnd();
                        //response = mailServices.SendMail(CustomConfigurationLib.EmailFromChangeMCOProcess, request.Email, null, null, CustomConfigurationLib.SubjectChangeMCOProcess, bodyHTML, lstattachFile, contentToReplace, null);
                        response = mailServices.SendMail(CustomConfigurationLib.EmailFromChangeMCOProcess, request.Email, null, null, CustomConfigurationLib.SubjectChangeMCOProcess, bodyHTML, null, contentToReplace, null);
                        response = new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForOK();
                        logger.Print_Response(response);

                    }
                    return response;

                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("SendResetPasswordEmail")]
        [HttpPost]
        public EResponseBase<SimpleEntity> SendResetPasswordEmail([FromBody] Mail_Request_v1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            string templateLanguage;
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    EResponseBase<SimpleEntity> response = null;
                    logger.Print_Request(request);
                    string url = CustomConfigurationLib.UrlSelfServices;
                    List<string> contentToReplace = new List<string>
                    {
                        $"UserFullName%{request.UserFullName}",
                        $"UserName%{request.UserName}",
                        $"NewPassword%{request.Password}",
                        $"urlSelfServices%{url}"
                    };
                    if (request.Language == "en")
                    {
                        templateLanguage = string.IsNullOrEmpty(request.Password) ? CustomConfigurationLib.TemplateChangePasswordMail : CustomConfigurationLib.TemplateResetPasswordMailEN;
                    }
                    else
                    {
                        templateLanguage = string.IsNullOrEmpty(request.Password) ? CustomConfigurationLib.TemplateChangePasswordMail : CustomConfigurationLib.TemplateResetPasswordMail;
                    }
                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(templateLanguage)))
                    {
                        string bodyHTML = reader.ReadToEnd();
                        response = mailServices.SendMail(CustomConfigurationLib.EmailFromChangeMCOProcess, request.EmailTo, null, null, request.Subject, bodyHTML, null, contentToReplace, null);
                        logger.Print_Response(response);
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("SendUserRegister")]
        [HttpPost]
        public EResponseBase<SimpleEntity> SendUserRegister([FromBody] Mail_Request_v2 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    EResponseBase<SimpleEntity> response = null;
                    logger.Print_Request($"emailTo: {request.Email}, contactFullName: {request.NameTo}");
                    List<string> contentToReplace = new List<string>
                    {
                        $"contactFullName%{request.NameTo}"
                    };

                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(CustomConfigurationLib.TemplateSendUserRegisterMail)))
                    {
                        string bodyHTML = reader.ReadToEnd();
                        response = mailServices.SendMail(CustomConfigurationLib.EmailFromChangeMCOProcess, request.Email, null, null, CustomConfigurationLib.SubjectUserRegister, bodyHTML, null, contentToReplace, null);
                        response = new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForOK();
                        logger.Print_Response(response);
                        return response;
                    }


                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("SendLinkQuitz")]
        [HttpPost]
        public EResponseBase<SimpleEntity> SendLinkQuitz([FromBody] Mail_Request_v2 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    EResponseBase<SimpleEntity> response = null;
                    logger.Print_Request($"emailTo: {request.Email}, contactFullName: {request.NameTo}");
                    string enlace = $"{AppSettings.Enlace}{request.EnrollmentHistoryID}";
                    List<string> contentToReplace = new List<string>
                    {
                        $"contactFullName%{request.NameTo}",
                        $"enlace%{enlace}"
                    };


                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(CustomConfigurationLib.TemplateSendLinkQuitzMail)))
                    {
                        string bodyHTML = reader.ReadToEnd();
                        response = mailServices.SendMail(CustomConfigurationLib.EmailFromChangeMCOProcess, request.Email, null, null, CustomConfigurationLib.SubjectSendSms, bodyHTML, null, contentToReplace, null);
                        response = new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForOK();
                        logger.Print_Response(response);
                        return response;
                    }


                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        private void ConfigureService()
        {
            mailServices.Transaction = RequestUtility.GetHeaders().Transaction;
            mailServices.Logger = logger;
        }

        private CustomHeader ConfigureLogHeader()
        {
            CustomHeader header = RequestUtility.GetHeaders();
            logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, header.Transaction);
            logger.Header = RequestHelpers.AuditUserData(header);
            return header;
        }


    }
}