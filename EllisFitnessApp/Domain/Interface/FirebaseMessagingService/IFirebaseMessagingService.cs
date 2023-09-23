using Domain.Messages;

namespace Domain.Interface.FirebaseMessagingService;

public interface IFirebaseMessagingService<T>
{
    Task<bool> SendMessageAsync(T message);

};