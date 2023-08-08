using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionApp1
{
    public static class EventHubTrigger2
    {
        [FunctionName("EventHubTrigger2")]
        public static async Task Run([EventHubTrigger("eventhub1", Connection = "EventHubConn2")] EventData[] events,
        [EventHub("eventhub2", Connection = "EventHubConn2")] IAsyncCollector<EventData> outputEvents,
            ILogger log)
        {
            foreach (EventData eventData in events)
            {
                // Do some processing:
                string newEventBody = eventData.SequenceNumber.ToString();

                log.LogInformation($"EventHubTrigger1 function processed a message: {newEventBody}");

                // Queue the message to be sent in the background by adding it to the collector.
                // If only the event is passed, an Event Hubs partition to be be assigned via
                // round-robin for each batch.
await outputEvents.AddAsync(new EventData(newEventBody));

                // If your scenario requires that certain events are grouped together in an
                // Event Hubs partition, you can specify a partition key.  Events added with 
                // the same key will always be assigned to the same partition.        
                await outputEvents.AddAsync(new EventData(newEventBody), "sample-key");
            }
        }
    }
}
