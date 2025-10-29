using CoffeeMaker.interfaces;

namespace CoffeeMaker.Models;

public class Cappuccino : ICoffeeRecipe
{
    public string Name => "Cappuccino";

    public Dictionary<string, int> RequiredIngredients()
    {
        return new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            { "Beans", 2 },
            { "Water", 1 },
            { "Milk", 1 },
        };
    }
}