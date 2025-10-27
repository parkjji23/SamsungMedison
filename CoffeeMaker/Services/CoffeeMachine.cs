using System.Reflection;
using CoffeeMaker.interfaces;
using CoffeeMaker.Models;

namespace CoffeeMaker;

using System;

public class CoffeeMachine
{
    private int _beans;
    private int _water;
    private int _milk;
    private readonly Dictionary<string, ICoffeeRecipe> _menu;

    public CoffeeMachine(int beans, int water, int milk)
    {
        _beans = beans;
        _water = water;
        _milk = milk;
        _menu = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(ICoffeeRecipe).IsAssignableFrom(t)
                        && !t.IsInterface
                        && !t.IsAbstract)
            .Select(t => Activator.CreateInstance(t) as ICoffeeRecipe)
            .Where(r => r != null)
            .ToDictionary(r => r.Name, r => r, StringComparer.OrdinalIgnoreCase);
        // 메뉴 확인용 출력
        Console.WriteLine("Loaded coffee recipes:");
        foreach (var kvp in _menu)
        {
            var recipe = kvp.Value;
            Console.Write($"- {recipe.Name} : ");
            Console.WriteLine(string.Join(", ", recipe.RequiredIngredients().Select(r => $"{r.Key} {r.Value}")));
        }
        
        Console.WriteLine($"Coffee machine ready (Beans: {_beans}, Water: {_water}, Milk: {_milk})");
    }

    public string MakeCoffee(string coffeeType)
    {
        // This method for making coffee has a design flaw:
        // it must be modified every time a new coffee type is introduced.
        Console.WriteLine($"\n> Order received for '{coffeeType}'");
        
        if (!_menu.ContainsKey(coffeeType))
        {
            throw new ArgumentException($"'{coffeeType}' is not a supported menu item.");
        } 
        var recipe = _menu[coffeeType];
        var requiredIngredients = recipe.RequiredIngredients();

        // 재고 확인
        foreach (var item in requiredIngredients)
        {
            string itemName = item.Key;
            int itemAmount = item.Value;
            
            int inventory = itemName.ToLower() switch
            {
                "beans" => _beans,
                "water" => _water,
                "milk" => _milk,
                _ => 0,
            };

            if (inventory < itemAmount)
            {
                throw new InvalidOperationException("Not enough ingredients.");
            }
        }

        Console.WriteLine($"Starting to make {recipe.Name}");
        foreach (var item in requiredIngredients)
        {
            switch (item.Key.ToLower())
            {
                case "beans": _beans -= item.Value; break;
                case "water": _water -= item.Value; break;
                case "milk": _milk -= item.Value; break;
            }
        }
        
        Console.WriteLine($"✅ {recipe.Name} is ready! (Remaining ingredients: Beans {_beans}, Water {_water}, Milk {_milk})");
        return recipe.Name;
    }
}