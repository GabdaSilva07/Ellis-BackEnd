using Domain.Interface.FirebaseMessagingService;
using Domain.Messages;
using Domain.Models.Logger.LogMessage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Domain.Logger.Interface.ILogger;
using LogLevel = Domain.Models.Logger.LogLevel;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IFirebaseMessagingService<IMessageModel> _firebaseMessagingService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IFirebaseMessagingService<IMessageModel> firebaseMessagingService)
    {
        _logger = logger;
        _firebaseMessagingService = firebaseMessagingService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        var testMessage = new TestMessage
        {
            Title = "Weather Update",
            Body = "It's sunny!",
            Token = "Your_FCM_Token",  // Replace with actual FCM token
            Topic = "weather_updates",
            ImageUrl = "https://example.com/image.jpg",  // Optional: Replace with actual image URL
            Data = new Dictionary<string, string>
            {
                { "key1", "value1" },
                { "key2", "value2" }
                // Add more key-value pairs as needed
            }
        };

        var result = await _firebaseMessagingService.SendMessageAsync(testMessage);

        if (result)
        {
            _logger.LogInformation("Message sent successfully.");
        }
        else
        {
            _logger.LogError("Failed to send the message.");
        }

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.UtcNow.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
            .ToArray();
    }

}