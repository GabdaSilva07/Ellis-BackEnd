using Authentification;
using DnsClient.Internal;
using Domain.Interface.Authentification;
using Domain.Logger.Interface.CQRS;
using Domain.Models.Config;
using Domain.Models.Logger;
using Domain.Models.Logger.LogMessage;
using Microsoft.Extensions.Options;
using MongoDb.Factories;
using ILogger = Domain.Logger.Interface.ILogger;
using Logger = Local.Logger.Logger;
using LogLevel = Domain.Models.Logger.LogLevel;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Get the configuration from appsettings.json
var config = builder.Configuration;

var environment = builder.Environment;
if (environment.IsDevelopment())
{
    config.AddJsonFile("appsettings.Development.json");
}

FirebaseApp.Create(new AppOptions(){
    Credential = GoogleCredential.FromFile("FireBase.json")
});

// Get MongoDB configuration and attach to MongoConfig
var mongoConfig = config.GetSection("MongoDB").Get<MongoConfig>();

ILogger logger = new Logger(LogSource.Api,
    new LogMessageCommandQueryFactory( Options.Create(mongoConfig), "LogMessage"),
    LogLevel.Debug);



// Add services to the container.

builder.Services.Configure<MongoConfig>(config.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoConfig>();
builder.Services.AddSingleton<ILogger>(logger);
builder.Services.AddSingleton<IFireBaseAuthentification, FireBaseAuthentification>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add authentication services to the container.
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "Firebase";
    })
    .AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationHandler>("Firebase", _ => { });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Allow swagger to be used without authentication
app.UseWhen(context => !context.Request.Path.StartsWithSegments("/swagger"), appBuilder =>
{
    appBuilder.UseMiddleware<FirebaseAuthMiddleware>();
});



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

