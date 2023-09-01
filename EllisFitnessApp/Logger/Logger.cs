using Domain.Logger.Interface.CQRS;
using Domain.Models.Logger;
using MongoDb.Factories;
using Serilog;
using Serilog.Events;
using ILogger = Domain.Logger.Interface.ILogger;

namespace Local.Logger;

public class Logger : ILogger
{
    private readonly LogLevel _LogLevel;


    private readonly LogMessageCommandQueryFactory _LogMessageFactory;
    private readonly LogSource _LogSource;
    private readonly Serilog.ILogger _SerilogConsoleLogger;
    private readonly Serilog.ILogger _SerilogDatabaseLogger;

    public Logger()
    {
    }


    public Logger(LogSource logSource, LogMessageCommandQueryFactory commandFactory,
        LogLevel logLevel = LogLevel.Debug)
    {
        _LogSource = logSource;
        _LogMessageFactory = commandFactory;
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
        message.TimeStamp = DateTime.Now;
        message.LogSource = _LogSource;
        var prefix = !string.IsNullOrEmpty(message.Subject) ? $"{message.Subject} - " : "";

        switch (message.LogLevel)
        {
            case LogLevel.Debug:
                await LogDebugAsync(message, logToDatabase);
                break;
            case LogLevel.Information:
                await LogInformationAsync(message, logToDatabase);
                break;
            case LogLevel.Error:
                await LogErrorAsync(message, logToDatabase);
                break;
            case LogLevel.Warning:
                await LogWarningAsync(message, logToDatabase);
                break;
            case LogLevel.Fatal:
                await LogFatalAsync(message, logToDatabase);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public Task LogAsync(string message, LogLevel logLevel, bool logToDatabase)
    {
        return LogAsync(new LogMessage { Message = message, LogLevel = logLevel },
            logToDatabase);
    }

    public void Log(LogMessage message, bool logToDatabase)
    {

        LogAsync(message, logToDatabase).GetAwaiter().GetResult();
    }

    public void Log(string message, LogLevel logLevel, bool logToDatabase)
    {
        Log(new LogMessage { Message = message, LogLevel = logLevel },
            logToDatabase);
    }

    private async Task LogDebugAsync(LogMessage message, bool logToDatabase)
    {
        await Task.Factory.StartNew(async () =>
        {
            _SerilogConsoleLogger.Debug(message.Message ?? string.Empty);
            if (logToDatabase) await LogToDatabase(message);
        });
    }

    private async Task LogInformationAsync(LogMessage message, bool logToDatabase)
    {
        await Task.Factory.StartNew(async () =>
        {
            _SerilogConsoleLogger.Information(message.Message ?? string.Empty);
            if (logToDatabase) await LogToDatabase(message);
        });
    }

    private async Task LogWarningAsync(LogMessage message, bool logToDatabase)
    {
        await Task.Factory.StartNew(async () =>
        {
            _SerilogConsoleLogger.Warning(message.Message ?? string.Empty);
            if (logToDatabase) await LogToDatabase(message);
        });
    }

    private async Task LogErrorAsync(LogMessage message, bool logToDatabase )
    {
        await Task.Factory.StartNew(async () =>
        {
            _SerilogConsoleLogger.Error(message.Message ?? string.Empty);
            if (logToDatabase) await LogToDatabase(message);
        });
    }

    private async Task LogFatalAsync(LogMessage message, bool logToDatabase)
    {
        await Task.Factory.StartNew(async () =>
        {
            _SerilogConsoleLogger.Fatal(message.Message ?? string.Empty);
            if (logToDatabase) await LogToDatabase(message);
        });
    }


    private void LogToDatabase(LogMessage logMessage)
    {
        ThreadPool.QueueUserWorkItem(async _ =>
        {
            try
            {
                Task.Factory.StartNew(async () =>
                {
                    var command = await _LogMessageFactory.CreateInsertCommand();
                    await command.ExecuteAsync(logMessage);
                });
            }
            catch (Exception e)
            {
                _SerilogConsoleLogger.Error(e, "Error logging to database");
            }
        });
    }
}