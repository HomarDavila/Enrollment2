using Audit.WebApi;
using Common;
using Common.Others;
using Newtonsoft.Json;
using NLog;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Core.API.Controllers
{
    [RoutePrefix("api/Demo/v1")]
    public class DemoController : ApiController
    {
        private readonly IDemoService demoService = DependecyFactory.GetInstance<IDemoService>();
        Logger logger = NLog.LogManager.GetCurrentClassLogger();

        [AuditApi]
        [Route("Get")]
        [HttpGet]
        public EResponseBase<Domain.EntityModel.Demo> Get()
        {
            var transactionId = RequestUtility.GetTransactionId((this.Request != null ? this.Request.Headers : null));
            logger.Info(CustomException.CustomTittleMethod("Inicio", transactionId));
            try
            {
                string dataRequest = JsonConvert.SerializeObject(null);
                logger.Info(CustomException.CustomInfoMessage(string.Format("Parámetros de Request: {0}", dataRequest), transactionId));
                logger.Info(CustomException.CustomInfoMessage("Procesando ...", transactionId));
                var response = demoService.GetAll(transactionId);
                string dataResponse = JsonConvert.SerializeObject(response);
                logger.Info(CustomException.CustomInfoMessage(string.Format("Json object del Response: {0}", dataResponse), transactionId));
                return response;
            }
            catch (Exception ex)
            {
                logger.Error(ex.CustomErrorMessage(transactionId));
                return UtilitariesResponse<Domain.EntityModel.Demo>.setResponseBaseForException(ex);
            }
            finally
            {
                logger.Info(CustomException.CustomTittleMethod("Fin", transactionId));
            }
        }

        [AuditApi]
        [Route("Get/{id}")]
        [HttpGet]
        public EResponseBase<Domain.EntityModel.Demo> Get(int id)
        {
            var transactionId = RequestUtility.GetTransactionId((this.Request != null ? this.Request.Headers : null));
            logger.Info(CustomException.CustomTittleMethod("Inicio", transactionId));
            try
            {
                string dataRequest = JsonConvert.SerializeObject(id);
                logger.Info(CustomException.CustomInfoMessage(string.Format("Parámetros de Request: {0}", dataRequest), transactionId));
                logger.Info(CustomException.CustomInfoMessage("Procesando ...", transactionId));
                var response = demoService.Get(transactionId, id);
                string dataResponse = JsonConvert.SerializeObject(response);
                logger.Info(CustomException.CustomInfoMessage(string.Format("Json object del Response: {0}", dataResponse), transactionId));
                return response;
            }
            catch (Exception ex)
            {
                logger.Error(ex.CustomErrorMessage(transactionId));
                return UtilitariesResponse<Domain.EntityModel.Demo>.setResponseBaseForException(ex);
            }
            finally
            {
                logger.Info(CustomException.CustomTittleMethod("Fin", transactionId));
            }
        }

        [AuditApi]
        [Route("RegisterOrUpdate/{demo}")]
        [HttpPost]
        public EResponseBase<Domain.EntityModel.Demo> RegisterOrUpdate([FromBody] Domain.EntityModel.Demo requestDTO)
        {
            var transactionId = RequestUtility.GetTransactionId((this.Request != null ? this.Request.Headers : null));
            logger.Info(CustomException.CustomTittleMethod("Inicio", transactionId));
            try
            {
                string dataRequest = JsonConvert.SerializeObject(requestDTO);
                logger.Info(CustomException.CustomInfoMessage(string.Format("Parámetros de Request: {0}", dataRequest), transactionId));
                logger.Info(CustomException.CustomInfoMessage("Procesando ...", transactionId));
                var response = demoService.InsertOrUpdate(transactionId, requestDTO);
                string dataResponse = JsonConvert.SerializeObject(response);
                logger.Info(CustomException.CustomInfoMessage(string.Format("Json object del Response: {0}", dataResponse), transactionId));
                return response;
            }
            catch (Exception ex)
            {
                logger.Error(ex.CustomErrorMessage(transactionId));
                return UtilitariesResponse<Domain.EntityModel.Demo>.setResponseBaseForException(ex);
            }
            finally
            {
                logger.Info(CustomException.CustomTittleMethod("Fin", transactionId));
            }
        }

        [AuditApi]
        [Route("Delete/{demo}")]
        [HttpPost]
        public EResponseBase<Domain.EntityModel.Demo> Delete([FromBody] Domain.EntityModel.Demo request)
        {
            var transactionId = RequestUtility.GetTransactionId((this.Request != null ? this.Request.Headers : null));
            logger.Info(CustomException.CustomTittleMethod("Inicio", transactionId));
            try
            {
                string dataRequest = JsonConvert.SerializeObject(request);
                logger.Info(CustomException.CustomInfoMessage(string.Format("Parámetros de Request: {0}", dataRequest), transactionId));
                logger.Info(CustomException.CustomInfoMessage("Procesando ...", transactionId));
                var response = demoService.Delete(transactionId, request);
                string dataResponse = JsonConvert.SerializeObject(response);
                logger.Info(CustomException.CustomInfoMessage(string.Format("Json object del Response: {0}", dataResponse), transactionId));
                return response;
            }
            catch (Exception ex)
            {
                logger.Error(ex.CustomErrorMessage(transactionId));
                return UtilitariesResponse<Domain.EntityModel.Demo>.setResponseBaseForException(ex);
            }
            finally
            {
                logger.Info(CustomException.CustomTittleMethod("Fin", transactionId));
            }
        }
    }
}