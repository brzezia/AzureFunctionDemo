using System;
using AzureFunctionDemo.AzureGrid;
using AzureFunctionDemo.Helper;
using AzureFunctionDemo.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunctionDemo.TimerTriggerAf
{
    public  class TimerTrigger
    {
        private readonly IEventGridSender<Bike> _eventGridSender;
        public TimerTrigger(IEventGridSender<Bike> eventGridSender)
        {
            _eventGridSender = eventGridSender;
        }
        [FunctionName("TimerTrigDemo")]
        public  void Run([TimerTrigger("*/600 * * * * *")]TimerInfo myTimer, ILogger log)
        {


            var bike = new Bike {
                Id = Guid.NewGuid(),
                Date = DateTime.Now.ToString(),
                Name = DataGenerator.GenerateName(6)
            };

            _eventGridSender.SendAsync(bike);

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
