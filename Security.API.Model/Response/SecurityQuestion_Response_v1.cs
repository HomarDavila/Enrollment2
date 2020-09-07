using System;
using System.Collections.Generic;
using System.Text;

namespace Security.API.Model.Response
{
    public class SecurityQuestion_Response_v1
    {
        public int QuestionID { get; set; }
        public string QuestionES { get; set; }
        public string QuestionEN { get; set; }
        public string Answer { get; set; }
    }
}
