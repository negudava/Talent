using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace TalentLeaveRound
{
    public static class TalentLeaveRound
    {
        [FunctionName("TalentLeaveRound")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            //JObject talentreq = null;
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            // Get request body
            dynamic data = await req.Content.ReadAsAsync<object>();
            double amount = data.Amount;
            amount = Math.Round(amount, 0);
            //12.54 == 12.51

            if (amount > 0)
            {
                responseMessage = req.CreateResponse(HttpStatusCode.OK);
                responseMessage.Content = new StringContent(JsonConvert.SerializeObject(amount), System.Text.Encoding.UTF8, "application/json");
            }
            return amount <= 0
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Not a valid request")
                : responseMessage;

        }
    }
}
