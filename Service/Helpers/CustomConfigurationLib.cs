
using Common;
using System;

namespace Service.Helpers
{
    public class CustomConfigurationLib : ConfigurationLib
    {
        public string Grant_type => "password";
        public string TokenType => "bearer";

        public static string ConnectionString => System.Configuration.ConfigurationManager.ConnectionStrings["EnrollmentConnectionDB"].ConnectionString;
        public static bool ValidarAutorizacion => Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["ValidarAutorizacion"]);
        public static string Host => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Host"]);
        public static int Port => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["Port"]);
        public static string UserName => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["UserName"]);
        public static string Password => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Password"]);
        public static bool EnableSSL => Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["EnableSSL"]);

        public static string DateOfInitEnrollmentProcess => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["DateOfInitEnrollmentProcess"]);
        public static string DateOfInitEnrollmentForActives => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["DateOfInitEnrollmentForActives"]);
        public static string DateOfEndEnrollmentForActives => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["DateOfEndEnrollmentForActives"]);
        public static int DaysAfterCertificationDate => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["DaysAfterCertificationDate"]);
        public static int DayOfBreakEffectiveDate => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["DayOfBreakEffectiveDate"]);
        public static string PMGNoIdentificado => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["PMGNoIdentificado"]);

        public static int CodigoNewEnrollmentIsTheSame => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoNewEnrollmentIsTheSame"]);
        public static string MensajeNewEnrollmentIsTheSameES => System.Configuration.ConfigurationManager.AppSettings["MensajeNewEnrollmentIsTheSameES"];
        public static string MensajeNewEnrollmentIsTheSameEN => System.Configuration.ConfigurationManager.AppSettings["MensajeNewEnrollmentIsTheSameEN"];
        public static int CodigoPcpCapacity => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoPcpCapacity"]);
        public static string MensajePCPCapacityES => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["MensajePCPCapacityES"]);
        public static string MensajePCPCapacityEN => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["MensajePCPCapacityEN"]);
        public static int CodigoMcoCapacity => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoMcoCapacity"]);
        public static string MensajeMcoCapacityES => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["MensajeMcoCapacityES"]);
        public static string MensajeMcoCapacityEN => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["MensajeMcoCapacityEN"]);
        public static int CodigoValidateElegibility => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoValidateElegibility"]);
        public static string MensajeValidateEligibilityES => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["MensajeValidateEligibilityES"]);
        public static string MensajeValidateEligibilityEN => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["MensajeValidateEligibilityEN"]);
        public static int CodigoValidateCertificationDate => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoValidateCertificationDate"]);
        public static string MensajeValidateCertificationDateES => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["MensajeValidateCertificationDateES"]);
        public static string MensajeValidateCertificationDateEN => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["MensajeValidateCertificationDateEN"]);
        public static int CodigoValidateIfExistChangePrevious => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoValidateIfExistChangePrevious"]);
        public static string MensajeValidateIfExistChangePreviousES => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["MensajeValidateIfExistChangePreviousES"]);
        public static string MensajeValidateIfExistChangePreviousEN => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["MensajeValidateIfExistChangePreviousEN"]);
        public static int CodigoValidateIfExistChangeInProcess => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoValidateIfExistChangeInProcess"]);
        public static string MensajeValidateIfExistChangeInProcessES => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["MensajeValidateIfExistChangeInProcessES"]);
        public static string MensajeValidateIfExistChangeInProcessEN => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["MensajeValidateIfExistChangeInProcessEN"]);
        public static int CodigoTimeOffEnrrollment => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoTimeOffEnrrollment"]);
        public static string MensajeTimeOffEnrrollmentES => System.Configuration.ConfigurationManager.AppSettings["MensajeTimeOffEnrrollmentES"];
        public static string MensajeTimeOffEnrrollmentEN => System.Configuration.ConfigurationManager.AppSettings["MensajeTimeOffEnrrollmentEN"];

        /*Begin Core API*/
        public string CoreAPI_UrlBase => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_UrlBase"];
        public string CoreAPI_ServicePreffix => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_ServicePreffix"];
        public int CoreAPI_Timeout => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["SecondsTimeOutCoreAPI"]);
        public bool CoreAPI_IgnoreSSL => Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["CoreAPI_IgnoreSSL"]);
        public string CoreAPI_MemberController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_MemberController"];
        public string CoreAPI_Member_ExistUser => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_ExistUser"];
        /*End Core API*/

    }
}
