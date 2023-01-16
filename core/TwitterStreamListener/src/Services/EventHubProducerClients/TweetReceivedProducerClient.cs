namespace Services.EventHubProducerClients;

public class TweetReceivedProducerClient : ProducerClient
{
    public TweetReceivedProducerClient(string eventHubConnectionString, string eventHubName) 
        : base(eventHubConnectionString, eventHubName)
    {
    }
}