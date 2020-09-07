using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.Model.Response
{
    public class PersonPcpResponseV2
    {
        public int Id { get; set; }
        public string NPI { get; set; }
        public string FullName
        {
            get; set;// { return $"{FirstName} {FirstLastName} {SecondLastName}"; }
        }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public bool? Enabled { get; set; }
        public bool OverCapacity { get; }
        public GenderResponseV1 Gender { get; set; }
    }
}
