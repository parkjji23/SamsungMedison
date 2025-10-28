using System.Reflection;
using CoffeeMaker.interfaces;
using CoffeeMaker.Models;

namespace CoffeeMaker;

using System;

public class CoffeeMachine
{
    private readonly IngredientManager _inventory;
    private readonly MenuRecipe _menu;

    public CoffeeMachine(Dictionary<string, int> ingredients)
    {
        _inventory = new IngredientManager(ingredients);
        _inventory.Status();

        _menu = new MenuRecipe();
        _menu.Status();
    }

    public string MakeCoffee(string coffeeType)
    {
        // This method for making coffee has a design flaw:
        // it must be modified every time a new coffee type is introduced.
        Console.WriteLine($"\n> Order received for '{coffeeType}'");
        
        // 레시피 체크
        var recipe = _menu.GetRecipe(coffeeType);
        var recipeIngredients = recipe.RequiredIngredients();

        Console.WriteLine($"Starting to make {recipe.Name}");
        // 재고 체크 및 차감
        _inventory.CheckAndConsume(recipeIngredients);
        
        // 커피 제작
        Console.WriteLine($"✅ {recipe.Name} is ready! (Remaining ingredients: "+
                          string.Join(", ", recipeIngredients.Select(kv => $"{kv.Key}: {kv.Value}"))+")");
        return recipe.Name;
    }
}