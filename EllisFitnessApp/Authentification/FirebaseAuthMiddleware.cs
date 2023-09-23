using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Interface.Authentification;
using Domain.Logger.Interface;
using Domain.Models.Logger;
using Domain.Models.Logger.LogMessage;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Authentification
{
    public class FirebaseAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IFireBaseAuthentification _fireBaseAuthentification;
        private readonly ILogger _logger;
        private const string AuthorizationHeader = "Authorization";
        private const string BearerPrefix = "Bearer ";

        public FirebaseAuthMiddleware(RequestDelegate next, IFireBaseAuthentification fireBaseAuthentification, ILogger logger)
        {
            _next = next;
            _logger = logger;
            _fireBaseAuthentification = fireBaseAuthentification;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(AuthorizationHeader, out var authHeader))
            {
                await LogAndSetResponse("Authorization header not found.", LogLevel.Debug, StatusCodes.Status401Unauthorized);
                return;
            }

            if (!authHeader.ToString().StartsWith(BearerPrefix))
            {
                await LogAndSetResponse("Invalid authorization type. Only Bearer is supported.", LogLevel.Debug, StatusCodes.Status401Unauthorized);
                return;
            }

            var idToken = authHeader.ToString().Substring(BearerPrefix.Length).Trim();

            try
            {
                FirebaseToken decodedToken = await _fireBaseAuthentification.VerifyIdTokenAsync(idToken).ConfigureAwait(false);
                if (_next != null)
                {
                    // Check if the user is authorized before continuing with the middleware pipeline
                    if (decodedToken.Claims.TryGetValue("email_verified", out object? emailVerified) && emailVerified is bool isEmailVerified && isEmailVerified)
                    {
                        _logger.Log(new LogMessage { Message = $"User {decodedToken.Uid} is authorized.", LogLevel = LogLevel.Debug }, false);
                        await _next(context).ConfigureAwait(false);
                    }
                    else
                    {
                        // If the user is not authorized, log the message and set the response without calling the next middleware
                        
                        // If the user is not authorized, log the message and also build a authentication link and log it
                        var email = decodedToken.Claims["email"].ToString();
                        var authLink = await FirebaseAuth.DefaultInstance.GenerateEmailVerificationLinkAsync(email).ConfigureAwait(false);
                        
                        Console.WriteLine(authLink);
                        
                        await LogAndSetResponse($"User {decodedToken.Uid} is not authorized.", LogLevel.Debug, StatusCodes.Status403Forbidden);
                    }
                }
            }
            catch (FirebaseAuthException)
            {
                await LogAndSetResponse("Invalid token.", LogLevel.Debug, StatusCodes.Status401Unauthorized);
            }

            async Task LogAndSetResponse(string message, LogLevel logLevel, int statusCode)
            {
                _logger.Log(new LogMessage { Message = message, LogLevel = logLevel }, false);
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                var payload = JsonSerializer.Serialize(new { message });
                await context.Response.WriteAsync(payload).ConfigureAwait(false);
            }
        }
    }
}
