using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Models.Tweet;
using Services.Tweet;

namespace WebAPI.Endpoints.Tweets;

[HttpGet("tweets"), AllowAnonymous]
public class GetMostRecentTweetsEndpoint : Endpoint<GetMostRecentTweetsRequest, IList<TweetModel>>
{
    private readonly ITweetService _tweetService;

    public GetMostRecentTweetsEndpoint(ITweetService tweetService)
    {
        _tweetService = tweetService;
    }

    public override async Task HandleAsync(GetMostRecentTweetsRequest request, CancellationToken cancellationToken)
    {
        var mostRecentTweets = await _tweetService.GetMostRecentTweetsAsync(request.Count, cancellationToken);

        await SendOkAsync(mostRecentTweets, cancellationToken);
    }
}