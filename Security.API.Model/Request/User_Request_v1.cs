using System;
using System.Collections.Generic;
using System.Text;

namespace Security.API.Model.Request
{
    public class User_Request_v1
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PassWithoutEncrypt { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public string SSNLast4 { get; set; }
        public string ZipCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        //public string DateOfBirth { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
        public bool? OptIn { get; set; }
        public string Email2 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string MPI { get; set; }

        public List<UserRol_Request_v1> Roles { get; set; }
    }

}
