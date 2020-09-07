using System;
using System.Collections.Generic;
using System.Text;

namespace Security.API.Model.Response
{
    public class OptionRol_Response_v2
    {
        public int ApplicationId { get; set; }
        public Application_Response_v1 Application { get; set; }
        public int OptionId { get; set; }
        public Option_Response_v1 Option { get; set; }
        public int RolId { get; set; }
        public Rol_Response_v1 Rol { get; set; }
    }
}
