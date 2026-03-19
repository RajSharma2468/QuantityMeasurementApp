using Spectre.Console;
using QuantityMeasurementBusinessLayer.DTOs;
using System.Text;

namespace QuantityMeasurementConsoleApp.Helpers;

public static class ConsoleHelper
{
    #region Basic Console Operations

    public static void PrintHeader(string title)
    {
        AnsiConsole.Write(new Rule($"[yellow]{title}[/]").RuleStyle("grey").Centered());
        AnsiConsole.WriteLine();
    }

    public static void PrintSubHeader(string title)
    {
        AnsiConsole.MarkupLine($"[cyan]=== {title} ===[/]");
        AnsiConsole.WriteLine();
    }

    public static void PrintSuccess(string message)
    {
        AnsiConsole.MarkupLine($"[green]✓ {message}[/]");
    }

    public static void PrintError(string message)
    {
        AnsiConsole.MarkupLine($"[red]✗ {message}[/]");
    }

    public static void PrintWarning(string message)
    {
        AnsiConsole.MarkupLine($"[yellow]⚠ {message}[/]");
    }

    public static void PrintInfo(string message)
    {
        AnsiConsole.MarkupLine($"[blue]ℹ {message}[/]");
    }

    public static void PrintDebug(string message)
    {
        AnsiConsole.MarkupLine($"[grey]{message}[/]");
    }

    public static void PrintEmptyLine()
    {
        AnsiConsole.WriteLine();
    }

    public static void ClearScreen()
    {
        AnsiConsole.Clear();
    }

    public static void WaitForKeyPress(string message = "Press any key to continue...")
    {
        AnsiConsole.MarkupLine($"[grey]{message}[/]");
        Console.ReadKey(true);
    }

    #endregion

    #region Input Operations

    public static string GetUserInput(string prompt, bool required = true)
    {
        while (true)
        {
            var input = AnsiConsole.Ask<string>($"[cyan]{prompt}:[/]");
            
            if (!required || !string.IsNullOrWhiteSpace(input))
                return input;
            
            PrintError("Input cannot be empty. Please try again.");
        }
    }

    public static double GetUserDouble(string prompt, double min = 0, double max = double.MaxValue)
    {
        while (true)
        {
            var input = AnsiConsole.Ask<string>($"[cyan]{prompt}:[/]");
            
            if (double.TryParse(input, out double result))
            {
                if (result >= min && result <= max)
                    return result;
                
                PrintError($"Value must be between {min} and {max}. Please try again.");
            }
            else
            {
                PrintError("Invalid number. Please try again.");
            }
        }
    }

    public static int GetUserInt(string prompt, int min = 0, int max = int.MaxValue)
    {
        while (true)
        {
            var input = AnsiConsole.Ask<string>($"[cyan]{prompt}:[/]");
            
            if (int.TryParse(input, out int result))
            {
                if (result >= min && result <= max)
                    return result;
                
                PrintError($"Value must be between {min} and {max}. Please try again.");
            }
            else
            {
                PrintError("Invalid number. Please try again.");
            }
        }
    }

    public static bool GetUserYesNo(string prompt, bool defaultValue = true)
    {
        var defaultText = defaultValue ? "Y/n" : "y/N";
        return AnsiConsole.Confirm($"[cyan]{prompt}?[/] [grey]({defaultText})[/]", defaultValue);
    }

    public static T GetUserSelection<T>(string title, List<T> items) where T : notnull
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<T>()
                .Title($"[yellow]{title}[/]")
                .PageSize(10)
                .AddChoices(items));
    }

    public static List<T> GetUserMultiSelection<T>(string title, List<T> items) where T : notnull
    {
        return AnsiConsole.Prompt(
            new MultiSelectionPrompt<T>()
                .Title($"[yellow]{title}[/]")
                .PageSize(10)
                .InstructionsText("[grey](Press [green]<space>[/] to toggle, [green]<enter>[/] to accept)[/]")
                .AddChoices(items));
    }

    #endregion

    #region Table Operations

    public static void PrintTable(string title, Dictionary<string, object> data)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.Title($"[yellow]{title}[/]");
        
        table.AddColumn("Property");
        table.AddColumn("Value");

        foreach (var item in data)
        {
            table.AddRow(
                $"[cyan]{item.Key}[/]",
                FormatValue(item.Value)
            );
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    public static void PrintResultsTable(List<Dictionary<string, object>> results, string title = "Results")
    {
        if (!results.Any())
        {
            PrintInfo("No results found.");
            return;
        }

        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.Title($"[yellow]{title}[/]");

        foreach (var key in results.First().Keys)
        {
            table.AddColumn($"[cyan]{key}[/]");
        }

        foreach (var result in results)
        {
            var row = result.Values.Select(FormatValue).ToArray();
            table.AddRow(row);
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    // FIXED: Changed QuantityMeasurementResultDTO to QuantityResultDTO
    public static void PrintMeasurementTable(QuantityResultDTO result)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.Title($"[yellow]Operation: {result.Operation.ToUpper()}[/]");

        table.AddColumn("Property");
        table.AddColumn("Value");

        table.AddRow("[cyan]First Quantity[/]", $"{result.ThisValue} {result.ThisUnit}");
        table.AddRow("[cyan]Second Quantity[/]", $"{result.ThatValue} {result.ThatUnit}");
        
        if (!string.IsNullOrEmpty(result.ResultString))
        {
            var color = result.ResultString == "true" ? "green" : "red";
            table.AddRow("[cyan]Result[/]", $"[{color}]{result.ResultString}[/]");
        }
        else if (result.ResultValue != 0)
        {
            table.AddRow("[cyan]Result[/]", $"{result.ResultValue} {result.ResultUnit}");
        }

        if (result.IsError)
        {
            table.AddRow("[cyan]Error[/]", $"[red]{result.ErrorMessage}[/]");
        }

        table.AddRow("[cyan]Timestamp[/]", result.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    private static string FormatValue(object value)
    {
        return value switch
        {
            null => "[grey]null[/]",
            bool b => b ? "[green]Yes[/]" : "[red]No[/]",
            DateTime dt => dt.ToString("yyyy-MM-dd HH:mm:ss"),
            double d => d.ToString("F2"),
            _ => value.ToString() ?? string.Empty
        };
    }

    #endregion

    #region Progress Operations

    public static async Task ShowProgressAsync(string task, Func<Task> action)
    {
        await AnsiConsole.Status()
            .StartAsync($"[yellow]{task}...[/]", async ctx =>
            {
                try
                {
                    await action();
                    ctx.Status("[green]Completed![/]");
                }
                catch (Exception ex)
                {
                    ctx.Status($"[red]Failed: {ex.Message}[/]");
                    throw;
                }
            });
    }

    public static void ShowProgress(string task, Action action)
    {
        AnsiConsole.Status()
            .Start($"[yellow]{task}...[/]", ctx =>
            {
                try
                {
                    action();
                    ctx.Status("[green]Completed![/]");
                }
                catch (Exception ex)
                {
                    ctx.Status($"[red]Failed: {ex.Message}[/]");
                    throw;
                }
            });
    }

    #endregion

    #region Menu Operations

    public static void ShowMainMenu()
    {
        var rule = new Rule("[yellow]Quantity Measurement System[/]");
        rule.Justification = Justify.Center;
        AnsiConsole.Write(rule);
        
        var menu = new[]
        {
            "[cyan]1.[/] Compare Quantities",
            "[cyan]2.[/] Convert Quantities",
            "[cyan]3.[/] Add Quantities",
            "[cyan]4.[/] Subtract Quantities",
            "[cyan]5.[/] Multiply Quantities",
            "[cyan]6.[/] Divide Quantities",
            "[cyan]7.[/] View History",
            "[cyan]8.[/] View Statistics",
            "[cyan]9.[/] Settings",
            "[cyan]0.[/] Exit"
        };

        foreach (var item in menu)
        {
            AnsiConsole.MarkupLine($"   {item}");
        }
        
        AnsiConsole.WriteLine();
    }

    #endregion

    #region Chart Operations

    public static void PrintBarChart(string title, Dictionary<string, int> data)
    {
        var chart = new BarChart()
            .Width(60)
            .Label($"[yellow]{title}[/]")
            .CenterLabel();

        foreach (var item in data)
        {
            chart.AddItem(item.Key, item.Value, GetRandomColor());
        }

        AnsiConsole.Write(chart);
    }

    private static Color GetRandomColor()
    {
        var colors = new[] 
    { 
        Color.Red, Color.Blue, Color.Green, Color.Yellow, 
        Color.Purple, Color.Magenta1, Color.Cyan1  // Removed Orange
    };
    var random = new Random();
    return colors[random.Next(colors.Length)];
    }

    #endregion
}