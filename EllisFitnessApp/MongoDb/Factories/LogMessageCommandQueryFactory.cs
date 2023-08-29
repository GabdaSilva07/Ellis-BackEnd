using Amazon.Runtime.Internal.Util;
using Domain.Logger.Interface.CQRS;

namespace MongoDb.Factories;

public class LogMessageCommandQueryFactory : ICommandFactory<LogMessage, LogMessage>, IQueryFactory<LogMessage>
{
    public ICommand<LogMessage, LogMessage> CreateInsertCommand()
    {
        throw new NotImplementedException();
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