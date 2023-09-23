namespace Domain.Messages;

public class TestMessage : IMessageModel
{
    public string? Title { get; set; }
    public string? Body { get; set; }
    public string? Token { get; set; }
    public string? Topic { get; set; }
}