namespace QuantityMeasurementConsoleApp.Services;

public class ConsoleService
{
    public void DisplayMenu()
    {
        Console.WriteLine("=== Quantity Measurement Console App ===");
        Console.WriteLine("1. Register");
        Console.WriteLine("2. Login");
        Console.WriteLine("3. Encrypt Text");
        Console.WriteLine("4. Decrypt Text");
        Console.WriteLine("5. Exit");
        Console.Write("Select option: ");
    }

    public string GetInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    public void DisplayResult(string result)
    {
        Console.WriteLine($"Result: {result}");
        Console.WriteLine();
    }
}