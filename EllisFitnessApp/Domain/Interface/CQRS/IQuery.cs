namespace Domain.Logger.Interface.CQRS;

public interface IQuery<T>
{
    Task<T> ExecuteAsync();
}