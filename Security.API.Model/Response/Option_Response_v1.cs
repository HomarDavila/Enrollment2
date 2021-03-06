﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Security.API.Model.Response
{
    public class Option_Response_v1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string URL { get; set; }
        public int OptionTypeId { get; set; }
        public OptionType_Response_v1 OptionType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
    }
}
