using Mediti2.WebConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Proxy
{
    public class ProxyMeditiConnector : IDisposable
    {
        WebMeditiConector_V1Client client = null;

        public void Dispose()
        {
            client = null;
        }

        public List<int> FindPerson(string firstName, string lastName1, string ssnLast4, DateTime dateOfBirth)
        {
            client = new WebMeditiConector_V1Client();
            try
            {
                var response = client.FindPerson(firstName, lastName1, ssnLast4, dateOfBirth);
                client.Close();
                return response.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                client = null;
            }
        }

        public bool IsSecurityAnswerCorrect(int personIdMediti, int securityQuestionId, string answered)
        {
            client = new WebMeditiConector_V1Client();
            try
            {
                var response = client.IsSecurityAnswerCorrect(personIdMediti, securityQuestionId, answered);
                client.Close();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                client = null;
            }
        }

    }
}
