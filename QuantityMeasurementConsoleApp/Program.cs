using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using QuantityMeasurementConsoleApp.Interfaces;
using QuantityMeasurementConsoleApp.Services;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementRepositoryLayer.Repositories;

namespace QuantityMeasurementConsoleApp
{
    class Program
    {
        private static IServiceProvider? _serviceProvider;
        
        static void Main(string[] args)
        {
            try
            {
                SetupServices();
                
                if (_serviceProvider == null)
                {
                    Console.WriteLine("Failed to initialize services");
                    return;
                }
                
                ILogger<Program> logger = _serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Application Started");
                
                IConfiguration config = _serviceProvider.GetRequiredService<IConfiguration>();
                string? useDb = config.GetSection("Database:UseDatabase").Value;
                
                Console.WriteLine("========================================");
                Console.WriteLine("QUANTITY MEASUREMENT APPLICATION");
                Console.WriteLine("========================================");
                
                if (useDb == "True" || useDb == "true")
                {
                    Console.WriteLine("Using DATABASE repository");
                }
                else
                {
                    Console.WriteLine("Using CACHE repository");
                }
                
                IMenu menu = _serviceProvider.GetRequiredService<IMenu>();
                menu.ShowMainMenu();
                
                logger.LogInformation("Application Ended");
            }
            catch (Exception ex)
            {
                Console.WriteLine("FATAL ERROR: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        
        private static void SetupServices()
        {
            ServiceCollection services = new ServiceCollection();
            
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            
            services.AddSingleton<IConfiguration>(config);
            
            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });
            
            IConfigurationSection dbSection = config.GetSection("Database");
            string? useDatabase = dbSection["UseDatabase"];
            
            if (useDatabase == "True" || useDatabase == "true")
            {
                services.AddSingleton<IQuantityMeasurementRepository, QuantityMeasurementDatabaseRepository>();
            }
            else
            {
                services.AddSingleton<IQuantityMeasurementRepository, QuantityMeasurementCacheRepository>();
            }
            
            services.AddSingleton<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();
            services.AddSingleton<IMenu, Menu>();
            
            _serviceProvider = services.BuildServiceProvider();
        }
    }
}