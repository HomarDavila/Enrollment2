using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Common.Proxy
{
    public class ProxyBase
    {
        public static HttpClient GetClient(int secondsTimeOutWS, string urlBase, string transactionId, string origen, string tokenType, string accessToken)
        {
            HttpClient client = new HttpClient();
            if (!String.IsNullOrEmpty(tokenType) && !String.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
            }
            client.BaseAddress = new Uri(urlBase);
            client.Timeout = TimeSpan.FromSeconds(secondsTimeOutWS);
            if (!String.IsNullOrEmpty(transactionId)) client.DefaultRequestHeaders.Add(AppConstants.TransactionId, transactionId);
            if (!String.IsNullOrEmpty(origen)) client.DefaultRequestHeaders.Add(AppConstants.Origin, origen);
            return client;

        }

        public static string GetURL(string urlBase, string tokenPrefix)
        {
            return string.Format("{0}{1}", urlBase, tokenPrefix);
        }
        public static string GetURL(string urlBase, string servicePrefix, string controller, string action)
        {
            return string.Format("{0}{1}{2}{3}", urlBase, servicePrefix, controller, action);
        }
        public static string GetURL(string urlBase, string servicePrefix, string controller, string action, object id)
        {
            return string.Format(
                    "{0}{1}{2}{3}/{4}",
                    urlBase,
                    servicePrefix,
                    controller,
                    action,
                    id);
        }

        public static string GetURL(string urlBase, string servicePrefix, string controller, string action, int MemberId, string phone)
        {
            return string.Format(
                    "{0}{1}{2}{3}/{4}/{5}",
                    urlBase,
                    servicePrefix,
                    controller,
                    action,
                    MemberId,
                    phone);
        }

        public static string GetURL(string urlBase, string servicePrefix, string controller, string action, string param, bool isQueryParam = false)
        {
            if (isQueryParam)
            {
                return string.Format(
                    "{0}{1}{2}{3}?{4}",
                    urlBase,
                    servicePrefix,
                    controller,
                    action,
                    param);
            }
            else
            {
                return string.Format(
                                   "{0}{1}{2}{3}/{4}",
                                   urlBase,
                                   servicePrefix,
                                   controller,
                                   action,
                                   param);
            }
        }
        public static void validResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new WSNotFoundException("URL no valida o no encontrada");
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new WSNotAuthorized("Usuario no autorizado.");
            }
            if (response.StatusCode >= System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Ocurrio un error interno en la llamada al servicio");
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Ocurrio un error interno en la llamada al servicio");
            }
        }
    }
}
