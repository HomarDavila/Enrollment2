using System;
using System.Collections.Generic;
using System.Text;

namespace Security.API.Model.Request
{
    public class Mail_Request_v1
    {
        public string UserFullName { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Language { get; set; }
    }
}
