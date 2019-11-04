using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunctionDemo.TriggersExamples
{
    public static class QueueTrigger
    {
        [FunctionName("QueueTrigger")]
        [StorageAccount("StorageAccount")]
        public static void Run([QueueTrigger("myqueue-items"/*Connection = "StorageAccount"*/)]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
