namespace Services.HashTag;

public interface IHashTagService
{
    public Task<IList<string>> GetTop10HashTagsByHourWindowAsync(int hourWindow,
        CancellationToken cancellationToken = default);
}