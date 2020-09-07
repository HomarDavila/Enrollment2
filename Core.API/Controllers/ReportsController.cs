using Audit.WebApi;
using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Core.API.Helpers;
using Core.API.Model;
using Core.API.Model.Response;
using CoreAPI.Common;
using Domain.Custom_Models;
using Domain.Entity_Models;
using Renci.SshNet;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;

namespace Core.API.Controllers
{
    //[AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
    [RoutePrefix("api/Reports/v1")]
    public class ReportsController : ApiController
    {
        private readonly IReportsServices reportsServices = DependencyFactory.GetInstance<IReportsServices>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;

        public ReportsController()
        {
            reportsServices = DependencyFactory.GetInstance<IReportsServices>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        [Route("GetReportsWeb")]
        [HttpPost]
        public EResponseBase<StatisticsResponseV1> GetReportsWeb([FromBody] StatisticsRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<EnrollmentStatistics> responseJSON = reportsServices.GetReportsWeb(request);
                    logger.Print_Response(responseJSON);
                    EResponseBase<StatisticsResponseV1> response = Mapper.Map<EResponseBase<StatisticsResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<StatisticsResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("InsertStatistic")]
        [HttpPost]
        public EResponseBase<StatisticsResponseV1> InsertStatistic([FromBody] StatisticsRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<EnrollmentStatistics> responseJSON = reportsServices.InsertStatistic(request);
                    logger.Print_Response(responseJSON);
                    EResponseBase<StatisticsResponseV1> response = Mapper.Map<EResponseBase<StatisticsResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<StatisticsResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        private void ConfigureService()
        {
            reportsServices.Transaction = RequestUtility.GetHeaders().Transaction;
            reportsServices.Logger = logger;
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