using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Custom_Models
{
    public class MemberCustomModelV2
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string MiddleName { get; set; }
        public string MPIShort { get; set; }
        public string Last4SSN { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public FamilyCustomModel Family { get; set; }
    }
}
