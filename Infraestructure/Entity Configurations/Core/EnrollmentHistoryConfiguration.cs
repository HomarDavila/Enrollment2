using Domain.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Entity_Configurations
{

    public class EnrollmentHistoryConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<EnrollmentHistory>
    {
        public EnrollmentHistoryConfiguration()
         : this("Enrollment")
        {
        }
        public EnrollmentHistoryConfiguration(string schema)
        {
            ToTable("EnrollmentHistories", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.SSN).HasColumnName(@"SSN").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(9);
            Property(x => x.Suffix).HasColumnName(@"Suffix").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(2);
            Property(x => x.MPI).HasColumnName(@"MPI").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(13);
            Property(x => x.MPIShort).HasColumnName(@"MPIShort").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(11);
            Property(x => x.MPIContactMember).HasColumnName(@"MPIContactMember").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(11);
            Property(x => x.FirstLastName).HasColumnName(@"FirstLastName").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(50);
            Property(x => x.SecondLastName).HasColumnName(@"SecondLastName").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(50);
            Property(x => x.FirstName).HasColumnName(@"FirstName").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.MiddleName).HasColumnName(@"MiddleName").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(50);
            Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").HasColumnType("datetime").IsOptional();
            Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").HasColumnType("datetime").IsOptional();
            Property(x => x.Enabled).HasColumnName(@"Enabled").HasColumnType("bit").IsOptional();
        }
    }
}
