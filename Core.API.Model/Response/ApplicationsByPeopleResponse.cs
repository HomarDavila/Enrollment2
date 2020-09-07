using System;

namespace Domain.Custom_Models
{
    public class ApplicationsByPeopleResponse
    {
        public string ContactFullName { get; set; }
        public string MemberFullName { get; set; }        
        public string ApplicationNumber { get; set; }        
        public DateTime? ExpirationDate { get; set; }        
        public DateTime? EfectivityDate { get; set; }
        public int ApplicationMemberID { get; set; }
    }
}