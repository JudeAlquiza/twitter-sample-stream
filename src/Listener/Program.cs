using Listener;
using Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddServiceLayer();
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
