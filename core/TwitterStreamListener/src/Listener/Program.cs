using Listener.Workers;
using Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddServiceLayer();

        services.AddHostedService<TwitterStreamListenerWorker>();
    })
    .Build();

host.Run();
