using Data;

namespace Services.HashTag;

public class HashTagService : IHashTagService
{
    private readonly TwitterStreamDbContext _twitterStreamDbContext;

    public HashTagService(TwitterStreamDbContext twitterStreamDbContext)
    {
        _twitterStreamDbContext = twitterStreamDbContext;
    }

    public async Task<IList<string>> GetTop10HashTagsByHourWindowAsync(int hourWindow, 
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var hashTags =
            await _twitterStreamDbContext.Procedures.SpGetTop10HashTagsByHourWindowAsync(hourWindow,
                cancellationToken: cancellationToken);

        return hashTags.Select(h => h.HashTag).ToList();
    }
}