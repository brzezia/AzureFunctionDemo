using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunctionDemo;
using Microsoft.Azure.WebJobs.Hosting;
using AzureFunctionDemo.Model;
using AzureFunctionDemo.Mappers;

[assembly: WebJobsStartup(typeof(Startup))]
namespace AzureFunctionDemo
{
    public class CosmosDemoReceiver
    {
        private readonly IConverter _converter;
        public CosmosDemoReceiver(IConverter converter)
        {
            _converter = converter;
        }

        [FunctionName("CosmosDemo")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            var Car = await _converter.Convert<Car>(req);

            return (ActionResult)new OkObjectResult($"Ok");
        
        }
    }
}
