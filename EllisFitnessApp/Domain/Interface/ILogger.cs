namespace Domain.Logger.Interface;

public interface ILogger
{

        Task LogAsync(LogMessage message, bool logToAzure);

        Task LogAsync(string message, LogLevel logLevel, bool logToAzure);

        void Log(LogMessage message, bool logToAzure);

        void Log(string message, LogLevel logLevel, bool logToAzure);
    
}