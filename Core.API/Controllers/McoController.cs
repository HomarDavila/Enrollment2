using Audit.WebApi;
using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.HttpHelpers;
using Common.Logging;
using Core.API.Helpers;
using Domain.Custom_Models;
using Newtonsoft.Json;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Web.Http;

namespace Core.API.Controllers
{
    //[AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
    [RoutePrefix("api/Mco/v1")]
    public class McoController : ApiController
    {
        private readonly IMcoServices mcoServices = DependencyFactory.GetInstance<IMcoServices>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;

        public McoController()
        {
            mcoServices = DependencyFactory.GetInstance<IMcoServices>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        //[Authorize]
        [Route("Get/{showEnrollmentProcess}")]
        [HttpGet]
        public EResponseBase<McoResponseV1> Get(bool showEnrollmentProcess = false)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(showEnrollmentProcess);
                    EResponseBase<Domain.Entity_Models.ManagedCareOrganization> responseJSON = mcoServices.Get(showEnrollmentProcess);
                    logger.Print_Response(responseJSON);
                    EResponseBase<McoResponseV1> response = Mapper.Map<EResponseBase<McoResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<McoResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        private void ConfigureService()
        {
            mcoServices.Transaction = RequestUtility.GetHeaders().Transaction;
            mcoServices.Logger = logger;
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
