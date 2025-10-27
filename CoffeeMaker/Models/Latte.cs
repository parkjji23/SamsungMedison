using CoffeeMaker.interfaces;

namespace CoffeeMaker.Models;

public class Latte : ICoffeeRecipe
{
    public string Name => "Latte";

    public Dictionary<string, int> RequiredIngredients()
    {
        return new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            { "Beans", 1 },
            { "Milk", 2 },
        };
    }
}