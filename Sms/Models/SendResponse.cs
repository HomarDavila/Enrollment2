using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Models
{
    public class SendResponse
    {
        public string Id { get; set; }
        public string[] To { get; set; }
        public string From { get; set; }
        public string Canceled { get; set; }
        public string Body { get; set; }
        public string Type { get; set; }
        public string Created_at { get; set; }
        public string Modified_at { get; set; }
        public string Delivery_report { get; set; }
        public string Expire_at { get; set; }
        public string Flash_message { get; set; }
    }
}
