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
using Service.Interfaces.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Security.API.Controllers
{
    [RoutePrefix("api/identity/UserRol/v1")]
    public class UserRolController : ApiController
    {
        private readonly IUserRolService service;
        private readonly IApplicationService service2;
        private readonly IRolService service3;
        private readonly IUserService service4;
        private readonly IConfigurationLib config;
        private readonly ICustomLog logger;


        public UserRolController()
        {
            service = DependencyFactory.GetInstance<IUserRolService>();
            service2 = DependencyFactory.GetInstance<IApplicationService>();
            service3 = DependencyFactory.GetInstance<IRolService>();
            service4 = DependencyFactory.GetInstance<IUserService>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }


        [Authorize]
        [Route("Get")]
        [HttpGet]
        public EResponseBase<UserRol_Response_v1> Get()
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(null);
                    EResponseBase<UserRol> responseJSON = service.Get();
                    logger.Print_Response(responseJSON);
                    EResponseBase<UserRol_Response_v1> response = Mapper.Map<EResponseBase<UserRol_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<UserRol_Response_v1>(config).setResponseBaseForException(ex);
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
        public EResponseBase<UserRol_Response_v1> Get(int id)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(id);
                    EResponseBase<UserRol> responseJSON = service.Get(id);
                    EResponseBase<UserRol_Response_v1> response = Mapper.Map<EResponseBase<UserRol_Response_v1>>(responseJSON);
                    if (response.Code == config.CodigoExito)
                    {
                        response.objeto.Application = Mapper.Map<EResponseBase<Application_Response_v1>>(service2.Get(response.objeto.ApplicationId)).objeto;
                        response.objeto.Rol = Mapper.Map<EResponseBase<Rol_Response_v1>>(service3.Get(response.objeto.RoleId)).objeto;
                        response.objeto.User = Mapper.Map<EResponseBase<User_Response_v1>>(service4.Get(response.objeto.UserId)).objeto;
                    }
                    logger.Print_Response(response);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<UserRol_Response_v1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }


        [Route("RegisterOrUpdate")]
        [HttpPost]
        public EResponseBase<UserRol_Response_v1> RegisterOrUpdate([FromBody] UserRol_Request_v1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(request);
                    UserRol requestConvert = Mapper.Map<UserRol>(request);
                    EResponseBase<UserRol> responseJSON = service.InsertOrUpdate(requestConvert);
                    logger.Print_Response(responseJSON);
                    EResponseBase<UserRol_Response_v1> response = Mapper.Map<EResponseBase<UserRol_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<UserRol_Response_v1>(config).setResponseBaseForException(ex);
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
        public EResponseBase<UserRol_Response_v1> DisabledOREnabled(int id, bool enabled)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(String.Format("id: {0},  enabled: {1}", id, enabled));
                    EResponseBase<UserRol> responseJSON = service.Disabled(id, enabled);
                    logger.Print_Response(responseJSON);
                    EResponseBase<UserRol_Response_v1> response = Mapper.Map<EResponseBase<UserRol_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<UserRol_Response_v1>(config).setResponseBaseForException(ex);
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
            service2.Transaction = RequestUtility.GetHeaders().Transaction;
            service3.Transaction = RequestUtility.GetHeaders().Transaction;
            service4.Transaction = RequestUtility.GetHeaders().Transaction;
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
