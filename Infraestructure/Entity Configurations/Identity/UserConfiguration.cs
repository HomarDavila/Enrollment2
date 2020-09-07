using Domain.Entity_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Entity_Configurations
{
    public class UserConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<User>
    {
        public UserConfiguration()
         : this("Identity")
        {
        }
        public UserConfiguration(string schema)
        {
            ToTable("IdentityUsers", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.FirstName).HasColumnName(@"FirstName").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(30);
            Property(x => x.LastName1).HasColumnName(@"LastName1").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(30);
            Property(x => x.LastName2).HasColumnName(@"LastName2").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(30);
            Property(x => x.SSNLast4).HasColumnName(@"SSNLast4").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(9);
            Property(x => x.Email).HasColumnName(@"Email").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.PasswordHash).HasColumnName(@"PasswordHash").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.SecurityStamp).HasColumnName(@"SecurityStamp").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.DateOfBirth).HasColumnName(@"DateOfBirth").HasColumnType("datetime").IsOptional();
            Property(x => x.PhoneNumber).HasColumnName(@"PhoneNumber").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.UserName).HasColumnName(@"UserName").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.Email).HasColumnName(@"Email").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.IsAdministrator).HasColumnName(@"IsAdministrator").HasColumnType("bit").IsOptional();
            Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").HasColumnType("datetime").IsOptional();
            Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").HasColumnType("datetime").IsOptional();
            Property(x => x.Enabled).HasColumnName(@"Enabled").HasColumnType("bit").IsOptional();
            Property(x => x.OptIn).HasColumnName(@"OptIn").HasColumnType("bit").IsOptional();
            Property(t => t.UserName).IsRequired().HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_UserName", 1) { IsUnique = true }));
            Property(t => t.MemberId).HasColumnName(@"MemberId").HasColumnType("int").IsOptional();
        }
    }
}
