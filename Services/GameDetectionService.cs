using System.Diagnostics;
using System.Windows.Threading;
using TaskbarHider.Models;

namespace TaskbarHider.Services;

public class GameDetectionService
{
    private readonly TaskbarService _taskbarService;
    private readonly SettingsManager _settingsManager;
    private DispatcherTimer? _detectionTimer;
    private HashSet<string> _gameProcesses = new();
    private bool _gameRunning;

    // List of common game processes
    private static readonly string[] KnownGameProcesses = new[]
    {
        "steam", "steamwebhelper", "gameoverlayui",
        "dota2", "csgo", "valorant", "vgc", "valorantwatcher",
        "minecraft", "javaw",
        "gta5", "gta6",
        "overwatch2", "overwatch",
        "fortnite", "epiconlineservices",
        "roblox", "rbxplayer",
        "apex", "r5apex",
        "pubg", "tslgame"
    };

    public GameDetectionService(TaskbarService taskbarService, SettingsManager settingsManager)
    {
        _taskbarService = taskbarService;
        _settingsManager = settingsManager;
    }

    public void Start()
    {
        if (!_settingsManager.Settings.EnableGameDetection) return;

        _detectionTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(2)
        };
        _detectionTimer.Tick += (s, e) => DetectFullscreenGame();
        _detectionTimer.Start();
    }

    public void Stop()
    {
        _detectionTimer?.Stop();
    }

    private void DetectFullscreenGame()
    {
        try
        {
            var processes = Process.GetProcesses();
            bool gameDetected = processes.Any(p =>
                KnownGameProcesses.Contains(p.ProcessName.ToLower()) &&
                p.MainWindowHandle != IntPtr.Zero);

            if (gameDetected && !_gameRunning)
            {
                _taskbarService.ShowTaskbar();
                _gameRunning = true;
            }
            else if (!gameDetected && _gameRunning)
            {
                _gameRunning = false;
            }
        }
        catch { }
    }

    public bool IsGameRunning => _gameRunning;
}