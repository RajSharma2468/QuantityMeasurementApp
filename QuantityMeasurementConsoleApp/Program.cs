using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace QuantityMeasurementConsoleApp;

class Program
{
    private static readonly HttpClient _client;
    private static readonly string _apiUrl = "http://localhost:5283";
    private static readonly string _apiKey = "MySecretApiKey123";  // Must match API key in middleware
    private static string _token = "";

    static Program()
    {
        // Create handler to ignore SSL certificate errors for development
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
        _client = new HttpClient(handler);
        
        // Add API key header to all requests
        _client.DefaultRequestHeaders.Add("X-API-Key", _apiKey);
    }

    static async Task Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== QUANTITY MEASUREMENT ===");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Convert Units");
            Console.WriteLine("4. View History");
            Console.WriteLine("0. Exit");
            Console.Write("\nChoose: ");
            
            switch (Console.ReadLine())
            {
                case "1": await Register(); break;
                case "2": await Login(); break;
                case "3": await Convert(); break;
                case "4": await History(); break;
                case "0": return;
            }
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }
    }

    static async Task Register()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTER ===\n");

        // Username
        string username;
        do
        {
            Console.Write("Username (min 3 chars): ");
            username = Console.ReadLine() ?? "";
            if (username.Length < 3) Console.WriteLine("✗ Too short!");
        } while (username.Length < 3);

        // Email
        string email;
        do
        {
            Console.Write("Email: ");
            email = Console.ReadLine() ?? "";
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                Console.WriteLine("✗ Invalid email!");
        } while (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"));

        // Password
        string password;
        do
        {
            Console.Write("Password (8+ chars, 1 uppercase, 1 number, 1 special): ");
            password = ReadPassword();
            Console.WriteLine();
            
            var isValid = password.Length >= 8 &&
                         Regex.IsMatch(password, "[A-Z]") &&
                         Regex.IsMatch(password, "[a-z]") &&
                         Regex.IsMatch(password, "\\d") &&
                         Regex.IsMatch(password, "[!@#$%^&*]");
                         
            if (!isValid) Console.WriteLine("✗ Weak password!");
        } while (password.Length < 8 || !Regex.IsMatch(password, "[A-Z]") || !Regex.IsMatch(password, "\\d"));

        // Confirm
        string confirm;
        do
        {
            Console.Write("Confirm: ");
            confirm = ReadPassword();
            Console.WriteLine();
            if (password != confirm) Console.WriteLine("✗ Passwords don't match!");
        } while (password != confirm);

        // Register
        var data = new { username, email, password };
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        
        try
        {
            var response = await _client.PostAsync($"{_apiUrl}/api/Auth/register", content);
            var result = await response.Content.ReadAsStringAsync();
            
            if (response.IsSuccessStatusCode)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n✓ Registered successfully!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n✗ Registration failed: {result}");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n✗ Error: {ex.Message}");
        }
        finally
        {
            Console.ResetColor();
        }
    }

    static async Task Login()
    {
        Console.Clear();
        Console.WriteLine("=== LOGIN ===\n");
        
        Console.Write("Username: ");
        var username = Console.ReadLine();
        Console.Write("Password: ");
        var password = ReadPassword();

        var data = new { username, password };
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        
        try
        {
            var response = await _client.PostAsync($"{_apiUrl}/api/Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                _token = doc.RootElement.GetProperty("data").GetProperty("token").GetString() ?? "";
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n✓ Logged in as {username}!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n✗ Login failed!");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n✗ Error: {ex.Message}");
        }
        finally
        {
            Console.ResetColor();
        }
    }

    static async Task Convert()
    {
        if (string.IsNullOrEmpty(_token)) 
        { 
            Console.WriteLine("\n✗ Please login first!"); 
            return; 
        }

        Console.Clear();
        Console.WriteLine("=== CONVERT UNITS ===\n");
        
        Console.Write("Category (length/weight/temperature): ");
        var category = Console.ReadLine();
        Console.Write("From Unit: ");
        var from = Console.ReadLine();
        Console.Write("To Unit: ");
        var to = Console.ReadLine();
        Console.Write("Value: ");
        double.TryParse(Console.ReadLine(), out double value);

        var data = new { category, fromUnit = from, toUnit = to, value };
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        
        try
        {
            var response = await _client.PostAsync($"{_apiUrl}/api/Measurement/convert", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var output = doc.RootElement.GetProperty("data").GetProperty("outputValue").GetDouble();
                var formula = doc.RootElement.GetProperty("data").GetProperty("formula").GetString();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n✓ Result: {value} {from} = {output} {to}");
                Console.WriteLine($"  Formula: {formula}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n✗ Conversion failed!");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n✗ Error: {ex.Message}");
        }
        finally
        {
            Console.ResetColor();
        }
    }

    static async Task History()
    {
        if (string.IsNullOrEmpty(_token)) 
        { 
            Console.WriteLine("\n✗ Please login first!"); 
            return; 
        }

        try
        {
            var response = await _client.GetAsync($"{_apiUrl}/api/Measurement/history");
            
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var history = doc.RootElement.GetProperty("data").EnumerateArray();
                
                Console.WriteLine("\n=== CONVERSION HISTORY ===\n");
                foreach (var h in history)
                {
                    Console.WriteLine($"{h.GetProperty("fromUnit")} → {h.GetProperty("toUnit")}: " +
                                     $"{h.GetProperty("inputValue")} = {h.GetProperty("outputValue")}");
                }
            }
            else
            {
                Console.WriteLine("\n✗ Failed to get history!");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n✗ Error: {ex.Message}");
        }
        finally
        {
            Console.ResetColor();
        }
    }

    static string ReadPassword()
    {
        string pass = "";
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter) break;
            if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
            {
                pass = pass[..^1];
                Console.Write("\b \b");
            }
            else if (key.KeyChar != '\b')
            {
                pass += key.KeyChar;
                Console.Write("*");
            }
        }
        Console.WriteLine();
        return pass;
    }
}