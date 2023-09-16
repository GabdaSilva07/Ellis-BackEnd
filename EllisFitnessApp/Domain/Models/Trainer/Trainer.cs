using MongoDB.Bson.Serialization.Attributes;
using Domain.Models;

namespace Domain.Models;

public class Trainer : User
{

    [BsonElement("ClientIds")]
    public List<string> ClientIds { get; set; }
}