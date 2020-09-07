using Domain.Custom_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.Model.Response
{
    public class StatusResponseV1
    {
        public int Id { get; set; }
        public string NameES { get; set; }
        public string NameEN { get; set; }
        public string Description { get; set; }
        public bool? AllowChange { get; set; }
        public string BusinessStatus { get; set; }
    }
}
