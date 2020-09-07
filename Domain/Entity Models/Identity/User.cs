using Common;
using Domain.Custom_Models;
using Domain.Entity_Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity_Models
{
    public class User : IdentityUser<int, UserLogin, UserRol, UserClaim>, IAuditEntity
    {
        public string FirstName { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public string SSNLast4 { get; set; }
        public string ZipCode { get; set; }
        public DateTime? DateOfBirth { get; set; }

        private string fullName;
        [NotMapped]
        public string FullName
        {
            get { return $"{FirstName} {LastName1} {LastName2}"; }
            set
            {
                fullName = value;
            }
        }


        [NotMapped]
        public string PassWithoutEncrypt { get; set; }

        public bool? IsAdministrator { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
        public int? MemberId { get; set; }
        public bool? OptIn { get; set; }

        [NotMapped]
        public List<Role> listRoles { get; set; }

        [NotMapped]
        public List<OptionCustomModel> listOptions { get; set; }

        public string Email2 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string MPI { get; set; }
        public bool HasDefaultCredentials { get; set; }
        public Member Member { get; set; }

    }
}
