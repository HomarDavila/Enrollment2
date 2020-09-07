using Common.Logging;
using Microsoft.Owin.Security;
using Security.API.Helpers;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Text;

namespace Security.API.Providers
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly ICustomLog logger = DependencyFactory.GetInstance<ICustomLog>();
        private readonly IAudienceService service2 = DependencyFactory.GetInstance<IAudienceService>();

        private readonly string _issuer = string.Empty;

        public CustomJwtFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            string audienceId = CustomConfigurationLib.AudienceId;
            InMemorySymmetricSecurityKey securityKey = new InMemorySymmetricSecurityKey(Encoding.ASCII.GetBytes(CustomConfigurationLib.AudienceSecret));
            SigningCredentials signingKey = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);
            DateTimeOffset? issued = data.Properties.IssuedUtc;
            DateTimeOffset? expires = data.Properties.ExpiresUtc;
            JwtSecurityToken token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string jwt = handler.WriteToken(token);
            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, string.Empty.GetTransaction());
            ConfigureService();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                ////JwtSecurityTokenHandler
                System.IdentityModel.Tokens.JwtSecurityTokenHandler tokenHandler = new System.IdentityModel.Tokens.JwtSecurityTokenHandler();
                System.Security.Claims.ClaimsPrincipal claimsPrincipal;
                try
                {
                    System.IdentityModel.Tokens.JwtSecurityToken tokenReceived = new System.IdentityModel.Tokens.JwtSecurityToken(protectedText);
                    //Configure Validation parameters// Now its Generalized//token must have issuer and audience
                    string issuer = tokenReceived.Issuer;
                    List<string> strAudience = (List<string>)tokenReceived.Audiences;
                    string audience = strAudience.Count > 0 ? strAudience[0].ToString() : string.Empty;
                    Domain.Entity_Models.Identity.Audience audForContext = service2.Get(audience).objeto;
                    byte[] symmetricKey = Encoding.ASCII.GetBytes(audForContext.Base64Secret);
                    TokenValidationParameters validationParameters = new System.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidAudience = audience,
                        IssuerSigningKey = new System.IdentityModel.Tokens.InMemorySymmetricSecurityKey(symmetricKey),
                        ValidIssuer = issuer,
                        RequireExpirationTime = false
                    };
                    System.IdentityModel.Tokens.SecurityToken validatedToken;
                    //if token gets validated claimsPrincipal has value otherwise it throws exception                
                    claimsPrincipal = tokenHandler.ValidateToken(protectedText, validationParameters, out validatedToken);
                    AuthenticationProperties props = new AuthenticationProperties(new Dictionary<string, string> { { "audience", audience } });
                    AuthenticationTicket ticket = new AuthenticationTicket((System.Security.Claims.ClaimsIdentity)claimsPrincipal.Identity, props);
                    return ticket;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    throw;
                }
            }
        }

        private void ConfigureService()
        {
            service2.Transaction = RequestUtility.GetHeaders().Transaction;
            service2.Logger = logger;
        }
    }
}