namespace Core.API.Model
{
    public class EmailRequestV1
    {
        public int MemberID { get; set; }
        public bool Contact { get; set; }
        public bool ContactSMS { get; set; }
        public string Email { get; set; }
        public string NameTo { get; set; }
        public string NameFile { get; set; }
        public string Phone { get; set; }
        public int EnrollmentHistoryID { get; set; }

    }
}
