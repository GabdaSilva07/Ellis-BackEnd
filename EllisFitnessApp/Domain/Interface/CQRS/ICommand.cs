namespace Domain.Logger.Interface.CQRS;

public interface ICommand<T, K>
{
    Task<K> ExecuteAsync(T data, string collectionName);
}