using System;

namespace QuantityMeasurementApp.Core.Helpers;

public static class ConsoleHelper
{
    public static void WriteHeader(string title)
    {
        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine($"    {title}");
        Console.WriteLine(new string('=', 50));
    }

    public static void WriteSubHeader(string title)
    {
        Console.WriteLine($"\n{new string('-', 30)}");
        Console.WriteLine($"  {title}");
        Console.WriteLine(new string('-', 30));
    }

    public static void WriteSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"✓ {message}");
        Console.ResetColor();
    }

    public static void WriteError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"✗ {message}");
        Console.ResetColor();
    }

    public static void WriteInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"ℹ {message}");
        Console.ResetColor();
    }

    public static void WriteWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"⚠ {message}");
        Console.ResetColor();
    }

    public static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            string input = Console.ReadLine();
            
            if (!string.IsNullOrWhiteSpace(input) && double.TryParse(input, out double result))
            {
                return result;
            }
            
            WriteError("Invalid number. Please try again.");
        }
    }

    public static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write($"{prompt} ({min}-{max}): ");
            string input = Console.ReadLine();
            
            if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int result) && result >= min && result <= max)
            {
                return result;
            }
            
            WriteError($"Invalid input. Please enter a number between {min} and {max}.");
        }
    }

    public static bool ReadConfirmation(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (y/n): ");
            string input = Console.ReadLine()?.Trim().ToLower();
            
            if (input == "y" || input == "yes")
                return true;
            if (input == "n" || input == "no")
                return false;
            
            WriteError("Please enter 'y' or 'n'.");
        }
    }

    public static void PressAnyKeyToContinue()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey(true);
    }
}