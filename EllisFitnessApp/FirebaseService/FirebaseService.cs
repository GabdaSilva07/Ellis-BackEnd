using Google.Apis.Auth.OAuth2;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using System;
using Domain.Interface.Firebase;

namespace FirebaseService
{
    public class FirebaseService : IFirebaseService
    {
        private readonly FirebaseAuth _auth;

        public FirebaseService()
        {
            var serviceAccountKeyPath = "/Users/gabrielsilva/Source/Ellis/BackEnd/EllisFitnessApp/FirebaseService/firebase.json";

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(serviceAccountKeyPath),
            });

            _auth = FirebaseAuth.DefaultInstance;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var firebaseJsonPath = "/Users/gabrielsilva/Source/Ellis/BackEnd/EllisFitnessApp/FirebaseService/firebase.json";

            Console.WriteLine(firebaseJsonPath);
            var credential = GoogleCredential.FromFile(firebaseJsonPath)
                .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
            var accessToken = await ((ITokenAccess)credential).GetAccessTokenForRequestAsync();
            return accessToken;
        }

        public async Task<string> CreateCustomTokenAsync(string userId)
        {
            string customToken = await _auth.CreateCustomTokenAsync(userId);
            return customToken;
        }
    }
}