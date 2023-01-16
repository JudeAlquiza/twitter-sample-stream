namespace Services.Tweet;

public interface ITweetService
{
    public Task<int> GetTotalNumberOfTweetsAsync(CancellationToken cancellationToken = default);
}