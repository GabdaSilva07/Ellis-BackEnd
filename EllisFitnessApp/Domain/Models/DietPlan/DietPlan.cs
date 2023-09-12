using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models.DietPlan;

public class DietPlan
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; }

    [BsonElement("Description")]
    public string Description { get; set; }

    [BsonElement("MaxCalories")]
    public int MaxCalories { get; set; }

    [BsonElement("Meals")]
    public List<Meal> Meals { get; set; }
}