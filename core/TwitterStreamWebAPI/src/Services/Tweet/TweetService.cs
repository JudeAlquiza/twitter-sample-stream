using Data;
using Microsoft.EntityFrameworkCore;

namespace Services.Tweet;

public class TweetService : ITweetService
{
    private readonly TwitterStreamDbContext _twitterStreamDbContext;

    public TweetService(TwitterStreamDbContext twitterStreamDbContext)
    {
        _twitterStreamDbContext = twitterStreamDbContext;
    }

    public async Task<int> GetTotalNumberOfTweetsAsync(CancellationToken cancellationToken = default)
    {
        var totalNumberOfTweets = await _twitterStreamDbContext.Tweets.CountAsync(cancellationToken);

        return totalNumberOfTweets;
    }
}