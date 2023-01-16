using Azure.Messaging.EventHubs;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Services.EventHubProducerClients;
using System.Text;

namespace Services.Services;

public class EventHubService : IEventHubService
{
    private readonly TweetReceivedProducerClient _tweetReceivedProducerClient;
    private readonly HashTagReceivedProducerClient _hashTagReceivedProducerClient;
    private readonly ILogger<EventHubService> _logger;

    public EventHubService(
        TweetReceivedProducerClient tweetReceivedProducerClient,
        HashTagReceivedProducerClient hashTagReceivedProducerClient,
        ILogger<EventHubService> logger)
    {
        _tweetReceivedProducerClient = tweetReceivedProducerClient;
        _hashTagReceivedProducerClient = hashTagReceivedProducerClient;
        _logger = logger;
    }

    public async Task SendTweetsToEventHubAsync(IList<TweetEventHubMessageModel> models, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var eventBatch = await _tweetReceivedProducerClient.EventHubProducerClient.CreateBatchAsync(cancellationToken);

        foreach (var model in models)
        {
            var modelJson = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            if (!eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(modelJson))))
            {
                // Log error here or store this json to a persistent storage for further processing.
                var errorMessage = $"Event {modelJson} is too large for the batch and cannot be sent.";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }
        }

        try
        {
            await _tweetReceivedProducerClient.EventHubProducerClient.SendAsync(eventBatch, cancellationToken);
            _logger.LogInformation("Tweet event has been published.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public async Task SendHashTagsToEventHubAsync(IList<HashTagEventHubMessageModel> models, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var eventBatch = await _hashTagReceivedProducerClient.EventHubProducerClient.CreateBatchAsync(cancellationToken);

        foreach (var model in models)
        {
            var modelJson = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            if (!eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(modelJson))))
            {
                // Log error here or store this json to a persistent storage for further processing.
                var errorMessage = $"Event {modelJson} is too large for the batch and cannot be sent.";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }
        }

        try
        {
            await _hashTagReceivedProducerClient.EventHubProducerClient.SendAsync(eventBatch, cancellationToken);
            _logger.LogInformation("Hash tag event has been published.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new Exception(ex.Message);
        }
    }
}