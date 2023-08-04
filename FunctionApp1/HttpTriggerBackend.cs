using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.IO;

namespace FunctionsDotNetDapr
{
    public static class HttpTriggerBackend
    {

        private static HttpClient httpClient = new HttpClient();

        [FunctionName("HttpTriggerBackend")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "HttpTriggerBackend")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("HttpTriggerWithDaprServiceInvocationbackendend Function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            return new OkObjectResult($"{requestBody}");
        }
    }
}
