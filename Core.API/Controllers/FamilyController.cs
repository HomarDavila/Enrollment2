
using Audit.WebApi;
using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Core.API.Helpers;
using Domain.Custom_Models;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Web.Http;

namespace Core.API.Controllers
{
    //[AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
    [RoutePrefix("api/Family/v1")]
    public class FamilyController : ApiController
    {
        private readonly IFamilyServices familyServices = DependencyFactory.GetInstance<IFamilyServices>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;

        public FamilyController()
        {
            familyServices = DependencyFactory.GetInstance<IFamilyServices>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        //[Authorize]
        [Route("Get")]
        [HttpGet]
        public EResponseBase<FamilyResponseV1> Get()
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(null);
                    EResponseBase<Domain.Entity_Models.Family> responseJSON = familyServices.Get();
                    logger.Print_Response(responseJSON);
                    EResponseBase<FamilyResponseV1> response = Mapper.Map<EResponseBase<FamilyResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<FamilyResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        //[Authorize]
        [Route("GetById/{FamilyId}")]
        [HttpGet]
        public EResponseBase<FamilyResponseV1> Get(int FamilyId)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(FamilyId);
                    EResponseBase<Domain.Entity_Models.Family> responseJSON = familyServices.Get(FamilyId);
                    logger.Print_Response(responseJSON);
                    EResponseBase<FamilyResponseV1> response = Mapper.Map<EResponseBase<FamilyResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<FamilyResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        private void ConfigureService()
        {
            familyServices.Transaction = RequestUtility.GetHeaders().Transaction;
            familyServices.Logger = logger;
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
