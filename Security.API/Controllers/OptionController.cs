using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Domain.Entity_Models.Identity;
using Security.API.Helpers;
using Security.API.Model.Request;
using Security.API.Model.Response;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Web.Http;

namespace Security.API.Controllers
{
    [RoutePrefix("api/identity/Option/v1")]
    public class OptionController : ApiController
    {
        private readonly IOptionService service;
        private readonly IConfigurationLib config;
        private readonly ICustomLog logger;


        public OptionController()
        {
            service = DependencyFactory.GetInstance<IOptionService>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        [Authorize]
        [Route("Get")]
        [HttpGet]
        public EResponseBase<Option_Response_v1> Get()
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(null);
                    EResponseBase<Option> responseJSON = service.Get();
                    logger.Print_Response(responseJSON);
                    EResponseBase<Option_Response_v1> response = Mapper.Map<EResponseBase<Option_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<Option_Response_v1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Authorize]
        [Route("Get/{id}")]
        [HttpGet]
        public EResponseBase<Option_Response_v1> Get(int id)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(id);
                    EResponseBase<Option> responseJSON = service.Get(id);
                    logger.Print_Response(responseJSON);
                    EResponseBase<Option_Response_v1> response = Mapper.Map<EResponseBase<Option_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<Option_Response_v1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Authorize]
        [Route("GetByCode/{code}")]
        [HttpGet]
        public EResponseBase<Option_Response_v1> GetByCode(string code)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(code);
                    EResponseBase<Option> responseJSON = service.GetByCode(code);
                    logger.Print_Response(responseJSON);
                    EResponseBase<Option_Response_v1> response = Mapper.Map<EResponseBase<Option_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<Option_Response_v1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Authorize]
        [Route("GetByType/{typeId}")]
        [HttpGet]
        public EResponseBase<Option_Response_v1> GetByType(int typeId)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(typeId);
                    EResponseBase<Option> responseJSON = service.GetByTypeId(typeId);
                    logger.Print_Response(responseJSON);
                    EResponseBase<Option_Response_v1> response = Mapper.Map<EResponseBase<Option_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<Option_Response_v1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Authorize]
        [Route("RegisterOrUpdate")]
        [HttpPost]
        public EResponseBase<Option_Response_v1> RegisterOrUpdate([FromBody] Option_Request_v1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(request);
                    Option requestConvert = Mapper.Map<Option>(request);
                    EResponseBase<Option> responseJSON = service.InsertOrUpdate(requestConvert);
                    logger.Print_Response(responseJSON);
                    EResponseBase<Option_Response_v1> response = Mapper.Map<EResponseBase<Option_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<Option_Response_v1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Authorize]
        [Route("Disabled/{id}/{enabled}")]
        [HttpGet]
        public EResponseBase<Option_Response_v1> DisabledOREnabled(int id, bool enabled)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(String.Format("id: {0},  enabled: {1}", id, enabled));
                    EResponseBase<Option> responseJSON = service.Disabled(id, enabled);
                    logger.Print_Response(responseJSON);
                    EResponseBase<Option_Response_v1> response = Mapper.Map<EResponseBase<Option_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<Option_Response_v1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        private void ConfigureService()
        {
            service.Transaction = RequestUtility.GetHeaders().Transaction;
            service.Logger = logger;
        }

        private CustomHeader ConfigureLogHeader()
        {
            CustomHeader header = RequestUtility.GetHeaders();
            logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, header.Transaction);
            logger.Header = RequestHelpers.AuditUserData(header);
            return header;
        }


    }
}
