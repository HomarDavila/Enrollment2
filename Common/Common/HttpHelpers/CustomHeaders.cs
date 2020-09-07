using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class CustomHeaders
    {
        public string TransactionId { get; set; }
        public CustomHeaders()
        {
            if (String.IsNullOrEmpty(TransactionId)) TransactionId = Guid.NewGuid().ToString();
        }
    }
}
