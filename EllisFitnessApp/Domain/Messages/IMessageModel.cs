namespace Domain.Messages;

public interface IMessageModel
{
    string? Title { get; set; }
    string? Body { get; set; }
    string? Token { get; set; }
    string? Topic { get; set; }
}