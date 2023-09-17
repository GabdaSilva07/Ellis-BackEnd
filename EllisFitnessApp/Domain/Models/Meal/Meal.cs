using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models;

public class Meal
{
    [BsonElement("MealName")]
    public string MealName { get; set; }

    [BsonElement("FoodItems")]
    public List<FoodItem> FoodItems { get; set; }

    [BsonIgnore]
    public int TotalCalories
    {
        get => CalculateTotalCalories();
    }

    private int CalculateTotalCalories()
    {
        int totalCalories = 0;
        foreach (var foodItem in FoodItems)
        {
            totalCalories += foodItem.Calories;
        }
        return totalCalories;
    }
}
