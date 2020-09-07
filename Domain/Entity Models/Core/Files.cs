using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity_Models
{
    public class Files : IAuditEntity, IEquatable<Files>
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        public Member Member { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
        // Important: Used to operate the distinct linq method
        public bool Equals(Files other)
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
            Files other = obj as Files;
            if (other != null)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }
        [NotMapped]
        public bool Content { get; set; }
    }
}
