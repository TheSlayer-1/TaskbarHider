using TaskbarHider.Models;
using Microsoft.Win32;

namespace TaskbarHider.Services;

public class SettingsManager
{
    private AppSettings _settings;

    public AppSettings Settings
    {
        get => _settings;
        set
        {
            _settings = value;
            _settings.Save();
            ApplyStartupRegistry();
        }
    }

    public SettingsManager()
    {
        _settings = AppSettings.Load();
    }

    public void SetHideDelay(int seconds)
    {
        _settings.HideDelaySeconds = Math.Clamp(seconds, 1, 30);
        Settings = _settings;
    }

    public void SetStartWithWindows(bool enabled)
    {
        _settings.StartWithWindows = enabled;
        Settings = _settings;
    }

    public void SetGameDetection(bool enabled)
    {
        _settings.EnableGameDetection = enabled;
        Settings = _settings;
    }

    private void ApplyStartupRegistry()
    {
        try
        {
            var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            if (key == null) return;

            if (_settings.StartWithWindows)
            {
                key.SetValue("TaskbarHider", System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
            else
            {
                key.DeleteValue("TaskbarHider", false);
            }

            key.Close();
        }
        catch { }
    }
}