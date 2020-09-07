using Common;
using Common.HttpHelpers;
using Common.Logging;
using Common.Proxy;
using Core.API.Model;
using Core.API.Model.Response;
using Domain.Custom_Models;
using EnrollmentPrincipalWebApp.Helpers;
using System;
using System.Threading.Tasks;

namespace EnrollmentPrincipalWebApp.Proxy
{
    public class ProxyCoreAPI
    {
        public async Task<EResponseBase<StatisticsResponseV1>> InsertStatistic(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, StatisticsRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<StatisticsRequestV1, StatisticsResponseV1> service = new CustomProxyREST<StatisticsRequestV1, StatisticsResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_ReportsController,
                                                            config.CoreAPI_Reports_InsertStatistic
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<StatisticsResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_ReportsController,
                                                                           config.CoreAPI_Reports_InsertStatistic,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };

            }
        }
    }
}