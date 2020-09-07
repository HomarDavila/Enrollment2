using Domain.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Entity_Configurations
{
    public class EnrollmentPeriodConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<EnrollmentPeriod>
    {
        public EnrollmentPeriodConfiguration()
        : this("Enrollment")
        {
        }
        public EnrollmentPeriodConfiguration(string schema)
        {
            ToTable("Period", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").HasColumnType("datetime").IsOptional();
            Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.PeriodIni).HasColumnName(@"PeriodIni").HasColumnType("datetime").IsOptional();
            Property(x => x.PeriodFin).HasColumnName(@"PeriodFin").HasColumnType("datetime").IsOptional();
            Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").HasColumnType("datetime").IsOptional();
            Property(x => x.Enabled).HasColumnName(@"Enabled").HasColumnType("bit").IsOptional();
        }

    }
}
