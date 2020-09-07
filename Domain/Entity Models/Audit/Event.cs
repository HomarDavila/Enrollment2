using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity_Models
{
    public class Event
    {
        public int EventId { get; set; }
        public DateTime InsertedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Data { get; set; }
    }
}
