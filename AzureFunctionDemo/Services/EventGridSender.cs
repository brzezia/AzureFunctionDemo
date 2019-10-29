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
        Task<bool> SendAsync(T car);
    }

    public class EventGridSender<T> : IEventGridSender<T> where T: IEntity
    {
        private readonly EventGridConfig _options;
        public EventGridSender(IOptions<EventGridConfig> options)
        {
            _options = options.Value;
        }
        public async Task<bool> SendAsync(T entity)
        {
            var topicKey = _options.TopicKey;
            var credentials = new TopicCredentials(topicKey);

            var client = new EventGridClient(credentials);

            var events =  CreateEvent(entity);

           var response = await client.PublishEventsWithHttpMessagesAsync(
                topicHostname: _options.TopicHostName,
                events: events
                );

            return response.Response.IsSuccessStatusCode;
        }

        private IList<EventGridEvent> CreateEvent(T entity)
        {

            return new List<EventGridEvent>
            {
              new EventGridEvent
              {
                  Id = entity.Id.ToString("N"),
                  Data = entity,
                  DataVersion = "1",
                  EventTime = DateTime.Now,
                  EventType = "CarCreated",
                  Subject = _options.Subject
              }
            };
        }
    }


}