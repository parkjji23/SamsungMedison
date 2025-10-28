using CoffeeMaker.interfaces;

namespace CoffeeMaker;

public class IngredientManager
{
    private readonly Dictionary<string, int> _ingredients;
    public IngredientManager(Dictionary<string, int> ingredients)
    {
        _ingredients = new Dictionary<string, int>(ingredients,  StringComparer.OrdinalIgnoreCase);
    }

    public void Status()
    {
        Console.WriteLine("Coffee machine ready (" + string.Join(", ", _ingredients.Select(kv => $"{kv.Key}: {kv.Value}"))+")");
    }
    
    // 재고 확인 및 차감
    public void CheckAndConsume(Dictionary<string, int> recipeIngredients)
    {
        Check(recipeIngredients);
        Consume(recipeIngredients);
    }
    public void Check(Dictionary<string, int> required)
    {
        foreach (var item in required)
        {
            if (!_ingredients.TryGetValue(item.Key, out int available) || available < item.Value)
            {
                throw new InvalidOperationException($"Not enough ingredients {item.Key}.");
            }
        }
    }

    public void Consume(Dictionary<string, int> required)
    {
        foreach (var item in required)
        {
            if (_ingredients.ContainsKey(item.Key))
            {
                _ingredients[item.Key] -= item.Value;
            }
        }
    }
}