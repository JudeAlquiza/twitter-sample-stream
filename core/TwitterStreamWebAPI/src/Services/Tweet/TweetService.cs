using Data;
using Microsoft.EntityFrameworkCore;
using Models.Tweet;

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

    public async Task<IList<TweetModel>> GetMostRecentTweetsAsync(int count, CancellationToken cancellationToken = default)
    {
        var mostRecentTweets 
            = await _twitterStreamDbContext.Tweets
                .OrderByDescending(t => t.CreatedAt)
                .Take(count)
                .Select(t => new TweetModel { Id = t.Id, TweetId = t.TweetId, Text = t.Text, CreatedAt = t.CreatedAt })
                .ToListAsync(cancellationToken);

        return mostRecentTweets;
    }
}