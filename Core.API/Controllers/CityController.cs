using Audit.WebApi;
using Common;
using Common.Others;
using Common.Proxies;
using Core.API.Helpers;
using CoreAPI.Common;
using Domain.Custom_Models;
using Newtonsoft.Json;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Core.API.Controllers
{
    [AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
    [RoutePrefix("api/City/v1")]
    public class CityController : ApiController
    {
        private readonly ICityServices cityServices = DependecyFactory.GetInstance<ICityServices>();
        private static readonly Common.Logging.CustomLogManager logger = new Common.Logging.CustomLogManager(log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));

        [Route("Get")]
        [HttpGet]
        public EResponseBase<CitiesResponse> GetCities()
        {
            string transactionId = RequestUtility.GetTransactionId();
            logger.Transaction = transactionId;
            cityServices.Transaction = transactionId;
            logger.Print_InitMethod();
            try
            {
                string dataRequest = JsonConvert.SerializeObject(null);
                logger.Print_Request(dataRequest);
                var response = cityServices.GetCities(transactionId);                          
                string dataResponse = JsonConvert.SerializeObject(response);
                logger.Print_Response(dataResponse);
                return response;
            }
            catch (Exception ex)
            {
                logger.CustomError(ex.Message);
                return UtilitariesResponse<CitiesResponse>.setResponseBaseForException(ex);
            }
            finally
            {
                logger.Print_EndMethod();
            }
        }              
    }
}
