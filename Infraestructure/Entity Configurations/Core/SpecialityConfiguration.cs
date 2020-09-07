using Domain.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Entity_Configurations
{

    public class SpecialityConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Speciality>
    {
        public SpecialityConfiguration()
         : this("Enrollment")
        {
        }
        public SpecialityConfiguration(string schema)
        {
            ToTable("Specialities", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName(@"Name").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.Nombre).HasColumnName(@"Nombre").HasColumnType("varchar").IsOptional().IsUnicode(true).HasMaxLength(100);
            Property(x => x.Code).HasColumnName(@"Code").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(5);
            Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").HasColumnType("datetime").IsOptional();
            Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").HasColumnType("datetime").IsOptional();
            Property(x => x.Enabled).HasColumnName(@"Enabled").HasColumnType("bit").IsOptional();
        }
    }
}
