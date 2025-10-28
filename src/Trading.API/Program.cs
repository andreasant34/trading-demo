using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Trading.API.Middleware;
using Trading.API.Services;
using Trading.Core.Extensions;
using Trading.Core.Interfaces;
using Trading.Infrastructure.Data.Extensions;
using Trading.Infrastructure.MessageBus.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddTradingCoreServices();
builder.Services.AddTradingDataServices(x => x.UseNpgsql(builder.Configuration.GetConnectionString("PostgresTradingDatabase")));
builder.Services.AddMessageBusServices(builder.Configuration, false);

builder.Services.AddScoped<RequestIdMiddleware>();
builder.Services.AddHttpLogging(options =>
{
    _ = options.RequestHeaders.Add(RequestIdMiddleware.RequestIdHeaderKey);
    _ = options.ResponseHeaders.Add(RequestIdMiddleware.RequestIdHeaderKey);
    options.MediaTypeOptions.AddText("application/json");
    options.LoggingFields =
        HttpLoggingFields.RequestHeaders |
        HttpLoggingFields.RequestBody |
        HttpLoggingFields.ResponseHeaders |
        HttpLoggingFields.ResponseBody;
});
builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Host.UseSerilog((context, configuration) =>
{
    _ = configuration.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.UseMiddleware<RequestIdMiddleware>();
app.UseHttpLogging();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();
