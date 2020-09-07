using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Custom_Models
{
    public class SecurityAnswer
    {
        public int QuestionID { get; set; }
        public string QuestionES { get; set; }
        public string QuestionEN { get; set; }
        public string Answer { get; set; }
    }
}
