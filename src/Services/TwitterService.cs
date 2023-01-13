using Tweetinvi;
using Tweetinvi.Events.V2;

namespace Services;

public interface ITwitterService
{
    Task SampleStreamV2StartListeningAsync(
        EventHandler<TweetV2ReceivedEventArgs> onTweetReceived, CancellationToken cancellationToken = default); 
}

public class TwitterService : ITwitterService
{
    private readonly ITwitterClient _twitterClient;

    public TwitterService(ITwitterClient twitterClient)
    {
        _twitterClient = twitterClient;
    }

    public async Task SampleStreamV2StartListeningAsync(
        EventHandler<TweetV2ReceivedEventArgs> onTweetReceived, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var sampleStreamV2 = _twitterClient.StreamsV2.CreateSampleStream();
        sampleStreamV2.TweetReceived += onTweetReceived;

        await sampleStreamV2.StartAsync();
    }
}