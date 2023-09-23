using Domain.Interface.Authentification;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using Domain.Logger.Interface;
using Domain.Models.Logger.LogMessage;
using ILogger = Domain.Logger.Interface.ILogger;
using LogLevel = Domain.Models.Logger.LogLevel; 


namespace API.Controllers.AuthController;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IFireBaseAuthentification _fireBaseAuthentification;
    
    public AuthController(ILogger logger, IFireBaseAuthentification fireBaseAuthentification)
    {
        _logger = logger;
        _fireBaseAuthentification = fireBaseAuthentification;
    }
    
    [HttpPost("verifyToken")]
    public async Task<IActionResult> VerifyToken([FromBody] string idToken)
    {
        try
        {
            var decodedToken = await _fireBaseAuthentification.VerifyIdTokenAsync(idToken);
            _logger.Log(
                new LogMessage 
                { 
                    Message = $"Token verification for {idToken} was successful.", 
                    LogLevel = LogLevel.Debug, 
                }, 
                true);
                
            // Perform any additional checks and return appropriate response
            return Ok(decodedToken);
        }
        catch (FirebaseAuthException ex)
        {
            // Handle error and return appropriate response
            return BadRequest(ex.Message);
        }
    }
}