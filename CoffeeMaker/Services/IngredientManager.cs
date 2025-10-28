using CoffeeMaker.interfaces;

namespace CoffeeMaker;

public class IngredientManager
{
    private int _beans;
    private int _water;
    private int _milk;
    public IngredientManager(int beans, int water, int milk)
    {
        _beans = beans;
        _water = water;
        _milk = milk;
    }
    
    // 재고 확인 및 차감
    public void Check(Dictionary<string, int> required)
    {
        foreach (var item in required)
        {
            int available = item.Key.ToLower() switch
            {
                "beans" => _beans,
                "water" => _water,
                "milk" => _milk,
                _ => 0,
            };

            if (available < item.Value)
            {
                throw new InvalidOperationException($"Not enough ingredients {item.Key}.");
            }
        }
    }

    public void Consume(Dictionary<string, int> required)
    {
        foreach (var item in required)
        {
            switch (item.Key.ToLower())
            {
                case "beans": _beans -= item.Value; break;
                case "water": _water -= item.Value; break;
                case "milk": _milk -= item.Value; break;
            }
        }
    }
}