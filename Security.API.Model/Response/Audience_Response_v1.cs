using System;
using System.Collections.Generic;
using System.Text;

namespace Security.API.Model.Response
{
    public class Audience_Response_v1
    {
        public string Id { get; set; }
        public string Base64Secret { get; set; }
        public string Name { get; set; }
    }
}
