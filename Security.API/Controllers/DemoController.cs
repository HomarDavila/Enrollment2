using AutoMapper;
using Common;
using Service.DependecyInjection;
using Common.Logging;
using Common.Platform.AspNet.Logging;
using Security.API.Helpers;
using Domain.Custom_Models.CoreAPI.Demo.Request;
using Domain.Custom_Models.CoreAPI.Demo.Response;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json;
using Service.Implementations;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Security.API.Controllers
{
    [RoutePrefix("api/Demo/v1")]
    public class DemoController : ApiController
    {
        private readonly IDemoService demoService = DependencyFactory.GetInstance<IDemoService>();
        private static readonly CustomLogManager logger = new CustomLogManager(log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));
        private ConfigurationLib config = new ConfigurationLib();

        [Route("Get")]
        [HttpGet]
        public EResponseBase<GetResponse> Get()
        {
            var header = RequestUtility.GetHeaders();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Transaction = header.Transaction;
                demoService.Transaction = header.Transaction;
                logger.Print_InitMethod();
                try
                {
                    string dataRequest = JsonConvert.SerializeObject(null);
                    logger.Print_Request(dataRequest);
                    var responseJSON = demoService.Get();
                    string dataResponse = JsonConvert.SerializeObject(responseJSON);
                    logger.Print_Response(dataResponse);
                    var response = Mapper.Map<EResponseBase<GetResponse>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.CustomError(ex);
                    return new UtilitariesResponse<GetResponse>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

      

        [Route("Get/{id}")]
        [HttpGet]
        public EResponseBase<GetResponse> Get(int id)
        {
            var header = RequestUtility.GetHeaders();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Transaction = header.Transaction;
                demoService.Transaction = header.Transaction;
                logger.Print_InitMethod();
                try
                {
                    string dataRequest = JsonConvert.SerializeObject(id);
                    logger.Print_Request(dataRequest);
                    var responseJSON = demoService.Get(id);
                    string dataResponse = JsonConvert.SerializeObject(responseJSON);
                    logger.Print_Response(dataResponse);
                    var response = Mapper.Map<EResponseBase<GetResponse>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.CustomError(ex);
                    return new UtilitariesResponse<GetResponse>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("RegisterOrUpdate")]
        [HttpPost]
        public EResponseBase<RegisterOrUpdateResponse> RegisterOrUpdate([FromBody] RegisterOrUpdateRequest request)
        {
            var header = RequestUtility.GetHeaders();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Transaction = header.Transaction;
                demoService.Transaction = header.Transaction;
                logger.Print_InitMethod();
                try
                {
                    string dataRequest = JsonConvert.SerializeObject(request);
                    logger.Print_Request(dataRequest);                 
                    var requestConvert = Mapper.Map<Demo>(request);
                    var responseJSON = demoService.InsertOrUpdate(requestConvert);
                    string dataResponse = JsonConvert.SerializeObject(responseJSON);
                    logger.Print_Response(dataResponse);
                    var response = Mapper.Map<EResponseBase<RegisterOrUpdateResponse>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.CustomError(ex);
                    return new UtilitariesResponse<RegisterOrUpdateResponse>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("Delete")]
        [HttpGet]
        public EResponseBase<DeleteResponse> Delete(int id)
        {
            var header = RequestUtility.GetHeaders();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Transaction = header.Transaction;
                demoService.Transaction = header.Transaction;
                logger.Print_InitMethod();
                try
                {
                    string dataRequest = JsonConvert.SerializeObject(id);
                    logger.Print_Request(dataRequest);
                    var responseJSON = demoService.Delete(id);
                    string dataResponse = JsonConvert.SerializeObject(responseJSON);
                    logger.Print_Response(dataResponse);
                    var response = Mapper.Map<EResponseBase<DeleteResponse>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.CustomError(ex);
                    return new UtilitariesResponse<DeleteResponse>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("Disabled/{id}/{enabled}")]
        [HttpGet]
        public EResponseBase<DeleteResponse> DisabledOREnabled(int id,  bool enabled)
        {
            var header = RequestUtility.GetHeaders();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Transaction = header.Transaction;
                demoService.Transaction = header.Transaction;
                logger.Print_InitMethod();
                try
                {
                    string dataRequest = JsonConvert.SerializeObject(String.Format("id: {0},  enabled: {1}", id, enabled));
                    logger.Print_Request(dataRequest);
                    var responseJSON = demoService.Disabled(id,enabled);
                    string dataResponse = JsonConvert.SerializeObject(responseJSON);
                    logger.Print_Response(dataResponse);
                    var response = Mapper.Map<EResponseBase<DeleteResponse>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.CustomError(ex);
                    return new UtilitariesResponse<DeleteResponse>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }
    }
}
