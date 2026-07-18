using System.Windows;
using TaskbarHider.Services;

namespace TaskbarHider.Views;

public partial class SettingsWindow : Window
{
    private readonly SettingsManager _settingsManager;

    public SettingsWindow()
    {
        InitializeComponent();
        _settingsManager = new SettingsManager();
        LoadSettings();
    }

    private void LoadSettings()
    {
        DelaySlider.Value = _settingsManager.Settings.HideDelaySeconds;
        DelayValue.Text = _settingsManager.Settings.HideDelaySeconds.ToString();
        StartupCheckbox.IsChecked = _settingsManager.Settings.StartWithWindows;
        GameDetectionCheckbox.IsChecked = _settingsManager.Settings.EnableGameDetection;
        MultiMonitorCheckbox.IsChecked = _settingsManager.Settings.EnableMultiMonitor;
    }

    private void DelaySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        int value = (int)e.NewValue;
        DelayValue.Text = value.ToString();
        _settingsManager.SetHideDelay(value);
    }

    private void StartupCheckbox_Checked(object sender, RoutedEventArgs e)
    {
        _settingsManager.SetStartWithWindows(true);
    }

    private void StartupCheckbox_Unchecked(object sender, RoutedEventArgs e)
    {
        _settingsManager.SetStartWithWindows(false);
    }

    private void GameDetection_Checked(object sender, RoutedEventArgs e)
    {
        _settingsManager.Settings.EnableGameDetection = true;
        _settingsManager.Settings.Save();
    }

    private void GameDetection_Unchecked(object sender, RoutedEventArgs e)
    {
        _settingsManager.Settings.EnableGameDetection = false;
        _settingsManager.Settings.Save();
    }

    private void MultiMonitor_Checked(object sender, RoutedEventArgs e)
    {
        _settingsManager.Settings.EnableMultiMonitor = true;
        _settingsManager.Settings.Save();
    }

    private void MultiMonitor_Unchecked(object sender, RoutedEventArgs e)
    {
        _settingsManager.Settings.EnableMultiMonitor = false;
        _settingsManager.Settings.Save();
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}