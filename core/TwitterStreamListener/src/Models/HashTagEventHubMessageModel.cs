namespace Models;

public class HashTagEventHubMessageModel
{
    public string? TweetId { get; set; }

    public string? HashTag { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }
}