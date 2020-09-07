using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.Model.Response
{
    public class FamilyResponseV4
    {
        public string ContactFirstName { get; set; }
        public string ContactFirstLastName { get; set; }
        public string ContactSecondLastName { get; set; }
        public string ApplicationNumber { get; set; }
        public string ResidenceAddressZipCode { get; set; }
    }
}
