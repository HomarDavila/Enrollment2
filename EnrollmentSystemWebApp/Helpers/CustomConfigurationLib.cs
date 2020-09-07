
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnrollmentSystemWebApp.Helpers
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
        public string ChangeEnrollmentEnabledJustCause => "ChangeEnrollmentEnabledJustCause";
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

        public string SecurityAPI_UserController => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_UserController"];
        public string SecurityAPI_User_GetByNameAction => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_User_GetByNameAction"];

        public string SecurityAPI_UserRolController => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_UserRolController"];
        public string SecurityAPI_UserRol_RegisterOrUpdateAction => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_UserRol_RegisterOrUpdateAction"];

        public string SecurityAPI_SecurityQuestionController => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_SecurityQuestionController"]);
        public string SecurityAPI_SecurityQuestion_GetAction => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_SecurityQuestion_GetAction"]);

        public string SecurityAPI_MailController => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_MailController"];
        public string SecurityAPI_Mail_SendResetPasswordEmailAction => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_Mail_SendResetPasswordEmailAction"];
        public string SecurityAPI_Mail_SendConfirmationEmail => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_Mail_SendConfirmationEmail"];

        public string SecurityAPI_Mail_SendLinkQuitz => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_Mail_SendLinkQuitz"];

        /*End Security API*/

        /*Roles*/

        public string Role_AdministradorSystem => System.Configuration.ConfigurationManager.AppSettings["AdministradorSystem"];
        public string Role_Consuler => System.Configuration.ConfigurationManager.AppSettings["consuler"];
        public string Role_CallCenter => System.Configuration.ConfigurationManager.AppSettings["call_center"];
        /*End Roles*/
        public string NotApllyJustCauseId => System.Configuration.ConfigurationManager.AppSettings["NotApllyJustCauseId"];

        public string urlMembers => System.Configuration.ConfigurationManager.AppSettings["urlMembers"];


        /*Begin Core API*/
        public string CoreAPI_UrlBase => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_UrlBase"];
        public string CoreAPI_ServicePreffix => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_ServicePreffix"];
        public int CoreAPI_Timeout => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["SecondsTimeOutCoreAPI"]);
        public bool CoreAPI_IgnoreSSL => Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["CoreAPI_IgnoreSSL"]);

        public string CoreAPI_McoController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_McoController"];
        public string CoreAPI_Mco_Get => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Mco_Get"];

        public string CoreAPI_PcpController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_PcpController"];
        public string CoreAPI_Pcp_Get => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Pcp_Get"];
        public string CoreAPI_Pcp_GetWithFilters => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Pcp_GetWithFilters"];
        public string CoreAPI_Pcp_GetWithFiltersToList => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Pcp_GetWithFiltersToList"];
        public string CoreAPI_PrimaryCarePhysicianDetailController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_PrimaryCarePhysicianDetailController"];
        public string CoreAPI_PrimaryCarePhysicianDetail_GetWithFiltersToList => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_PrimaryCarePhysicianDetail_GetWithFiltersToList"];
        public string CoreAPI_Member_ChangePersonMcoEnabled => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_ChangePersonMcoEnabled"];
        public string CoreAPI_Member_ChangeEnrollmentEnabledJustCause => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_ChangeEnrollmentEnabledJustCause"];

        public string CoreAPI_PmgController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_PmgController"];
        public string CoreAPI_Pmg_Get => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Pmg_Get"];

        public string CoreAPI_SpecialityController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_SpecialityController"];
        public string CoreAPI_Speciality_Get => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Speciality_Get"];
        public string CoreAPI_MunicipalityController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_MunicipalityController"];
        public string CoreAPI_Municipality_Get => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Municipality_Get"];
        public string CoreAPI_Speciality_GetByPCPId => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Speciality_GetByPCPId"]; 
        public string CoreAPI_PrimaryMedicalGroupController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_PrimaryMedicalGroupController"];
        public string CoreAPI_PrimaryMedicalGroup_GetByPCPId => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_PrimaryMedicalGroup_GetByPCPId"];
        public string CoreAPI_ReasonJustCauseController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_ReasonJustCauseController"];
        public string CoreAPI_ReasonJustCause_Get => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_ReasonJustCause_Get"];
        public string CoreAPI_ReasonJustCause_GetReasonJustCauseById => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_ReasonJustCause_GetReasonJustCauseById"];

        public string CoreAPI_MemberController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_MemberController"];
        public string CoreAPI_ReportController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_ReportController"];
        public string CoreAPI_Member_Get => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_Get"];
        public string CoreAPI_Member_GetEmail => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_GetEmail"];
        public string CoreAPI_Member_GetMembersById => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_GetMembersById"];
        public string CoreAPI_Member_GetByFilters => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_GetByFilters"];
        public string CoreAPI_Report_GetReportInscripciones => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Report_GetReportInscripciones"];
        public string CoreAPI_Report_GetReportJustaCausa => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Report_GetReportJustaCausa"];
        public string CoreAPI_Member_ChangeEnrollment => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_ChangeEnrollment"];
        public string CoreAPI_Member_ChangeEnrollmentReject => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_ChangeEnrollmentReject"];
        public string CoreAPI_Member_GetEnrollmentHistory => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_GetEnrollmentHistory"];
        public string CoreAPI_Member_GetEnrHistory => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_GetEnrHistory"];
        public string CoreAPI_Member_GetEnrStatistics => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_GetEnrStatistics"];
        public string CoreAPI_Member_GetEnrPeriod => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_GetEnrPeriod"];
        public string CoreAPI_Member_SendEnrPeriod => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_SendEnrPeriod"];

        public string CoreAPI_Member_GetStatistics => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_GetStatistics"];
        public string CoreAPI_Member_SendStatistics => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_SendStatistics"];

        public string CoreAPI_FileController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_FileController"];
        public string CoreAPI_File_GetFile => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_File_GetFile"];
        public string CoreAPI_File_GetPDF => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_File_GetPDF"];
        public string CoreAPI_File_SetPDF => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_File_SetPDF"];
        public string CoreAPI_File_GetEnrollmentFiles => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_File_GetEnrollmentFiles"];
        public string CoreAPI_File_DisabledEnrollmentFiles => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_File_DisabledEnrollmentFiles"];
        public string CoreAPI_File_SetEnrollmentFiles => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_File_SetEnrollmentFiles"];
        public string CoreAPI_PcpPmgMcoController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_PcpPmgMcoController"];
        public string CoreAPI_PcpPmgMco_GetPcpPmgMco => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_PcpPmgMco_GetPcpPmgMco"];
        public string CoreAPI_CommonController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_CommonController"];
        public string CoreAPI_Common_CreatePDF => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Common_CreatePDF"];
        public string CoreAPI_Member_SendSms => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_SendSms"];


        public string CoreAPI_ReportsController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_ReportsController"];
        public string CoreAPI_Reports_GetReportsWeb => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Reports_GetReportsWeb"];
        public string CoreAPI_Reports_InsertStatistic => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Reports_InsertStatistic"];

        public string CoreAPI_EnrollmentHistoryController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_EnrollmentHistoryController"];
        public string CoreAPI_EnrollmentHistory_GetOnlyRejects  => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_EnrollmentHistory_GetOnlyRejects"];
        /*End Core API*/


        public string SelftService_BusquedaProveedores => System.Configuration.ConfigurationManager.AppSettings["BusquedaProveedores"];
        public string SubjectChangePasswordProcess => Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SubjectChangePasswordProcess"]);



        #region SelfServices
        /*Begin Core API*/

        /*End Core API*/

        public string ConnectionString => System.Configuration.ConfigurationManager.ConnectionStrings["EnrollmentConnectionDB"].ConnectionString;


        /*SecurityApi*/
        public string SecurityApi_UrlBase => System.Configuration.ConfigurationManager.AppSettings["SecurityApi_UrlBase"];
        public string SecurityApi_ServicePreffix => System.Configuration.ConfigurationManager.AppSettings["SecurityApi_ServicePreffix"];
        public int SecurityApi_Timeout => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["SecurityApi_Timeout"]);
        public bool SecurityApi_IgnoreSSL => Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["SecurityApi_IgnoreSSL"]);



        /*Begin MailConnectorAPI*/
        public string CoreAPI_OverCapacityController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_OverCapacityController"];
        public string CoreAPI_OverCapacity_GetAllOvercapacity => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_OverCapacity_GetAllOvercapacity"];

        #endregion

    }
}
