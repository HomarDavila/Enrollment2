using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Domain.Entity_Models;
using Newtonsoft.Json;
using Security.API.Helpers;
using Security.API.Model.Request;
using Security.API.Model.Response;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Web.Http;

namespace Security.API.Controllers
{
    [RoutePrefix("api/identity/User/v1")]
    public class UserController : ApiController
    {
        private readonly IIdentityService serviceIdentityService;
        private readonly IUserService service;
        private readonly IConfigurationLib config;
        private readonly ICustomLog logger;


        public UserController()
        {

            serviceIdentityService = DependencyFactory.GetInstance<IIdentityService>();
            service = DependencyFactory.GetInstance<IUserService>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        [Authorize]
        [Route("Get")]
        [HttpGet]
        public EResponseBase<User_Response_v1> Get()
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(null);
                    EResponseBase<User> responseJSON = service.Get();
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
        [Route("GetByName")]
        [HttpPost]
        public EResponseBase<User_Response_v1> GetByName([FromBody] User_Request_Filters_v10 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<User> responseJSON = service.GetByUserName(request.UserName);
                    //logger.Print_Response(responseJSON);
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

        //[Authorize]
        [Route("GetById/{UserId}")]
        [HttpGet]
        public EResponseBase<User_Response_v1> GetById(int UserId)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(UserId);
                    EResponseBase<User> responseJSON = service.Get(UserId);
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
        [Route("Disabled/{id}/{enabled}")]
        [HttpGet]
        public EResponseBase<User_Response_v1> DisabledOREnabled(int id, bool enabled)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    string dataRequest = JsonConvert.SerializeObject(String.Format("id: {0},  enabled: {1}", id, enabled));
                    logger.Print_Request(dataRequest);
                    EResponseBase<User> responseJSON = service.Disabled(id, enabled);
                    string dataResponse = JsonConvert.SerializeObject(responseJSON);
                    logger.Print_Response(dataResponse);
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

        [Route("SetUser")]
        [HttpPost]
        public EResponseBase<User_Response_v1> SetUser([FromBody] User_Request_v1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    User oUser = new User
                    {
                        Id = request.Id,
                        FirstName = request.FirstName,
                        LastName1 = request.LastName1,
                        LastName2 = request.LastName2,
                        DateOfBirth = request.DateOfBirth,
                        //oUser.DateOfBirth = Convert.ToDateTime(request.DateOfBirth);
                        //oUser.DateOfBirth = Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", request.DateOfBirth));
                        SSNLast4 = request.SSNLast4,
                        PhoneNumber = request.PhoneNumber,
                        Email = request.Email,
                        OptIn = request.OptIn,
                        Email2 = request.Email2,
                        PhoneNumber2 = request.PhoneNumber2,
                        MPI = request.MPI
                    };
                    EResponseBase<User> responseJSON = service.InsertOrUpdate(oUser);
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
