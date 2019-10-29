using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AzureFunctionDemo.Model;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Extensions.Options;
using Microsoft.Rest;

namespace AzureFunctionDemo.AzureGrid
{
    public interface IEventGridSender<T>
    {
        Task<bool> SendAsync(Car car);
    }

    public class EventGridSender<T> : IEventGridSender<T>
    {
        private readonly EventGridConfig _options;
        public EventGridSender(IOptions<EventGridConfig> options)
        {
            _options = options.Value;
        }
        public async Task<bool> SendAsync(Car car)
        {
            var topicKey = _options.TopicKey;
            var credentials = new TopicCredentials(topicKey);

            var client = new EventGridClient(credentials);


            var events =  CreateEvent(car);

           var response = await client.PublishEventsWithHttpMessagesAsync(
                topicHostname: _options.TopicHostName,
                events: events
                );

            return response.Response.IsSuccessStatusCode;
        }

        private IList<EventGridEvent> CreateEvent(Car car)
        {

            return new List<EventGridEvent>
            {
              new EventGridEvent
              {
                  Id = car.Id.ToString("N"),
                  Data = car,
                  DataVersion = "1",
                  EventTime = DateTime.Now,
                  EventType = "CarCreated",
                  Subject = _options.Subject
              }
            };
        }
    }


}