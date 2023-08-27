namespace Domain.Models.Logger;

public class LogMessage
{
  public int Id { get; set; }
  public string Message { get; set; }
  public LogLevel LogLevel { get; set; }
  public LogSource LogSource { get; set; }
  public DateTime TimeStamp { get; set; }
  
}