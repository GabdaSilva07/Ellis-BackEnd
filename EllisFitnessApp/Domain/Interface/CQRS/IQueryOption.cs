using System.Linq.Expressions;

namespace Domain.Logger.Interface.CQRS;

public interface IQueryOption<T>
{
    object? Filter { get; set; }
    object? Projection { get; set; }
    object? Sort { get; set; }
    int? Limit { get; set; }
    int? Skip { get; set; }
    int? PageSize { get; set; }
    int? PageNumber { get; set; }
    bool? IncludeReferences { get; set; }
    string? Search { get; set; }
    public Expression<Func<T, bool>>? Predicate { get; set; }

}
