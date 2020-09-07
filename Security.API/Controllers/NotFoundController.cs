using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Security.API.Controllers
{
    public class NotFoundController : ApiController
    {
        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public IHttpActionResult ErrorNotFound()
        {
            return NotFound();
        }
    }
}
