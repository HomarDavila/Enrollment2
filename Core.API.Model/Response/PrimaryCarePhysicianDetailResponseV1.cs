using Domain.Custom_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.Model.Response
{
    public class PrimaryCarePhysicianDetailResponseV1
    {
        public int Id { get; set; }
        public int? MunicipalityId { get; set; }
        public MunicipalityResponseV1 Municipality { get; set; }
        public int? SpecialityId { get; set; }
        public SpecialityResponseV1 Speciality { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int? Capacity { get; set; }
        public int? AmountOfLivesEnrolled { get; set; }
        public int AmountOfLivesPending { get; set; }
        public bool OverCapacity { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
        public ICollection<PcpPmgMcoResponseV1> AvailableManagedCareOrganizations { get; set; }
    }
}
