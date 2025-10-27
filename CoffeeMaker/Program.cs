using CoffeeMaker;

public class Program
{
    public static void Main(string[] args)
    {
        var machine = new CoffeeMachine(beans: 10, water: 10, milk: 10);
        try
        {
            machine.MakeCoffee("Latte"); // 1beans, 2milk
            machine.MakeCoffee("Americano"); // 1beans, 2water
            machine.MakeCoffee("Latte"); // Should succeed
            machine.MakeCoffee("Latte"); // Should fail due to lack of ingredients
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}