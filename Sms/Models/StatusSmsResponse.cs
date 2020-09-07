using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Models
{
    public class StatusSmsResponse
    {
        public string Type { get; set; }
        public string Batch_id { get; set; }
        public int Total_message_count { get; set; }
        public List<StatusSinch> Statuses { get; set; }
    }
}
