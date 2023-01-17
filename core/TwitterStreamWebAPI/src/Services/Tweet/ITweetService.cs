using Models.Tweet;

namespace Services.Tweet;

public interface ITweetService
{
    public Task<int> GetTotalNumberOfTweetsAsync(CancellationToken cancellationToken = default);

    public Task<IList<TweetModel>> GetMostRecentTweetsAsync(int count, CancellationToken cancellationToken = default);
}