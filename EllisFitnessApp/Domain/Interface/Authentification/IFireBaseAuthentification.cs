using Domain.Models.Auth;
using FirebaseAdmin.Auth;

namespace Domain.Interface.Authentification;

public interface IFireBaseAuthentification
{
    public Task<FirebaseToken> VerifyIdTokenAsync(string idToken);
}