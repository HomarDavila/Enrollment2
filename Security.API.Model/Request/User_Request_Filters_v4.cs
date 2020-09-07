using System;
using System.Collections.Generic;
using System.Text;

namespace Security.API.Model.Request
{
    public class User_Request_Filters_v4
    {
        public int Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
