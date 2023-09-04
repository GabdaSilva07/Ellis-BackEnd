using MongoDB.Bson.Serialization.Attributes;
using User = Domain.Models.User.User;

namespace Domain.Models.Trainer;

public class Trainer : User.User
{
    
    [BsonElement("ClientIds")]
    public List<string> ClientIds { get; set; }
}