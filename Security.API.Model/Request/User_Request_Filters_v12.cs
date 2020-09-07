using System;
using System.Collections.Generic;
using System.Text;

namespace Security.API.Model.Request
{
    public class User_Request_Filters_v12
    {
        public string UserName { get; set; }
        public string MPI { get; set; }
        public string NewPassword { get; set; }
    }
}
