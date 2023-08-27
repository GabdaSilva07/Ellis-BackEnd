namespace Domain.Logger.Interface.CQRS;

public interface IQuery<T>
{
    Task<IEnumerable<T>> ExecuteAsync(IQueryOption<T> queryOption);
}