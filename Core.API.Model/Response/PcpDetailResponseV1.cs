using Domain.Custom_Models;
using System;

namespace Core.API.Model.Response
{
    public class PcpDetailResponseV1
    {
        public int Id { get; set; }
        public int? PrimaryCarePhysicianId { get; set; }
        public string Phone { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
    }
}