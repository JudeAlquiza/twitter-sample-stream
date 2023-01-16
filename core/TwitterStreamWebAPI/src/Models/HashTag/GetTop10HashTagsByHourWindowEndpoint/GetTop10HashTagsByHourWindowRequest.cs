using FastEndpoints;

namespace Models.HashTag.GetTop10HashTagsByHourWindowEndpoint;

public class GetTop10HashTagsByHourWindowRequest
{
    [BindFrom("hourWindow")] 
    public int HourWindow { get; set; } = 1;
}