using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.EventHubProducerClients;
using Services.Services;
using Tweetinvi;

namespace Services;

public static class StartupExtensions
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services)
    {
        services.AddEventHubProducerClient();
        services.AddTwitterClient();

        services.AddSingleton<IEventHubService, EventHubService>();
        services.AddSingleton<ITwitterService, TwitterService>();

        return services;
    }

    private static IServiceCollection AddEventHubProducerClient(this IServiceCollection services)
    {
        services.AddSingleton(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            var eventHubConnectionString = configuration["EventHub:ConnectionString"];
            var eventHubName = configuration["TweetEventHub:Name"];

            if (string.IsNullOrEmpty(eventHubConnectionString))
            {
                throw new ArgumentNullException(nameof(eventHubConnectionString),
                    "Azure event hub's connection string is not set or properly configured");
            }

            if (string.IsNullOrEmpty(eventHubName))
            {
                throw new ArgumentNullException(nameof(eventHubName),
                    "Azure event hub's name is not set or properly configured");
            }

            return new TweetReceivedProducerClient(eventHubConnectionString, eventHubName);
        });

        services.AddSingleton(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            var eventHubConnectionString = configuration["EventHub:ConnectionString"];
            var eventHubName = configuration["HashTagEventHub:Name"];

            if (string.IsNullOrEmpty(eventHubName))
            {
                throw new ArgumentNullException(nameof(eventHubName),
                    "Azure event hub's name is not set or properly configured");
            }

            return new HashTagReceivedProducerClient(eventHubConnectionString!, eventHubName);
        });

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
                    "Twitter API's consumer key is not set or properly configured");
            }

            var consumerSecret = configuration["TwitterApi:ConsumerSecret"];

            if (string.IsNullOrEmpty(consumerSecret))
            {
                throw new ArgumentNullException(nameof(consumerSecret),
                    "Twitter API's consumer secret is not set or properly configured");
            }

            var bearerToken = configuration["TwitterApi:BearerToken"];

            if (string.IsNullOrEmpty(bearerToken))
            {
                throw new ArgumentNullException(nameof(bearerToken),
                    "Twitter API's bearer token is not set or properly configured");
            }

            return new TwitterClient(consumerKey, consumerSecret, bearerToken);
        });

        return services;
    }
}