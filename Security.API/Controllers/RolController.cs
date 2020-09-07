using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Domain.Entity_Models;
using Security.API.Helpers;
using Security.API.Model.Request;
using Security.API.Model.Response;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Web.Http;

namespace Security.API.Controllers
{
    [RoutePrefix("api/identity/Rol/v1")]
    public class RolController : ApiController
    {
        private readonly IRolService service;
        private readonly IConfigurationLib config;
        private readonly ICustomLog logger;


        public RolController()
        {
            service = DependencyFactory.GetInstance<IRolService>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        [Authorize]
        [Route("Get")]
        [HttpGet]
        public EResponseBase<Rol_Response_v1> Get()
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(null);
                    EResponseBase<Role> responseJSON = service.Get();
                    logger.Print_Response(responseJSON);
                    EResponseBase<Rol_Response_v1> response = Mapper.Map<EResponseBase<Rol_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<Rol_Response_v1>(config).setResponseBaseForException(ex);
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
        public EResponseBase<Rol_Response_v1> Get(int id)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(id);
                    EResponseBase<Role> responseJSON = service.Get(id);
                    logger.Print_Response(responseJSON);
                    EResponseBase<Rol_Response_v1> response = Mapper.Map<EResponseBase<Rol_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<Rol_Response_v1>(config).setResponseBaseForException(ex);
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
        public EResponseBase<Rol_Response_v1> RegisterOrUpdate([FromBody] Rol_Request_v1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(request);
                    Role requestConvert = Mapper.Map<Role>(request);
                    EResponseBase<Role> responseJSON = service.InsertOrUpdate(requestConvert);
                    logger.Print_Response(responseJSON);
                    EResponseBase<Rol_Response_v1> response = Mapper.Map<EResponseBase<Rol_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<Rol_Response_v1>(config).setResponseBaseForException(ex);
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
        public EResponseBase<Rol_Response_v1> DisabledOREnabled(int id, bool enabled)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(String.Format("id: {0},  enabled: {1}", id, enabled));
                    EResponseBase<Role> responseJSON = service.Disabled(id, enabled);
                    logger.Print_Response(responseJSON);
                    EResponseBase<Rol_Response_v1> response = Mapper.Map<EResponseBase<Rol_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<Rol_Response_v1>(config).setResponseBaseForException(ex);
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
