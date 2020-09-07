namespace Infraestructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        InsertedDate = c.DateTime(),
                        LastUpdatedDate = c.DateTime(),
                        Data = c.String(),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "Identity.IdentityRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 500, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false, defaultValue: "System"),
                        CreatedOn = c.DateTime(defaultValueSql: "GETDATE()"),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedOn = c.DateTime(),
                        Enabled = c.Boolean(defaultValue: true),                    
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Identity.IdentityRolUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false, defaultValue: "System"),
                        CreatedOn = c.DateTime(defaultValueSql: "GETDATE()"),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedOn = c.DateTime(),
                        Enabled = c.Boolean(defaultValue: true),
                    
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Identity.IdentityRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("Identity.IdentityUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "Identity.IdentityUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(),
                        UserName = c.String(nullable: false, maxLength: 500, unicode: false),
                        FirstName = c.String(maxLength: 30, unicode: false),
                        LastName1 = c.String(maxLength: 30, unicode: false),
                        LastName2 = c.String(maxLength: 30, unicode: false),
                        SSNLast4 = c.String(maxLength: 9, unicode: false),
                        DateoFBirth = c.DateTime(),
                        IsAdministrator = c.Boolean(),                    
                        Email = c.String(maxLength: 500, unicode: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 500, unicode: false),
                        SecurityStamp = c.String(maxLength: 500, unicode: false),
                        PhoneNumber = c.String(maxLength: 100, unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),                        
                        CreatedBy = c.String(maxLength: 100, unicode: false, defaultValue: "System"),
                        CreatedOn = c.DateTime(defaultValueSql: "GETDATE()"),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedOn = c.DateTime(),
                        Enabled = c.Boolean(defaultValue: true),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true);
            
            CreateTable(
                "Identity.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Identity.IdentityUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "Identity.IdentityUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("Identity.IdentityUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "Identity.IdentityApplications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 500, unicode: false),
                        URL = c.String(maxLength: 500, unicode: false),
                        Code = c.String(maxLength: 100, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false, defaultValue: "System"),
                        CreatedOn = c.DateTime(defaultValueSql: "GETDATE()"),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedOn = c.DateTime(),
                        Enabled = c.Boolean(defaultValue: true),
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Identity.IdentityOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 500, unicode: false),
                        Code = c.String(maxLength: 100, unicode: false),
                        URL = c.String(maxLength: 500, unicode: false),
                        OptionTypeId = c.Int(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false, defaultValue: "System"),
                        CreatedOn = c.DateTime(defaultValueSql: "GETDATE()"),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedOn = c.DateTime(),
                        Enabled = c.Boolean(defaultValue: true),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Identity.IdentityOptionTypes", t => t.OptionTypeId, cascadeDelete: true)
                .Index(t => t.OptionTypeId);
            
            CreateTable(
                "Identity.IdentityOptionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 500, unicode: false),
                        Code = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Identity.IdentityOptionRoles",
                c => new
                    {
                        ApplicationId = c.Int(nullable: false),
                        OptionId = c.Int(nullable: false),
                        RolId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationId, t.OptionId, t.RolId })
                .ForeignKey("Identity.IdentityApplications", t => t.ApplicationId, cascadeDelete: true)
                .ForeignKey("Identity.IdentityOptions", t => t.OptionId, cascadeDelete: true)
                .ForeignKey("Identity.IdentityRoles", t => t.RolId, cascadeDelete: true)
                .Index(t => t.ApplicationId)
                .Index(t => t.OptionId)
                .Index(t => t.RolId);
            
            CreateTable(
                "Identity.IdentityAudiences",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32, unicode: false),
                        Base64Secret = c.String(maxLength: 80, unicode: false),
                        Name = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Configuration.Configurations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100, unicode: false),
                        Code = c.String(maxLength: 100, unicode: false),
                        Description = c.String(maxLength: 500, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false, defaultValue: "System"),
                        CreatedOn = c.DateTime(defaultValueSql: "GETDATE()"),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedOn = c.DateTime(),
                        Enabled = c.Boolean(defaultValue: true),
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Configuration.ConfigurationDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 100, unicode: false),
                        Description = c.String(maxLength: 500, unicode: false),
                        StringValue = c.String(maxLength: 500, unicode: false),
                        AdditionalStringValue = c.String(maxLength: 500, unicode: false),
                        NumericValue = c.Double(),
                        AdditionalNumericValue = c.Double(),
                        ConfigurationId = c.Int(nullable: false),
                        ParentConfigurationDetailId = c.Int(),
                        CreatedBy = c.String(maxLength: 100, unicode: false, defaultValue: "System"),
                        CreatedOn = c.DateTime(defaultValueSql: "GETDATE()"),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedOn = c.DateTime(),
                        Enabled = c.Boolean(defaultValue: true),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.Configurations", t => t.ConfigurationId, cascadeDelete: true)
                .Index(t => t.ConfigurationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Configuration.ConfigurationDetails", "ConfigurationId", "Configuration.Configurations");
            DropForeignKey("Identity.IdentityOptionRoles", "RolId", "Identity.IdentityRoles");
            DropForeignKey("Identity.IdentityOptionRoles", "OptionId", "Identity.IdentityOptions");
            DropForeignKey("Identity.IdentityOptionRoles", "ApplicationId", "Identity.IdentityApplications");
            DropForeignKey("Identity.IdentityOptions", "OptionTypeId", "Identity.IdentityOptionTypes");
            DropForeignKey("Identity.IdentityRolUsers", "UserId", "Identity.IdentityUsers");
            DropForeignKey("Identity.IdentityUserLogins", "UserId", "Identity.IdentityUsers");
            DropForeignKey("Identity.IdentityUserClaims", "UserId", "Identity.IdentityUsers");
            DropForeignKey("Identity.IdentityRolUsers", "RoleId", "Identity.IdentityRoles");
            DropIndex("Configuration.ConfigurationDetails", new[] { "ConfigurationId" });
            DropIndex("Identity.IdentityOptionRoles", new[] { "RolId" });
            DropIndex("Identity.IdentityOptionRoles", new[] { "OptionId" });
            DropIndex("Identity.IdentityOptionRoles", new[] { "ApplicationId" });
            DropIndex("Identity.IdentityOptions", new[] { "OptionTypeId" });
            DropIndex("Identity.IdentityUserLogins", new[] { "UserId" });
            DropIndex("Identity.IdentityUserClaims", new[] { "UserId" });
            DropIndex("Identity.IdentityUsers", new[] { "UserName" });
            DropIndex("Identity.IdentityRolUsers", new[] { "RoleId" });
            DropIndex("Identity.IdentityRolUsers", new[] { "UserId" });
            DropTable("Configuration.ConfigurationDetails");
            DropTable("Configuration.Configurations");
            DropTable("Identity.IdentityAudiences");
            DropTable("Identity.IdentityOptionRoles");
            DropTable("Identity.IdentityOptionTypes");
            DropTable("Identity.IdentityOptions");
            DropTable("Identity.IdentityApplications");
            DropTable("Identity.IdentityUserLogins");
            DropTable("Identity.IdentityUserClaims");
            DropTable("Identity.IdentityUsers");
            DropTable("Identity.IdentityRolUsers");
            DropTable("Identity.IdentityRoles");
            DropTable("dbo.Event");
        }
    }
}
