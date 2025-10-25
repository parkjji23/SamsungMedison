using CoffeeMaker;

public class Program
{
    public static void Main(string[] args)
    {
        var machine = new CoffeeMachine(beans: 10, water: 10, milk: 10);
        try
        {
            machine.MakeCoffee("Latte");
            machine.MakeCoffee("Americano");
            machine.MakeCoffee("Latte"); // Should succeed
            machine.MakeCoffee("Latte"); // Should fail due to lack of ingredients
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}