using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Domain.Entity_Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Service;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Security.API.App_Start
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private readonly IIdentityService service = DependencyFactory.GetInstance<IIdentityService>();
        private readonly IAudienceService service2 = DependencyFactory.GetInstance<IAudienceService>();
        private readonly ICustomLog logger = DependencyFactory.GetInstance<ICustomLog>();

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, string.Empty.GetTransaction());
            ConfigureService();

            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                ApplicationUserManager userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
                service._userManager = userManager;
                try
                {
                    //string dataRequest = JsonConvert.SerializeObject(context.UserName);
                    //bool existUser = service.ExistUser(context.UserName);
                    //if (!existUser)
                    //{
                    //    context.SetError(AppConstants.FunctionalError, "500");
                    //    logger.Error(String.Format("El usuario ingresado no es correcto o no existe. User {0}", context.UserName));
                    //    return;
                    //}
                    User user = await service.Login(context.UserName, context.Password);
                    if (user == null)
                    {
                        context.SetError(AppConstants.FunctionalError, "1600");
                        logger.Error(String.Format("El usuario o password es incorrecto. User {0}", context.UserName));
                        return;
                    }
                    if (user.Enabled.HasValue)
                    {
                        if (!user.Enabled.Value)
                        {
                            context.SetError(AppConstants.FunctionalError, "600");
                            logger.Error(String.Format("El usuario no se encuentra activo. User {0}", context.UserName));
                            return;
                        }
                    }


                    //if (user != null)
                    //{
                    //    EResponseBase<Role> roleResponse = service.GetRolesByUserId(user.Id, context.ClientId);
                    //    if (!roleResponse.IsOK)
                    //    {
                    //        context.SetError(AppConstants.FunctionalError, "1700");
                    //        logger.Error(String.Format("El usuario no tiene un rol asignado. User {0}", context.UserName));
                    //        return;
                    //    }
                    //}

                    ClaimsIdentity oAuthIdentity = await service.GenerateUserIdentityAsync(user, OAuthDefaults.AuthenticationType);
                    ClaimsIdentity cookiesIdentity = await service.GenerateUserIdentityAsync(user, CookieAuthenticationDefaults.AuthenticationType);

                    AuthenticationProperties properties = CreateProperties(user);                    
                    AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
                    context.Validated(ticket);
                    context.Request.Context.Authentication.SignIn(cookiesIdentity);
                    logger.Error(String.Format("Login OK. User {0}", context.UserName));
                }
                catch (Exception ex)
                {
                    context.SetError("invalid_grant", ex.Message);
                    logger.Error(ex);
                }
            }

        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, string.Empty.GetTransaction());
            ConfigureService();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                try
                {
                    string clientId = string.Empty;
                    string clientSecret = string.Empty;
                    string symmetricKeyAsBase64 = string.Empty;
                    if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
                    {
                        context.TryGetFormCredentials(out clientId, out clientSecret);
                    }
                    if (context.ClientId == null)
                    {
                        context.SetError(AppConstants.TechnicalError, "client_Id is not set");
                        logger.Error("client_Id is not set");
                        return Task.FromResult<object>(null);
                    }
                    Domain.Entity_Models.Identity.Audience audience = service2.Get(context.ClientId).objeto;
                    if (audience == null)
                    {
                        context.SetError(AppConstants.TechnicalError, string.Format("Invalid client_id '{0}'", context.ClientId));
                        logger.Error(string.Format("Invalid client_id '{0}'", context.ClientId));
                        return Task.FromResult<object>(null);
                    }
                    context.Validated();
                    return Task.FromResult<object>(null);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return Task.FromException(ex);
                }
            }

        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(User user)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", user.UserName },
                { "HasDefaultCredentials", user.HasDefaultCredentials.ToString() },
                { "CreatedBy", user.CreatedBy }

            };
            return new AuthenticationProperties(data);
        }

        private void ConfigureService()
        {
            service.Transaction = string.Empty.GetTransaction();
            service.Logger = logger;
            service2.Transaction = string.Empty.GetTransaction();
            service2.Logger = logger;
        }



    }
}