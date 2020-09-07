using Domain.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Entity_Configurations
{
    public class ConfigurationDetailConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ConfigurationDetail>
    {
        public ConfigurationDetailConfiguration()
            : this("Configuration")
        {
        }

        public ConfigurationDetailConfiguration(string schema)
        {
            ToTable("ConfigurationDetails", schema);
            Property(x => x.Code).HasColumnName(@"Code").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.StringValue).IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.AdditionalStringValue).IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.NumericValue).IsOptional();
            Property(x => x.AdditionalNumericValue).IsOptional();
            Property(x => x.Description).IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.ParentConfigurationDetailId).IsOptional();
            Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").HasColumnType("datetime").IsOptional();
            Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").HasColumnType("datetime").IsOptional();
            Property(x => x.Enabled).HasColumnName(@"Enabled").HasColumnType("bit").IsOptional();

        }
    }
}
