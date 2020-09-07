using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Custom_Models
{
    public class ApplicationPeopleResponse
    {
        public int PersonId { get; set; }
        public string MPI { get; set; }
        public string Last4SSN { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public List<ApplicationsByPeopleResponse>  Applications { get; set; }
    }
}
