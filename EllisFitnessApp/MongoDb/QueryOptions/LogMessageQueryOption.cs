using System.Linq.Expressions;
using Amazon.Runtime.Internal.Util;
using Domain.Logger.Interface.CQRS;

namespace MongoDb.QueryOptions;

public class LogMessageQueryOption : IQueryOption<LogMessage>
{
    public object? Filter { get; set; }
    public object? Projection { get; set; }
    public object? Sort { get; set; }
    public int? Limit { get; set; }
    public int? Skip { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
    public bool? IncludeReferences { get; set; }
    public string? Search { get; set; }
    public Expression<Func<LogMessage, bool>>? Predicate { get; set; }
}