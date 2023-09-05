using Domain.Interface.Authentification;
using Domain.Models.Auth;

namespace Authentification;

public class FireBaseAuthentification : IFireBaseAuthentification
{
    public Task<string> RegisterAsync(AuthRequest authRequest)
    {
        throw new NotImplementedException();
    }

}