using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Trading.Core.Extensions;
using Trading.Infrastructure.MessageBus.Extensions;
using Serilog;
using Microsoft.Extensions.DependencyInjection;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var hostBuilder = Host
    .CreateDefaultBuilder()
    .ConfigureAppConfiguration(x => x.AddJsonFile("appsettings.json"))
    .ConfigureServices((context, services) =>
    {
        services.AddTradingCoreServices();
        services.AddMessageBusServices(context.Configuration, true);
        services.AddLogging(loggingBuilder => 
        {
            loggingBuilder.AddSerilog(dispose:true);
        });
    });

var host = hostBuilder.Build();
await host.RunAsync();