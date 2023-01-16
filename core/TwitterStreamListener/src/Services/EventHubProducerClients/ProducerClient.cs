using Azure.Messaging.EventHubs.Producer;

namespace Services.EventHubProducerClients;

public abstract class ProducerClient
{
    protected ProducerClient(string eventHubConnectionString, string eventHubName)
    {
        EventHubProducerClient = new EventHubProducerClient(eventHubConnectionString, eventHubName);
    }

    public EventHubProducerClient EventHubProducerClient { get; }
}