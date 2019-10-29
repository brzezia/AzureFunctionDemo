using System;
using System.Collections.Generic;
using Microsoft.Azure.EventGrid.Models;

namespace AzureFunctionDemo.AzureGrid
{
    public class EventComposer<T>
    {
        public EventComposer()
        {
        }

        public IEnumerable<EventGridEvent> ComposeEvent(T data, string eventType, string subject)
        {
            var eventItem = new EventGridEvent
            {
                Subject = subject,
                EventType = eventType,
                EventTime = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                Data = data
            };

            return new EventGridEvent[] { eventItem };

        }
    }
}