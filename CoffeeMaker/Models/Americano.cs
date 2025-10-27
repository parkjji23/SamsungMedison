using CoffeeMaker.interfaces;

namespace CoffeeMaker.Models;

public class Americano : ICoffeeRecipe
{
    public string Name => "Americano";
    public int BeansRequired => 1;
    public int WaterRequired => 2;
    public int MilkRequired => 0;
}