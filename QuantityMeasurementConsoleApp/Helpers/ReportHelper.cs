using System.Text;
using Spectre.Console;
using QuantityMeasurementBusinessLayer.DTOs;  // Add this using

namespace QuantityMeasurementConsoleApp.Helpers;

public static class ReportHelper
{
    // Fix all methods - change QuantityMeasurementResultDTO to QuantityResultDTO
    
    public static async Task GenerateSummaryReportAsync(List<QuantityResultDTO> data)
    {
        if (!data.Any())
        {
            ConsoleHelper.PrintInfo("No data to generate report");
            return;
        }

        var report = new StringBuilder();
        report.AppendLine("=".PadRight(80, '='));
        report.AppendLine("QUANTITY MEASUREMENT SYSTEM - SUMMARY REPORT");
        report.AppendLine("=".PadRight(80, '='));
        report.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        report.AppendLine($"Total Records: {data.Count}");
        report.AppendLine();

        // Statistics by operation
        var operations = data.GroupBy(x => x.Operation);
        report.AppendLine("OPERATION STATISTICS");
        report.AppendLine("-".PadRight(40, '-'));
        foreach (var op in operations)
        {
            var successCount = op.Count(x => !x.IsError);
            var errorCount = op.Count(x => x.IsError);
            report.AppendLine($"{op.Key.ToUpper()}: Total={op.Count()}, Success={successCount}, Errors={errorCount}");
        }
        report.AppendLine();

        // Error summary
        var errors = data.Where(x => x.IsError).ToList();
        if (errors.Any())
        {
            report.AppendLine("ERROR SUMMARY");
            report.AppendLine("-".PadRight(40, '-'));
            foreach (var error in errors.Take(5))
            {
                report.AppendLine($"[{error.CreatedAt:yyyy-MM-dd HH:mm}] {error.Operation}: {error.ErrorMessage}");
            }
            if (errors.Count > 5)
            {
                report.AppendLine($"... and {errors.Count - 5} more errors");
            }
            report.AppendLine();
        }

        // Recent activity
        report.AppendLine("RECENT ACTIVITY");
        report.AppendLine("-".PadRight(40, '-'));
        foreach (var item in data.OrderByDescending(x => x.CreatedAt).Take(5))
        {
            var status = item.IsError ? "FAILED" : "SUCCESS";
            report.AppendLine($"[{item.CreatedAt:yyyy-MM-dd HH:mm}] {item.Operation}: {status}");
        }

        report.AppendLine("=".PadRight(80, '='));

        // Display report
        ConsoleHelper.PrintEmptyLine();
        AnsiConsole.MarkupLine($"[cyan]{report.ToString()}[/]");

        // Save report to file
        var reportPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            $"QuantityReport_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
        await File.WriteAllTextAsync(reportPath, report.ToString());
        ConsoleHelper.PrintSuccess($"Report saved to: {reportPath}");
    }

    public static void PrintOperationStatistics(List<QuantityResultDTO> data)
    {
        if (!data.Any())
        {
            ConsoleHelper.PrintInfo("No statistics available");
            return;
        }

        var stats = new Dictionary<string, int>();
        var successStats = new Dictionary<string, int>();
        var errorStats = new Dictionary<string, int>();

        foreach (var item in data)
        {
            stats[item.Operation] = stats.GetValueOrDefault(item.Operation) + 1;
            if (item.IsError)
                errorStats[item.Operation] = errorStats.GetValueOrDefault(item.Operation) + 1;
            else
                successStats[item.Operation] = successStats.GetValueOrDefault(item.Operation) + 1;
        }

        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.Title("[yellow]Operation Statistics[/]");
        table.AddColumn("Operation");
        table.AddColumn("Total");
        table.AddColumn("Success");
        table.AddColumn("Errors");
        table.AddColumn("Success Rate");

        foreach (var op in stats.Keys.OrderBy(x => x))
        {
            var total = stats[op];
            var success = successStats.GetValueOrDefault(op);
            var errors = errorStats.GetValueOrDefault(op);
            var successRate = total > 0 ? (double)success / total : 0;

            table.AddRow(
                $"[cyan]{op}[/]",
                total.ToString(),
                $"[green]{success}[/]",
                errors > 0 ? $"[red]{errors}[/]" : "[grey]0[/]",
                $"{successRate:P1}"
            );
        }

        AnsiConsole.Write(table);
    }

    public static void PrintDailyActivity(List<QuantityResultDTO> data)
    {
        if (!data.Any())
            return;

        var dailyStats = data
            .GroupBy(x => x.CreatedAt.Date)
            .OrderBy(x => x.Key)
            .ToDictionary(
                x => x.Key.ToString("MMM dd"),
                x => x.Count()
            );

        ConsoleHelper.PrintBarChart("Daily Activity", dailyStats);
    }
}