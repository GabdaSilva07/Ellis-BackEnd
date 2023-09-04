using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models.Workout;

public class Workout
{
    [BsonElement("ExerciseName")]
    public string ExerciseName { get; set; }

    [BsonElement("ExerciseType")]
    public ExerciseType.ExerciseType ExerciseType { get; set; }

    [BsonElement("Sets")]
    public List<Set.Set> Sets { get; set; }
}