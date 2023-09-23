using Domain.Messages;

namespace Domain.Interface.FirebaseMessagingService;

public interface IFirebaseMessagingService<T>
{
    Task SendMessageAsync<T>(T message) where T : IMessageModel;

}