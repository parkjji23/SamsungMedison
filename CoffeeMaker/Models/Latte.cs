using CoffeeMaker.interfaces;

namespace CoffeeMaker.Models;

public class Latte : ICoffeeRecipe
{
    public string Name => "Latte";
    public int BeansRequired => 1;
    public int WaterRequired => 0;
    public int MilkRequired => 2;
}