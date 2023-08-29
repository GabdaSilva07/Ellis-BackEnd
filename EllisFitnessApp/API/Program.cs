using Domain.Logger.Interface.CQRS;
using Domain.Models.Logger;
using MongoDb.Factories;
using ILogger = Domain.Logger.Interface.ILogger;
using Logger = Local.Logger;
using LogLevel = Domain.Models.Logger.LogLevel;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new Local.Logger.Logger(LogSource.Api,
    new LogMessageCommandQueryFactory("mongodb://localhost:27017",
        "ElisFitnessAppDev",
        "LogMessages"),
    LogLevel.Debug);

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