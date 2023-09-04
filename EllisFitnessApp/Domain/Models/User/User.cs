using Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("FirebaseUid")]
    public string FirebaseUid { get; set; }

    [BsonElement("Roles")]
    public UserRole Roles { get; set; }

    [BsonElement("FirstName")]
    public string FirstName { get; set; }

    [BsonElement("LastName")]
    public string LastName { get; set; }

    [BsonElement("Email")]
    public string Email { get; set; }

    [BsonElement("PhoneNumber")]
    public string PhoneNumber { get; set; }

    [BsonElement("DateOfBirth")]
    public DateTime DateOfBirth { get; set; }

    [BsonElement("ProfilePictureUrl")]
    public string? ProfilePictureUrl { get; set; }
}