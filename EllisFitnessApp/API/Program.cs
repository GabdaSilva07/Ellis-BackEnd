using DnsClient.Internal;
using Domain.Logger.Interface.CQRS;
using Domain.Models.Config;
using Domain.Models.Logger;
using Domain.Models.Logger.LogMessage;
using Microsoft.Extensions.Options;
using MongoDb.Factories;
using ILogger = Domain.Logger.Interface.ILogger;
using Logger = Local.Logger.Logger;
using LogLevel = Domain.Models.Logger.LogLevel;
using MongoDb.Factories;

var builder = WebApplication.CreateBuilder(args);

// Get the configuration from appsettings.json
var config = builder.Configuration;

var environment = builder.Environment;
if (environment.IsDevelopment())
{
    config.AddJsonFile("appsettings.Development.json");
}

// Get MongoDB configuration and attach to MongoConfig
var mongoConfig = config.GetSection("MongoDB").Get<MongoConfig>();

ILogger logger = new Logger(LogSource.Api,
    new LogMessageCommandQueryFactory( Options.Create(mongoConfig), "LogMessage"),
    LogLevel.Debug);

// Add services to the container.

builder.Services.Configure<MongoConfig>(config.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoConfig>();
builder.Services.AddSingleton<ILogger>(logger);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

