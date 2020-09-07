using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace EnrollmentSurvey.Idioma
{
    public static class Helpers
    {
        public static string ingles = "en-US";
        public static string espanol = "es-ES";
        public static void setIdioma(string lang)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
        }
    }
}