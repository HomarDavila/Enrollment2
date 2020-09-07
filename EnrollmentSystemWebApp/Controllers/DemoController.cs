using Audit.Mvc;
using Common.CustomExtensions;
using Common.Proxies;
using Domain.EntityModel;
using EnrollmentSystemWebApp.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EnrollmentSystemWebApp.Controllers
{
    [Audit(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = false, IncludeRequestBody = false)]
    public class DemoController : Controller
    {        
        public async Task<ActionResult> Index()
        {
            var transactionId = string.Empty;
            CustomProxyREST<Demo> proxy = new CustomProxyREST<Demo>();
            var response = await proxy.Get(CustomConfigurationLib.CoreAPI_UrlBase,
                                            CustomConfigurationLib.CoreAPI_ServicePreffix,
                                            CustomConfigurationLib.CoreAPI_DemoController,
                                            CustomConfigurationLib.CoreAPI_Demo_Get,
                                            null,
                                            null,
                                            transactionId.NewGUID(),
                                            false,
                                            CustomConfigurationLib.SecondsTimeOutCoreAPI);
            if (response.Code.Equals(CustomConfigurationLib.CodigoExito) || response.Code.Equals(CustomConfigurationLib.CodigoErrorNoDataFound))
            {
                return View(response.listado);
            }
            else
            {
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Example/Details/5        
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var transactionId = string.Empty;
            CustomProxyREST<Demo> proxy = new CustomProxyREST<Demo>();
            var response = await proxy.Get(CustomConfigurationLib.CoreAPI_UrlBase,
                                           CustomConfigurationLib.CoreAPI_ServicePreffix,
                                           CustomConfigurationLib.CoreAPI_DemoController,
                                           CustomConfigurationLib.CoreAPI_Demo_Get,
                                           null,
                                           null,
                                           transactionId.NewGUID(),
                                           false,
                                           CustomConfigurationLib.SecondsTimeOutCoreAPI,
                                           id.Value);

            if (response.Code.Equals(CustomConfigurationLib.CodigoExito))
            {
                return View(response.objeto);
            }
            else
            {
                return Json(response);
            }
        }

        // GET: Example/Create        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Example/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DemoId,Name,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn,Enabled")] Demo demo)
        {
            if (ModelState.IsValid)
            {
                var transactionId = string.Empty;
                CustomProxyREST<Demo, Demo> proxy = new CustomProxyREST<Demo, Demo>();
                var response = await proxy.Post(CustomConfigurationLib.CoreAPI_UrlBase,
                                                CustomConfigurationLib.CoreAPI_ServicePreffix,
                                                CustomConfigurationLib.CoreAPI_DemoController,
                                                CustomConfigurationLib.CoreAPI_Demo_Get,
                                                null,
                                                null,
                                                transactionId.NewGUID(),
                                                false,
                                                CustomConfigurationLib.SecondsTimeOutCoreAPI,
                                                demo);

                if (response.Code.Equals(CustomConfigurationLib.CodigoExito))
                {
                    return View(demo);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(demo);
        }

        // GET: Example/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var transactionId = string.Empty;
            CustomProxyREST<Demo> proxy = new CustomProxyREST<Demo>();
            var response = await proxy.Get(CustomConfigurationLib.CoreAPI_UrlBase,
                                           CustomConfigurationLib.CoreAPI_ServicePreffix,
                                           CustomConfigurationLib.CoreAPI_DemoController,
                                           CustomConfigurationLib.CoreAPI_Demo_Get,
                                           null,
                                           null,
                                           transactionId.NewGUID(),
                                           false,
                                           CustomConfigurationLib.SecondsTimeOutCoreAPI,
                                           id.Value);

            if (response.Code.Equals(CustomConfigurationLib.CodigoExito))
            {
                return View(response.objeto);
            }
            else
            {
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Example/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DemoId,Name,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn,Enabled")] Demo demo)
        {
            if (ModelState.IsValid)
            {
                var transactionId = string.Empty;
                CustomProxyREST<Demo, Demo> proxy = new CustomProxyREST<Demo, Demo>();
                var response = await proxy.Post(CustomConfigurationLib.CoreAPI_UrlBase,
                                                CustomConfigurationLib.CoreAPI_ServicePreffix,
                                                CustomConfigurationLib.CoreAPI_DemoController,
                                                CustomConfigurationLib.CoreAPI_Demo_Get,
                                                null,
                                                null,
                                                transactionId.NewGUID(),
                                                false,
                                                CustomConfigurationLib.SecondsTimeOutCoreAPI,
                                                demo);

                if (response.Code.Equals(CustomConfigurationLib.CodigoExito))
                {
                    return View(demo);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(demo);
        }

        // GET: Example/Delete/5        
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var transactionId = string.Empty;
            CustomProxyREST<Demo> proxy = new CustomProxyREST<Demo>();
            var response = await proxy.Get(CustomConfigurationLib.CoreAPI_UrlBase,
                                           CustomConfigurationLib.CoreAPI_ServicePreffix,
                                           CustomConfigurationLib.CoreAPI_DemoController,
                                           CustomConfigurationLib.CoreAPI_Demo_Get,
                                           null,
                                           null,
                                           transactionId.NewGUID(),
                                           false,
                                           CustomConfigurationLib.SecondsTimeOutCoreAPI,
                                           id.Value);

            if (response.Code.Equals(CustomConfigurationLib.CodigoExito))
            {
                return View(response.objeto);
            }
            else
            {
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Example/Delete/5        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //Demo demo = await db.Demoes.FindAsync(id);
            //db.Demoes.Remove(demo);
            //await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
