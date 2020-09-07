using System;

namespace Core.API.Model.Response
{
    public class PersonPcpResponseV1
    {
        public int Id { get; set; }
        public string FederalTaxId { get; set; }
        public string NPI { get; set; }
        public string FullName
        {
            get; set;// { return $"{FirstName} {FirstLastName} {SecondLastName}"; }
        }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
        public bool OverCapacity { get; }
        public GenderResponseV1 Gender { get; set; }
    }
}