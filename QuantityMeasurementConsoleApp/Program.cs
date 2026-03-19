using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementConsoleApp.UI;
using QuantityMeasurementModelLayer.Interfaces;
using QuantityMeasurementRepositoryLayer.Context;
using QuantityMeasurementRepositoryLayer.Repositories;
using Spectre.Console;

namespace QuantityMeasurementConsoleApp;

class Program
{
    static async Task Main(string[] args)
    {
        // Setup DI Container
        var services = new ServiceCollection();
        ConfigureServices(services);
        
        var serviceProvider = services.BuildServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        
        try
        {
            AnsiConsole.Write(new FigletText("Quantity Measurement").Color(Color.Cyan1));
            AnsiConsole.MarkupLine("[yellow]Welcome to Quantity Measurement System[/]");
            AnsiConsole.MarkupLine("[grey]A layered architecture console application[/]");
            AnsiConsole.WriteLine();
            
            // Initialize database
            await InitializeDatabase(serviceProvider);
            
            // Run main menu
            var menu = new MainMenu(serviceProvider);
            await menu.RunAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Application error");
            AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
        }
        finally
        {
            AnsiConsole.MarkupLine("[grey]Press any key to exit...[/]");
            Console.ReadKey();
        }
    }
    
    private static void ConfigureServices(IServiceCollection services)
    {
        // Add logging
        services.AddLogging(configure => 
        {
            configure.AddConsole();
            configure.SetMinimumLevel(LogLevel.Information);
        });
        
        // Add database context
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=QuantityMeasurementDb;Trusted_Connection=True;"));
        
        // Register repositories
        services.AddScoped<IQuantityMeasurementRepository, QuantityMeasurementRepository>();
        
        // Register services
        services.AddScoped<IQuantityMeasurementService, QuantityMeasurementService>();
    }
    
    private static async Task InitializeDatabase(IServiceProvider serviceProvider)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            
            await context.Database.EnsureCreatedAsync();
            
            AnsiConsole.MarkupLine("[green]Database initialized successfully![/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Database initialization failed: {ex.Message}[/]");
        }
    }
}