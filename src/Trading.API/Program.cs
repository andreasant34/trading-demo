using Microsoft.EntityFrameworkCore;
using Trading.Core.Extensions;
using Trading.Infrastructure.Data;
using Trading.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TradingDbContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("PostgresTradingDatabase")));
builder.Services.AddTradingCoreServices();
builder.Services.AddTradingDataServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
