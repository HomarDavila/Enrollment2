using System;
using System.Collections.Generic;
using System.Text;

namespace Security.API.Model.Request
{
    public class User_Request_Filters_v7
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public string SSN { get; set; }
        public bool IsAdministrator { get; set; }

    }
}
