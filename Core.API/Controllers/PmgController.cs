using Audit.WebApi;
using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Core.API.Helpers;
using Core.API.Model.Response;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Web.Http;

namespace Core.API.Controllers
{
    //[AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
    [RoutePrefix("api/Pmg/v1")]
    public class PmgController : ApiController
    {
        private readonly IPmgServices pmgServices = DependencyFactory.GetInstance<IPmgServices>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;

        public PmgController()
        {
            pmgServices = DependencyFactory.GetInstance<IPmgServices>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        //[Authorize]
        [Route("Get/{ShowForChangeEnrollmentProcess}")]
        [HttpGet]
        public EResponseBase<PmgResponseV1> Get(bool ShowForChangeEnrollmentProcess)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(ShowForChangeEnrollmentProcess);
                    EResponseBase<Domain.Entity_Models.PrimaryMedicalGroup> responseJSON = pmgServices.Get(ShowForChangeEnrollmentProcess);
                    logger.Print_Response(responseJSON);
                    EResponseBase<PmgResponseV1> response = Mapper.Map<EResponseBase<PmgResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<PmgResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        private void ConfigureService()
        {
            pmgServices.Transaction = RequestUtility.GetHeaders().Transaction;
            pmgServices.Logger = logger;
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
