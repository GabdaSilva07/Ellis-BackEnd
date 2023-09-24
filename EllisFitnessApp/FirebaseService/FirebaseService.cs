using Google.Apis.Auth.OAuth2;

namespace FirebaseService;


public class FirebaseService
{
    public async Task<string> GetAccessTokenAsync()
    {
        var firebaseJsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FirebaseService/firebase.json");
        var credential = GoogleCredential.FromFile(firebaseJsonPath)
            .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
        var accessToken = await ((ITokenAccess)credential).GetAccessTokenForRequestAsync();
        return accessToken;
    }
}