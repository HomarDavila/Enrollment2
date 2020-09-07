using Domain.Entity_Models;
using Domain.Entity_Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Entity_Configurations
{
    public class UserClaimConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<UserClaim>
    {
        public UserClaimConfiguration()
         : this("Identity")
        {
        }
        public UserClaimConfiguration(string schema)
        {
            ToTable("IdentityUserClaims", schema);
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
        }
    }
}
