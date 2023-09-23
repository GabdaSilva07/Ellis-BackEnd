using System.Security.Claims;
using System.Text;
using System.Text.Json;
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
                var decodedToken = await _fireBaseAuthentification.VerifyIdTokenAsync(idToken).ConfigureAwait(false);

                if (decodedToken.Claims.Any(claim => claim.Key == "role" && claim.Value == "authorized_role"))
                {
                    _logger.Log(new LogMessage { Message = $"User {decodedToken.Uid} is authorized.", LogLevel = LogLevel.Debug }, true);
                    await _next(context).ConfigureAwait(false);
                }
                else
                {
                    await LogAndSetResponse($"User {decodedToken.Uid} is not authorized.", LogLevel.Debug, StatusCodes.Status403Forbidden);
                }
            }
            catch (FirebaseAuthException)
            {
                await LogAndSetResponse("Invalid token.", LogLevel.Debug, StatusCodes.Status401Unauthorized);
            }

            async Task LogAndSetResponse(string message, LogLevel logLevel, int statusCode)
            {
                _logger.Log(new LogMessage { Message = message, LogLevel = logLevel }, true);
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                var payload = JsonSerializer.Serialize(new { message });
                await context.Response.WriteAsync(payload).ConfigureAwait(false);
            }
        }
    }
}
