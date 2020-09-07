using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnrollmentPrincipalWebApp.Helpers
{
    public class CustomConfigurationLib : ConfigurationLib
    {
        public string CoreAPI_UrlBase => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_UrlBase"];
        public string CoreAPI_ServicePreffix => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_ServicePreffix"];
        public int CoreAPI_Timeout => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["SecondsTimeOutCoreAPI"]);
        public bool CoreAPI_IgnoreSSL => Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["CoreAPI_IgnoreSSL"]);
        public string CoreAPI_ReportsController => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_ReportsController"];
        public string CoreAPI_Reports_InsertStatistic => System.Configuration.ConfigurationManager.AppSettings["CoreAPI_Reports_InsertStatistic"];
        public string SelftService_BusquedaProveedores => System.Configuration.ConfigurationManager.AppSettings["BusquedaProveedores"];
        public string UrlCensos => System.Configuration.ConfigurationManager.AppSettings["UrlCensos"];
        public string SelftService_SelftServices => System.Configuration.ConfigurationManager.AppSettings["SelftServices"];
        public string Url_HacerCitas => System.Configuration.ConfigurationManager.AppSettings["Url_HacerCitas"];
        public bool ModalEnabled => Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["ModalEnabled"]);
        public string ModalTitleES => System.Configuration.ConfigurationManager.AppSettings["ModalTitleES"];
        public string ModalTitleEN => System.Configuration.ConfigurationManager.AppSettings["ModalTitleEN"];
        public string ModalMsgES => System.Configuration.ConfigurationManager.AppSettings["ModalMsgES"];
        public string ModalMsg2ES => System.Configuration.ConfigurationManager.AppSettings["ModalMsg2ES"];
        public string ModalMsgEN => System.Configuration.ConfigurationManager.AppSettings["ModalMsgEN"];
        public string ModalMsg2EN => System.Configuration.ConfigurationManager.AppSettings["ModalMsg2EN"];
    }
}