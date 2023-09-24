using Domain.Interface.FirebaseMessagingService;
using Domain.Messages;
using FirebaseAdmin.Messaging;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace FCM.Messaging
{
    public class FirebaseMessagingService<T> : IFirebaseMessagingService<T> where T : IMessageModel
    {
        private readonly HttpClient _httpClient;
        private readonly FirebaseService.FirebaseService _firebaseService;

        public FirebaseMessagingService(FirebaseService.FirebaseService firebaseService)
        {
            _httpClient = new HttpClient();
            _firebaseService = firebaseService;
        }

        public async Task<bool> SendMessageAsync(T message)
        {
            var firebaseMessage = ConvertToFirebaseMessage(message);

            var accessToken = await _firebaseService.GetAccessTokenAsync();  // Get the access token
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var jsonMessage = JsonConvert.SerializeObject(firebaseMessage);
            var response = await _httpClient.PostAsync("https://fcm.googleapis.com/v1/projects/ellisfitnessappdev/messages:send", new StringContent(jsonMessage, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        private Message ConvertToFirebaseMessage(T messageModel)
        {
            var firebaseMessage = new Message()
            {
                Notification = new Notification()
                {
                    Title = messageModel.Title,
                    Body = messageModel.Body
                },
                Token = messageModel.Token,
                Topic = messageModel.Topic
            };

            return firebaseMessage;
        }
    }
}