using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Collections.Generic;

namespace FunctionApp1
{
    public static class HttpTriggerFrontend
    {
        private static string HttpProtocol = !System.Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME").Contains("localhost")  ? "https" : "http";


        private static HttpClient httpClient = new HttpClient();

        [FunctionName("HttpTriggerFrontend")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "HttpTriggerFrontend")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("HttpTriggerFrontend Function processed a request.");

            string HttpTriggerBackenURL = @$"{HttpProtocol}://" + System.Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME") + "/api/HttpTriggerBackend";

            var values = new Dictionary<string, string>();
            values.Add("Donuts", "Maple");
            var content = new FormUrlEncodedContent(values);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, HttpTriggerBackenURL)
            {
                Content = content
            };

            HttpResponseMessage response = await httpClient.SendAsync(request);
            string responseBody = await response.Content.ReadAsStringAsync();

            return new OkObjectResult(responseBody);
        }
    }
}
