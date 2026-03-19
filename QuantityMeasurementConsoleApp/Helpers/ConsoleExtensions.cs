namespace QuantityMeasurementConsoleApp.Helpers;

public static class ConsoleExtensions
{
    // Remove the WithColor method since it's already in FormatHelper
    
    public static string PadBoth(this string str, int length, char paddingChar = ' ')
    {
        int spaces = length - str.Length;
        int padLeft = spaces / 2 + str.Length;
        return str.PadLeft(padLeft, paddingChar).PadRight(length, paddingChar);
    }

    public static string ToYesNo(this bool value)
    {
        return value ? "Yes" : "No";
    }

    public static string ToOnOff(this bool value)
    {
        return value ? "ON" : "OFF";
    }

   

    public static string TruncateWithEllipsis(this string str, int maxLength)
    {
        if (string.IsNullOrEmpty(str) || str.Length <= maxLength)
            return str;

        return str.Substring(0, maxLength - 3) + "...";
    }
}