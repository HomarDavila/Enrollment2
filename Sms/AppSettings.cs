using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Sms
{
    public static class AppSettings
    {
        public static string SinchToken => ConfigurationManager.AppSettings["SinchToken"];
        public static string SinchServiceplanId => ConfigurationManager.AppSettings["SinchServiceplanId"];
        public static string SinchNumberFrom => ConfigurationManager.AppSettings["SinchNumberFrom"];
        public static string Enlace => ConfigurationManager.AppSettings["EnlaceEncuesta"];
        public static string Message => ConfigurationManager.AppSettings["Message"];
    }
}
