using Audit.WebApi;
using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.HttpHelpers;
using Common.Logging;
using Core.API.Helpers;
using Core.API.Model.Response;
using Domain.Custom_Models;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Linq;
using System.Web.Http;

namespace Core.API.Controllers
{
    //[AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
    [RoutePrefix("api/Pcp/v1")]
    public class PcpController : ApiController
    {
        private readonly IPcpServices pcpServices = DependencyFactory.GetInstance<IPcpServices>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;

        public PcpController()
        {
            pcpServices = DependencyFactory.GetInstance<IPcpServices>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        //[Authorize]
        [Route("Get/{ShowForChangeEnrollmentProcess}")]
        [HttpGet]
        public EResponseBase<PersonPcpResponseV1> Get(bool ShowForChangeEnrollmentProcess)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(null);
                    EResponseBase<Domain.Entity_Models.PersonPrimaryCarePhysician> responseJSON = pcpServices.Get(ShowForChangeEnrollmentProcess);
                    logger.Print_Response(responseJSON);
                    EResponseBase<PersonPcpResponseV1> response = Mapper.Map<EResponseBase<PersonPcpResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<PersonPcpResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }

        }

        //[Authorize]
        [Route("GetByFilters")]
        [HttpPost]
        public EResponseBase<PcpResponseV1> Get([FromBody] PcpRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<PrimaryCarePhysicianCustomModel> responseJSON = pcpServices.Get(request.PcpFullName, request.NPI, request.SpecialityId, request.PmgId, request.ShowForChangeEnrollmentProcess, request.lst_McoId);
                    logger.Print_Response(responseJSON);
                    EResponseBase<PcpResponseV1> response = Mapper.Map<EResponseBase<PcpResponseV1>>(responseJSON);
                    if (response.listado != null) response.listado = response.listado.Any() ? response.listado.Take(100).ToList() : response.listado;//1198 max
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<PcpResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }

        }

        //[Authorize]
        [Route("GetByFiltersToList")]
        [HttpPost]
        public EResponseBase<PcpResponseV2> GetByFiltersToList([FromBody] PcpRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<PrimaryCarePhysicianCustomModel> responseJSON = pcpServices.GetByFiltersToList(request.PcpFullName, request.NPI, request.SpecialityId, request.PmgId, request.ShowForChangeEnrollmentProcess, request.lst_McoId, request.MunicipalityId);
                    logger.Print_Response(responseJSON);
                    EResponseBase<PcpResponseV2> response = Mapper.Map<EResponseBase<PcpResponseV2>>(responseJSON);
                    //if (response.listado != null) response.listado = response.listado.Any() ? response.listado.Take(100).ToList() : response.listado;//1198 max
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<PcpResponseV2>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }

        }

        private void ConfigureService()
        {
            pcpServices.Transaction = RequestUtility.GetHeaders().Transaction;
            pcpServices.Logger = logger;
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
