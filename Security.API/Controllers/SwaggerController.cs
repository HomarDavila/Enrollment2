using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Security.API.Controllers
{
    [AllowAnonymous]
    public class SwaggerController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Index()
        {
            return Redirect(new Uri(Request.RequestUri.AbsoluteUri + "/swagger").ToString());
        }
    }
}
