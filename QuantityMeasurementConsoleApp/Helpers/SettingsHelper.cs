using System.Text.Json;

namespace QuantityMeasurementConsoleApp.Helpers;

public class AppSettings
{
    public bool UseColors { get; set; } = true;
    public bool ShowTimestamps { get; set; } = true;
    public int HistoryPageSize { get; set; } = 10;
    public string DefaultMeasurementType { get; set; } = "LengthUnit";
    public string DefaultUnit { get; set; } = "Feet";
    public bool AutoSaveResults { get; set; } = true;
    public bool ShowDebugInfo { get; set; } = false;
    public string Theme { get; set; } = "Default";
}

public static class SettingsHelper
{
    private static readonly string _settingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "QuantityMeasurementApp",
        "settings.json");

    private static AppSettings _currentSettings = new AppSettings(); // Initialize with default

    static SettingsHelper()
    {
        LoadSettings();
    }

    public static AppSettings Current => _currentSettings;

    public static void LoadSettings()
    {
        try
        {
            if (File.Exists(_settingsPath))
            {
                var json = File.ReadAllText(_settingsPath);
                var settings = JsonSerializer.Deserialize<AppSettings>(json);
                if (settings != null)
                    _currentSettings = settings;
            }
            else
            {
                _currentSettings = new AppSettings();
                SaveSettings();
            }
        }
        catch
        {
            _currentSettings = new AppSettings();
        }
    }

    public static void SaveSettings()
    {
        try
        {
            var directory = Path.GetDirectoryName(_settingsPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var json = JsonSerializer.Serialize(_currentSettings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_settingsPath, json);
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError($"Failed to save settings: {ex.Message}");
        }
    }

    public static void ShowSettingsMenu()
    {
        while (true)
        {
            ConsoleHelper.ClearScreen();
            ConsoleHelper.PrintHeader("Settings");

            var settings = new Dictionary<string, object>
            {
                ["Use Colors"] = _currentSettings.UseColors ? "Yes" : "No",
                ["Show Timestamps"] = _currentSettings.ShowTimestamps ? "Yes" : "No",
                ["History Page Size"] = _currentSettings.HistoryPageSize,
                ["Default Measurement Type"] = _currentSettings.DefaultMeasurementType,
                ["Default Unit"] = _currentSettings.DefaultUnit,
                ["Auto Save Results"] = _currentSettings.AutoSaveResults ? "Yes" : "No",
                ["Show Debug Info"] = _currentSettings.ShowDebugInfo ? "Yes" : "No",
                ["Theme"] = _currentSettings.Theme
            };

            ConsoleHelper.PrintTable("Current Settings", settings);
            ConsoleHelper.PrintEmptyLine();

            var choices = new List<string>
            {
                "1. Toggle Colors",
                "2. Toggle Timestamps",
                "3. Change History Page Size",
                "4. Change Default Measurement Type",
                "5. Change Default Unit",
                "6. Toggle Auto Save",
                "7. Toggle Debug Info",
                "8. Change Theme",
                "9. Reset to Defaults",
                "0. Back to Main Menu"
            };

            var choice = ConsoleHelper.GetUserSelection("Select setting to change", choices);
            
            if (choice.StartsWith("0"))
            {
                SaveSettings();
                return;
            }

            switch (choice[0])
            {
                case '1':
                    _currentSettings.UseColors = !_currentSettings.UseColors;
                    ConsoleHelper.PrintSuccess($"Colors {(_currentSettings.UseColors ? "enabled" : "disabled")}");
                    break;
                case '2':
                    _currentSettings.ShowTimestamps = !_currentSettings.ShowTimestamps;
                    ConsoleHelper.PrintSuccess($"Timestamps {(_currentSettings.ShowTimestamps ? "enabled" : "disabled")}");
                    break;
                case '3':
                    _currentSettings.HistoryPageSize = ConsoleHelper.GetUserInt("Enter page size", 5, 50);
                    break;
                case '4':
                    _currentSettings.DefaultMeasurementType = ValidationHelper.GetValidMeasurementType();
                    break;
                case '5':
                    _currentSettings.DefaultUnit = ValidationHelper.GetValidUnit(_currentSettings.DefaultMeasurementType);
                    break;
                case '6':
                    _currentSettings.AutoSaveResults = !_currentSettings.AutoSaveResults;
                    ConsoleHelper.PrintSuccess($"Auto save {(_currentSettings.AutoSaveResults ? "enabled" : "disabled")}");
                    break;
                case '7':
                    _currentSettings.ShowDebugInfo = !_currentSettings.ShowDebugInfo;
                    ConsoleHelper.PrintSuccess($"Debug info {(_currentSettings.ShowDebugInfo ? "enabled" : "disabled")}");
                    break;
                case '8':
                    var themes = new List<string> { "Default", "Dark", "Light", "High Contrast" };
                    _currentSettings.Theme = ConsoleHelper.GetUserSelection("Select theme", themes);
                    break;
                case '9':
                    if (ConsoleHelper.GetUserYesNo("Reset all settings to defaults"))
                    {
                        _currentSettings = new AppSettings();
                        ConsoleHelper.PrintSuccess("Settings reset to defaults");
                    }
                    break;
            }

            SaveSettings();
            ConsoleHelper.WaitForKeyPress();
        }
    }
}