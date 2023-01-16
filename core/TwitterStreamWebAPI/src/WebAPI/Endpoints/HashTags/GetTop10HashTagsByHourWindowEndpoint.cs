using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Models.HashTag.GetTop10HashTagsByHourWindowEndpoint;
using Services.HashTag;

namespace WebAPI.Endpoints.HashTags;


[HttpGet("hash-tags/rpc/get-top-10"), AllowAnonymous]
public class GetTop10HashTagsByHourWindowEndpoint : Endpoint<GetTop10HashTagsByHourWindowRequest, IList<string>>
{
    private readonly IHashTagService _hashTagService;

    public GetTop10HashTagsByHourWindowEndpoint(IHashTagService hashTagService)
    {
        _hashTagService = hashTagService;
    }

    public override async Task HandleAsync(GetTop10HashTagsByHourWindowRequest request, CancellationToken cancellationToken)
    {
        var top10HashTags =
            await _hashTagService.GetTop10HashTagsByHourWindowAsync(request.HourWindow, cancellationToken);

        await SendOkAsync(top10HashTags, cancellationToken);
    }
}