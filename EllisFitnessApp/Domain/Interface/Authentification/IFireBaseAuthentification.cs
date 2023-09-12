using Domain.Models.Auth;

namespace Domain.Interface.Authentification;

public interface IFireBaseAuthentification
{
    Task<string> RegisterAsync(AuthRequest authRequest);
}