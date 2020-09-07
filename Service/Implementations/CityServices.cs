using Common;
using Common.CustomExtensions;
using Common.Others;
using Domain.Custom_Models;
using MeditiWebApp.Proxy;
using Newtonsoft.Json;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class CityServices : ICityServices
    {
        private static readonly Common.Logging.CustomLogManager logger = new Common.Logging.CustomLogManager(log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));
        public string Transaction { get; set; }

        public EResponseBase<CitiesResponse> GetCities(string transactionId)
        {
            var result = new EResponseBase<CitiesResponse>();
            var proxy = new ProxyMCOConnector();
            List<CitiesResponse> responseConverted = new List<CitiesResponse>();
            IList<Mediti2.McoConnector.SimpleEntity_V1> responseMember = null;
            try
            {
                logger.Transaction = Transaction.GetGUID();
                logger.CustomDebug("Inicio de Bussiness Services ...");
                string dataRequest = JsonConvert.SerializeObject(null);
                logger.Print_Request(dataRequest, printDebug: true);

                logger.CustomDebug("Invocando WS:  ...");
                responseMember = proxy.GetCities();
                string dataResponseT = JsonConvert.SerializeObject(responseMember);
                logger.Print_Response(dataResponseT, printDebug: true);
                logger.CustomDebug("Fin de Invocacion a WS");

                logger.CustomDebug("Seteando Response ...");
                var responseT = setResponse(responseConverted, responseMember);
                result = UtilitariesResponse<CitiesResponse>.setResponseBaseForList(responseT.AsQueryable());

                string dataResponse = JsonConvert.SerializeObject(result);
                logger.Print_Response(dataResponse, printDebug: true);
                logger.CustomDebug("Fin de Bussiness Services ...");
                return result;
            }
            catch (Exception e)
            {
                result = UtilitariesResponse<CitiesResponse>.setResponseBaseForException(e);
                logger.CustomError(e.Message);
            }
            finally
            {
                proxy.Dispose();
                proxy = null;
            }
            return result;
        }

        private static List<CitiesResponse> setResponse(List<CitiesResponse> responseConverted, IList<Mediti2.McoConnector.SimpleEntity_V1> responseMember)
        {
            if (responseMember.Any())
            {
                foreach (var obj in responseMember)
                {
                    CitiesResponse temp = new CitiesResponse()
                    {
                        CityId = obj.ID,
                        Name = obj.Name
                    };
                    responseConverted.Add(temp);
                }
            }           
            return responseConverted;
        }
    }
}
