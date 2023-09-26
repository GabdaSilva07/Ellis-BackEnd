using Domain.Interface.FirebaseMessagingService;
using Domain.Messages;
using FirebaseAdmin.Messaging;
using System.Net.Http;
using System.Text;
using Domain.Interface.Firebase;
using Newtonsoft.Json;

namespace FCM.Messaging
{
    public class FirebaseMessagingService<T> : IFirebaseMessagingService<T> where T : IMessageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IFirebaseService _firebaseService;

        public FirebaseMessagingService(HttpClient httpClient, IFirebaseService firebaseService)
        {
            _httpClient = httpClient;
            _firebaseService = firebaseService;
        }

        public async Task<bool> SendMessageAsync(T message)
        {
            var firebaseMessage = ConvertToFirebaseMessage(message);
            var accessToken = await _firebaseService.GetAccessTokenAsync();  // Get the access token
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var jsonMessage = JsonConvert.SerializeObject(firebaseMessage);
            var response = await _httpClient.PostAsync("https://fcm.googleapis.com/v1/projects/ellisfitnessappdev/messages:send", new StringContent(jsonMessage, Encoding.UTF8, "application/json"));
            var responseContent = await response.Content.ReadAsStringAsync();
            // Use a logging framework to log the response status, authorization header, and JSON message at a debug level
            // instead of writing them to the console.
            // Log.Debug($"Response Status: {response.StatusCode}, Content: {responseContent}");
            // Log.Debug($"Authorization Header: {_httpClient.DefaultRequestHeaders.Authorization}");
            // Log.Debug($"JSON Message: {jsonMessage}");
            return response.IsSuccessStatusCode;
        }
        private FcmMessage ConvertToFirebaseMessage(T messageModel)
        {
            var firebaseMessage = new FcmMessage()
            {
                Message = new FcmMessage.MessageBody()
                {
                    Notification = new FcmMessage.Notification()
                    {
                        Title = messageModel.Title,
                        Body = messageModel.Body,
                        Image = messageModel.ImageUrl
                    },
                    Data = messageModel.Data,
                    Topic = messageModel.Topic
                }
            };

            return firebaseMessage;
        }
    }
}