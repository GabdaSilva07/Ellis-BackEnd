using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models;

public class Workout
{
    [BsonElement("ExerciseName")]
    public string ExerciseName { get; set; }

    [BsonElement("ExerciseType")]
    public ExerciseType ExerciseType { get; set; }

    [BsonElement("Sets")]
    public List<Set> Sets { get; set; }
}