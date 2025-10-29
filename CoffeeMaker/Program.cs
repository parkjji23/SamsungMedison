using CoffeeMaker;

public class Program
{
    public static void Main(string[] args)
    {
        var ingredients = new Dictionary<string, int>
        {
            {"Beans", 10},
            {"Water", 10},
            {"Milk", 10},
        };
        
        var machine = new CoffeeMachine(ingredients);
        try
        {
            // Should succeed
            machine.MakeCoffee("Latte"); // 1beans, 2milk
            machine.MakeCoffee("Americano"); // 1beans, 2water
            machine.MakeCoffee("Cappuccino"); // 2beans, 1water, 1milk (New Menu)
            
            // Not a supported menu item
            machine.MakeCoffee("Malcha-Latte"); 
            
            // Should fail due to lack of ingredients
            var largeOrders = new[] { "Latte", "Latte", "Latte", "Latte", "Latte", "Latte", "Latte" };
            foreach (var orders in largeOrders)
            {
                machine.MakeCoffee(orders); 
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}