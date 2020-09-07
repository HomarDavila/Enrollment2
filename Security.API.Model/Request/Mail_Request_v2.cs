using System;
using System.Collections.Generic;
using System.Text;

namespace Security.API.Model.Request
{
    public class Mail_Request_v2
    {
        public bool Contact { get; set; }
        public string Email { get; set; }
        public string NameTo { get; set; }
        public string NameFile { get; set; }

        public string Phone { get; set; }
        public int EnrollmentHistoryID { get; set; }
    }
}
