using System.Windows;
using TaskbarHider.Services;

namespace TaskbarHider;

public partial class App : Application
{
    private TaskbarService? _taskbarService;
    private MouseActivityMonitor? _mouseMonitor;
    private GameDetectionService? _gameDetector;
    private SettingsManager? _settingsManager;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        // Initialize services
        _settingsManager = new SettingsManager();
        _taskbarService = new TaskbarService();
        _mouseMonitor = new MouseActivityMonitor(_taskbarService, _settingsManager);
        _gameDetector = new GameDetectionService(_taskbarService, _settingsManager);
        
        // Start monitoring
        _mouseMonitor.Start();
        _gameDetector.Start();
        
        // Hide main window
        MainWindow?.Hide();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
        _mouseMonitor?.Stop();
        _gameDetector?.Stop();
        _taskbarService?.ShowTaskbar();
    }
}