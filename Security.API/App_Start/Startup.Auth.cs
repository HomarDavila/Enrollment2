using Domain.Entity_Models;
using Infraestructure.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Security.API.App_Start;
using Security.API.Helpers;
using Security.API.Providers;
using Service;
using Service.DependecyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Security.API
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public static void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request            
            app.CreatePerOwinContext<ApplicationUserManager>(Startup.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                //AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(480),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true,
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AccessTokenFormat = new CustomJwtFormat(CustomConfigurationLib.Issuer)
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

        }

        public void ConfigureClient(IAppBuilder app)
        {
            string issuer = CustomConfigurationLib.Issuer;
            string audience = CustomConfigurationLib.AudienceId;
            byte[] secret = Encoding.ASCII.GetBytes(CustomConfigurationLib.AudienceSecret);


            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {

                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audience },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                    }
                });

        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            IUserManagerService service = DependencyFactory.GetInstance<IUserManagerService>();
            ApplicationUserManager manager = service.Create();
            manager.UserValidator = new UserValidator<User, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            Microsoft.Owin.Security.DataProtection.IDataProtectionProvider dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

    }
}