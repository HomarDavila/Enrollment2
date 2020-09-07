using System;

namespace Domain.Custom_Models
{
    public class ApplicationMemberInfoResponse : PersonBase
    {
        public int ApplicationMemberId { get; set; }              
        public string Elegibility { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }      
    }
}