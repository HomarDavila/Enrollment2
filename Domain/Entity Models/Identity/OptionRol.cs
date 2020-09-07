using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity_Models.Identity
{
    public class OptionRol
    {
        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public Application Application { get; set; }
        public int OptionId { get; set; }
        [ForeignKey("OptionId")]
        public Option Option { get; set; }
        public int RolId { get; set; }
        [ForeignKey("RolId")]
        public Role Rol { get; set; }
    }
}
