using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity_Models.Core
{
    public class Puntuation : IAuditEntity, IEquatable<Puntuation>
    {
        public int Id { get; set; }

        public int Puntos { get; set; }

        public int PreguntaID { get; set; }

        public int EnrollmentHistoryID { get; set; }

        [ForeignKey("EnrollmentHistoryID")]
        public EnrollmentHistory EnrollmentHistory { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }

        public bool Equals(Puntuation other)
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
            Puntuation other = obj as Puntuation;
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
