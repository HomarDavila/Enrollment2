using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity_Models
{
    public class Status : IAuditEntity, IEquatable<Status>
    {
        public int Id { get; set; }
        public string NameES { get; set; }
        public string NameEN { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
        public bool? AllowChange { get; set; }
        public string BusinessStatus { get; set; }
        // Important: Used to operate the distinct linq method
        public bool Equals(Status other)
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
            Status other = obj as Status;
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
