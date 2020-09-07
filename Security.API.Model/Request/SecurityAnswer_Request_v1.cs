using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.API.Model.Request
{
    public class SecurityAnswer_Request_v1
    {
        public int QuestionID { get; set; }
        public string Answer { get; set; }
    }
}
