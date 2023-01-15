using Services;
using Tweetinvi.Events.V2;

namespace Listener;

public class Worker : BackgroundService
{
    private readonly ITwitterService _twitterService;
    private readonly ILogger<Worker> _logger;

    public Worker(ITwitterService twitterService, ILogger<Worker> logger)
    {
        _twitterService = twitterService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // By default, the Console's output encoding is ASCII and some tweets are 
        // at a different language, hence different set of characters and will appear 
        // as '?' in the feed, we set this to UTF8 to make sure those other characters
        // are displayed properly.
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("Listening to Twitter Sample Stream V2");

        await _twitterService.SampleStreamV2StartListeningAsync(OnTweetReceived, stoppingToken);
    }

    private static void OnTweetReceived(object? sender, TweetV2ReceivedEventArgs eventArgs)
    {
        Console.WriteLine("--------------- Tweet Received ---------------");

        Console.WriteLine($"Tweet Id: {eventArgs.Tweet.Id}");
        Console.WriteLine($"Tweet Id: {eventArgs.Tweet}");
        Thread.Sleep(2000);

        Console.WriteLine("----------------------------------------------\n");
    }
}