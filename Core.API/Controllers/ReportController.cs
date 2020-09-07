using Audit.WebApi;
using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Core.API.Helpers;
using Core.API.Model.Request;
using Core.API.Model.Response;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Web.Http;

namespace Core.API.Controllers
{
    //[AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
    [RoutePrefix("api/Report/v1")]
    public class ReportController : ApiController
    {
        private readonly IReportServices ReportServices = DependencyFactory.GetInstance<IReportServices>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;

        public ReportController()
        {
            ReportServices = DependencyFactory.GetInstance<IReportServices>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        //[Authorize]
        [Route("GetReportInscripciones")]
        [HttpPost]
        public EResponseBase<ReportResponseV1> GetReportInscripciones([FromBody] ReportRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<Domain.Entity_Models.EnrollmentHistory> responseJSON = ReportServices.GetReportInscripciones(request.Fecha, request.Inscripcion);
                    logger.Print_Response(responseJSON);
                    EResponseBase<ReportResponseV1> response = Mapper.Map<EResponseBase<ReportResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<ReportResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        //[Authorize]
        [Route("GetReportJustaCausa")]
        [HttpPost]
        public EResponseBase<ReportResponseV1> GetReportJustaCausa([FromBody] ReportRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<Domain.Entity_Models.EnrollmentHistory> responseJSON = ReportServices.GetReportInscripciones(request.Fecha, request.Inscripcion);
                    logger.Print_Response(responseJSON);
                    EResponseBase<ReportResponseV1> response = Mapper.Map<EResponseBase<ReportResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<ReportResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        private void ConfigureService()
        {
            ReportServices.Transaction = RequestUtility.GetHeaders().Transaction;
            ReportServices.Logger = logger;
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