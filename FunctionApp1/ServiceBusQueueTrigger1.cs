using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionApp1
{
    public class ServiceBusQueueTrigger1
    {
        [FunctionName("ServiceBusQueueTrigger1")]
        [return: ServiceBus("myqueue2ForProcTest", Connection = "ServiceBusConnection")]
        public static string Run(
    [ServiceBusTrigger("myqueueForProcTest", Connection = "ServiceBusConnection")]
    string myQueueItem,
    Int32 deliveryCount,
    DateTime enqueuedTimeUtc,
    string messageId,
    ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            log.LogInformation($"EnqueuedTimeUtc={enqueuedTimeUtc}");
            log.LogInformation($"DeliveryCount={deliveryCount}");
            log.LogInformation($"MessageId={messageId}");
            return myQueueItem;
        }
    }
}
