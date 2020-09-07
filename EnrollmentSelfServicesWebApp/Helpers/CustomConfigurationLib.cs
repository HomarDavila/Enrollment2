
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnrrolmentSelfServicesWebApp.Helpers
{
    public class CustomConfigurationLib : ConfigurationLib
    {
        /*Begin Authentication*/
        public string HomeController => "Home";
        public string HomeAction => "Index";
        public string NotAuthorizedController => "User";
        public string NotAuthorizedAction => "Login";

        public string Grant_type => "password";
        public string TokenType => "bearer";
        public string SessionToken => "SessionToken";
        public string SessionUser => "SessionUser";
        public string ChangePersonMcoEnabled => "ChangePersonMcoEnabled";
        public string SessionEnrollmentHistory => "SessionEnrollmentHistory";
        public string AudienceId => System.Configuration.ConfigurationManager.AppSettings["AudienceId"];
        public string AudienceSecret => System.Configuration.ConfigurationManager.AppSettings["AudienceSecret"];
        public string Issuer => System.Configuration.ConfigurationManager.AppSettings["Issuer"];
        public int RoleByDefault => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["RoleByDefault"]);
        public bool ValidarAutorizacion => Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["ValidarAutorizacion"]);
        public string ApplicationCode => System.Configuration.ConfigurationManager.AppSettings["ApplicationCode"];
        /*End Authentication*/

        /*Begin Security API*/
        public string SecurityAPI_URLBase => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_URLBase"];
        public bool SecurityAPI_IgnoreSSL => Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_IgnoreSSL"]);
        public int SecurityAPI_Timeout => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_Timeout"]);
        public string SecurityAPI_ServicePrefix => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_ServicePrefix"];
        public string SecurityAPI_TokenPrefix => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_TokenPrefix"];

        public string SecurityAPI_IdentityController => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_IdentityController"];
        public string SecurityAPI_Identity_GetOptionsByUserIdAction => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_Identity_GetOptionsByUserIdAction"];
        public string SecurityAPI_Identity_RegisterAction => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_Identity_RegisterAction"];
        public string SecurityAPI_Identity_ResetPasswordAction => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_Identity_ResetPasswordAction"];
        public string SecurityAPI_Identity_ChangePasswordAction => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_Identity_ChangePasswordAction"];
        public string SecurityAPI_Identity_ChangePasswordActionExternal => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_Identity_ChangePasswordActionExternal"];
        public string SecurityAPI_UserController => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_UserController"];
        public string SecurityAPI_User_GetByNameAction => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_User_GetByNameAction"];
        public string SecurityAPI_User_GetById => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_User_GetById"];
        public string SecurityAPI_User_SetUser => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_User_SetUSer"];

        public string SecurityAPI_UserRolController => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_UserRolController"];
        public string SecurityAPI_UserRol_RegisterOrUpdateAction => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_UserRol_RegisterOrUpdateAction"];

        public string SecurityAPI_SecurityQuestionController => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_SecurityQuestionController"]);
        public string SecurityAPI_SecurityQuestion_GetAction => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_SecurityQuestion_GetAction"]);

        public string SecurityAPI_MailController => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_MailController"];
        public string SecurityAPI_Mail_SendResetPasswordEmailAction => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_Mail_SendResetPasswordEmailAction"];
        public string SecurityAPI_Mail_SendConfirmationEmail => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_Mail_SendConfirmationEmail"];
        public string SecurityAPI_Mail_SendUserRegister => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_Mail_SendUserRegister"];
        public string SecurityAPI_Mail_SendLinkQuitz => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_Mail_SendLinkQuitz"];
        /*End Security API*/


        /*Begin Core API*/
        public string CoreAPI_UrlBase => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_UrlBase"];
        public string CoreAPI_ServicePreffix => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_ServicePreffix"];
        public int CoreAPI_Timeout => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["SecondsTimeOutCoreAPI"]);
        public bool CoreAPI_IgnoreSSL => Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["CoreAPI_IgnoreSSL"]);

        public string CoreAPI_McoController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_McoController"];
        public string CoreAPI_Mco_Get => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Mco_Get"];

        public string CoreAPI_PcpController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_PcpController"];
        public string CoreAPI_Pcp_Get => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Pcp_Get"];
        public string CoreAPI_ManagedCareOrganizationController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_ManagedCareOrganizationController"];
        public string CoreAPI_ManagedCareOrganization_Get => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_ManagedCareOrganization_Get"];
        public string CoreAPI_MunicipalityController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_MunicipalityController"];
        public string CoreAPI_Municipality_Get => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Municipality_Get"];
        public string CoreAPI_Pcp_GetWithFilters => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Pcp_GetWithFilters"];
        public string CoreAPI_Pcp_GetWithFiltersToList => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Pcp_GetWithFiltersToList"];
        public string CoreAPI_PrimaryCarePhysicianDetailController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_PrimaryCarePhysicianDetailController"];
        public string CoreAPI_PrimaryCarePhysicianDetail_GetWithFiltersToList => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_PrimaryCarePhysicianDetail_GetWithFiltersToList"];

        public string CoreAPI_PmgController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_PmgController"];
        public string CoreAPI_Pmg_Get => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Pmg_Get"];

        public string CoreAPI_SpecialityController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_SpecialityController"];
        public string CoreAPI_Speciality_Get => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Speciality_Get"];
        public string CoreAPI_Speciality_GetByPCPId => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Speciality_GetByPCPId"];
        public string CoreAPI_PrimaryMedicalGroupController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_PrimaryMedicalGroupController"];
        public string CoreAPI_PrimaryMedicalGroup_GetByPCPId => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_PrimaryMedicalGroup_GetByPCPId"];

        public string CoreAPI_MemberController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_MemberController"];
        public string CoreAPI_Member_GetEnrPeriod => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_GetEnrPeriod"];        
        public string CoreAPI_Member_Get => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_Get"];
        public string CoreAPI_Member_GetMembersById => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_GetMembersById"];
        public string CoreAPI_Member_ChangePersonMcoEnabled => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_ChangePersonMcoEnabled"];
        public string CoreAPI_Member_GetByFilters => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_GetByFilters"];
        public string CoreAPI_Member_ChangeEnrollment => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_ChangeEnrollment"];
        public string CoreAPI_Member_GetEnrollmentHistory => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_GetEnrollmentHistory"];
        public string CoreAPI_Member_SendSms => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_SendSms"];
        public string CoreAPI_CommonController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_CommonController"];
        public string CoreAPI_Common_CreatePDF => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Common_CreatePDF"];

        public string CoreAPI_FileController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_FileController"];
        public string CoreAPI_File_SetPDF => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_File_SetPDF"];
        public string CoreAPI_File_GetPDF => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_File_GetPDF"];


        public string CoreAPI_ReportsController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_ReportsController"];
        public string CoreAPI_Reports_InsertStatistic => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Reports_InsertStatistic"];


        /*End Core API*/



        public string SubjectChangePasswordProcess => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SubjectChangePasswordProcess"]);
        public string url_Gov => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["urlGOV"]);
        public string url_Vital => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["urlVital"]);
        public string url_ASES => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["urlASES"]);


    }
}
