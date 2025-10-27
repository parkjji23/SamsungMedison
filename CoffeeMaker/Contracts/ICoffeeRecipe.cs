namespace CoffeeMaker.interfaces;

public interface ICoffeeRecipe
{
    string Name { get; }
    Dictionary<string, int> RequiredIngredients();
}