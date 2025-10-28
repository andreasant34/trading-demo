using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Trading.Core.Extensions;
using Trading.Infrastructure.MessageBus.Extensions;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var hostBuilder = Host
    .CreateDefaultBuilder()
    .ConfigureAppConfiguration(x => x.AddJsonFile("appsettings.json"))
    .ConfigureServices((context, services) =>
    {
        _ = services.AddTradingCoreServices();
        _ = services.AddMessageBusServices(context.Configuration, true);
        _ = services.AddLogging(loggingBuilder =>
        {
            _ = loggingBuilder.AddSerilog(dispose: true);
        });
    });

var host = hostBuilder.Build();
await host.RunAsync();