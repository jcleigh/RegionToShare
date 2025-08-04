using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.Json;

namespace RegionToShare.Configuration;

public class ConfigurationService
{
    private readonly IConfiguration _configuration;
    private readonly string _userConfigPath;
    private AppSettings _appSettings;

    public ConfigurationService()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

        _userConfigPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "RegionToShare",
            "user-settings.json");

        // Add user settings if they exist
        if (File.Exists(_userConfigPath))
        {
            builder.AddJsonFile(_userConfigPath, optional: true, reloadOnChange: false);
        }

        _configuration = builder.Build();
        _appSettings = LoadSettings();
        
        // Subscribe to property changes to save settings
        _appSettings.PropertyChanged += OnSettingsChanged;
    }

    public AppSettings Settings => _appSettings;

    private AppSettings LoadSettings()
    {
        var settings = new AppSettings();
        
        // Load from configuration
        var userSection = _configuration.GetSection("UserSettings");
        if (userSection.Exists())
        {
            settings.WindowPlacement = userSection["WindowPlacement"] ?? "";
            settings.FramesPerSecond = userSection.GetValue<int>("FramesPerSecond", 15);
            settings.DrawShadowCursor = userSection.GetValue<bool>("DrawShadowCursor", false);
            settings.ThemeColor = userSection["ThemeColor"] ?? "SteelBlue";
            settings.StartActivated = userSection.GetValue<bool>("StartActivated", false);
        }

        return settings;
    }

    private void OnSettingsChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        try
        {
            var userSettings = new
            {
                UserSettings = new
                {
                    _appSettings.WindowPlacement,
                    _appSettings.FramesPerSecond,
                    _appSettings.DrawShadowCursor,
                    _appSettings.ThemeColor,
                    _appSettings.StartActivated
                }
            };

            Directory.CreateDirectory(Path.GetDirectoryName(_userConfigPath)!);
            
            var json = JsonSerializer.Serialize(userSettings, new JsonSerializerOptions 
            { 
                WriteIndented = true 
            });
            
            File.WriteAllText(_userConfigPath, json);
        }
        catch
        {
            // Ignore save errors for now
        }
    }
}