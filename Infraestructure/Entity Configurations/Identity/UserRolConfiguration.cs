using Domain.Entity_Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Entity_Configurations.Identity
{
    public class UserRolConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<UserRol>
    {
        public UserRolConfiguration()
         : this("Identity")
        {
        }
        public UserRolConfiguration(string schema)
        {
            ToTable("IdentityRolUsers", schema);
        }
    }
}
