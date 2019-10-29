using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AzureFunctionDemo.AzureGrid
{
    public class EventGridConfig
    {
        public string TopicKey { get; set; }
        public string TopicHostName{ get; set; }
        public string Subject { get; set; }

        //for custom event grid sender with HttpClient
        public string EventKey { get; internal set; }
        public string EventBaseAddress { get; internal set; }
    }
}