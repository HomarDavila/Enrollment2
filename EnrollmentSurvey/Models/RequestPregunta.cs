using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnrollmentSurvey.Models
{
    public class RequestPregunta
    {
        public int PreguntaId { get; set; }
        public string Spanish { get; set; }
        public string English { get; set; }
        public int Puntuacion { get; set; }
        public int TipoPregunta { get; set; }
    }
    public enum EnumTipoPregunta
    {
        Slider=1,
        OptionButton=2
    }
}