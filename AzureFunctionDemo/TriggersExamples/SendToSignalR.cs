using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace AzureFunctionDemo.TriggersExamples
{
    public static class SendToSignalR
    {
        [FunctionName("SendToSignalR")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous,"post", Route = null)] object req,
            [SignalR(HubName ="chat")] IAsyncCollector<SignalRMessage> signalMessages,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

           await signalMessages.AddAsync(
                new SignalRMessage()
                {
                    Target = "DemoMessage",
                    Arguments = new[] { req }
                });

            return new OkResult();

        }
    }
}
