using System;
using System.Collections.Generic;
using System.Text;

namespace Security.API.Model.Request
{
    public class User_Request_Filters_v3
    {
        public string OptionCode { get; set; }
        public int RolId { get; set; }
        public string ApplicationCode { get; set; }
    }
}
