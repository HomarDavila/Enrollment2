using System;
using System.Collections.Generic;
using System.Text;

namespace Security.API.Model.Response
{
    public class UserRol_Response_v1
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int ApplicationId { get; set; }

        public Application_Response_v1 Application { get; set; }
        public Rol_Response_v1 Rol { get; set; }
        public User_Response_v1 User { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
    }
}
