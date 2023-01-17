using Models.HashTag.GetTop10HashTagsByHourWindowEndpoint;

namespace Services.HashTag;

public interface IHashTagService
{
    public Task<IList<HashTagModel>> GetTop10HashTagsByHourWindowAsync(int hourWindow,
        CancellationToken cancellationToken = default);
}