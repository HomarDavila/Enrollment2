
using Common;
using System;

namespace Security.API.Helpers
{
    public class CustomConfigurationLib : ConfigurationLib
    {


        public static string ConnectionString => System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public static string AudienceId => System.Configuration.ConfigurationManager.AppSettings["AudienceId"];
        public static string AudienceSecret => System.Configuration.ConfigurationManager.AppSettings["AudienceSecret"];
        public static string Issuer => System.Configuration.ConfigurationManager.AppSettings["Issuer"];
        public static bool DisableSwagger => Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings["DisableSwagger"]);

        public static string EmailFromChangeMCOProcess => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EmailFromChangeMCOProcess"]);        
        public static string SubjectChangeMCOProcess => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SubjectChangeMCOProcess"]);
        public static string SubjectUserRegister => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SubjectUserRegister"]);
        public static string SubjectSendSms => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SubjectSendSms"]);

        public static string TemplateSendConfirmationMail => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TemplateSendConfirmationMail"]);
        public static string TemplateSendUserRegisterMail => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TemplateSendUserRegisterMail"]);
        public static string TemplateSendLinkQuitzMail => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TemplateSendLinkQuitzMail"]);

        public static string TemplateResetPasswordMail => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TemplateResetPasswordMail"]);
        public static string TemplateResetPasswordMailEN => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TemplateResetPasswordMailEN"]);
        public static string TemplateChangePasswordMail => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TemplateChangePasswordMail"]);
        public static string UrlSelfServices => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["UrlSelfServices"]);


        public static string PathEnrollmentCreatePDF => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["PathEnrollmentCreatePDF"]);
        public static string FTPServer => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["FTPServer"]);
        public static string FTPUser => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["FTPUser"]);
        public static string FTPPassword => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["FTPPassword"]);
        public static int FTPPort => Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["FTPPort"]);
        public static string FTPUploadFile => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["FTPUploadFile"]);
    }
}
