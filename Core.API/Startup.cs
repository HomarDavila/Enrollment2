using Audit.Core;
using AutoMapper;
using Common;
using Core.API;
using Core.API.Model.Response;
using CoreAPI.Common;
using Domain.Custom_Models;
using Domain.Entity_Models;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System.IO;
using System.Text;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]
namespace Core.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            app.Use(async (context, next) =>
            {
                // Keep the original stream in a separate
                // variable to restore it later if necessary.
                Stream stream = context.Request.Body;
                // Optimization: don't buffer the request if
                // there was no stream or if it is rewindable.
                if (stream == Stream.Null || stream.CanSeek)
                {
                    await next();
                    return;
                }
                try
                {
                    using (MemoryStream buffer = new MemoryStream())
                    {
                        // Copy the request stream to the memory stream.
                        await stream.CopyToAsync(buffer);
                        // Rewind the memory stream.
                        buffer.Position = 0L;
                        // Replace the request stream by the memory stream.
                        context.Request.Body = buffer;
                        // Invoke the rest of the pipeline.
                        await next();
                    }
                }

                finally
                {
                    // Restore the original stream.
                    context.Request.Body = stream;
                }
            });
            log4net.Config.XmlConfigurator.Configure();
            ConfigureOAuth(app);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(GlobalConfiguration.Configuration);
            Audit.Core.Configuration.Setup()
            .UseSqlServer(config => config.ConnectionString(CustomConfigurationLib.ConnectionString)
            .Schema("dbo")
                .TableName("Event")
                .IdColumnName("EventId")
                .JsonColumnName("Data")
                .LastUpdatedColumnName("LastUpdatedDate"));

            Mapper.Initialize(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.CreateMap(typeof(EResponseBase<>), typeof(EResponseBase<>));
                cfg.CreateMap<ContractStatus, ContractStatusResponseV1>();
                cfg.CreateMap<EnrollmentHistory, EnrollmentHistoryResponseV1>();
                cfg.CreateMap<Family, FamilyResponseV1>();
                cfg.CreateMap<Family, FamilyResponseV2>();
                cfg.CreateMap<Gender, GenderResponseV1>();
                cfg.CreateMap<Language, LanguageResponseV1>();
                cfg.CreateMap<ManagedCareOrganization, McoResponseV1>();
                cfg.CreateMap<ManagedCareOrganization, McoResponseV2>();
                cfg.CreateMap<Member, MemberResponseV1>();
                cfg.CreateMap<Member, MemberResponseV2>();
                cfg.CreateMap<Municipality, MunicipalityResponseV1>();
                cfg.CreateMap<PrimaryCarePhysicianDetail, PcpDetailResponseV1>();
                cfg.CreateMap<PrimaryCarePhysician, PcpResponseV1>();
                cfg.CreateMap<PrimaryCarePhysicianCustomModel, PcpResponseV1>();
                cfg.CreateMap<PersonPrimaryCarePhysician, PersonPcpResponseV1>();
                cfg.CreateMap<PersonPrimaryCarePhysician, PersonPcpResponseV2>();
                cfg.CreateMap<PrimaryMedicalGroup, PmgResponseV1>();
                cfg.CreateMap<PrimaryMedicalGroup, PmgResponseV2>();
                cfg.CreateMap<PcpPmgMco, PcpPmgMcoResponseV1>();
                cfg.CreateMap<Speciality, SpecialityResponseV1>();
                cfg.CreateMap<Speciality, SpecialityResponseV2>();
                cfg.CreateMap<OverCapacityResponseV1, OverCapacityResponseV1>();
                cfg.CreateMap<EnrollmentHistory, ReportResponseV1>();
                cfg.CreateMap<PrimaryCarePhysicianDetailCustomModel, PrimaryCarePhysicianDetailResponseV1>();
                cfg.CreateMap<ReasonJustCause, ReasonJustCauseResponseV1>();
                cfg.CreateMap<PrimaryCarePhysicianCustomModel, PcpResponseV2>();
            });
        }

        public void ConfigureOAuth(IAppBuilder app)
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
    }
}