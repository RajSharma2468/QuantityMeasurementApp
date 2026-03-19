using Microsoft.Extensions.DependencyInjection;
using QuantityMeasurementBusinessLayer.DTOs;
using QuantityMeasurementBusinessLayer.Interfaces;
using Spectre.Console;

namespace QuantityMeasurementConsoleApp.UI;

public class MainMenu
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IQuantityMeasurementService _service;
    
    public MainMenu(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _service = serviceProvider.GetRequiredService<IQuantityMeasurementService>();
    }
    
    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Select an operation:[/]")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Compare Quantities",
                        "Convert Quantities",
                        "Add Quantities",
                        "Subtract Quantities",
                        "Multiply Quantities",
                        "Divide Quantities",
                        "View Operation History",
                        "View Measurements by Type",
                        "View Operation Count",
                        "View Error History",
                        "Exit"
                    }));
            
            switch (choice)
            {
                case "Compare Quantities":
                    await CompareQuantities();
                    break;
                case "Convert Quantities":
                    await ConvertQuantities();
                    break;
                case "Add Quantities":
                    await AddQuantities();
                    break;
                case "Subtract Quantities":
                    await SubtractQuantities();
                    break;
                case "Multiply Quantities":
                    await MultiplyQuantities();
                    break;
                case "Divide Quantities":
                    await DivideQuantities();
                    break;
                case "View Operation History":
                    await ViewOperationHistory();
                    break;
                case "View Measurements by Type":
                    await ViewMeasurementsByType();
                    break;
                case "View Operation Count":
                    await ViewOperationCount();
                    break;
                case "View Error History":
                    await ViewErrorHistory();
                    break;
                case "Exit":
                    return;
            }
            
            AnsiConsole.MarkupLine("[grey]Press any key to continue...[/]");
            Console.ReadKey();
        }
    }
    
    private async Task CompareQuantities()
    {
        AnsiConsole.MarkupLine("[yellow]=== Compare Quantities ===[/]");
        
        var input = GetUserInput();
        
        try
        {
            var result = await _service.CompareQuantitiesAsync(input);
            
            var table = new Table();
            table.AddColumn("Property");
            table.AddColumn("Value");
            
            table.AddRow("First Quantity", $"{result.ThisValue} {result.ThisUnit}");
            table.AddRow("Second Quantity", $"{result.ThatValue} {result.ThatUnit}");
            table.AddRow("Result", result.ResultString ?? "N/A");
            table.AddRow("Status", result.IsError ? "[red]Error[/]" : "[green]Success[/]");
            
            AnsiConsole.Write(table);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
        }
    }
    
    private async Task ConvertQuantities()
    {
        AnsiConsole.MarkupLine("[yellow]=== Convert Quantities ===[/]");
        
        var input = GetUserInput();
        
        try
        {
            var result = await _service.ConvertQuantitiesAsync(input);
            
            var table = new Table();
            table.AddColumn("Property");
            table.AddColumn("Value");
            
            table.AddRow("Original Value", $"{result.ThisValue} {result.ThisUnit}");
            table.AddRow("Target Unit", result.ThatUnit);
            table.AddRow("Converted Value", $"{result.ResultValue} {result.ResultUnit}");
            table.AddRow("Status", result.IsError ? "[red]Error[/]" : "[green]Success[/]");
            
            AnsiConsole.Write(table);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
        }
    }
    
    private async Task AddQuantities()
    {
        AnsiConsole.MarkupLine("[yellow]=== Add Quantities ===[/]");
        
        var input = GetUserInput();
        
        try
        {
            var result = await _service.AddQuantitiesAsync(input);
            
            var table = new Table();
            table.AddColumn("Property");
            table.AddColumn("Value");
            
            table.AddRow("First Quantity", $"{result.ThisValue} {result.ThisUnit}");
            table.AddRow("Second Quantity", $"{result.ThatValue} {result.ThatUnit}");
            table.AddRow("Sum", $"{result.ResultValue} {result.ResultUnit}");
            table.AddRow("Status", result.IsError ? "[red]Error[/]" : "[green]Success[/]");
            
            AnsiConsole.Write(table);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
        }
    }
    
    private async Task SubtractQuantities()
    {
        AnsiConsole.MarkupLine("[yellow]=== Subtract Quantities ===[/]");
        
        var input = GetUserInput();
        
        try
        {
            var result = await _service.SubtractQuantitiesAsync(input);
            
            var table = new Table();
            table.AddColumn("Property");
            table.AddColumn("Value");
            
            table.AddRow("First Quantity", $"{result.ThisValue} {result.ThisUnit}");
            table.AddRow("Second Quantity", $"{result.ThatValue} {result.ThatUnit}");
            table.AddRow("Difference", $"{result.ResultValue} {result.ResultUnit}");
            table.AddRow("Status", result.IsError ? "[red]Error[/]" : "[green]Success[/]");
            
            AnsiConsole.Write(table);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
        }
    }
    
    private async Task MultiplyQuantities()
    {
        AnsiConsole.MarkupLine("[yellow]=== Multiply Quantities ===[/]");
        
        var input = GetUserInput();
        
        try
        {
            var result = await _service.MultiplyQuantitiesAsync(input);
            
            var table = new Table();
            table.AddColumn("Property");
            table.AddColumn("Value");
            
            table.AddRow("First Quantity", $"{result.ThisValue} {result.ThisUnit}");
            table.AddRow("Second Quantity", $"{result.ThatValue} {result.ThatUnit}");
            table.AddRow("Product", $"{result.ResultValue} {result.ResultUnit}");
            table.AddRow("Status", result.IsError ? "[red]Error[/]" : "[green]Success[/]");
            
            AnsiConsole.Write(table);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
        }
    }
    
    private async Task DivideQuantities()
    {
        AnsiConsole.MarkupLine("[yellow]=== Divide Quantities ===[/]");
        
        var input = GetUserInput();
        
        try
        {
            var result = await _service.DivideQuantitiesAsync(input);
            
            var table = new Table();
            table.AddColumn("Property");
            table.AddColumn("Value");
            
            table.AddRow("First Quantity", $"{result.ThisValue} {result.ThisUnit}");
            table.AddRow("Second Quantity", $"{result.ThatValue} {result.ThatUnit}");
            table.AddRow("Quotient", $"{result.ResultValue} {result.ResultUnit}");
            table.AddRow("Status", result.IsError ? "[red]Error[/]" : "[green]Success[/]");
            
            AnsiConsole.Write(table);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
        }
    }
    
    private async Task ViewOperationHistory()
    {
        var operation = AnsiConsole.Ask<string>("Enter operation type (compare/convert/add/subtract/multiply/divide):");
        
        var results = await _service.GetOperationHistoryAsync(operation);
        
        if (!results.Any())
        {
            AnsiConsole.MarkupLine("[yellow]No history found[/]");
            return;
        }
        
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Operation");
        table.AddColumn("Input");
        table.AddColumn("Result");
        table.AddColumn("Date");
        
        foreach (var result in results.Take(10))
        {
            table.AddRow(
                result.Id.ToString(),
                result.Operation,
                $"{result.ThisValue} {result.ThisUnit} & {result.ThatValue} {result.ThatUnit}",
                result.ResultString ?? result.ResultValue.ToString(),
                result.CreatedAt.ToString("yyyy-MM-dd HH:mm")
            );
        }
        
        AnsiConsole.Write(table);
    }
    
    private async Task ViewMeasurementsByType()
    {
        var type = AnsiConsole.Ask<string>("Enter measurement type (LengthUnit/WeightUnit/VolumeUnit/TemperatureUnit):");
        
        var results = await _service.GetMeasurementsByTypeAsync(type);
        
        if (!results.Any())
        {
            AnsiConsole.MarkupLine("[yellow]No measurements found[/]");
            return;
        }
        
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Operation");
        table.AddColumn("Value");
        table.AddColumn("Unit");
        table.AddColumn("Date");
        
        foreach (var result in results.Take(10))
        {
            table.AddRow(
                result.Id.ToString(),
                result.Operation,
                result.ThisValue.ToString(),
                result.ThisUnit,
                result.CreatedAt.ToString("yyyy-MM-dd HH:mm")
            );
        }
        
        AnsiConsole.Write(table);
    }
    
    private async Task ViewOperationCount()
    {
        var operation = AnsiConsole.Ask<string>("Enter operation type:");
        
        var count = await _service.GetOperationCountAsync(operation);
        
        AnsiConsole.MarkupLine($"[green]Total {operation} operations: {count}[/]");
    }
    
    private async Task ViewErrorHistory()
    {
        var errors = await _service.GetErrorHistoryAsync();
        
        if (!errors.Any())
        {
            AnsiConsole.MarkupLine("[green]No errors found![/]");
            return;
        }
        
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Operation");
        table.AddColumn("Error Message");
        table.AddColumn("Date");
        
        foreach (var error in errors.Take(10))
        {
            table.AddRow(
                error.Id.ToString(),
                error.Operation,
                error.ErrorMessage ?? "Unknown error",
                error.CreatedAt.ToString("yyyy-MM-dd HH:mm")
            );
        }
        
        AnsiConsole.Write(table);
    }
    
    private QuantityInputDTO GetUserInput()
    {
        var input = new QuantityInputDTO();
        
        AnsiConsole.MarkupLine("[cyan]Enter first quantity:[/]");
        input.ThisQuantity.Value = AnsiConsole.Ask<double>("Value:");
        input.ThisQuantity.Unit = AnsiConsole.Ask<string>("Unit (Feet/Inches/Meter/Gram/Kilogram etc.):");
        input.ThisQuantity.MeasurementType = AnsiConsole.Ask<string>("Measurement Type (LengthUnit/WeightUnit/VolumeUnit/TemperatureUnit):");
        
        AnsiConsole.MarkupLine("[cyan]Enter second quantity:[/]");
        input.ThatQuantity.Value = AnsiConsole.Ask<double>("Value:");
        input.ThatQuantity.Unit = AnsiConsole.Ask<string>("Unit:");
        input.ThatQuantity.MeasurementType = AnsiConsole.Ask<string>("Measurement Type:");
        
        return input;
    }
}