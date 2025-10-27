namespace CoffeeMaker.interfaces;

public interface ICoffeeRecipe
{
    string Name { get; }
    int BeansRequired { get; }
    int WaterRequired { get; }
    int MilkRequired { get; }
}