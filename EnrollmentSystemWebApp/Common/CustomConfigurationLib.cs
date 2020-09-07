
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnrollmentSystemWebApp.Common
{
    public class CustomConfigurationLib : ConfigurationLib
    {     
        
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

        public string CoreAPI_PmgController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_PmgController"];
        public string CoreAPI_Pmg_Get => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Pmg_Get"];

        public string CoreAPI_SpecialityController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_SpecialityController"];
        public string CoreAPI_Speciality_Get => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Speciality_Get"];

        public string CoreAPI_MemberController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_MemberController"];
        public string CoreAPI_Member_Get => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_Get"];
        public string CoreAPI_Member_GetByFilters => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_GetByFilters"];
        public string CoreAPI_Member_ChangeEnrollment => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_ChangeEnrollment"];
        public string CoreAPI_Member_GetEnrollmentHistory => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Member_GetEnrollmentHistory"];
        /*End Core API*/

        public string ConnectionString => System.Configuration.ConfigurationManager.ConnectionStrings["EnrollmentConnectionDB"].ConnectionString;


        /*SecurityApi*/
        public string SecurityApi_UrlBase => System.Configuration.ConfigurationManager.AppSettings["SecurityApi_UrlBase"];
        public string SecurityApi_ServicePreffix => System.Configuration.ConfigurationManager.AppSettings["SecurityApi_ServicePreffix"];
        public int SecurityApi_Timeout => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["SecurityApi_Timeout"]);
        public bool SecurityApi_IgnoreSSL => Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["SecurityApi_IgnoreSSL"]);

        /*Begin MailConnectorAPI*/
        public string SecurityAPI_MailController => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_MailController"];
        public string SecurityAPI_Mail_SendConfirmationEmail => System.Configuration.ConfigurationManager.AppSettings["SecurityAPI_Mail_SendConfirmationEmail"];

        /*Begin MailConnectorAPI*/
        public string CoreAPI_OverCapacityController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_OverCapacityController"];
        public string CoreAPI_OverCapacity_GetAllOvercapacity => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_OverCapacity_GetAllOvercapacity"];
    }
}
