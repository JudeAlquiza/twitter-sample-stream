using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tweetinvi;

namespace Services;

public static class StartupExtensions
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services)
    {
        services.AddTwitterClient();

        services.AddSingleton<ITwitterService, TwitterService>();

        return services;
    }

    private static IServiceCollection AddTwitterClient(this IServiceCollection services)
    {
        services.AddSingleton<ITwitterClient>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            var consumerKey = configuration["TwitterApi:ConsumerKey"];

            if (string.IsNullOrEmpty(consumerKey))
            {
                throw new ArgumentNullException(nameof(consumerKey),
                    "Twitter API\'s consumer key is not set or properly configured");
            }

            var consumerSecret = configuration["TwitterApi:ConsumerSecret"];

            if (string.IsNullOrEmpty(consumerSecret))
            {
                throw new ArgumentNullException(nameof(consumerSecret),
                    "Twitter API\'s consumer secret is not set or properly configured");
            }

            var bearerToken = configuration["TwitterApi:BearerToken"];

            if (string.IsNullOrEmpty(bearerToken))
            {
                throw new ArgumentNullException(nameof(bearerToken),
                    "Twitter API\'s bearer token is not set or properly configured");
            }

            return new TwitterClient(consumerKey, consumerSecret, bearerToken);
        });

        return services;
    }
}