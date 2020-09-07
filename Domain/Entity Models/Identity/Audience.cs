using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity_Models.Identity
{
    public class Audience
    {
        public string Id { get; set; }
        public string Base64Secret { get; set; }
        public string Name { get; set; }
    }
}
