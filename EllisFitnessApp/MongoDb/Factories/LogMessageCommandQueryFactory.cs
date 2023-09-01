using Amazon.Runtime.Internal.Util;
using Domain.Logger.Interface.CQRS;
using Domain.Models.Config;
using Microsoft.Extensions.Options;
using MongoDb.Commands;

namespace MongoDb.Factories;

public class LogMessageCommandQueryFactory : ICommandFactory<LogMessage, LogMessage>, IQueryFactory<LogMessage>
{
    
    private readonly IOptions<MongoConfig> _Config;
    private readonly string _collectionName;
    
    public LogMessageCommandQueryFactory(IOptions<MongoConfig> config, string collectionName)
    {
        _Config = config;
        _collectionName = collectionName;
    }

    
    public async Task<ICommand<LogMessage, LogMessage>> CreateInsertCommand()
    {
        return new InsertCommand<LogMessage>(_Config, _collectionName);
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