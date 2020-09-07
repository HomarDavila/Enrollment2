using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.HttpHelpers;
using Common.Logging;
using Domain.Custom_Models;
using Domain.Entity_Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Security.API.Helpers;
using Security.API.Model.Request;
using Security.API.Model.Response;
using Service;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;

namespace Security.API.Controllers
{
    [RoutePrefix("api/identity/Identity/v1")]
    public class IdentityController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }
        private readonly IIdentityService service;
        private readonly IConfigurationLib config;
        private readonly ICustomLog logger;

        public IdentityController()
        {
            service = DependencyFactory.GetInstance<IIdentityService>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        [Route("ExistUser")]
        [HttpPost]
        public EResponseBase<SimpleEntity> ExistUser([FromBody] User_Request_Filters_v1 user)
        {
            CustomHeader customHeader = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(customHeader)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(user);
                    bool responseJSON = Task.Run(() => service.ExistUser(user.UserName)).Result;
                    logger.Print_Response(responseJSON);
                    if (responseJSON) return new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForOK();
                    else return new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForNoDataFound();
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("Register")]
        [HttpPost]
        public EResponseBase<SimpleEntity> Register([FromBody] User_Request_v1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    request.Enabled = true;
                    request.CreatedBy = "System";
                    request.CreatedOn = DateTime.Now;
                    logger.Print_Request(request);

                    User requestConvert = Mapper.Map<User>(request);

                    EResponseBase<SimpleEntity> responseJSON = Task.Run(() => service.Register(requestConvert)).Result;
                    logger.Print_Response(responseJSON);
                    return responseJSON;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("ResetPassword")]
        [HttpPost]
        public EResponseBase<User_Response_v2> ResetPassword([FromBody] User_Request_Filters_v8 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(request);
                    Guid g = Guid.NewGuid();
                    string newPassword = g.ToString().Substring(1, 8);
                    EResponseBase<User> responseJSON = Task.Run(() => service.ResetPassword(request.UserName, newPassword)).Result;
                    logger.Print_Response(responseJSON);
                    EResponseBase<User_Response_v2> response = Mapper.Map<EResponseBase<User_Response_v2>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<User_Response_v2>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Authorize]
        [Route("ChangePassword")]
        [HttpPost]
        public EResponseBase<User_Response_v1> ChangePassword([FromBody] User_Request_Filters_v4 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<User> responseJSON = Task.Run(() => service.ChangePassword(request.Id, request.OldPassword, request.NewPassword)).Result;
                    logger.Print_Response(responseJSON);
                    EResponseBase<User_Response_v1> response = Mapper.Map<EResponseBase<User_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<User_Response_v1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("ChangePasswordExternal")]
        [HttpPost]
        public EResponseBase<User_Response_v1> ChangePasswordExternal([FromBody] User_Request_Filters_v12 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<User> responseJSON = Task.Run(() => service.ChangePasswordExternal(request.UserName, request.MPI, request.NewPassword)).Result;
                    logger.Print_Response(responseJSON);
                    EResponseBase<User_Response_v1> response = Mapper.Map<EResponseBase<User_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<User_Response_v1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }


        [Authorize]
        [Route("GetRolesByUserId")]
        [HttpPost]
        public EResponseBase<Rol_Response_v1> GetRolesByUserId([FromBody] User_Request_Filters_v2 request)
        {

            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<Role> responseJSON = service.GetRolesByUserId(request.UserId, request.ApplicationCode);
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
        [Route("GetOptionsByUserId")]
        [HttpPost]
        public EResponseBase<OptionRol_Response_v2> GetOptionsByUserId([FromBody] User_Request_Filters_v9 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<Domain.Entity_Models.Identity.OptionRol> responseJSON = service.GetOptionsByUserId(request.UserId, request.ApplicationCode);
                    logger.Print_Response(responseJSON);
                    EResponseBase<OptionRol_Response_v2> response = Mapper.Map<EResponseBase<OptionRol_Response_v2>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<OptionRol_Response_v2>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Authorize]
        [Route("HavePermissions")]
        [HttpPost]
        public EResponseBase<SimpleEntity> HavePermissions([FromBody] User_Request_Filters_v3 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(request);
                    bool responseJSON = service.HavePermissions(request.OptionCode, request.RolId, request.ApplicationCode);
                    logger.Print_Response(responseJSON);
                    if (responseJSON) return new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForOK();
                    else return new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForNoDataFound();
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForException(ex);
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
            service._userManager = UserManager;
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
