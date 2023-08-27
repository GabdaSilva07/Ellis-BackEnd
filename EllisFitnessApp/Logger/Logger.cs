using Domain.Logger.Interface.CQRS;
using Domain.Models.Logger;
using Serilog;
using Serilog.Events;
using ILogger = Domain.Logger.Interface.ILogger;

namespace Logger;

public class Logger : ILogger
{
    private readonly LogLevel _LogLevel;

    private readonly ICommandFactory<LogMessage, LogMessage> _LogMessageCommandFactory;
    private readonly LogSource _LogSource;
    private readonly Serilog.ILogger _SerilogConsoleLogger;
    private readonly Serilog.ILogger _SerilogDatabaseLogger;

    public Logger()
    {
    }

    public Logger(LogSource logSource, ICommandFactory<LogMessage, LogMessage> commandFactory,
        LogLevel logLevel = LogLevel.Debug)
    {
        _LogSource = logSource;
        _LogMessageCommandFactory = commandFactory;
        _LogLevel = logLevel;

        var minimumSerilogLogLevel = LogEventLevel.Debug;

        switch (logLevel)
        {
            case LogLevel.Debug:
                minimumSerilogLogLevel = LogEventLevel.Debug;
                break;
            case LogLevel.Information:
                minimumSerilogLogLevel = LogEventLevel.Information;
                break;
            case LogLevel.Error:
                minimumSerilogLogLevel = LogEventLevel.Error;
                break;
            case LogLevel.Warning:
                minimumSerilogLogLevel = LogEventLevel.Warning;
                break;
            case LogLevel.Fatal:
                minimumSerilogLogLevel = LogEventLevel.Fatal;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _SerilogConsoleLogger = new LoggerConfiguration()
            .MinimumLevel.Is(minimumSerilogLogLevel)
            .WriteTo.Console()
            .CreateLogger();
    }

    public async Task LogAsync(LogMessage message, bool logToDatabase)
    {
        
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