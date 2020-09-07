
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Common
{
    public class CustomConfigurationLib : ConfigurationLib
    {
        public static string ConnectionString => System.Configuration.ConfigurationManager.ConnectionStrings["EnrollmentConnectionDB"].ConnectionString;
        public static bool DisableSwagger => Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings["DisableSwagger"]);
        public static string AudienceId => System.Configuration.ConfigurationManager.AppSettings["AudienceId"];
        public static string AudienceSecret => System.Configuration.ConfigurationManager.AppSettings["AudienceSecret"];
        public static string Issuer => System.Configuration.ConfigurationManager.AppSettings["Issuer"];
        public static string PathEnrollmentCreatePDF => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["PathEnrollmentCreatePDF"]);
        public static string PathEnrollmentFiles => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["PathEnrollmentFiles"]);
        public static string FTPServer => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["FTPServer"]);
        public static string FTPUser => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["FTPUser"]);
        public static string FTPPassword => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["FTPPassword"]);
        public static int FTPPort => Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["FTPPort"]);
        public static string FTPFileChangeMCO => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["FTPFileChangeMCO"]);
        public static string FTPFileEnrollment => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["FTPFileEnrollment"]);
    }
}


