using AutoMapper;
using Common;
using Service.DependecyInjection;
using Common.Logging;
using Common.Platform.AspNet.Logging;
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
using Domain.Entity_Models;
using Core.API.Model.Response;
using Core.API.Model.Request;
using Security.API.Helpers;
using Security.API.Model.Response;
using Security.API.Model.Request;
using Domain.Entity_Models.Identity;

namespace Security.API.Controllers
{
    [RoutePrefix("api/Application/v1")]
    public class ApplicationController : ApiController
    {
        private readonly IApplicationService service = DependencyFactory.GetInstance<IApplicationService>();
        private static readonly CustomLogManager logger = new CustomLogManager(log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));
        private ConfigurationLib config = new ConfigurationLib();

        [Route("Get")]
        [HttpGet]
        public EResponseBase<Application_Response_v1> Get()
        {
            var header = RequestUtility.GetHeaders();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Transaction = header.Transaction;
                service.Transaction = header.Transaction;
                logger.Print_InitMethod();
                try
                {
                    string dataRequest = JsonConvert.SerializeObject(null);
                    logger.Print_Request(dataRequest);
                    var responseJSON = service.Get();
                    string dataResponse = JsonConvert.SerializeObject(responseJSON);
                    logger.Print_Response(dataResponse);
                    var response = Mapper.Map<EResponseBase<Application_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.CustomError(ex);
                    return new UtilitariesResponse<Application_Response_v1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("Get/{id}")]
        [HttpGet]
        public EResponseBase<Application_Response_v1> Get(int id)
        {
            var header = RequestUtility.GetHeaders();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Transaction = header.Transaction;
                service.Transaction = header.Transaction;
                logger.Print_InitMethod();
                try
                {
                    string dataRequest = JsonConvert.SerializeObject(id);
                    logger.Print_Request(dataRequest);
                    var responseJSON = service.Get(id);
                    string dataResponse = JsonConvert.SerializeObject(responseJSON);
                    logger.Print_Response(dataResponse);
                    var response = Mapper.Map<EResponseBase<Application_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.CustomError(ex);
                    return new UtilitariesResponse<Application_Response_v1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }
                
        [Route("GetByFilters")]
        [HttpPost]
        public EResponseBase<Application_Response_v1> GetByFilters(Application_Request_Filters_v1 request)
        {
            var header = RequestUtility.GetHeaders();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Transaction = header.Transaction;
                service.Transaction = header.Transaction;
                logger.Print_InitMethod();
                try
                {
                    string dataRequest = JsonConvert.SerializeObject(request);
                    logger.Print_Request(dataRequest);
                    var responseJSON = service.GetByFilters(request.Name, request.URL, request.Code);
                    string dataResponse = JsonConvert.SerializeObject(responseJSON);
                    logger.Print_Response(dataResponse);
                    var response = Mapper.Map<EResponseBase<Application_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.CustomError(ex);
                    return new UtilitariesResponse<Application_Response_v1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("RegisterOrUpdate")]
        [HttpPost]
        public EResponseBase<Application_Response_v1> RegisterOrUpdate([FromBody] Application_Request_v1 request)
        {
            var header = RequestUtility.GetHeaders();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Transaction = header.Transaction;
                service.Transaction = header.Transaction;
                logger.Print_InitMethod();
                try
                {
                    string dataRequest = JsonConvert.SerializeObject(request);
                    logger.Print_Request(dataRequest);                 
                    var requestConvert = Mapper.Map<Application>(request);
                    var responseJSON = service.InsertOrUpdate(requestConvert);
                    string dataResponse = JsonConvert.SerializeObject(responseJSON);
                    logger.Print_Response(dataResponse);
                    var response = Mapper.Map<EResponseBase<Application_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.CustomError(ex);
                    return new UtilitariesResponse<Application_Response_v1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }


        [Route("Disabled/{id}/{enabled}")]
        [HttpGet]
        public EResponseBase<Application_Response_v1> DisabledOREnabled(int id,  bool enabled)
        {
            var header = RequestUtility.GetHeaders();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Transaction = header.Transaction;
                service.Transaction = header.Transaction;
                logger.Print_InitMethod();
                try
                {
                    string dataRequest = JsonConvert.SerializeObject(String.Format("id: {0},  enabled: {1}", id, enabled));
                    logger.Print_Request(dataRequest);
                    var responseJSON = service.Disabled(id,enabled);
                    string dataResponse = JsonConvert.SerializeObject(responseJSON);
                    logger.Print_Response(dataResponse);
                    var response = Mapper.Map<EResponseBase<Application_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.CustomError(ex);
                    return new UtilitariesResponse<Application_Response_v1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }
    }
}
