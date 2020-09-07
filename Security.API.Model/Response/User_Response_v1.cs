using Domain.Custom_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Security.API.Model.Response
{
    public class User_Response_v1
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public string SSNLast4 { get; set; }
        public DateTime? DateOfBirth { get; set; }


        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public bool? IsAdministrator { get; set; }
        public int AccessFailedCount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
        public int MemberId { get; set; }
        public bool? OptIn { get; set; }
        public List<Rol_Response_v1> listRoles { get; set; }
        public List<Option_Response_v2> listOptions { get; set; }
        public string Email2 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string MPI { get; set; }
        public bool HasDefaultCredentials { get; set; }
        public MemberResponseV2 Member { get; set; }
    }
}
