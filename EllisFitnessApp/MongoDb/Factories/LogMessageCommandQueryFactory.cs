using Domain.Logger.Interface.CQRS;
using Domain.Models.Config;
using Domain.Models.Logger.LogMessage;
using Microsoft.Extensions.Options;
using MongoDb.Commands;

namespace MongoDb.Factories;

public class LogMessageCommandQueryFactory : ICommandFactory<LogMessage, LogMessage>, IQueryFactory<LogMessage>
{

    private readonly IOptions<MongoConfig> _config;
    private readonly string _collectionName;

    public LogMessageCommandQueryFactory(IOptions<MongoConfig> config, string collectionName)
    {
        _config = config;
        _collectionName = collectionName;
    }


    public async Task<ICommand<LogMessage, LogMessage>> CreateInsertCommand()
    {
        return new InsertCommand<LogMessage>(_config, _collectionName);
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