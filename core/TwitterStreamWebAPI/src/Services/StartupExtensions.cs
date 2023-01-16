using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.HashTag;
using Services.Tweet;

namespace Services;

public static class StartupExtensions
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TwitterStreamDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("TwitterStreamDb")));

        services.AddScoped<IHashTagService, HashTagService>();

        services.AddScoped<ITweetService, TweetService>();

        return services;
    }
}