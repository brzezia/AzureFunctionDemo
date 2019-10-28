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
    public class CosmosDemoReader
    {
        private readonly IConverter _converter;
        public CosmosDemoReader(IConverter converter)
        {
            _converter = converter;
        }

        [FunctionName("CosmosDemoReader")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            [CosmosDB(

                databaseName: "democardb",
                collectionName: "cars",
                ConnectionStringSetting = "CosmosDBconnString")
            ] DocumentClient  documentClient,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var collectionUri = UriFactory.CreateDocumentCollectionUri("democardb", "cars");
            var id =  _converter.ReadIdFromQuery(req);

            var query = documentClient.CreateDocumentQuery<Car>(collectionUri);
            var result2 = await query.Where(x => x.Id == id).AsDocumentQuery().ExecuteNextAsync<Car>();


            //var result = query.Select(x=>x);  also works

            return (ActionResult)new OkObjectResult(result2);

        }
    }
}
