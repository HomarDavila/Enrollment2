using Common.Logging;
using Newtonsoft.Json;
using Sms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sms
{
    public class Sinch
    {
        public async Task<StatusSmsResponse> SendAndCheckSms( string number, int colaid)
        {
            try
            {
                number = number.Replace("(", "").Replace(")", "").Replace(" ","").Replace("-","");

                List<string> numbers = new List<string>();
                numbers.Add(number);

                string message = AppSettings.Message;
                message = message.Replace("{enlace}", $"{AppSettings.Enlace}{colaid}");
                var response = await SendSms(message, numbers);

                if (response == null)
                    return new StatusSmsResponse()
                    {
                        Type = "-1",
                        Batch_id = "0",
                        Total_message_count = 0,
                        Statuses = null
                    };

                var status = await StatusSms(response.Id);
                if (status.Statuses[0].Code == 401 || status.Statuses[0].Code == 0)
                {
                    return status;
                }
                return new StatusSmsResponse()
                {
                    Type = "-1",
                    Batch_id = "0",
                    Total_message_count = 0,
                    Statuses = null
                };
            }
            catch (Exception)
            {
                return new StatusSmsResponse()
                {
                    Type = "-1",
                    Batch_id = "0",
                    Total_message_count = 0,
                    Statuses = null
                };
            }
        }

        public async Task<SendResponse> SendSms(string message, List<string> numbers)
        {
            try
            {
                string codpais = "1";
                numbers = numbers.Where(x => !string.IsNullOrEmpty(x.Trim()) && x.Length == 10).Distinct().ToList();
                numbers = numbers.Select(x => codpais + x.Trim()).ToList();

                if (numbers.Count == 0)
                    return null;

                BodySinch body = new BodySinch
                {
                    to = numbers.ToArray(),
                    from = AppSettings.SinchNumberFrom,
                    body = message
                };


                string request = JsonConvert.SerializeObject(body);
                var content = new StringContent(request, Encoding.UTF8, "application/json");

                HttpClient http = new HttpClient
                {
                    BaseAddress = new Uri("https://api.clxcommunications.com")
                };

                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AppSettings.SinchToken);

                string url = $"/xms/v1/{AppSettings.SinchServiceplanId}/batches";

                var response = await http.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SendResponse>(result);
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                return null;
            }
        }

        public async Task<StatusSmsResponse> StatusSms(string IdSMS)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri("https://api.clxcommunications.com")
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AppSettings.SinchToken);

                StatusSmsResponse obj = new StatusSmsResponse();

                string url = $"/xms/v1/{AppSettings.SinchServiceplanId}/batches/{IdSMS}/delivery_report?type=full";
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                string result = await response.Content.ReadAsStringAsync();
                obj = JsonConvert.DeserializeObject<StatusSmsResponse>(result);

                do
                {
                    Thread.Sleep(1000);
                    response = await client.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    result = await response.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<StatusSmsResponse>(result);
                } while (obj.Statuses[0].Code == 400);

                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
