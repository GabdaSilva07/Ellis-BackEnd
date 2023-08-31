using Domain.Logger.Interface.CQRS;
using Domain.Models.Logger;
using MongoDb.Factories;
using ILogger = Domain.Logger.Interface.ILogger;
using Logger = Local.Logger.Logger;
using LogLevel = Domain.Models.Logger.LogLevel;
using MongoDb.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ILogger logger = new Logger(LogSource.Api,
    new LogMessageCommandQueryFactory("mongodb://localhost:27017",
        "ElisFitnessAppDev",
        "LogMessages"));

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

