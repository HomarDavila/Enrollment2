using Domain.Custom_Models;

namespace Core.API.Model.Response
{
    public class AvailablePcpPmgMcoResponseV1
    {
        public int McoId { get; set; }
        public McoResponseV1 MCO { get; set; }
        public int? PmgId { get; set; }
        public PmgResponseV1 PMG { get; set; }
        public int? PcpId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
