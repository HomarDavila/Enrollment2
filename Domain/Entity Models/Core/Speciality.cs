using Common;
using System;

namespace Domain.Entity_Models
{
    public class Speciality : IAuditEntity, IEquatable<Speciality>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public bool? ShowItOnChangeEnrollmentProcess { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
        // Important: Used to operate the distinct linq method
        public bool Equals(Speciality other)
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
            Speciality other = obj as Speciality;
            if (other != null)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }
        public string Nombre { get; set; }
    }
}
