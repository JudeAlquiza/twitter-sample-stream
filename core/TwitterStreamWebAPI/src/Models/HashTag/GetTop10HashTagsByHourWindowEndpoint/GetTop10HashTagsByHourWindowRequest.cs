using FastEndpoints;

namespace Models.HashTag.GetTop10HashTagsByHourWindowEndpoint;

public class GetTop10HashTagsByHourWindowRequest
{
    const int maxHourWindow = 24;

    private int _hourWindow = 12;

    [BindFrom("hourWindow")]
    public int HourWindow
    {
        get => _hourWindow;
        set => _hourWindow = (value > maxHourWindow) ? maxHourWindow : value;
    }
}