using Bogus;
using Data;
using Microsoft.EntityFrameworkCore;
using Services.Tweet;

namespace UnitTests;

public class TweetServiceTests
{
    [Fact]
    public async Task GetTotalNumberOfTweetsAsyncIsCalled_ReturnTotalNumberOfTweetsAsync()
    {
        // Arrange
        var builder = new DbContextOptionsBuilder<TwitterStreamDbContext>();
        builder.UseInMemoryDatabase(nameof(GetTotalNumberOfTweetsAsyncIsCalled_ReturnTotalNumberOfTweetsAsync));
        var dbContext = new TwitterStreamDbContext(builder.Options);

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();

        var faker = new Faker();
        var sampleTweets = new List<Tweet>
        {
            new() { Id = 1, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-1) },
            new() { Id = 2, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-2) },
            new() { Id = 3, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-3) },
            new() { Id = 4, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-4) },
            new() { Id = 5, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-5) },
            new() { Id = 6, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-6) },
            new() { Id = 7, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-7) },
            new() { Id = 8, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-8) },
            new() { Id = 9, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-9) },
            new() { Id = 10, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-10) },
            new() { Id = 11, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-11) },
            new() { Id = 12, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-12) },
            new() { Id = 13, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-13) },
            new() { Id = 14, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-14) },
            new() { Id = 15, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-15) }
        };
        await dbContext.Tweets.AddRangeAsync(sampleTweets);
        await dbContext.SaveChangesAsync();

        var twitterService = new TweetService(dbContext);

        // Act
        var totalNumberOfTweets = await twitterService.GetTotalNumberOfTweetsAsync();

        // Assert
        Assert.Equal(sampleTweets.Count, totalNumberOfTweets);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(6)]
    [InlineData(10)]
    [InlineData(15)]
    public async Task GivenCount_GetMostRecentTweetsAsyncIsCalled_ReturnCountNumberOfMostRecentTweets(int count)
    {
        // Arrange
        var builder = new DbContextOptionsBuilder<TwitterStreamDbContext>();
        builder.UseInMemoryDatabase(nameof(GivenCount_GetMostRecentTweetsAsyncIsCalled_ReturnCountNumberOfMostRecentTweets));
        var dbContext = new TwitterStreamDbContext(builder.Options);

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();

        var faker = new Faker();
        var sampleTweets = new List<Tweet>
        {
            new() { Id = 1, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-1) },
            new() { Id = 2, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-2) },
            new() { Id = 3, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-3) },
            new() { Id = 4, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-4) },
            new() { Id = 5, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-5) },
            new() { Id = 6, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-6) },
            new() { Id = 7, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-7) },
            new() { Id = 8, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-8) },
            new() { Id = 9, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-9) },
            new() { Id = 10, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-10) },
            new() { Id = 11, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-11) },
            new() { Id = 12, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-12) },
            new() { Id = 13, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-13) },
            new() { Id = 14, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-14) },
            new() { Id = 15, TweetId = Guid.NewGuid().ToString(), Text = faker.Lorem.Sentence(), CreatedAt = DateTime.Now.AddHours(-15) }
        };
        await dbContext.Tweets.AddRangeAsync(sampleTweets);
        await dbContext.SaveChangesAsync();

        var twitterService = new TweetService(dbContext);

        // Act
        var mostRecentTweets = await twitterService.GetMostRecentTweetsAsync(count);

        // Assert
        Assert.Equal(count, mostRecentTweets.Count);

        for (var i = 0; i < count; i++)
        {
            Assert.Equal(i + 1, mostRecentTweets[i].Id);
        }
    }
}

