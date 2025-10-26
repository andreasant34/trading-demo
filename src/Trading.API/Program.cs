using Microsoft.EntityFrameworkCore;
using System.Reflection;
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
builder.Services.AddAutoMapper(x => x.AddMaps(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
