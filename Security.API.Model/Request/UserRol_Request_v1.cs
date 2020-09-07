using System;
using System.Collections.Generic;
using System.Text;

namespace Security.API.Model.Request
{
    public class UserRol_Request_v1
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int ApplicationId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
    }
}
