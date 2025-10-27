using CoffeeMaker.interfaces;

namespace CoffeeMaker.Models;

public class Americano : ICoffeeRecipe
{
    public string Name => "Americano";

    public Dictionary<string, int> RequiredIngredients()
    {
        return new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            { "Beans", 1 },
            { "Water", 2 },
        };
    }
}