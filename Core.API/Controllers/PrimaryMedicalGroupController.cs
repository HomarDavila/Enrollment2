using Audit.WebApi;
using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Core.API.Helpers;
using Core.API.Model.Request;
using Core.API.Model.Response;
using Domain.Custom_Models;
using Domain.Entity_Models;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Web.Http;

namespace Core.API.Controllers
{
    [RoutePrefix("api/PrimaryMedicalGroup/v1")]
    public class PrimaryMedicalGroupController : ApiController
    {
        private readonly IPrimaryMedicalGroupServices PrimaryMedicalGroupServices = DependencyFactory.GetInstance<IPrimaryMedicalGroupServices>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;

        public PrimaryMedicalGroupController()
        {
            PrimaryMedicalGroupServices = DependencyFactory.GetInstance<IPrimaryMedicalGroupServices>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        [Route("GetByPCPId")]
        [HttpPost]
        public EResponseBase<PmgResponseV1> GetByPCPId([FromBody] PmgRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            EResponseBase<PrimaryMedicalGroup> responseJSON;
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(null);
                    responseJSON = PrimaryMedicalGroupServices.GetByPCPId(request.PCPId, request.ShowForChangeEnrollmentProcess);
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
            PrimaryMedicalGroupServices.Transaction = RequestUtility.GetHeaders().Transaction;
            PrimaryMedicalGroupServices.Logger = logger;
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
