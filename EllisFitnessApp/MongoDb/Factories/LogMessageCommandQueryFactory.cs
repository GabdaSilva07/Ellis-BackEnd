using Amazon.Runtime.Internal.Util;
using Domain.Logger.Interface.CQRS;
using MongoDb.Commands;

namespace MongoDb.Factories;

public class LogMessageCommandQueryFactory : ICommandFactory<LogMessage, LogMessage>, IQueryFactory<LogMessage>
{

    private readonly string _ConnectionString;
    private readonly string _DatabaseName;
    private readonly string _CollectionName;
    
    public LogMessageCommandQueryFactory(string connectionString, string databaseName, string collectionName)
    {
        _ConnectionString = connectionString;
        _DatabaseName = databaseName;
        _CollectionName = collectionName;
    }

    
    public async Task<ICommand<LogMessage, LogMessage>> CreateInsertCommand()
    {
        return new InsertCommand<LogMessage>(_ConnectionString, _DatabaseName, _CollectionName);
    }

    public ICommand<LogMessage, LogMessage> CreateUpdateCommand()
    {   
        throw new NotImplementedException();
    }

    public ICommand<LogMessage, LogMessage> CreateDeleteCommand()
    {
        throw new NotImplementedException();
    }

    public Task<LogMessage> CreateQuery()
    {
        throw new NotImplementedException();
    }
}