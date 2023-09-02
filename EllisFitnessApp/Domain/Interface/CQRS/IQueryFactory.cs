namespace Domain.Logger.Interface.CQRS;

public interface IQueryFactory<T>
{
    Task<T> CreateQuery();
}