using Domain.Models.Logger;
using Domain.Models.Logger.LogMessage;

namespace Domain.Logger.Interface;

public interface ILogger
{

        Task LogAsync(LogMessage message, bool logToDatabase);

        Task LogAsync(string message, LogLevel logLevel, bool logToDatabase);

        void Log(LogMessage message, bool logToDatabase);

        void Log(string message, LogLevel logLevel, bool logToDatabase);
    
}