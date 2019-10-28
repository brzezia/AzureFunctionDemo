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
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System.Linq;

[assembly: WebJobsStartup(typeof(Startup))]
namespace AzureFunctionDemo
{
    public class CosmosDemoReaderV2
    {
        private readonly IConverter _converter;
        public CosmosDemoReaderV2(IConverter converter)
        {
            _converter = converter;
        }

        [FunctionName("CosmosDemoReaderV2")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            [CosmosDB(

                databaseName: "democardb",
                collectionName: "cars",
                ConnectionStringSetting = "CosmosDBconnString",
            Id ="{Query.id}")] Car  car,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return (ActionResult)new OkObjectResult(car);

        }
    }
}
