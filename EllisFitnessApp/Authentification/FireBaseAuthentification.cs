using Domain.Interface.Authentification;
using Domain.Models.Auth;
using FirebaseAdmin.Auth;

namespace Authentification;

public class FireBaseAuthentification : IFireBaseAuthentification
{
    public async Task<FirebaseToken> VerifyIdTokenAsync(string idToken)
    {
        return await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
    }
}