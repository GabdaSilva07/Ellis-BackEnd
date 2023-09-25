public class FcmMessage
{
  public MessageBody? Message { get; set; }

  public class MessageBody
  {
    public string? Topic { get; set; }
    public Notification? Notification { get; set; }
    public Dictionary<string, string>? Data { get; set; }
  }

  public class Notification
  {
    public string? Title { get; set; }
    public string? Body { get; set; }
    public string? Image { get; set; }
  }
}