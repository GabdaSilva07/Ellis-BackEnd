using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Domain.Interface.Authentification;
using Domain.Models.Logger.LogMessage;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Authentification;

public class FirebaseAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IFireBaseAuthentification _fireBaseAuthentification;

    public FirebaseAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IFireBaseAuthentification fireBaseAuthentification)
        : base(options, logger, encoder, clock)
    {
        _fireBaseAuthentification = fireBaseAuthentification;

    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Authorization header not found.");
        }

        var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
        var idToken = authHeader.Parameter;

        try
        {
            var decodedToken = await _fireBaseAuthentification.VerifyIdTokenAsync(idToken);

            // Check if the email is verified
            if (!(bool)decodedToken.Claims["email_verified"])
            {
                return AuthenticateResult.Fail("Email not verified.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, decodedToken.Uid)
                // Add other claims as needed
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
        catch (FirebaseAuthException)
        {
            return AuthenticateResult.Fail("Invalid Firebase ID token.");
        }
    }
}