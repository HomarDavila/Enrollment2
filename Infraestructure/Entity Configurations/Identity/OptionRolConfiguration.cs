using Domain.Entity_Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Entity_Configurations
{
    public class OptionRolConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<OptionRol>
    {
        public OptionRolConfiguration()
         : this("Identity")
        {
        }
        public OptionRolConfiguration(string schema)
        {
            ToTable("IdentityOptionRoles", schema);
            HasKey(table => new { table.ApplicationId, table.OptionId, table.RolId });
        }
    }
}
