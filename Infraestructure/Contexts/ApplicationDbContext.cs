using Common;
using Common.Logging;
using Domain.Entity_Models;
using Domain.Entity_Models.Core;
using Domain.Entity_Models.Identity;
using Infraestructure.Entity_Configurations;
using Infraestructure.Entity_Configurations.Identity;
//using Infraestructure.Entity_Configurations;
//using Infraestructure.Entity_Configurations.Identity;
using Infraestructure.Ioc;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Common;
using System.Data.Entity;

namespace Infraestructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, UserLogin, UserRol, UserClaim>
    {
        private ICustomLog logger = DependencyLocator.GetInstance<ICustomLog>();
        private IConfigurationLib config = DependencyLocator.GetInstance<IConfigurationLib>();
        public DbSet<Domain.Entity_Models.Event> Events { get; set; }
        public virtual DbSet<ContractStatus> ContractStatus { get; set; }
        public virtual DbSet<EnrollmentHistory> EnrollmentHistories { get; set; }
        public virtual DbSet<EnrollmentPeriod> EnrollmentPeriod { get; set; }
        public virtual DbSet<EnrollmentStatistics> EnrollmentStatistics { get; set; }
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<ManagedCareOrganization> ManagedCareOrganizations { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Municipality> Municipalities { get; set; }
        public virtual DbSet<PersonPrimaryCarePhysician> PersonPrimaryCarePhysicians { get; set; }
        public virtual DbSet<PrimaryCarePhysician> PrimaryCarePhysicians { get; set; }
        public virtual DbSet<PrimaryCarePhysicianDetail> PrimaryCarePhysicianDetails { get; set; }
        public virtual DbSet<PrimaryMedicalGroup> PrimaryMedicalGroups { get; set; }
        public virtual DbSet<Speciality> Specialities { get; set; }
        public virtual DbSet<PcpPmgMco> PcpPmgMcos { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<ReasonJustCause> ReasonJustCauses { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Puntuation> Puntuations { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<OptionRol> OptionRols { get; set; }
        public virtual DbSet<UserRol> UserRols { get; set; }

        public ApplicationDbContext() : base("EnrollmentConnectionDB")
        {
            Database.CommandTimeout = config.SecondsTimeOutBD;
            if (config.LogSQLQueries)
            {
                logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, string.Empty.GetTransaction());
                Database.Log = (dbLog => logger.Debug(dbLog));
            }
            Configuration.ValidateOnSaveEnabled = false;
        }

        public ApplicationDbContext(DbConnection connection) : base(connection, contextOwnsConnection: true)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new EventConfiguration());
            modelBuilder.Configurations.Add(new ApplicationConfiguration());
            modelBuilder.Configurations.Add(new RolConfiguration());
            modelBuilder.Configurations.Add(new OptionConfiguration());
            modelBuilder.Configurations.Add(new OptionRolConfiguration());
            modelBuilder.Configurations.Add(new OptionTypeConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new AudienceConfiguration());
            modelBuilder.Configurations.Add(new ConfigurationConfiguration());
            modelBuilder.Configurations.Add(new ConfigurationDetailConfiguration());

            modelBuilder.Configurations.Add(new ContractStatusConfiguration());
            modelBuilder.Configurations.Add(new EnrollmentHistoryConfiguration());
            modelBuilder.Configurations.Add(new EnrollmentPeriodConfiguration());
            modelBuilder.Configurations.Add(new EnrollmentStatisticsConfiguration());
            modelBuilder.Configurations.Add(new FamilyConfiguration());
            modelBuilder.Configurations.Add(new GenderConfiguration());
            modelBuilder.Configurations.Add(new LanguageConfiguration());
            modelBuilder.Configurations.Add(new ManagedCareOrganizationConfiguration());
            modelBuilder.Configurations.Add(new MemberConfiguration());
            modelBuilder.Configurations.Add(new MunicipalityConfiguration());
            modelBuilder.Configurations.Add(new PersonPrimaryCarePhysicianConfiguration());
            modelBuilder.Configurations.Add(new PrimaryCarePhysicianConfiguration());
            modelBuilder.Configurations.Add(new PrimaryCarePhysicianDetailConfiguration());
            modelBuilder.Configurations.Add(new PrimaryMedicalGroupConfiguration());
            modelBuilder.Configurations.Add(new PcpPmgMcoConfiguration());
            modelBuilder.Configurations.Add(new SpecialityConfiguration());
            modelBuilder.Configurations.Add(new FileConfiguration());
            modelBuilder.Configurations.Add(new ReasonJustCauseConfiguration());
            modelBuilder.Configurations.Add(new StatusConfiguration());
            //Debido a que en userconfiguration ya se esta heredando se debe configurar aca.
            modelBuilder.Entity<UserClaim>().ToTable("IdentityUserClaims", "Identity");
            modelBuilder.Entity<UserClaim>().HasKey(x => x.Id);
            modelBuilder.Entity<UserClaim>().Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<UserLogin>().ToTable("IdentityUserLogins", "Identity");
            modelBuilder.Entity<UserRol>().ToTable("IdentityRolUsers", "Identity");
            modelBuilder.Entity<UserRol>().HasKey(table => new { table.Id });
            modelBuilder.Entity<UserRol>().Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            modelBuilder.Entity<UserRol>().Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").HasColumnType("datetime").IsOptional();
            modelBuilder.Entity<UserRol>().Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            modelBuilder.Entity<UserRol>().Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").HasColumnType("datetime").IsOptional();
            modelBuilder.Entity<UserRol>().Property(x => x.Enabled).HasColumnName(@"Enabled").HasColumnType("bit").IsOptional();
        }

        public static System.Data.Entity.DbModelBuilder CreateModel(System.Data.Entity.DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new EventConfiguration(schema));
            modelBuilder.Configurations.Add(new UserConfiguration(schema));
            modelBuilder.Configurations.Add(new ApplicationConfiguration(schema));
            modelBuilder.Configurations.Add(new RolConfiguration(schema));
            modelBuilder.Configurations.Add(new OptionConfiguration(schema));
            modelBuilder.Configurations.Add(new OptionRolConfiguration(schema));
            modelBuilder.Configurations.Add(new OptionTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new AudienceConfiguration(schema));
            modelBuilder.Configurations.Add(new ConfigurationConfiguration(schema));
            modelBuilder.Configurations.Add(new ConfigurationDetailConfiguration(schema));
            modelBuilder.Configurations.Add(new ContractStatusConfiguration(schema));
            modelBuilder.Configurations.Add(new EnrollmentHistoryConfiguration(schema));
            modelBuilder.Configurations.Add(new EnrollmentPeriodConfiguration(schema));
            modelBuilder.Configurations.Add(new EnrollmentStatisticsConfiguration(schema));
            modelBuilder.Configurations.Add(new FamilyConfiguration(schema));
            modelBuilder.Configurations.Add(new GenderConfiguration(schema));
            modelBuilder.Configurations.Add(new LanguageConfiguration(schema));
            modelBuilder.Configurations.Add(new ManagedCareOrganizationConfiguration(schema));
            modelBuilder.Configurations.Add(new MemberConfiguration(schema));
            modelBuilder.Configurations.Add(new MunicipalityConfiguration(schema));
            modelBuilder.Configurations.Add(new PersonPrimaryCarePhysicianConfiguration(schema));
            modelBuilder.Configurations.Add(new PrimaryCarePhysicianConfiguration(schema));
            modelBuilder.Configurations.Add(new PrimaryCarePhysicianDetailConfiguration(schema));
            modelBuilder.Configurations.Add(new PrimaryMedicalGroupConfiguration(schema));
            modelBuilder.Configurations.Add(new PcpPmgMcoConfiguration(schema));
            modelBuilder.Configurations.Add(new SpecialityConfiguration(schema));
            modelBuilder.Configurations.Add(new FileConfiguration(schema));
            modelBuilder.Configurations.Add(new ReasonJustCauseConfiguration(schema));
            modelBuilder.Configurations.Add(new StatusConfiguration(schema));
            //Debido a que en userconfiguration ya se esta heredando se debe configurar aca.
            modelBuilder.Entity<UserClaim>().ToTable("IdentityUserClaims", schema);
            modelBuilder.Entity<UserClaim>().HasKey(x => x.Id);
            modelBuilder.Entity<UserClaim>().Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<UserLogin>().ToTable("IdentityUserLogins", schema);
            modelBuilder.Entity<UserRol>().ToTable("IdentityRolUsers", schema);
            modelBuilder.Entity<UserRol>().HasKey(table => new { table.Id });
            modelBuilder.Entity<UserRol>().Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            modelBuilder.Entity<UserRol>().Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").HasColumnType("datetime").IsOptional();
            modelBuilder.Entity<UserRol>().Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            modelBuilder.Entity<UserRol>().Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").HasColumnType("datetime").IsOptional();
            modelBuilder.Entity<UserRol>().Property(x => x.Enabled).HasColumnName(@"Enabled").HasColumnType("bit").IsOptional();
            return modelBuilder;
        }

    }
}
