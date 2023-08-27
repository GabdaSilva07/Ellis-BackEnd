using Domain.Logger.Interface;
using Domain.Models.Logger;

namespace Logger;

public class Logger : ILogger
{
    
    public Logger()
    {
    }
    
    
    
    public Task LogAsync(LogMessage message, bool logToDatabase)
    {
        throw new NotImplementedException();
    }

    public Task LogAsync(string message, LogLevel logLevel, bool logToDatabase)
    {
        throw new NotImplementedException();
    }

    public void Log(LogMessage message, bool logToDatabase)
    {
        throw new NotImplementedException();
    }

    public void Log(string message, LogLevel logLevel, bool logToDatabase)
    {
        throw new NotImplementedException();
    }
}