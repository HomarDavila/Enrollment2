using Domain.Entity_Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Entity_Configurations.Identity
{
    public class OptionTypeConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<OptionType>
    {
        public OptionTypeConfiguration()
         : this("Identity")
        {
        }
        public OptionTypeConfiguration(string schema)
        {
            ToTable("IdentityOptionTypes", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName(@"Name").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.Code).HasColumnName(@"Code").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
        }
    }
}
