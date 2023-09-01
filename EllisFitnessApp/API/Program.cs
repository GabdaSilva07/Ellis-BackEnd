using DnsClient.Internal;
using Domain.Logger.Interface.CQRS;
using Domain.Models.Config;
using Domain.Models.Logger;
using Microsoft.Extensions.Options;
using MongoDb.Factories;
using ILogger = Domain.Logger.Interface.ILogger;
using Logger = Local.Logger.Logger;
using LogLevel = Domain.Models.Logger.LogLevel;
using MongoDb.Factories;

var builder = WebApplication.CreateBuilder(args);

// Get the configuration from appsettings.json
var config = builder.Configuration;

// Check if is development or production environment if development use app-settings.Development.json
var environment = builder.Environment;
if (environment.IsDevelopment())
{
    config.AddJsonFile("appsettings.Development.json");
}

// Get MongoDB configuration and attach to MongoConfig
var mongoConfig = config.GetSection("MongoConfig").Get<MongoConfig>();


// Add services to the container.

ILogger logger = new Logger(LogSource.Api,
    new LogMessageCommandQueryFactory( Options.Create(mongoConfig), "LogMessage"),
    LogLevel.Debug);



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



logger.LogAsync(new LogMessage{Message = "Hello World"}, true);

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

