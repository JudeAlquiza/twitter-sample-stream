using Models;

namespace Services.Services;

public interface IEventHubService
{
    Task SendTweetsToEventHubAsync(IList<TweetEventHubMessageModel> models, CancellationToken cancellationToken = default);

    Task SendHashTagsToEventHubAsync(IList<HashTagEventHubMessageModel> models, CancellationToken cancellationToken = default);
}