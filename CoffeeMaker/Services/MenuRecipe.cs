using System.Reflection;
using CoffeeMaker.interfaces;

namespace CoffeeMaker;

public class MenuRecipe
{
    private readonly Dictionary<string, ICoffeeRecipe> _menu;

    public MenuRecipe()
    {
        _menu = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(ICoffeeRecipe).IsAssignableFrom(t)
                        && !t.IsInterface
                        && !t.IsAbstract)
            .Select(t => Activator.CreateInstance(t) as ICoffeeRecipe)
            .Where(r => r != null)
            .ToDictionary(r => r.Name, r => r, StringComparer.OrdinalIgnoreCase);
    }

    public void Status()
    {
        // 메뉴 확인용 출력
        Console.WriteLine("Loaded coffee recipes:");
        foreach (var kvp in _menu)
        {
            var recipe = kvp.Value;
            Console.Write($"- {recipe.Name} : ");
            Console.WriteLine(string.Join(", ", recipe.RequiredIngredients().Select(r => $"{r.Key} {r.Value}")));
        }
    }

    public ICoffeeRecipe GetRecipe(string coffeeType)
    {
        if (!_menu.ContainsKey(coffeeType))
        {
            throw new ArgumentException($"'{coffeeType}' is not a supported menu item.");
        }
        return _menu[coffeeType];
    }
}