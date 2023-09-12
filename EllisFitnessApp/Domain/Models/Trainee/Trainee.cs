using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models.Trainee;

public class Trainee : User
{
    [BsonElement("AssignedTrainerId")]
    public string? AssignedTrainerId { get; set; }

    [BsonElement("WorkoutPlanIds")]
    public List<string>? WorkoutPlanIds { get; set; }

    [BsonElement("DietPlanIds")]
    public List<string>? DietPlanIds { get; set; }
}