using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using AzureFunctionDemo.AzureGrid;
using EventJson;
using AzureFunctionDemo.Model;
using AzureFunctionDemo.Helper;

namespace AzureFunctionDemo.AzureGrid
{
    public class CustomEventGridSender<T> : IEventGridSender<T> where T: IEntity
    {

        private readonly IHttpClientFactory _clientFactory;
        protected readonly EventGridConfig _configuration;
        protected readonly ILogger _logger;
        public CustomEventGridSender(IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory, IOptions<EventGridConfig> configuration)
        {
            _clientFactory = httpClientFactory;
            _configuration = configuration.Value;
            _logger = loggerFactory.CreateLogger("CustomEventSender");
        }
        public async Task<bool> SendAsync(T data)
        {
            var client = _clientFactory.CreateClient("event-sender");

            var eventOutput = new EventComposer<T>()
                .ComposeEvent(data, $"{typeof(T).Name}_Event", /* _configuration.Subject*/ DataGenerator.GenerateName(8));

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("aeg-sas-key", _configuration.TopicKey);

            var url = $"https://{_configuration.TopicHostName}";
            client.BaseAddress = new Uri(url);

            var response = await client.PostAsync("api/events", new JsonContent(eventOutput));
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"Event sending failure: {response.ReasonPhrase}");
                return false;
            }
            _logger.LogInformation("Event was sent");
            return true;
        }

    }
}
