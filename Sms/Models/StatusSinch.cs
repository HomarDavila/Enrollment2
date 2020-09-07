using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Models
{
    public class StatusSinch
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public int Count { get; set; }
        public string[] Recipients { get; set; }
    }
}


