using Common;
using Common.AspNet.Logging;
using Common.Logging;
using Domain.Entity_Models;
using Domain.Entity_Models.Core;
using Domain.Entity_Models.Identity;
using Infraestructure.Context;
using Infraestructure.Repositories;
using LightInject;
using Mehdime.Entity;
using Service.Helpers;
using Service.Implementations;
using Service.Implementations.Core;
using Service.Implementations.Identity;
using Service.Interfaces;
using Service.Interfaces.Core;
using Service.Interfaces.Identity;

namespace Service.Config
{
    public class ServiceRegister : ICompositionRoot
    {
        public void Compose(IServiceRegistry container)
        {
            AmbientDbContextLocator ambientDbContextLocator = new AmbientDbContextLocator();
            CustomConfigurationLib configurationLib = new CustomConfigurationLib();
            container.Register<IDbContextScopeFactory>((x) => new DbContextScopeFactory(null));
            container.Register<IAmbientDbContextLocator, AmbientDbContextLocator>(new PerScopeLifetime());
            container.Register<IConfigurationLib, CustomConfigurationLib>();
            container.Register<ICustomLog, CustomLog4Net>();

            //Por cada repositorio            
            container.Register<IRepository<Application, ApplicationDbContext>>((x) => new Repository<Application, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<OptionRol, ApplicationDbContext>>((x) => new Repository<OptionRol, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<Option, ApplicationDbContext>>((x) => new Repository<Option, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<OptionType, ApplicationDbContext>>((x) => new Repository<OptionType, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<Role, ApplicationDbContext>>((x) => new Repository<Role, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<User, ApplicationDbContext>>((x) => new Repository<User, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<UserClaim, ApplicationDbContext>>((x) => new Repository<UserClaim, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<UserLogin, ApplicationDbContext>>((x) => new Repository<UserLogin, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<UserRol, ApplicationDbContext>>((x) => new Repository<UserRol, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<Audience, ApplicationDbContext>>((x) => new Repository<Audience, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<Configuration, ApplicationDbContext>>((x) => new Repository<Configuration, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<ConfigurationDetail, ApplicationDbContext>>((x) => new Repository<ConfigurationDetail, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<ContractStatus, ApplicationDbContext>>((x) => new Repository<ContractStatus, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<EnrollmentHistory, ApplicationDbContext>>((x) => new Repository<EnrollmentHistory, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<Family, ApplicationDbContext>>((x) => new Repository<Family, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<Gender, ApplicationDbContext>>((x) => new Repository<Gender, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<Language, ApplicationDbContext>>((x) => new Repository<Language, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<ManagedCareOrganization, ApplicationDbContext>>((x) => new Repository<ManagedCareOrganization, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<Member, ApplicationDbContext>>((x) => new Repository<Member, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<Municipality, ApplicationDbContext>>((x) => new Repository<Municipality, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<PersonPrimaryCarePhysician, ApplicationDbContext>>((x) => new Repository<PersonPrimaryCarePhysician, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<PrimaryCarePhysician, ApplicationDbContext>>((x) => new Repository<PrimaryCarePhysician, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<PrimaryCarePhysicianDetail, ApplicationDbContext>>((x) => new Repository<PrimaryCarePhysicianDetail, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<PrimaryMedicalGroup, ApplicationDbContext>>((x) => new Repository<PrimaryMedicalGroup, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<PcpPmgMco, ApplicationDbContext>>((x) => new Repository<PcpPmgMco, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<Speciality, ApplicationDbContext>>((x) => new Repository<Speciality, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<Files, ApplicationDbContext>>((x) => new Repository<Files, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<EnrollmentPeriod, ApplicationDbContext>>((x) => new Repository<EnrollmentPeriod, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<EnrollmentStatistics, ApplicationDbContext>>((x) => new Repository<EnrollmentStatistics, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<ReasonJustCause, ApplicationDbContext>>((x) => new Repository<ReasonJustCause, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            container.Register<IRepository<Puntuation, ApplicationDbContext>>((x) => new Repository<Puntuation, ApplicationDbContext>(ambientDbContextLocator, configurationLib));
            //container.Register<IRepository<ManagedCareOrganization, ApplicationDbContext>>((x) => new Repository<ManagedCareOrganization, ApplicationDbContext>(ambientDbContextLocator, configurationLib));

            //Por cada bussiness            
            container.Register<IEnrollmentHistoryServices, EnrollmentHistoryServices>();
            container.Register<IFamilyServices, FamilyServices>();
            container.Register<IPuntuationServices, PuntuationServices>();
            container.Register<IApplicationService, ApplicationService>();
            container.Register<IIdentityService, IdentityService>();
            container.Register<IOptionService, OptionService>();
            container.Register<IOptionRolService, OptionRolService>();
            container.Register<IOptionTypeService, OptionTypeService>();
            container.Register<IRolService, RolService>();
            container.Register<IUserRolService, UserRolService>();
            container.Register<IUserService, UserService>();
            container.Register<IUserManagerService, UserManagerService>();
            container.Register<IAudienceService, AudienceService>();
            container.Register<IMcoServices, McoServices>();
            container.Register<IPcpServices, PcpServices>();
            container.Register<IMemberServices, MemberServices>();
            container.Register<IPmgServices, PmgServices>();
            container.Register<ISpecialityServices, SpecialityServices>();
            container.Register<IMailService, MailService>();
            container.Register<IConfigurationService, ConfigurationService>();
            container.Register<IConfigurationDetailService, ConfigurationDetailService>();
            container.Register<IQuestionService, QuestionServices>();
            container.Register<IFileServices, FileServices>();
            container.Register<IPcpPmgMcoServices, PcpPmgMcoServices>();
            container.Register<IReportsServices, ReportsServices>();
            container.Register<IReportServices, ReportServices>();
            container.Register<IReasonJustCauseServices, ReasonJustCauseServices>();
            container.Register<IManagedCareOrganizationServices, ManagedCareOrganizationServices>();
            container.Register<IPrimaryMedicalGroupServices, PrimaryMedicalGroupServices>();
            container.Register<IPrimaryCarePhysicianDetailServices, PrimaryCarePhysicianDetailServices>();
            container.Register<IMunicipalityServices, MunicipalityServices>();
        }
    }
}
