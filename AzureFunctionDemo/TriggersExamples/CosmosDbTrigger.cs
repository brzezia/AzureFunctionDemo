using System;
using System.Collections.Generic;
using System.Linq;
using AzureFunctionDemo.AzureGrid;
using AzureFunctionDemo.Model;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunctionDemo.TriggersExamples
{
    public class CosmosDbTrigger
    {
        private readonly IEventGridSender<Pet> _eventGridSender;
        public CosmosDbTrigger(IEventGridSender<Pet> eventGridSender)
        {
            _eventGridSender = eventGridSender;
        }
        [FunctionName("CosmosDbTrigger")]
        public void Run([CosmosDBTrigger(
            databaseName: "democardb",
            collectionName: "cars",
             CreateLeaseCollectionIfNotExists = true,
            ConnectionStringSetting = "CosmosDBconnString")]IReadOnlyList<Document> input, ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                var pet = new Pet { Id = Guid.NewGuid(), Name = "cosmosTrigger"};
                _eventGridSender.SendAsync(pet);
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
            }
        }
    }
}
