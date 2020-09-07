using Domain.Custom_Models;
using System;

namespace Core.API.Model.Response
{
    public class PcpPmgMcoResponseV1
    {
        public int Id { get; set; }
        public int? PrimaryCarePhysicianId { get; set; }
        public int? PmgId { get; set; }
        public PmgResponseV1 PMG { get; set; }
        public int? McoId { get; set; }
        public McoResponseV1 MCO { get; set; }
        public int? PcpId { get; set; }
        public PcpResponseV1 PCP { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
    }
}