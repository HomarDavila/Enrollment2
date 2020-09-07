using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnrollmentSystemWebApp.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult AccesoDenegado()
        {
            return View();
        }
    }
}