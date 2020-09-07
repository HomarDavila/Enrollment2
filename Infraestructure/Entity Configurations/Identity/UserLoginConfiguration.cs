using Domain.Entity_Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Entity_Configurations.Identity
{
    public class UserLoginConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<UserLogin>
    {
        public UserLoginConfiguration()
         : this("Identity")
        {
        }
        public UserLoginConfiguration(string schema)
        {
            ToTable("IdentityUserLogins", schema);
        }
    }
}
