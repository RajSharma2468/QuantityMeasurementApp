using System.Text;
using QuantityMeasurementBusinessLayer.DTOs;

namespace QuantityMeasurementConsoleApp.Helpers;

public static class FormatHelper
{
    #region Number Formatting

    public static string FormatPercentage(double value, int decimals = 1)
    {
        return (value * 100).ToString("F" + decimals) + "%";
    }

    public static string FormatNumber(double value, int decimals = 2)
    {
        return value.ToString("N" + decimals);
    }

    public static string FormatDecimal(double value, int decimals = 2)
    {
        return value.ToString("F" + decimals);
    }

    public static string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = bytes;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len /= 1024;
        }
        return len.ToString("0.##") + " " + sizes[order];
    }

    #endregion

    #region Date and Time Formatting

    public static string FormatDateTime(DateTime dt, bool includeTime = true)
    {
        if (includeTime)
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        return dt.ToString("yyyy-MM-dd");
    }

    public static string FormatTimeSpan(TimeSpan ts)
    {
        if (ts.TotalDays >= 1)
            return ts.TotalDays.ToString("F1") + " days";
        if (ts.TotalHours >= 1)
            return ts.TotalHours.ToString("F1") + " hours";
        if (ts.TotalMinutes >= 1)
            return ts.TotalMinutes.ToString("F1") + " minutes";
        return ts.TotalSeconds.ToString("F1") + " seconds";
    }

    #endregion

    #region Text Formatting

    public static string Truncate(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
            return text;
        return text.Substring(0, maxLength - 3) + "...";
    }

    public static string ToTitleCase(string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;
        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
    }

    #endregion

    #region Measurement Formatting

    public static string FormatMeasurement(double value, string unit)
    {
        return value.ToString("F2") + " " + unit;
    }

    public static string FormatComparisonResult(bool isEqual)
    {
        return isEqual ? "✓ Equal" : "✗ Not Equal";
    }

    // FIXED: Changed QuantityMeasurementResultDTO to QuantityResultDTO
    public static string FormatOperationResult(QuantityResultDTO r)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Operation: " + r.Operation.ToUpper());
        sb.AppendLine("Input: " + r.ThisValue.ToString("F2") + " " + r.ThisUnit + " → " + r.ThatValue.ToString("F2") + " " + r.ThatUnit);
        if (r.IsError)
            sb.AppendLine("Error: " + r.ErrorMessage);
        else if (!string.IsNullOrEmpty(r.ResultString))
            sb.AppendLine("Result: " + r.ResultString);
        else
            sb.AppendLine("Result: " + r.ResultValue.ToString("F2") + " " + (r.ResultUnit ?? r.ThisUnit));
        sb.AppendLine("Time: " + FormatDateTime(r.CreatedAt));
        return sb.ToString();
    }

    #endregion

    #region Boolean Formatting

    public static string FormatYesNo(bool value)
    {
        return value ? "Yes" : "No";
    }

   
    #endregion

    #region Console Color Helpers

    public static string WithColor(this string text, string color)
    {
        return "[" + color + "]" + text + "[/]";
    }

    public static string AsSuccess(this string text)
    {
        return WithColor(text, "green");
    }

    public static string AsError(this string text)
    {
        return WithColor(text, "red");
    }

    public static string AsWarning(this string text)
    {
        return WithColor(text, "yellow");
    }

    public static string AsInfo(this string text)
    {
        return WithColor(text, "blue");
    }

    #endregion
}