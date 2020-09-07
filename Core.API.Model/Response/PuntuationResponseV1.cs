using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.Model.Response
{
    public class PuntuationResponseV1
    {
        public int Id { get; set; }

        public int Puntos { get; set; }

        public int PreguntaID { get; set; }

        public int EnrollmentHistoryID { get; set; }
    }
}
