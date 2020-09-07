using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace EnrollmentSystemWebApp.Helpers
{
    public class LanguageHelper
    {
        public const string LOGIN_NAME_SESSION_KEY = "LOGIN_NAME";
        public const string LANGUAGE_SESSION_KEY = "LANGUAGE";
        public const string CULTURE_SESSION_KEY = "CULTURE";

        public static string Language
        {
            get
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;

                HttpSessionState session = context.Session;

                if (session == null)
                {
                    return AppConstants.DefaultCulture;
                }

                string language = session[LANGUAGE_SESSION_KEY] as string;

                if (language == null)
                {
                    return AppConstants.DefaultLanguage;
                }

                return language;
            }

            set
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;

                context.Session[LANGUAGE_SESSION_KEY] = value;
            }
        }
        public static string Culture
        {
            get
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;

                HttpSessionState session = context.Session;

                if (session == null)
                {
                    return AppConstants.DefaultCulture;
                }

                string culture = session[CULTURE_SESSION_KEY] as string;

                if (culture == null)
                {
                    return AppConstants.DefaultCulture;
                }

                return culture;
            }

            set
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;

                context.Session[CULTURE_SESSION_KEY] = value;
            }
        }

    }
}