using Data;
using Models.HashTag.GetTop10HashTagsByHourWindowEndpoint;

namespace Services.HashTag;

public class HashTagService : IHashTagService
{
    private readonly TwitterStreamDbContext _twitterStreamDbContext;

    public HashTagService(TwitterStreamDbContext twitterStreamDbContext)
    {
        _twitterStreamDbContext = twitterStreamDbContext;
    }

    public async Task<IList<HashTagModel>> GetTop10HashTagsByHourWindowAsync(int hourWindow, 
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var hashTags =
            await _twitterStreamDbContext.Procedures.SpGetTop10HashTagsByHourWindowAsync(hourWindow,
                cancellationToken: cancellationToken);

        return hashTags.Select(h 
            => new HashTagModel { HashTag = h.HashTag, HashTagCount = h.HashTagCount }).ToList();
    }
}