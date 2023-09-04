using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models.FoodItem;

public class FoodItem
{
    [BsonElement("Name")]
    public string Name { get; set; }

    [BsonElement("Calories")]
    public int Calories { get; set; }

    [BsonElement("Protein")]
    public double Protein { get; set; }

    [BsonElement("Carbs")]
    public double Carbs { get; set; }

    [BsonElement("Fats")]
    public double Fats { get; set; }
}
