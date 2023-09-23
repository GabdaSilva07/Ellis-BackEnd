using Google.Apis.Auth.OAuth2;

namespace FirebaseService;


public class FirebaseService
{
    public async Task<string> GetAccessTokenAsync()
    {
        var credential = GoogleCredential.FromFile("./firebase.json")
            .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
        var accessToken = await ((ITokenAccess)credential).GetAccessTokenForRequestAsync();
        return accessToken;
    }
}