
using Common;
using Domain.Entity_Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity_Models
{
    public class PrimaryCarePhysician : IAuditEntity, IEquatable<PrimaryCarePhysician>
    {
        public int Id { get; set; }

        public int? PersonId { get; set; }
        [ForeignKey("PersonId")]
        public PersonPrimaryCarePhysician Person { get; set; }

        public int? SpecialityId { get; set; }
        [ForeignKey("SpecialityId")]
        public Speciality Speciality { get; set; }

        //public string City { get; set; }
        //public string ZipCode { get; set; }
        //public string State { get; set; }

        //public int? Capacity { get; set; }
        //public int? AmountOfLivesEnrolled { get; set; }

        //[NotMapped]
        //public int AmountOfLivesPending
        //{
        //    get
        //    {
        //        if (Capacity.HasValue && AmountOfLivesEnrolled.HasValue)
        //            return Capacity.Value - AmountOfLivesEnrolled.Value;
        //        else return 0;
        //    }
        //}

        //[NotMapped]
        //public bool OverCapacity
        //{
        //    get { return (AmountOfLivesEnrolled >= Capacity); }
        //}

        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }

        //[JsonIgnore]
        //public ICollection<PrimaryCarePhysicianDetail> Details { get; set; }

        // Important: Used to operate the distinct linq method
        public bool Equals(PrimaryCarePhysician other)
        {
            if (other == null) return false;
            if (!Id.Equals(other.Id)) return false;
            return true;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + Id.GetHashCode();
                return hash;
            }
        }
        public override bool Equals(object obj)
        {
            PrimaryCarePhysician other = obj as PrimaryCarePhysician;
            if (other != null)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }
    }
}
