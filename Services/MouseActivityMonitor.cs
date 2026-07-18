using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Threading;
using TaskbarHider.Models;

namespace TaskbarHider.Services;

public class MouseActivityMonitor
{
    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);

    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int X;
        public int Y;
    }

    private readonly TaskbarService _taskbarService;
    private readonly SettingsManager _settingsManager;
    private DispatcherTimer? _inactivityTimer;
    private DispatcherTimer? _monitoringTimer;
    private DateTime _lastMouseMove;
    private bool _taskbarShouldBeHidden;
    private int _taskbarHeight;

    public MouseActivityMonitor(TaskbarService taskbarService, SettingsManager settingsManager)
    {
        _taskbarService = taskbarService;
        _settingsManager = settingsManager;
        _lastMouseMove = DateTime.UtcNow;
        _taskbarHeight = TaskbarService.GetTaskbarHeight();
    }

    public void Start()
    {
        // Monitoring timer - checks cursor position every 100ms
        _monitoringTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };
        _monitoringTimer.Tick += (s, e) => CheckCursorPosition();
        _monitoringTimer.Start();

        // Inactivity timer - hides taskbar after delay
        _inactivityTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(500)
        };
        _inactivityTimer.Tick += (s, e) => CheckInactivity();
        _inactivityTimer.Start();
    }

    public void Stop()
    {
        _monitoringTimer?.Stop();
        _inactivityTimer?.Stop();
    }

    private void CheckCursorPosition()
    {
        GetCursorPos(out POINT cursorPos);

        // Get all monitor bounds
        var monitors = TaskbarService.GetAllMonitorBounds();
        if (monitors.Count == 0) return;

        // Check if cursor is near bottom edge of any monitor
        bool nearBottom = monitors.Any(monitor =>
            cursorPos.X >= monitor.Left &&
            cursorPos.X <= monitor.Right &&
            cursorPos.Y >= monitor.Bottom - _taskbarHeight - 5 && // 5px buffer
            cursorPos.Y <= monitor.Bottom);

        if (nearBottom)
        {
            // Cursor at bottom - show taskbar
            if (_taskbarService.IsTaskbarHidden)
            {
                _taskbarService.ShowTaskbar();
            }
            _lastMouseMove = DateTime.UtcNow;
            _taskbarShouldBeHidden = false;
        }
        else
        {
            // Cursor moved - reset inactivity timer
            _lastMouseMove = DateTime.UtcNow;
        }
    }

    private void CheckInactivity()
    {
        if (_taskbarService.IsTaskbarHidden) return;

        double inactivitySeconds = (DateTime.UtcNow - _lastMouseMove).TotalSeconds;
        int delaySeconds = _settingsManager.Settings.HideDelaySeconds;

        if (inactivitySeconds >= delaySeconds && !_taskbarShouldBeHidden)
        {
            _taskbarService.HideTaskbar();
            _taskbarShouldBeHidden = true;
        }
    }
}