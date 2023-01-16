using Tweetinvi.Events.V2;

namespace Services.Services;

public interface ITwitterService
{
    Task SampleStreamV2StartListeningAsync(
        EventHandler<TweetV2ReceivedEventArgs> onTweetReceived, CancellationToken cancellationToken = default);
}