using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace MeditiWebApp.Common
{
    public class SessionHelper
    {
        public const string LOGIN_NAME_SESSION_KEY = "LOGIN_NAME";
        public const string LANGUAGE_SESSION_KEY = "LANGUAGE";
        public const string CULTURE_SESSION_KEY = "CULTURE";

        public static bool ExistUserInSession()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
        public static void DestroyUserSession()
        {
            FormsAuthentication.SignOut();
        }
        public static int GetUser()
        {
            int user_id = 0;
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                if (ticket != null)
                {
                    user_id = Convert.ToInt32(ticket.UserData);
                }
            }
            return user_id;
        }
        public static void AddUserToSession(string id)
        {
            bool persist = true;
            var cookie = FormsAuthentication.GetAuthCookie("usuario", persist);

            cookie.Name = FormsAuthentication.FormsCookieName;
            cookie.Expires = DateTime.Now.AddMonths(3);

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, id);

            cookie.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
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