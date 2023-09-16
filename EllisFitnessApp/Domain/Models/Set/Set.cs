using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models;

public class Set
{
    [BsonElement("Reps")]
    public int Reps { get; set; }

    [BsonElement("Weight")]
    public double? Weight { get; set; }

    [BsonElement("Duration")]
    public int? Duration { get; set; }
}