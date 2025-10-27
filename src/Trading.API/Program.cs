using Microsoft.EntityFrameworkCore;
using Trading.API.Services;
using Trading.Core.Extensions;
using Trading.Core.Interfaces;
using Trading.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddTradingCoreServices();
builder.Services.AddTradingDataServices(x => x.UseNpgsql(builder.Configuration.GetConnectionString("PostgresTradingDatabase")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
