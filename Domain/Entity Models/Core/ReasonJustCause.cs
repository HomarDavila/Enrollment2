using Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity_Models
{
    public class ReasonJustCause : IAuditEntity, IEquatable<ReasonJustCause>
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public string Razon { get; set; }
        public string Description { get; set; }
        //public string Descripcion { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
        // Important: Used to operate the distinct linq method
        public bool Equals(ReasonJustCause other)
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
            ReasonJustCause other = obj as ReasonJustCause;
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
