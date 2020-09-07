using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using EnrollmentSurvey.Idioma;
using EnrollmentSurvey.Models;

namespace EnrollmentSurvey.Controllers
{
    public class EncuestaController : Controller
    {

        public ActionResult Index(string id , string cultura)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("NotFound");
            }
            int colaId = int.Parse(id);
            using (AsesQEntities context = new AsesQEntities())
            {
                var cola2 = context.EnrollmentHistories.Where(x => x.EstadoEncuesta != true).FirstOrDefault();
                var cola = context.EnrollmentHistories.Where(x => x.Id ==colaId  && x.EstadoEncuesta != true ).FirstOrDefault();
                if(cola == null)
                {
                    Session["mensaje"] = 1;
                    return RedirectToAction("Mensajes");
                }
            }
            if (cultura == "us")
            {
                Helpers.setIdioma(Helpers.ingles);
                ViewBag.idioma = "en";
            }
            else
            {
                Helpers.setIdioma(Helpers.espanol);
                ViewBag.idioma = "es";
            }

            ViewBag.IdCola = id;
            ViewBag.titulo = Resource.txt_encuesta_satisfaccion;
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult Mensajes(string cultura)
        {
            if (cultura == "us")
            {
                Helpers.setIdioma(Helpers.ingles);
                ViewBag.idioma = "en";
            }
            else
            {
                Helpers.setIdioma(Helpers.espanol);
                ViewBag.idioma = "es";
            }

            int mensaje = int.Parse(Session["mensaje"].ToString());
            if (mensaje == 1)
            {
                ViewBag.mensaje = Resource.txt_encuesta_completa;
                return View();
            }
            return RedirectToAction("NotFound");

        }


        public ActionResult Preguntas()
        {
             
            using (var context = new AsesQEntities())
            {

                try
                {
                     var listaPreguntas = (from pre in context.Preguntas
                                          select new RequestPregunta()
                                          {
                                              PreguntaId = pre.Id,
                                              Spanish = pre.Spanish,
                                              English = pre.English,
                                              Puntuacion = 0,
                                              TipoPregunta=pre.TipoPregunta.Value,
                                          }).ToList();
                    return Json(listaPreguntas, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {

                    throw;
                }

                
            }
            

        }


        [HttpPost]
        public ActionResult GuardarPreguntas(string idCola, RequestPregunta[] preguntas)
        {
            int ColaId = int.Parse(idCola);
            using (var context = new AsesQEntities())
            {
                if (!string.IsNullOrEmpty(idCola))
                {
                    try
                    {
                        foreach (var pregunta in preguntas)
                        {
                            context.Puntuacion.Add(new Puntuacion {
                                PreguntasId = pregunta.PreguntaId,
                                EnrollmentHistoryID = ColaId,
                                Puntos = pregunta.Puntuacion });
                        }
                        context.EnrollmentHistories.Where(x => x.Id == ColaId).FirstOrDefault().EstadoEncuesta = true;

                        context.SaveChanges();
                        return Json("correcto", JsonRequestBehavior.AllowGet);
                    }
                    catch (DbEntityValidationException e) 
                    {
                        var msg = new System.Text.StringBuilder();
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            msg.AppendLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");
                            foreach (var ve in eve.ValidationErrors)
                            {
                                msg.AppendLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                            }
                        }

                        return Json("error", JsonRequestBehavior.AllowGet);
                    }
                }
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
    }
}