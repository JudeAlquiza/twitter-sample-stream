using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Services.Tweet;

namespace WebAPI.Endpoints.Tweets;


[HttpGet("tweets/rpc/get-total-tweets"), AllowAnonymous]
public class GetTotalNumberOfTweetsEndpoint : EndpointWithoutRequest<int>
{
    private readonly ITweetService _tweetService;

    public GetTotalNumberOfTweetsEndpoint(ITweetService tweetService)
    {
        _tweetService = tweetService;
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var totalTweets = await _tweetService.GetTotalNumberOfTweetsAsync(cancellationToken);

        await SendOkAsync(totalTweets, cancellationToken);
    }
}