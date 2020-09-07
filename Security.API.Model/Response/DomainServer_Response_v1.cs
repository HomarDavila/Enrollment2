using System;

namespace Security.API.Model.Response
{
    public class DomainServer_Response_v1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
    }
}