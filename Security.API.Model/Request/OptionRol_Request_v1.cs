using System;
using System.Collections.Generic;
using System.Text;

namespace Security.API.Model.Request
{
    public class OptionRol_Request_v1
    {
        public int ApplicationId { get; set; }
        public int OptionId { get; set; }
        public int RolId { get; set; }
    }
}
