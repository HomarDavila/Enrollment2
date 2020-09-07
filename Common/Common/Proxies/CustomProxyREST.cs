using Common.Exceptions;
using Common.HttpHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.Proxy
{
    public class CustomProxyREST<K> : IDisposable where K : class, new()
    {
        private IConfigurationLib ConfigurationLib = null;
        public CustomProxyREST(IConfigurationLib _ConfigurationLib)
        {
            ConfigurationLib = _ConfigurationLib;
        }

        public async Task<EResponseBase<TokenResponse>> GetToken(
        string urlBase,
        string tokenPrefix,
        string transactionId,
        string origen,
        bool ignoreSSLCertificate,
        int secondsTimeOutWS,
        IEnumerable<KeyValuePair<string, string>> securityData)
        {
            try
            {
                string request = JsonConvert.SerializeObject(securityData);
                using (HttpClient httpClient = new HttpClient())
                {
                    using (FormUrlEncodedContent content = new FormUrlEncodedContent(securityData))
                    {
                        try
                        {
                            string url = ProxyBase.GetURL(urlBase, tokenPrefix);
                            content.Headers.Clear();
                            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                            HttpResponseMessage response = await httpClient.PostAsync(url, content);
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                string result = await response.Content.ReadAsStringAsync();
                                TokenResponse responseData = JsonConvert.DeserializeObject<TokenResponse>(result);
                                return new UtilitariesResponse<TokenResponse>(ConfigurationLib).setResponseBaseForOK(responseData);
                            }
                            else
                            {
                                string result = await response.Content.ReadAsStringAsync();
                                TokenErrorResponse responseData = JsonConvert.DeserializeObject<TokenErrorResponse>(result);
                                if (responseData.Error == AppConstants.TechnicalError)
                                {
                                    return new UtilitariesResponse<TokenResponse>(ConfigurationLib).setResponseBaseForExceptionUnexpected();
                                }
                                else
                                {
                                    int errorCode = Int32.Parse(responseData.ErrorDescription);
                                    string messageEs = ConfigurationLib.GetMessageEsByCode(errorCode);
                                    string messageEn = ConfigurationLib.GetMessageEnByCode(errorCode);
                                    SimpleEntity functionalError = new SimpleEntity()
                                    {
                                        Id = errorCode,
                                        NameEN = messageEn,
                                        NameES = messageEs
                                    };
                                    List<SimpleEntity> list = new List<SimpleEntity>() { functionalError };
                                    return new UtilitariesResponse<TokenResponse>(ConfigurationLib).setResponseBaseForValidationExceptionString(list);
                                }

                            }


                        }
                        catch (Exception ex)
                        {
                            return new UtilitariesResponse<TokenResponse>(ConfigurationLib).setResponseBaseForException(ex);
                        }
                    }
                }
            }
            catch (WSNotFoundException ex)
            {
                return new UtilitariesResponse<TokenResponse>(ConfigurationLib).setResponseBaseForException(ex);
            }
            catch (WSNotAuthorized ex)
            {
                return new UtilitariesResponse<TokenResponse>(ConfigurationLib).setResponseBaseForException(ex);
            }
            catch (Exception ex)
            {
                return new UtilitariesResponse<TokenResponse>(ConfigurationLib).setResponseBaseForException(ex);
            }
        }


        public async Task<EResponseBase<K>> Get(
            string urlBase,
            string servicePrefix,
            string controller,
            string action,
            string tokenType,
            string accessToken,
            string transactionId,
            string origen,
            bool ignoreSSLCertificate,
            int secondsTimeOutWS
            )
        {
            try
            {
                if (ignoreSSLCertificate) ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                using (HttpClient client = ProxyBase.GetClient(secondsTimeOutWS, urlBase, transactionId, origen, tokenType, accessToken))
                {
                    try
                    {
                        string url = ProxyBase.GetURL(urlBase, servicePrefix, controller, action);
                        HttpResponseMessage response = client.GetAsync(url).Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            ProxyBase.validResponse(response);
                            string result = await response.Content.ReadAsStringAsync();
                            EResponseBase<K> model = JsonConvert.DeserializeObject<EResponseBase<K>>(result);
                            return model;
                        }
                        else
                        {
                            if (response.StatusCode == HttpStatusCode.Unauthorized)
                                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForNoAuthorized();
                            else
                                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForExceptionUnexpected();
                        }

                    }
                    catch (Exception ex)
                    {
                        return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
                    }
                }
            }
            catch (WSNotFoundException ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
            catch (WSNotAuthorized ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
            catch (Exception ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
        }


        public async Task<EResponseBase<K>> Get(
             string urlBase,
             string servicePrefix,
             string controller,
             string action,
             string tokenType,
             string accessToken,
             string transactionId,
             string origen,
             bool ignoreSSLCertificate,
             int secondsTimeOutWS,
             string queryParams
             )
        {
            try
            {
                if (ignoreSSLCertificate) ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                using (HttpClient client = ProxyBase.GetClient(secondsTimeOutWS, urlBase, transactionId, origen, tokenType, accessToken))
                {
                    try
                    {
                        string url = ProxyBase.GetURL(urlBase, servicePrefix, controller, action, queryParams);
                        HttpResponseMessage response = client.GetAsync(url).Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            ProxyBase.validResponse(response);
                            string result = await response.Content.ReadAsStringAsync();
                            EResponseBase<K> model = JsonConvert.DeserializeObject<EResponseBase<K>>(result);
                            return model;
                        }
                        else
                        {
                            if (response.StatusCode == HttpStatusCode.Unauthorized)
                                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForNoAuthorized();
                            else
                                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForExceptionUnexpected();
                        }

                    }
                    catch (Exception ex)
                    {
                        return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
                    }
                }
            }
            catch (WSNotFoundException ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
            catch (WSNotAuthorized ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
            catch (Exception ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
        }

        public async Task<EResponseBase<K>> Get(
             string urlBase,
             string servicePrefix,
             string controller,
             string action,
             string tokenType,
             string accessToken,
             string transactionId,
             string origen,
             bool ignoreSSLCertificate,
             int secondsTimeOutWS,
             object id
             )
        {
            try
            {
                if (ignoreSSLCertificate) ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                using (HttpClient client = ProxyBase.GetClient(secondsTimeOutWS, urlBase, transactionId, origen, tokenType, accessToken))
                {
                    try
                    {
                        string url = ProxyBase.GetURL(urlBase, servicePrefix, controller, action, id);
                        HttpResponseMessage response = client.GetAsync(url).Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            ProxyBase.validResponse(response);
                            string result = await response.Content.ReadAsStringAsync();
                            EResponseBase<K> model = JsonConvert.DeserializeObject<EResponseBase<K>>(result);
                            return model;
                        }
                        else
                        {
                            if (response.StatusCode == HttpStatusCode.Unauthorized)
                                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForNoAuthorized();
                            else
                                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForExceptionUnexpected();
                        }
                    }
                    catch (Exception ex)
                    {
                        return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
                    }
                }
            }
            catch (WSNotFoundException ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
            catch (WSNotAuthorized ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
            catch (Exception ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
        }

        public async Task<EResponseBase<K>> Get(
             string urlBase,
             string servicePrefix,
             string controller,
             string action,
             string tokenType,
             string accessToken,
             string transactionId,
             string origen,
             bool ignoreSSLCertificate,
             int secondsTimeOutWS,
             int id,
             string phone
             )
        {
            try
            {
                if (ignoreSSLCertificate) ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                using (HttpClient client = ProxyBase.GetClient(secondsTimeOutWS, urlBase, transactionId, origen, tokenType, accessToken))
                {
                    try
                    {
                        string url = ProxyBase.GetURL(urlBase, servicePrefix, controller, action, id, phone);
                        HttpResponseMessage response = client.GetAsync(url).Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            ProxyBase.validResponse(response);
                            string result = await response.Content.ReadAsStringAsync();
                            EResponseBase<K> model = JsonConvert.DeserializeObject<EResponseBase<K>>(result);
                            return model;
                        }
                        else
                        {
                            if (response.StatusCode == HttpStatusCode.Unauthorized)
                                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForNoAuthorized();
                            else
                                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForExceptionUnexpected();
                        }
                    }
                    catch (Exception ex)
                    {
                        return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
                    }
                }
            }
            catch (WSNotFoundException ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
            catch (WSNotAuthorized ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
            catch (Exception ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CustomProxyREST() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class CustomProxyREST<T, K> : IDisposable where T : class, new() where K : class, new()
    {

        private IConfigurationLib ConfigurationLib = null;
        public CustomProxyREST(IConfigurationLib _ConfigurationLib)
        {
            ConfigurationLib = _ConfigurationLib;
        }

        public async Task<EResponseBase<K>> Post(
            string urlBase,
            string servicePrefix,
            string controller,
            string action,
            string tokenType,
            string accessToken,
            string transactionId,
            string origen,
            bool ignoreSSLCertificate,
            int secondsTimeOutWS,
            T model)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");
                using (HttpClient client = ProxyBase.GetClient(secondsTimeOutWS, urlBase, transactionId, origen, tokenType, accessToken))
                {
                    try
                    {
                        string url = ProxyBase.GetURL(urlBase, servicePrefix, controller, action);
                        HttpResponseMessage response = await client.PostAsync(url, content);
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            ProxyBase.validResponse(response);
                            string result = await response.Content.ReadAsStringAsync();
                            EResponseBase<K> modelResponse = JsonConvert.DeserializeObject<EResponseBase<K>>(result);
                            return modelResponse;
                        }
                        else
                        {
                            if (response.StatusCode == HttpStatusCode.Unauthorized)
                                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForNoAuthorized();
                            else
                                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForExceptionUnexpected();
                        }

                    }
                    catch (Exception ex)
                    {
                        return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
                    }
                }
            }
            catch (WSNotFoundException ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
            catch (WSNotAuthorized ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
            catch (Exception ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
        }


        public async Task<EResponseBase<K>> Put(
            string urlBase,
            string servicePrefix,
            string controller,
            string action,
            string tokenType,
            string accessToken,
            string transactionId,
            string origen,
            bool ignoreSSLCertificate,
            int secondsTimeOutWS,
            T model)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");
                using (HttpClient client = ProxyBase.GetClient(secondsTimeOutWS, urlBase, transactionId, origen, tokenType, accessToken))
                {
                    try
                    {
                        string url = ProxyBase.GetURL(urlBase, servicePrefix, controller, action);
                        HttpResponseMessage response = client.PutAsync(url, content).Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            ProxyBase.validResponse(response);
                            string result = await response.Content.ReadAsStringAsync();
                            EResponseBase<K> modelResponse = JsonConvert.DeserializeObject<EResponseBase<K>>(result);
                            return modelResponse;
                        }
                        else
                        {
                            if (response.StatusCode == HttpStatusCode.Unauthorized)
                                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForNoAuthorized();
                            else
                                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForExceptionUnexpected();
                        }

                    }
                    catch (Exception ex)
                    {
                        return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
                    }
                }
            }
            catch (WSNotFoundException ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
            catch (WSNotAuthorized ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
            catch (Exception ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
        }

        public async Task<EResponseBase<K>> Delete(
            string urlBase,
            string servicePrefix,
            string controller,
            string action,
            string tokenType,
            string accessToken,
            string transactionId,
            string origen,
            bool ignoreSSLCertificate,
            int secondsTimeOutWS,
            T model)
        {
            try
            {
                using (HttpClient client = ProxyBase.GetClient(secondsTimeOutWS, urlBase, transactionId, origen, tokenType, accessToken))
                {
                    try
                    {
                        string url = ProxyBase.GetURL(urlBase, servicePrefix, controller, action);
                        HttpResponseMessage response = client.DeleteAsync(url).Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            ProxyBase.validResponse(response);
                            string result = await response.Content.ReadAsStringAsync();
                            EResponseBase<K> modelResponse = JsonConvert.DeserializeObject<EResponseBase<K>>(result);
                            return modelResponse;
                        }
                        else
                        {
                            if (response.StatusCode == HttpStatusCode.Unauthorized)
                                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForNoAuthorized();
                            else
                                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForExceptionUnexpected();
                        }

                    }
                    catch (Exception ex)
                    {
                        return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
                    }
                }

            }
            catch (WSNotFoundException ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
            catch (WSNotAuthorized ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
            catch (Exception ex)
            {
                return new UtilitariesResponse<K>(ConfigurationLib).setResponseBaseForException(ex);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CustomProxyREST() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
