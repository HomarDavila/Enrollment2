using Common;
using Common.Logging;
using Common.Proxy;
using Core.API.Model.Response;
using Domain.Custom_Models;
using Service.Helpers;
using System.Threading.Tasks;

namespace Service.Proxy
{
    public class ProxyCoreAPI
    {
        public async Task<EResponseBase<MemberResponseV3>> GetPeopleByFilters(Transaction transaction, ICustomLog logger, IConfigurationLib configT, string token, MemberRequestV3 request)
        {
            CustomConfigurationLib config = (CustomConfigurationLib)configT;
            using (CustomProxyREST<MemberRequestV3, MemberResponseV3> service = new CustomProxyREST<MemberRequestV3, MemberResponseV3>(config))
            {
                logger.Print_InitMethod();
                logger.Info(string.Format("URL: {0} {1}",
                                       ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                        config.CoreAPI_ServicePreffix,
                                                        config.CoreAPI_MemberController,
                                                        config.CoreAPI_Member_ExistUser
                                                        ),
                                       "(GET)"));
                logger.Print_Request(null);
                EResponseBase<MemberResponseV3> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_MemberController,
                                                                           config.CoreAPI_Member_ExistUser,
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