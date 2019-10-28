using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Hosting;
using AzureFunctionDemo;
using AzureFunctionDemo.Mappers;
using AzureFunctionDemo.Model;

[assembly: WebJobsStartup(typeof(Startup))]
namespace AzureFunctionDemo.AzureGrid
{
    public class PublishToGrid
    {

        private readonly IEventGridSender<Car> _eventGridSender;
        private readonly IConverter _converter;
        public PublishToGrid(IConverter converter, IEventGridSender<Car> eventGridSender)
        {
            _converter = converter;
            _eventGridSender = eventGridSender;
        }
 
        [FunctionName("PublishToGrid")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var car = await _converter.ReadFromBody<Car>(req);
            car.Id = Guid.NewGuid();

            await _eventGridSender.SendAsync(car);

            return (ActionResult)new OkObjectResult($"Ok");
        }
    }
}
