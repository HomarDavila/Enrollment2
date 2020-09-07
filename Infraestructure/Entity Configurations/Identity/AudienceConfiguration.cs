using Domain.Entity_Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Entity_Configurations
{
    public class AudienceConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Audience>
    {
        public AudienceConfiguration()
         : this("Identity")
        {
        }
        public AudienceConfiguration(string schema)
        {
            ToTable("IdentityAudiences", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("varchar").IsUnicode(false).HasMaxLength(32);
            Property(x => x.Name).HasColumnName(@"Name").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.Base64Secret).HasColumnName(@"Base64Secret").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(80);
        }
    }
}
