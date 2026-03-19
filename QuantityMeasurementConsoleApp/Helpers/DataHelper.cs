using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuantityMeasurementBusinessLayer.DTOs;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Context;

namespace QuantityMeasurementConsoleApp.Helpers;

public class DataHelper
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DataHelper> _logger;

    public DataHelper(ApplicationDbContext context, ILogger<DataHelper> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> SeedSampleDataAsync()
    {
        try
        {
            if (await _context.QuantityMeasurements.AnyAsync())
            {
                ConsoleHelper.PrintInfo("Database already contains data. Skipping seed.");
                return false;
            }

            var sampleData = new List<QuantityMeasurement>
            {
                new()
                {
                    ThisValue = 1,
                    ThisUnit = "Feet",
                    ThisMeasurementType = "LengthUnit",
                    ThatValue = 12,
                    ThatUnit = "Inches",
                    ThatMeasurementType = "LengthUnit",
                    Operation = "compare",
                    ResultString = "true",
                    IsError = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                },
                new()
                {
                    ThisValue = 1,
                    ThisUnit = "Feet",
                    ThisMeasurementType = "LengthUnit",
                    ThatValue = 0,
                    ThatUnit = "Inches",
                    ThatMeasurementType = "LengthUnit",
                    Operation = "convert",
                    ResultValue = 12,
                    ResultUnit = "Inches",
                    IsError = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-4)
                }
            };

            await _context.QuantityMeasurements.AddRangeAsync(sampleData);
            await _context.SaveChangesAsync();

            ConsoleHelper.PrintSuccess("Sample data seeded successfully!");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error seeding sample data");
            ConsoleHelper.PrintError($"Failed to seed data: {ex.Message}");
            return false;
        }
    }
}