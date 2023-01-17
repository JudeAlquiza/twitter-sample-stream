using FastEndpoints;

namespace Models.Tweet;

public class GetMostRecentTweetsRequest
{
    const int maxCount = 20;

    private int _count = 10;

    [BindFrom("count")]
    public int Count
    {
        get => _count;
        set => _count = (value > maxCount) ? maxCount : value;
    }
}
