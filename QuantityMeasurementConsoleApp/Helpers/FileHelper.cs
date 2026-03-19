using System.Text;
using System.Text.Json;

namespace QuantityMeasurementConsoleApp.Helpers;

public static class FileHelper
{
    private static readonly string _appDataPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "QuantityMeasurementApp");

    static FileHelper()
    {
        // Ensure directory exists
        Directory.CreateDirectory(_appDataPath);
    }

    public static async Task<bool> ExportToJsonAsync<T>(List<T> data, string fileName)
    {
        try
        {
            var filePath = Path.Combine(_appDataPath, $"{fileName}_{DateTime.Now:yyyyMMdd_HHmmss}.json");
            
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(data, options);
            await File.WriteAllTextAsync(filePath, json, Encoding.UTF8);

            ConsoleHelper.PrintSuccess($"Data exported to: {filePath}");
            return true;
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError($"Failed to export data: {ex.Message}");
            return false;
        }
    }

    public static async Task<List<T>?> ImportFromJsonAsync<T>(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                ConsoleHelper.PrintError($"File not found: {filePath}");
                return null;
            }

            var json = await File.ReadAllTextAsync(filePath, Encoding.UTF8);
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<List<T>>(json, options);
            
            ConsoleHelper.PrintSuccess($"Data imported from: {filePath}");
            return data;
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError($"Failed to import data: {ex.Message}");
            return null;
        }
    }

    public static async Task<bool> ExportToCsvAsync<T>(List<T> data, string fileName)
    {
        try
        {
            var filePath = Path.Combine(_appDataPath, $"{fileName}_{DateTime.Now:yyyyMMdd_HHmmss}.csv");
            
            var properties = typeof(T).GetProperties();
            var csvLines = new List<string>();

            // Header
            csvLines.Add(string.Join(",", properties.Select(p => p.Name)));

            // Data
            foreach (var item in data)
            {
                var values = properties.Select(p => 
                {
                    var value = p.GetValue(item)?.ToString() ?? "";
                    return value.Contains(",") ? $"\"{value}\"" : value;
                });
                csvLines.Add(string.Join(",", values));
            }

            await File.WriteAllLinesAsync(filePath, csvLines, Encoding.UTF8);

            ConsoleHelper.PrintSuccess($"Data exported to: {filePath}");
            return true;
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError($"Failed to export data: {ex.Message}");
            return false;
        }
    }

    public static void ShowExportedFiles()
    {
        var files = Directory.GetFiles(_appDataPath, "*.json").ToList();
        files.AddRange(Directory.GetFiles(_appDataPath, "*.csv"));

        if (!files.Any())
        {
            ConsoleHelper.PrintInfo("No exported files found.");
            return;
        }

        var fileInfos = files.Select(f => new FileInfo(f))
            .OrderByDescending(f => f.CreationTime)
            .ToList();

        var tableData = new List<Dictionary<string, object>>();
        foreach (var file in fileInfos)
        {
            tableData.Add(new Dictionary<string, object>
            {
                ["Name"] = file.Name,
                ["Size"] = $"{file.Length / 1024.0:F1} KB",
                ["Created"] = file.CreationTime,
                ["Modified"] = file.LastWriteTime
            });
        }

        ConsoleHelper.PrintResultsTable(tableData, "Exported Files");
    }
}