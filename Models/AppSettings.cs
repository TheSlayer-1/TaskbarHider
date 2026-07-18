using System.Text.Json;
using System.IO;

namespace TaskbarHider.Models;

public class AppSettings
{
    public int HideDelaySeconds { get; set; } = 3;
    public bool StartWithWindows { get; set; } = false;
    public bool EnableGameDetection { get; set; } = true;
    public bool EnableMultiMonitor { get; set; } = true;

    private static readonly string ConfigPath = 
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
                     "TaskbarHider", "settings.json");

    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(ConfigPath)!);
        var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(ConfigPath, json);
    }

    public static AppSettings Load()
    {
        try
        {
            if (File.Exists(ConfigPath))
            {
                var json = File.ReadAllText(ConfigPath);
                return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
            }
        }
        catch { }
        
        return new AppSettings();
    }
}