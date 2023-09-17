using System.Security.Claims;
using Domain.Interface.Authentification;
using Domain.Logger.Interface;
using Domain.Models.Logger;
using Domain.Models.Logger.LogMessage;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Authentification;

public class FirebaseAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IFireBaseAuthentification _fireBaseAuthentification;
    private readonly ILogger _logger;

    public FirebaseAuthMiddleware(RequestDelegate next, IFireBaseAuthentification fireBaseAuthentification, ILogger logger)
    {
        _next = next;
        _logger = logger;
        _fireBaseAuthentification = fireBaseAuthentification;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string authHeader = context.Request.Headers["Authorization"];

        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            _logger.Log(
                new LogMessage 
                { 
                    Message = "Authorization header not found.",
                    LogLevel = LogLevel.Debug, 
                }, 
                true);
            context.Response.StatusCode = 401; // Unauthorized
            return;
        }

        var idToken = authHeader.Substring("Bearer ".Length).Trim();

        try
        {
            var decodedToken = await _fireBaseAuthentification.VerifyIdTokenAsync(idToken);

            // Check user's roles or other claims to verify authorization
            if (decodedToken.Claims.Any(claim => claim.Key == "role" && claim.Value == "authorized_role"))
            {
                _logger.Log(
                    new LogMessage
                    {
                        Message = $"User {decodedToken.Uid} is authorized.",
                        LogLevel = LogLevel.Debug,
                    },
                    true);
                await _next(context);
            }
            else
            {

                _logger.Log(
                    new LogMessage
                    {
                        Message = $"User {decodedToken.Uid} is not authorized.",
                        LogLevel = LogLevel.Debug,
                    },
                    true);
                context.Response.StatusCode = 403; // Forbidden
            }
        }
        catch (FirebaseAuthException)
        {
            _logger.Log(
                new LogMessage 
                { 
                    Message = "Invalid token.",
                    LogLevel = LogLevel.Debug, 
                }, 
                true);
            context.Response.StatusCode = 401; // Unauthorized
        }
    }
}