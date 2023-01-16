namespace Services.EventHubProducerClients;

public class HashTagReceivedProducerClient : ProducerClient
{
    public HashTagReceivedProducerClient(string eventHubConnectionString, string eventHubName) 
        : base(eventHubConnectionString, eventHubName)
    {
    }
}