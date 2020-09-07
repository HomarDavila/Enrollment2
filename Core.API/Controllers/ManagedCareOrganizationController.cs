using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Core.API.Helpers;
using Core.API.Model.Response;
using Domain.Entity_Models;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Web.Http;

namespace Core.API.Controllers
{
    [RoutePrefix("api/ManagedCareOrganization/v1")]
    public class ManagedCareOrganizationController : ApiController
    {
        private readonly IManagedCareOrganizationServices ManagedCareOrganizationServices = DependencyFactory.GetInstance<IManagedCareOrganizationServices>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;
        // GET: ManagedCareOrganization
        public ManagedCareOrganizationController()
        {
            ManagedCareOrganizationServices = DependencyFactory.GetInstance<IManagedCareOrganizationServices>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        //[Authorize]
        [Route("Get")]
        [HttpGet]
        public EResponseBase<ManagedCareOrganizationResponseV1> Get()
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(null);
                    EResponseBase<ManagedCareOrganization> responseJSON = ManagedCareOrganizationServices.Get();
                    logger.Print_Response(responseJSON);
                    EResponseBase<ManagedCareOrganizationResponseV1> response = Mapper.Map<EResponseBase<ManagedCareOrganizationResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<ManagedCareOrganizationResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        private void ConfigureService()
        {
            ManagedCareOrganizationServices.Transaction = RequestUtility.GetHeaders().Transaction;
            ManagedCareOrganizationServices.Logger = logger;
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