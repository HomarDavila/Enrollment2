using Audit.WebApi;
using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Core.API.Helpers;
using Core.API.Model.Request;
using Core.API.Model.Response;
using Domain.Entity_Models;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Web.Http;

namespace Core.API.Controllers
{
    //[AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
    [RoutePrefix("api/Speciality/v1")]
    public class SpecialityController : ApiController
    {
        private readonly ISpecialityServices specialityServices = DependencyFactory.GetInstance<ISpecialityServices>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;

        public SpecialityController()
        {
            specialityServices = DependencyFactory.GetInstance<ISpecialityServices>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        //[Authorize]
        [Route("Get/{ShowForChangeEnrollmentProcess}")]
        [HttpGet]
        public EResponseBase<SpecialityResponseV1> Get(bool ShowForChangeEnrollmentProcess)
        {
            CustomHeader header = ConfigureLogHeader();
            EResponseBase<Speciality> responseJSON;
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(null);
                    if (ShowForChangeEnrollmentProcess) responseJSON = specialityServices.Get(true);
                    else responseJSON = specialityServices.Get();
                    logger.Print_Response(responseJSON);
                    EResponseBase<SpecialityResponseV1> response = Mapper.Map<EResponseBase<SpecialityResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<SpecialityResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("GetByPCPId")]
        [HttpPost]
        public EResponseBase<SpecialityResponseV1> GetByPCPId([FromBody] PmgRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            EResponseBase<Speciality> responseJSON;
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(null);
                    responseJSON = specialityServices.GetByPCPId(request.PCPId, request.ShowForChangeEnrollmentProcess);
                    logger.Print_Response(responseJSON);
                    EResponseBase<SpecialityResponseV1> response = Mapper.Map<EResponseBase<SpecialityResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<SpecialityResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }


        private void ConfigureService()
        {
            specialityServices.Transaction = RequestUtility.GetHeaders().Transaction;
            specialityServices.Logger = logger;
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
