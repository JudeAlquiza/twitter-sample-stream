namespace Models;

public class TweetEventHubMessageModel
{
    public string? TweetId { get; set; }

    public string? Text { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}