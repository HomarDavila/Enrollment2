using Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Logging;
using Common.Proxy;
using Common.HttpHelpers;
using Service.Helpers;

namespace Service.Proxy
{
    public class ProxySecurityAPI
    {
        public async Task<EResponseBase<TokenResponse>> GetToken(CustomConfigurationLib config, string userName, string password, string clientID)
        {
            using (CustomProxyREST<TokenResponse> service = new CustomProxyREST<TokenResponse>(config))
            {
                string transactionId = string.Empty;
                string dataRequest = JsonConvert.SerializeObject(userName);
                var securityData = new List<KeyValuePair<string, string>>();
                securityData.Add(new KeyValuePair<string, string>("grant_type", config.Grant_type));
                securityData.Add(new KeyValuePair<string, string>("username", userName));
                securityData.Add(new KeyValuePair<string, string>("password", password));
                securityData.Add(new KeyValuePair<string, string>("client_Id", clientID));
                var response = await service.GetToken(config.SecurityAPI_URLBase,
                                                      config.SecurityAPI_TokenPrefix,
                                                      transactionId.GetTransaction().Id,
                                                      AppConstants.Web,
                                                      config.SecurityAPI_IgnoreSSL,
                                                      config.SecurityAPI_Timeout,
                                                      securityData);
                string dataResponse = JsonConvert.SerializeObject(response);
                return response;
            };
        }
        
    }
}