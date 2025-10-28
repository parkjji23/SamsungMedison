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
    private readonly IngredientManager _inventory;

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
        
        _inventory = new IngredientManager(beans, water, milk);
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
        var recipeIngredients = recipe.RequiredIngredients();

        // 재고 확인
        _inventory.Check(recipeIngredients);
        // 재고 차감
        Console.WriteLine($"Starting to make {recipe.Name}");
        _inventory.Consume(recipeIngredients);
        
        Console.WriteLine($"✅ {recipe.Name} is ready! (Remaining ingredients: Beans {_beans}, Water {_water}, Milk {_milk})");
        return recipe.Name;
    }

    
}