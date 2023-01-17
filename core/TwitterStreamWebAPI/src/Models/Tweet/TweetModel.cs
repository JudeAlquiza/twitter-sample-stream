namespace Models.Tweet;

public class TweetModel
{
    public int Id { get; set; }

    public string? TweetId { get; set; }

    public string? Text { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}