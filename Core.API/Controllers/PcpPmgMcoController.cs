using Audit.WebApi;
using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Core.API.Helpers;
using Core.API.Model;
using Core.API.Model.Response;
using Domain.Entity_Models;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Web.Http;

namespace Core.API.Controllers
{
    //[AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
    [RoutePrefix("api/PcpPmgMco/v1")]
    public class PcpPmgMcoController : ApiController
    {
        private readonly IPcpPmgMcoServices PcpPmgMcoServices = DependencyFactory.GetInstance<IPcpPmgMcoServices>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;
        // GET: PcpPmgMco
        public PcpPmgMcoController()
        {
            PcpPmgMcoServices = DependencyFactory.GetInstance<IPcpPmgMcoServices>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        //[Authorize]
        [Route("GetPcpPmgMco")]
        [HttpPost]
        public EResponseBase<PcpPmgMcoResponseV1> GetPcpPmgMco(PcpPmgMcoRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<PcpPmgMco> responseJSON = PcpPmgMcoServices.GetPcpPmgMco(request.McoId, request.PmgId, request.PcpId);
                    logger.Print_Response(responseJSON);
                    EResponseBase<PcpPmgMcoResponseV1> response = Mapper.Map<EResponseBase<PcpPmgMcoResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<PcpPmgMcoResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        private void ConfigureService()
        {
            PcpPmgMcoServices.Transaction = RequestUtility.GetHeaders().Transaction;
            PcpPmgMcoServices.Logger = logger;
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