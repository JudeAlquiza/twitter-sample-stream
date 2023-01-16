using Services.Services;
using System.Text.RegularExpressions;
using Models;
using Tweetinvi.Events.V2;

namespace Listener.Workers;

public class TwitterStreamListenerWorker : BackgroundService
{
    private readonly ITwitterService _twitterService;
    private readonly IEventHubService _eventHubService;
    private readonly ILogger<TwitterStreamListenerWorker> _logger;

    public TwitterStreamListenerWorker(
        ITwitterService twitterService, 
        IEventHubService eventHubService,
        ILogger<TwitterStreamListenerWorker> logger)
    {
        _twitterService = twitterService;
        _eventHubService = eventHubService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _twitterService.SampleStreamV2StartListeningAsync(OnTweetReceived, stoppingToken);

        _logger.LogInformation("Listening to Twitter Sample Stream V2...");
    }

    private async void OnTweetReceived(object? sender, TweetV2ReceivedEventArgs eventArgs)
    {
        _logger.LogInformation($"Received Tweet Id: {eventArgs.Tweet.Id}");

        try
        {
            var tweetEventHubMessageModel = new TweetEventHubMessageModel
            {
                TweetId = eventArgs.Tweet.Id,
                Text = eventArgs.Tweet.Text,
                CreatedAt = eventArgs.Tweet.CreatedAt
            };

            // List all hash tags.
            var hashTags = new List<HashTagEventHubMessageModel>();

            var regex = new Regex(@"#\w+");
            var matches = regex.Matches(eventArgs.Tweet.Text);
            foreach (var match in matches)
            {
                var matchString = match.ToString();

                if (!string.IsNullOrEmpty(matchString))
                {
                    hashTags.Add(new HashTagEventHubMessageModel
                    {
                        TweetId = eventArgs.Tweet.Id,
                        HashTag = matchString,
                        CreatedAt = eventArgs.Tweet.CreatedAt
                    });
                }
            }

            // This is so I don't get charge a lot of $$$$$ across
            // all my twitter stream project resources
            Thread.Sleep(1500);

            await _eventHubService.SendTweetsToEventHubAsync(new List<TweetEventHubMessageModel> { tweetEventHubMessageModel });
            await _eventHubService.SendHashTagsToEventHubAsync(hashTags);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred processing Tweet Id {eventArgs.Tweet.Id}, {ex.Message}");
        }
    }
}